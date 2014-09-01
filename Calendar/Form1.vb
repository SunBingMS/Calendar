Public Class Calendar

    'ComboBoxの有効性フラグ
    Private isReady As Boolean = False

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

        '年月ComboBoxのデフォールト値設定
        Dim currentDate As Date = Now
        YearComboBox_Update(currentDate.Year)
        MonthComboBox_Update(currentDate.Month)
        'カレンダー更新
        Calendar_Update(currentDate.Year, currentDate.Month, currentDate.Day)
        'ComboBox有効にする
        isReady = True
    End Sub

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

    'DataGridView更新処理
    Private Sub Calendar_Update(ByVal year As Integer, ByVal month As Integer, ByVal day As Integer)
        '----------------------
        For x = 0 To 6
            For y = 0 To 5
                DataGridView1.Item(x, y).Value = "-"
            Next
        Next
        '----------------------

        '当該月の日数算出
        Dim daysOfMonth As Integer = 0
        If month < 12 Then
            daysOfMonth = DateDiff("d", Str(year) + "-" & Str(month) + "-1", Str(year) + "-" + Str(month + 1) & "-1")
        Else
            daysOfMonth = DateDiff("d", Str(year) + "-" & Str(month) + "-1", Str(year + 1) + "-" + Str((month + 1) Mod 12) & "-1")
        End If

        '一日の曜日算出
        Dim monthStartWeek As Integer = Weekday(Str(year) + "-" + Str(month) + "-1")

        'カレンダーへ出力
        For i = 1 To daysOfMonth
            If i = day Then
                DataGridView1.Item((i - 2 + monthStartWeek) Mod 7, (i - 2 + monthStartWeek) \ 7).Selected = True
            End If
            DataGridView1.Item((i - 2 + monthStartWeek) Mod 7, (i - 2 + monthStartWeek) \ 7).Value = i
        Next

        'Select Case monthStartWeek
        '    Case 1
        '        For i = 1 To daysOfMonth
        '            DataGridView1.Item((i - 1) Mod 7, i - 2 + monthStartWeek).Value = i
        '        Next

        '    Case 2
        '        For i = 1 To daysOfMonth
        '            DataGridView1.Item((i - 1) Mod 7, i).Value = i
        '        Next

        '    Case 3
        '        For i = 1 To daysOfMonth
        '            DataGridView1.Item((i - 1) Mod 7, i + 1).Value = i
        '        Next

        '    Case 4


        '    Case 5


        '    Case 6


        '    Case 7

        'End Select


    End Sub

    '年ComboBoxの選択イベント
    Private Sub ComboBox_year_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox_year.SelectedValueChanged
        'ComboBox有効性フラグチェック
        If Not isReady Then
            Return
        End If

        'カレンダー更新
        Calendar_Update(ComboBox_year.Text, ComboBox_month.Text, 1)
    End Sub

    '月ComboBoxの選択イベント
    Private Sub ComboBox_month_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox_month.SelectedValueChanged
        'ComboBox有効性フラグチェック
        If Not isReady Then
            Return
        End If

        'カレンダー更新
        Calendar_Update(ComboBox_year.Text, ComboBox_month.Text, 1)
    End Sub

    '前月ボタンイベント
    Private Sub ButtonPre_Click(sender As Object, e As EventArgs) Handles ButtonPre.Click
        If ComboBox_month.SelectedIndex <= 0 Then
            ComboBox_month.SelectedIndex = ComboBox_month.Items.Count - 1
        Else
            ComboBox_month.SelectedIndex = ComboBox_month.SelectedIndex - 1
        End If


    End Sub
End Class
