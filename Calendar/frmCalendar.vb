Imports System.Data.OleDb

Public Class frmCalendar

    Private mbooReady As Boolean = False        'ComboBoxの有効性フラグ
    Private mintLastCorrectYear As Integer = 0  '年ComboBoxミス入力前の正しい値
    Private mintLastCorrectMonth As Integer = 0 '月ComboBoxミス入力前の正しい値

    ' Connection string for ADO.NET via OleDB
    Dim cn As OleDbConnection =
        New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=memo.mdb;")
    Dim cmd As OleDbCommand

#Region "Load"

    'Window Load処理
    Private Sub Calendar_Load(sender As Object, e As EventArgs) Handles Me.Load
        'カレンダーセルのサイズ設定
        DataGridView1.RowTemplate.Height = 50
        '年月ComboBox中身の初期化
        YearComboBox_Initial()
        MonthComboBox_Initial()

        For i = 1 To 6
            DataGridView1.Rows.Add("-", "-", "-", "-", "-", "-", "-")
        Next

        '今日の日付表示
        ButtonToday_Click(sender, New System.EventArgs())
        'ComboBox有効にする
        mbooReady = True
    End Sub

#End Region

#Region "ComboBox"

    '年のComboBox中身の初期化
    Private Sub YearComboBox_Initial()
        ComboBox_year.Items.Clear()

        Dim dtYear As New DataTable
        dtYear.Columns.Add("VALUE")
        dtYear.Columns.Add("TEXT")

        For i = 1000 To 3000
            dtYear.Rows.Add(i, i)
        Next

        ComboBox_year.DataSource = dtYear
        ComboBox_year.ValueMember = "VALUE"
        ComboBox_year.DisplayMember = "TEXT"
    End Sub

    '月のComboBox中身の初期化
    Private Sub MonthComboBox_Initial()
        ComboBox_month.Items.Clear()

        Dim dtMonth As New DataTable
        dtMonth.Columns.Add("VALUE")
        dtMonth.Columns.Add("TEXT")

        For i = 1 To 12
            dtMonth.Rows.Add(i, i)
        Next

        ComboBox_month.DataSource = dtMonth
        ComboBox_month.ValueMember = "VALUE"
        ComboBox_month.DisplayMember = "TEXT"
    End Sub

    '年ComboBoxの設定
    Private Sub YearComboBox_Update(year As Integer)
        ComboBox_year.SelectedValue = year
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
            daysOfMonth = DateDiff("d", Str(year) + "-" & Str(month) + "-1", Str(year) + "-" + Str(month + 1) & "-1")
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
                DataGridView1.Item(x, y).Value = "-"
                If x = 0 Then
                    DataGridView1.Item(x, y).Style.ForeColor = Color.Red
                    DataGridView1.Item(x, y).Style.BackColor = Color.FromArgb(255, 240, 240)
                ElseIf x = 6 Then
                    DataGridView1.Item(x, y).Style.ForeColor = Color.Blue
                    DataGridView1.Item(x, y).Style.BackColor = Color.FromArgb(240, 240, 255)
                Else
                    DataGridView1.Item(x, y).Style.ForeColor = Color.Black
                    DataGridView1.Item(x, y).Style.BackColor = Color.White
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
        cn.Open()
        cmd = New OleDbCommand()
        cmd.Connection = cn
        For i = 1 To daysOfMonth
            If i = day Then
                DataGridView1.Item((i - 2 + monthStartWeek) Mod 7, (i - 2 + monthStartWeek) \ 7).Selected = True
            End If
            DataGridView1.Item((i - 2 + monthStartWeek) Mod 7, (i - 2 + monthStartWeek) \ 7).Value = i

            Dim tdate As String = String.Format("{0:0000}", year) & String.Format("{0:00}", month) & String.Format("{0:00}", i)
            cmd.CommandText = "SELECT f_memo FROM tb_memo WHERE f_date =" & tdate
            Dim dr As OleDbDataReader = cmd.ExecuteReader

            If dr.HasRows Then
                dr.Read()
                If Not dr(0) = "" Then
                    DataGridView1.Item((i - 2 + monthStartWeek) Mod 7, (i - 2 + monthStartWeek) \ 7).Style.BackColor = Color.LightYellow
                End If
            End If
            dr.Close()
        Next
        cn.Close()

        '前月のカレンダーへ出力
        For i = 0 To monthStartWeek - 2
            DataGridView1.Item(i, 0).Style.ForeColor = Color.Gray
            DataGridView1.Item(i, 0).Style.BackColor = Color.FromArgb(240, 240, 240)
            DataGridView1.Item(i, 0).Value = daysOfPreMonth - monthStartWeek + 2 + i
        Next

        '次月のカレンダーへ出力
        For i = monthStartWeek + daysOfMonth To 6 * 7
            DataGridView1.Item((i - 1) Mod 7, (i - 1) \ 7).Style.ForeColor = Color.Gray
            DataGridView1.Item((i - 1) Mod 7, (i - 1) \ 7).Style.BackColor = Color.FromArgb(240, 240, 240)
            DataGridView1.Item((i - 1) Mod 7, (i - 1) \ 7).Value = i - monthStartWeek - daysOfMonth + 1
        Next
    End Sub

    'カレンダーのダブルクリックイベント
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If DataGridView1.SelectedCells.Item(0).Style.ForeColor = Color.Gray Then
            Dim tmp As String = DataGridView1.SelectedCells.Item(0).Value
            If DataGridView1.SelectedCells.Item(0).RowIndex >= 4 Then
                ButtonNext_Click(sender, New System.EventArgs())
            Else
                ButtonPre_Click(sender, New System.EventArgs())
            End If
            'セルの選択
            For x = 0 To 6
                For y = 0 To 5
                    If DataGridView1.Item(x, y).Value = tmp And
                       Not DataGridView1.Item(x, y).Style.ForeColor = Color.Gray Then
                        DataGridView1.Item(x, y).Selected = True
                    End If
                Next
            Next
        Else
            frmMemo.Initial(ComboBox_year.SelectedValue, ComboBox_month.SelectedValue, Integer.Parse(DataGridView1.SelectedCells.Item(0).Value))
            frmMemo.ShowDialog()
        End If
    End Sub

    'Enterボタンの時Tabと同じにする
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Separator Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

#End Region

#Region "ComboBox Event"

    '年ComboBoxのテキスト入力イベント
    Private Sub ComboBox_year_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox_year.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Separator Then
            ComboBox_year_Check(sender)
        End If
    End Sub

    '年ComboBoxテキストチェック処理
    Private Sub ComboBox_year_Check(sender As Object)
        Dim inputYear As Integer = 0
        If Integer.TryParse(ComboBox_year.Text, inputYear) Then
            inputYear = ComboBox_year.Text
            If inputYear >= 1000 And inputYear <= 3000 Then
                ComboBox_year.SelectedValue = inputYear
                Calendar_Update(ComboBox_year.SelectedValue, ComboBox_month.SelectedValue, 1)
                ComboBox_year.CausesValidation = False
                ComboBox_month.Select()
                ComboBox_year.CausesValidation = True
                Return
            End If
        End If
        MsgBox("入力した年をチェックしてください。")
        '入力前の日付表示
        YearComboBox_Update(mintLastCorrectYear)
    End Sub

    '年ComboBoxの入力制限
    Private Sub ComboBox_year_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox_year.KeyPress
        Dim currentKey As Integer = Convert.ToInt32(e.KeyChar)
        '0~9 と BackSpace以外の入力禁止
        If currentKey >= 48 And currentKey <= 57 Or currentKey = 8 Then
            Return
        Else
            e.Handled = True
        End If
    End Sub

    '年ComboBox
    Private Sub ComboBox_year_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ComboBox_year.Validating
        ComboBox_year.Select()
        SendKeys.Send("{Enter}")
    End Sub

    '年ComboBoxの選択イベント
    Private Sub ComboBox_year_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox_year.SelectedValueChanged
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
            If ComboBox_year.SelectedIndex <= 0 Then
                ComboBox_year.SelectedIndex = ComboBox_year.Items.Count - 1
            Else
                ComboBox_year.SelectedIndex = ComboBox_year.SelectedIndex - 1
            End If
        Else
            ComboBox_month.SelectedIndex = ComboBox_month.SelectedIndex - 1
        End If
    End Sub

    '次月ボタンイベント
    Private Sub ButtonNext_Click(sender As Object, e As EventArgs) Handles ButtonNext.Click
        If ComboBox_month.SelectedIndex >= ComboBox_month.Items.Count - 1 Then
            ComboBox_month.SelectedIndex = 0
            If ComboBox_year.SelectedIndex >= ComboBox_year.Items.Count - 1 Then
                ComboBox_year.SelectedIndex = 0
            Else
                ComboBox_year.SelectedIndex = ComboBox_year.SelectedIndex + 1
            End If
        Else
            ComboBox_month.SelectedIndex = ComboBox_month.SelectedIndex + 1
        End If
    End Sub

    '今日ボタンイベント
    Private Sub ButtonToday_Click(sender As Object, e As EventArgs) Handles ButtonToday.Click
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
