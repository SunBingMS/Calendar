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
        For i As Integer = 1 To 6
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
        For i As Integer = 1000 To 3000
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
        cmbMonth.Items.Clear()

        '月データテーブルの作成
        Dim dtMonth As New DataTable
        dtMonth.Columns.Add("VALUE")
        dtMonth.Columns.Add("TEXT")

        '1月～12月まで
        For i As Integer = 1 To 12
            dtMonth.Rows.Add(i, i)
        Next

        '月ComboBoxのバインド
        cmbMonth.DataSource = dtMonth
        cmbMonth.ValueMember = "VALUE"
        cmbMonth.DisplayMember = "TEXT"

    End Sub

    ''' <summary>
    ''' 年ComboBoxの設定
    ''' </summary>
    ''' <param name="pYear">設定したい年</param>
    ''' <remarks></remarks>
    Private Sub subSetCmbYear(ByVal pYear As Integer)

        cmbYear.SelectedValue = pYear

    End Sub

    ''' <summary>
    ''' 月ComboBoxの設定
    ''' </summary>
    ''' <param name="pMonth">設定したい月</param>
    ''' <remarks></remarks>
    Private Sub subSetCmbMonth(pMonth As Integer)

        cmbMonth.SelectedValue = pMonth

    End Sub

#End Region

#Region "Function"

    ''' <summary>
    ''' 月の日数算出
    ''' </summary>
    ''' <param name="pYear">年</param>
    ''' <param name="pMonth">月</param>
    ''' <returns>当該年月の日数</returns>
    ''' <remarks></remarks>
    Private Function fncCountDaysOfMonth(ByVal pYear As Integer, ByVal pMonth As Integer) As Integer

        '日数
        Dim lngDaysOfMonth As Long = 0

        If pMonth < 12 Then
            '1～11月の場合
            lngDaysOfMonth = DateDiff("d", _
                                      Str(pYear) + "-" & Str(pMonth) + "-1", _
                                      Str(pYear) + "-" + Str(pMonth + 1) & "-1")
        Else
            '12月の場合
            lngDaysOfMonth = DateDiff("d", _
                                      Str(pYear) + "-" & Str(pMonth) + "-1", _
                                      Str(pYear + 1) + "-" + Str((pMonth + 1) Mod 12) & "-1")
        End If

        Return CType(lngDaysOfMonth, Integer)

    End Function

#End Region

#Region "GridView"

    ''' <summary>
    ''' DataGridView更新処理
    ''' </summary>
    ''' <param name="pYear"></param>
    ''' <param name="pMonth"></param>
    ''' <param name="pDay"></param>
    ''' <remarks></remarks>
    Public Sub subUpdateCalendar(ByVal pYear As Integer, ByVal pMonth As Integer, ByVal pDay As Integer)

        'メンバー変数年月の設定
        mintLastCorrectYear = pYear
        mintLastCorrectMonth = pMonth

        '全セルのスタイル設定
        For x As Integer = 0 To 6
            For y As Integer = 0 To 5
                dgvCalendar.Item(x, y).Value = "-"
                If x = 0 Then
                    '日曜日
                    dgvCalendar.Item(x, y).Style.ForeColor = Color.Red
                    dgvCalendar.Item(x, y).Style.BackColor = Color.FromArgb(255, 240, 240)
                ElseIf x = 6 Then
                    '土曜日
                    dgvCalendar.Item(x, y).Style.ForeColor = Color.Blue
                    dgvCalendar.Item(x, y).Style.BackColor = Color.FromArgb(240, 240, 255)
                Else
                    '平日
                    dgvCalendar.Item(x, y).Style.ForeColor = Color.Black
                    dgvCalendar.Item(x, y).Style.BackColor = Color.White
                End If
            Next
        Next

        '当該月の日数算出
        Dim lngDaysOfMonth As Integer = fncCountDaysOfMonth(pYear, pMonth)

        '前月の日数算出
        Dim intPreYear As Integer = pYear
        Dim intPreMonth As Integer = pMonth - 1
        If intPreMonth <= 0 Then
            intPreMonth = 12
            intPreYear = pYear - 1
        End If
        Dim intDaysOfPreMonth As Integer = fncCountDaysOfMonth(intPreYear, intPreMonth)

        '一日の曜日算出
        Dim datDayOne As Date = CType(Str(pYear) + "-" + Str(pMonth) + "-1", Date)
        Dim intMonthStartWeek As Integer = Weekday(datDayOne)

        '日曜日の場合
        If intMonthStartWeek = 1 Then
            intMonthStartWeek = 8
        End If

        'カレンダーへ出力
        Try
            'DBオープン
            odbcnConnection.Open()
            odbcmdCommand.Connection = odbcnConnection

            For i As Integer = 1 To lngDaysOfMonth

                '今日の場合
                If i = pDay Then
                    'フォーカスの設定
                    dgvCalendar.Item((i - 2 + intMonthStartWeek) Mod 7, _
                                     (i - 2 + intMonthStartWeek) \ 7).Selected = True
                End If

                '日付の設定
                dgvCalendar.Item((i - 2 + intMonthStartWeek) Mod 7, _
                                 (i - 2 + intMonthStartWeek) \ 7).Value = i

                'DBで当該日のデータを検索する
                Dim strDate As String = String.Format("{0:0000}", pYear) & _
                                        String.Format("{0:00}", pMonth) & _
                                        String.Format("{0:00}", i)
                odbcmdCommand.CommandText = "SELECT f_memo FROM tb_memo WHERE f_date =" & strDate

                '当日のメモデータ取得
                Dim dr As OleDbDataReader = odbcmdCommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    '当日のメモは非空の場合
                    If Not dr(0).Equals("") Then
                        dgvCalendar.Item((i - 2 + intMonthStartWeek) Mod 7, (i - 2 + intMonthStartWeek) \ 7).Style.BackColor = Color.LightYellow
                    End If
                End If
                dr.Close()
            Next

        Catch ex As Exception

        Finally

        End Try
        odbcnConnection.Close()

        '前月のカレンダーへ出力
        For i = 0 To intMonthStartWeek - 2
            dgvCalendar.Item(i, 0).Style.ForeColor = Color.Gray
            dgvCalendar.Item(i, 0).Style.BackColor = Color.FromArgb(240, 240, 240)
            dgvCalendar.Item(i, 0).Value = intDaysOfPreMonth - intMonthStartWeek + 2 + i
        Next

        '次月のカレンダーへ出力
        For i = intMonthStartWeek + lngDaysOfMonth To 6 * 7
            dgvCalendar.Item((i - 1) Mod 7, (i - 1) \ 7).Style.ForeColor = Color.Gray
            dgvCalendar.Item((i - 1) Mod 7, (i - 1) \ 7).Style.BackColor = Color.FromArgb(240, 240, 240)
            dgvCalendar.Item((i - 1) Mod 7, (i - 1) \ 7).Value = i - intMonthStartWeek - lngDaysOfMonth + 1
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
            frmMemo.Initial(subUpdateCalendar.SelectedValue, ComboBox_month.SelectedValue, Integer.Parse(DataGridView1.SelectedCells.Item(0).Value))
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
    Private Sub subUpdateCalendar_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbYear.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Separator Then
            subUpdateCalendar_Check(sender)
        End If
    End Sub

    '年ComboBoxテキストチェック処理
    Private Sub subUpdateCalendar_Check(sender As Object)
        Dim inputYear As Integer = 0
        If Integer.TryParse(cmbYear.Text, inputYear) Then
            inputYear = subUpdateCalendar.Text
            If inputYear >= 1000 And inputYear <= 3000 Then
                cmbYear.SelectedValue = inputYear
                subUpdateCalendar(subUpdateCalendar.SelectedValue, ComboBox_month.SelectedValue, 1)
                cmbYear.CausesValidation = False
                cmbMonth.Select()
                cmbYear.CausesValidation = True
                Return
            End If
        End If
        MsgBox("入力した年をチェックしてください。")
        '入力前の日付表示
        subSetCmbYear(mintLastCorrectYear)
    End Sub

    '年ComboBoxの入力制限
    Private Sub subUpdateCalendar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbYear.KeyPress
        Dim currentKey As Integer = Convert.ToInt32(e.KeyChar)
        '0~9 と BackSpace以外の入力禁止
        If currentKey >= 48 And currentKey <= 57 Or currentKey = 8 Then
            Return
        Else
            e.Handled = True
        End If
    End Sub

    '年ComboBox
    Private Sub subUpdateCalendar_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmbYear.Validating
        cmbYear.Select()
        SendKeys.Send("{Enter}")
    End Sub

    '年ComboBoxの選択イベント
    Private Sub subUpdateCalendar_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbYear.SelectedValueChanged
        'ComboBox有効性フラグチェック
        If Not mbooReady Then
            Return
        End If

        'カレンダー更新
        subUpdateCalendar(subUpdateCalendar.SelectedValue, ComboBox_month.SelectedValue, 1)
    End Sub

    '月ComboBoxのテキスト入力イベント
    Private Sub ComboBox_month_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbMonth.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Separator Then
            ComboBox_month_Check(sender)
        End If
    End Sub

    '月ComboBoxテキストチェック処理
    Private Sub ComboBox_month_Check(sender As Object)
        Dim inputMonth As Integer = 0
        If Integer.TryParse(cmbMonth.Text, inputMonth) Then
            inputMonth = ComboBox_month.Text
            If inputMonth >= 1 And inputMonth <= 12 Then
                cmbMonth.SelectedValue = inputMonth
                subUpdateCalendar(subUpdateCalendar.SelectedValue, ComboBox_month.SelectedValue, 1)
                cmbMonth.CausesValidation = False
                ButtonToday.Select()
                cmbMonth.CausesValidation = True
                Return
            End If
        End If
        MsgBox("入力した月をチェックしてください。")
        '入力前の日付表示
        subSetCmbMonth(mintLastCorrectMonth)
    End Sub

    '月ComboBoxの入力制限
    Private Sub ComboBox_month_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbMonth.KeyPress
        Dim currentKey As Integer = Convert.ToInt32(e.KeyChar)
        '0~9 と BackSpace以外の入力禁止
        If currentKey >= 48 And currentKey <= 57 Or currentKey = 8 Then
            Return
        Else
            e.Handled = True
        End If
    End Sub

    '月ComboBox
    Private Sub ComboBox_month_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmbMonth.Validating
        cmbMonth.Select()
        SendKeys.Send("{Enter}")
    End Sub

    '月ComboBoxの選択イベント
    Private Sub ComboBox_month_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbMonth.SelectedValueChanged
        'ComboBox有効性フラグチェック
        If Not mbooReady Then
            Return
        End If

        'カレンダー更新
        subUpdateCalendar(subUpdateCalendar.SelectedValue, ComboBox_month.SelectedValue, 1)
    End Sub

#End Region

#Region "Button Event"

    '前月ボタンイベント
    Private Sub ButtonPre_Click(sender As Object, e As EventArgs) Handles ButtonPre.Click
        If cmbMonth.SelectedIndex <= 0 Then
            cmbMonth.SelectedIndex = cmbMonth.Items.Count - 1
            If cmbYear.SelectedIndex <= 0 Then
                cmbYear.SelectedIndex = cmbYear.Items.Count - 1
            Else
                cmbYear.SelectedIndex = cmbYear.SelectedIndex - 1
            End If
        Else
            cmbMonth.SelectedIndex = cmbMonth.SelectedIndex - 1
        End If
    End Sub

    '次月ボタンイベント
    Private Sub ButtonNext_Click(sender As Object, e As EventArgs) Handles ButtonNext.Click
        If cmbMonth.SelectedIndex >= cmbMonth.Items.Count - 1 Then
            cmbMonth.SelectedIndex = 0
            If cmbYear.SelectedIndex >= cmbYear.Items.Count - 1 Then
                cmbYear.SelectedIndex = 0
            Else
                cmbYear.SelectedIndex = cmbYear.SelectedIndex + 1
            End If
        Else
            cmbMonth.SelectedIndex = cmbMonth.SelectedIndex + 1
        End If
    End Sub

    '今日ボタンイベント
    Private Sub ButtonToday_Click(sender As Object, e As EventArgs) Handles ButtonToday.Click
        Call subUpdateCalendarOfToday()
    End Sub

    Private Sub subUpdateCalendarOfToday()
        '年月ComboBoxのデフォールト値設定
        Dim currentDate As Date = Now
        subSetCmbYear(currentDate.Year)
        subSetCmbMonth(currentDate.Month)
        'カレンダー更新
        subUpdateCalendar(currentDate.Year, currentDate.Month, currentDate.Day)
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
