'********************************************************************
'  システム            ：   カレンダーシステム
'  サブシステム名 　   ：   カレンダーメイン画面
'  クラス名　　　      ：   frmCalendar
'  機能概要　　　      ：   □□□□□□□□□□□□□□□□
'  作成日      　　　　：   2014/09/05
'  作成者      　　　　：   SKB 孫　氷
'  変更履歴    　　　　：   
'********************************************************************
Option Strict On
Option Explicit On
Option Compare Binary

Imports System.Data.OleDb

''' <summary>
''' カレンダーメイン画面フレーム
''' </summary>
''' <remarks></remarks>
Public Class frmCalendar

    Private mbooReady As Boolean = False        '年月ComboBoxの有効性フラグ
    Private mintLastCorrectYear As Integer = 0  '年ComboBoxミス入力前の正しい値
    Private mintLastCorrectMonth As Integer = 0 '月ComboBoxミス入力前の正しい値

#Region "Load"

    ''' <summary>
    ''' Window Load処理
    ''' </summary>
    ''' <param name="sender">イベント発生源オブジェクト</param>
    ''' <param name="e">イベントに関連する補足情報</param>
    ''' <remarks></remarks>
    Private Sub Calendar_Load(sender As Object, e As EventArgs) Handles Me.Load

        'カレンダーセルのサイズ設定
        dgvCalendar.RowTemplate.Height = 50

        '年月ComboBox中身の初期化
        subInitialCmbYear()
        subInitialCmbMonth()

        'カレンダーGridViewの初期化
        For i = 1 To 6
            dgvCalendar.Rows.Add("-", "-", "-", "-", "-", "-", "-")
        Next

        '今日の日付表示
        Call subUpdateCalendarOfToday()

        'ComboBox有効にする
        mbooReady = True

    End Sub

#End Region

#Region "ComboBox"

    ''' <summary>
    ''' 年ComboBox中身の初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub subInitialCmbYear()

        '年ComboBoxのクリア
        cmbYear.Items.Clear()

        '年データテーブルの作成
        Dim dtblYear As New DataTable
        dtblYear.Columns.Add("VALUE")
        dtblYear.Columns.Add("TEXT")

        '1000年～3000年まで
        For i = 1000 To 3000
            dtblYear.Rows.Add(i, i)
        Next

        '年ComboBoxのバインド
        cmbYear.DataSource = dtblYear
        cmbYear.ValueMember = "VALUE"
        cmbYear.DisplayMember = "TEXT"

    End Sub

    ''' <summary>
    ''' 月のComboBox中身の初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub subInitialCmbMonth()

        '月ComboBoxのクリア
        ComboBox_month.Items.Clear()

        '月データテーブルの作成
        Dim dtMonth As New DataTable
        dtMonth.Columns.Add("VALUE")
        dtMonth.Columns.Add("TEXT")

        '1月～12月まで
        For i = 1 To 12
            dtMonth.Rows.Add(i, i)
        Next

        '月ComboBoxのバインド
        ComboBox_month.DataSource = dtMonth
        ComboBox_month.ValueMember = "VALUE"
        ComboBox_month.DisplayMember = "TEXT"
    End Sub

    '年ComboBoxの設定
    Private Sub YearComboBox_Update(year As Integer)
        cmbYear.SelectedValue = year
    End Sub

    '月ComboBoxの設定
    Private Sub MonthComboBox_Update(month As Integer)
        ComboBox_month.SelectedValue = month
    End Sub

#End Region

#Region "Function"

    '月の日数算出
    Private Function DaysOfMonthCount(ByVal year As Integer, ByVal month As Integer) As Integer
        Dim daysOfMonth As Integer = 0
        If month < 12 Then
            daysOfMonth = CType(DateDiff("d", Str(year) + "-" & Str(month) + "-1", Str(year) + "-" + Str(month + 1) & "-1"), Integer)
        Else
            daysOfMonth = DateDiff("d", Str(year) + "-" & Str(month) + "-1", Str(year + 1) + "-" + Str((month + 1) Mod 12) & "-1")
        End If

        Return daysOfMonth
    End Function

#End Region

#Region "GridView"

    'DataGridView更新処理
    Public Sub Calendar_Update(ByVal year As Integer, ByVal month As Integer, ByVal day As Integer)
        mintLastCorrectYear = year
        mintLastCorrectMonth = month
        '全セルのスタイル設定
        For x = 0 To 6
            For y = 0 To 5
                dgvCalendar.Item(x, y).Value = "-"
                If x = 0 Then
                    dgvCalendar.Item(x, y).Style.ForeColor = Color.Red
                    dgvCalendar.Item(x, y).Style.BackColor = Color.FromArgb(255, 240, 240)
                ElseIf x = 6 Then
                    dgvCalendar.Item(x, y).Style.ForeColor = Color.Blue
                    dgvCalendar.Item(x, y).Style.BackColor = Color.FromArgb(240, 240, 255)
                Else
                    dgvCalendar.Item(x, y).Style.ForeColor = Color.Black
                    dgvCalendar.Item(x, y).Style.BackColor = Color.White
                End If
            Next
        Next

        '当該月の日数算出
        Dim daysOfMonth As Integer = DaysOfMonthCount(year, month)
        '前月の日数算出
        Dim preYear As Integer = year
        Dim preMonth As Integer = month - 1
        If preMonth <= 0 Then
            preMonth = 12
            preYear = year - 1
        End If
        Dim daysOfPreMonth As Integer = DaysOfMonthCount(preYear, preMonth)

        '一日の曜日算出
        Dim monthStartWeek As Integer = Weekday(Str(year) + "-" + Str(month) + "-1")
        If monthStartWeek = 1 Then
            monthStartWeek = 8
        End If

        'カレンダーへ出力
        odbcnConnection.Open()
        odbcmdCommand.Connection = odbcnConnection
        For i = 1 To daysOfMonth
            If i = day Then
                dgvCalendar.Item((i - 2 + monthStartWeek) Mod 7, (i - 2 + monthStartWeek) \ 7).Selected = True
            End If
            dgvCalendar.Item((i - 2 + monthStartWeek) Mod 7, (i - 2 + monthStartWeek) \ 7).Value = i

            Dim tdate As String = String.Format("{0:0000}", year) & String.Format("{0:00}", month) & String.Format("{0:00}", i)
            odbcmdCommand.CommandText = "SELECT f_memo FROM tb_memo WHERE f_date =" & tdate
            Dim dr As OleDbDataReader = odbcmdCommand.ExecuteReader

            If dr.HasRows Then
                dr.Read()
                If Not dr(0) = "" Then
                    dgvCalendar.Item((i - 2 + monthStartWeek) Mod 7, (i - 2 + monthStartWeek) \ 7).Style.BackColor = Color.LightYellow
                End If
            End If
            dr.Close()
        Next
        odbcnConnection.Close()

        '前月のカレンダーへ出力
        For i = 0 To monthStartWeek - 2
            dgvCalendar.Item(i, 0).Style.ForeColor = Color.Gray
            dgvCalendar.Item(i, 0).Style.BackColor = Color.FromArgb(240, 240, 240)
            dgvCalendar.Item(i, 0).Value = daysOfPreMonth - monthStartWeek + 2 + i
        Next

        '次月のカレンダーへ出力
        For i = monthStartWeek + daysOfMonth To 6 * 7
            dgvCalendar.Item((i - 1) Mod 7, (i - 1) \ 7).Style.ForeColor = Color.Gray
            dgvCalendar.Item((i - 1) Mod 7, (i - 1) \ 7).Style.BackColor = Color.FromArgb(240, 240, 240)
            dgvCalendar.Item((i - 1) Mod 7, (i - 1) \ 7).Value = i - monthStartWeek - daysOfMonth + 1
        Next
    End Sub

    'カレンダーのダブルクリックイベント
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCalendar.CellDoubleClick
        If dgvCalendar.SelectedCells.Item(0).Style.ForeColor = Color.Gray Then
            Dim tmp As String = DataGridView1.SelectedCells.Item(0).Value
            If dgvCalendar.SelectedCells.Item(0).RowIndex >= 4 Then
                ButtonNext_Click(sender, New System.EventArgs())
            Else
                ButtonPre_Click(sender, New System.EventArgs())
            End If
            'セルの選択
            For x = 0 To 6
                For y = 0 To 5
                    If DataGridView1.Item(x, y).Value = tmp And
                       Not DataGridView1.Item(x, y).Style.ForeColor = Color.Gray Then
                        dgvCalendar.Item(x, y).Selected = True
                    End If
                Next
            Next
        Else
            frmMemo.Initial(ComboBox_year.SelectedValue, ComboBox_month.SelectedValue, Integer.Parse(DataGridView1.SelectedCells.Item(0).Value))
            frmMemo.ShowDialog()
        End If
    End Sub

    'Enterボタンの時Tabと同じにする
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvCalendar.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Separator Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

#End Region

#Region "ComboBox Event"

    '年ComboBoxのテキスト入力イベント
    Private Sub ComboBox_year_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbYear.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Separator Then
            ComboBox_year_Check(sender)
        End If
    End Sub

    '年ComboBoxテキストチェック処理
    Private Sub ComboBox_year_Check(sender As Object)
        Dim inputYear As Integer = 0
        If Integer.TryParse(cmbYear.Text, inputYear) Then
            inputYear = ComboBox_year.Text
            If inputYear >= 1000 And inputYear <= 3000 Then
                cmbYear.SelectedValue = inputYear
                Calendar_Update(ComboBox_year.SelectedValue, ComboBox_month.SelectedValue, 1)
                cmbYear.CausesValidation = False
                ComboBox_month.Select()
                cmbYear.CausesValidation = True
                Return
            End If
        End If
        MsgBox("入力した年をチェックしてください。")
        '入力前の日付表示
        YearComboBox_Update(mintLastCorrectYear)
    End Sub

    '年ComboBoxの入力制限
    Private Sub ComboBox_year_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbYear.KeyPress
        Dim currentKey As Integer = Convert.ToInt32(e.KeyChar)
        '0~9 と BackSpace以外の入力禁止
        If currentKey >= 48 And currentKey <= 57 Or currentKey = 8 Then
            Return
        Else
            e.Handled = True
        End If
    End Sub

    '年ComboBox
    Private Sub ComboBox_year_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmbYear.Validating
        cmbYear.Select()
        SendKeys.Send("{Enter}")
    End Sub

    '年ComboBoxの選択イベント
    Private Sub ComboBox_year_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbYear.SelectedValueChanged
        'ComboBox有効性フラグチェック
        If Not mbooReady Then
            Return
        End If

        'カレンダー更新
        Calendar_Update(ComboBox_year.SelectedValue, ComboBox_month.SelectedValue, 1)
    End Sub

    '月ComboBoxのテキスト入力イベント
    Private Sub ComboBox_month_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox_month.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Separator Then
            ComboBox_month_Check(sender)
        End If
    End Sub

    '月ComboBoxテキストチェック処理
    Private Sub ComboBox_month_Check(sender As Object)
        Dim inputMonth As Integer = 0
        If Integer.TryParse(ComboBox_month.Text, inputMonth) Then
            inputMonth = ComboBox_month.Text
            If inputMonth >= 1 And inputMonth <= 12 Then
                ComboBox_month.SelectedValue = inputMonth
                Calendar_Update(ComboBox_year.SelectedValue, ComboBox_month.SelectedValue, 1)
                ComboBox_month.CausesValidation = False
                ButtonToday.Select()
                ComboBox_month.CausesValidation = True
                Return
            End If
        End If
        MsgBox("入力した月をチェックしてください。")
        '入力前の日付表示
        MonthComboBox_Update(mintLastCorrectMonth)
    End Sub

    '月ComboBoxの入力制限
    Private Sub ComboBox_month_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox_month.KeyPress
        Dim currentKey As Integer = Convert.ToInt32(e.KeyChar)
        '0~9 と BackSpace以外の入力禁止
        If currentKey >= 48 And currentKey <= 57 Or currentKey = 8 Then
            Return
        Else
            e.Handled = True
        End If
    End Sub

    '月ComboBox
    Private Sub ComboBox_month_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ComboBox_month.Validating
        ComboBox_month.Select()
        SendKeys.Send("{Enter}")
    End Sub

    '月ComboBoxの選択イベント
    Private Sub ComboBox_month_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox_month.SelectedValueChanged
        'ComboBox有効性フラグチェック
        If Not mbooReady Then
            Return
        End If

        'カレンダー更新
        Calendar_Update(ComboBox_year.SelectedValue, ComboBox_month.SelectedValue, 1)
    End Sub

#End Region

#Region "Button Event"

    '前月ボタンイベント
    Private Sub ButtonPre_Click(sender As Object, e As EventArgs) Handles ButtonPre.Click
        If ComboBox_month.SelectedIndex <= 0 Then
            ComboBox_month.SelectedIndex = ComboBox_month.Items.Count - 1
            If cmbYear.SelectedIndex <= 0 Then
                cmbYear.SelectedIndex = cmbYear.Items.Count - 1
            Else
                cmbYear.SelectedIndex = cmbYear.SelectedIndex - 1
            End If
        Else
            ComboBox_month.SelectedIndex = ComboBox_month.SelectedIndex - 1
        End If
    End Sub

    '次月ボタンイベント
    Private Sub ButtonNext_Click(sender As Object, e As EventArgs) Handles ButtonNext.Click
        If ComboBox_month.SelectedIndex >= ComboBox_month.Items.Count - 1 Then
            ComboBox_month.SelectedIndex = 0
            If cmbYear.SelectedIndex >= cmbYear.Items.Count - 1 Then
                cmbYear.SelectedIndex = 0
            Else
                cmbYear.SelectedIndex = cmbYear.SelectedIndex + 1
            End If
        Else
            ComboBox_month.SelectedIndex = ComboBox_month.SelectedIndex + 1
        End If
    End Sub

    '今日ボタンイベント
    Private Sub ButtonToday_Click(sender As Object, e As EventArgs) Handles ButtonToday.Click
        Call subUpdateCalendarOfToday()
    End Sub

    Private Sub subUpdateCalendarOfToday()
        '年月ComboBoxのデフォールト値設定
        Dim currentDate As Date = Now
        YearComboBox_Update(currentDate.Year)
        MonthComboBox_Update(currentDate.Month)
        'カレンダー更新
        Calendar_Update(currentDate.Year, currentDate.Month, currentDate.Day)
    End Sub

    '×ボタンのCauseValidation無効処理
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Select Case ((m.WParam.ToInt64() And &HFFFF) And &HFFF0)
            Case &HF060 ' The user chose to close the form.
                Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        End Select
        MyBase.WndProc(m)
    End Sub

#End Region

End Class
