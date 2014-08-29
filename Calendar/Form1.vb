Public Class Calendar

    'Window Load処理
    Private Sub Calendar_Load(sender As Object, e As EventArgs) Handles Me.Load
        DataGridView1.RowTemplate.Height = 50
        YearComboBox_Initial()

        For i = 1 To 6
            DataGridView1.Rows.Add("-", "-", "-", "-", "-", "-", "-")
        Next
        Dim currentDate As Date = Now
        YearComboBox_Update(currentDate.Year)
        MonthComboBox_Update(currentDate.Month)
        Calendar_Update(currentDate.Year, currentDate.Month, currentDate.Day)
    End Sub

    '年のComboBox内容の初期化
    Private Sub YearComboBox_Initial()
        ComboBox_year.Items.Clear()

        For i = 1000 To 3000
            ComboBox_year.Items.Add(i)
        Next
    End Sub

    '年ComboBoxの設定
    Private Sub YearComboBox_Update(year As Integer)
        ComboBox_year.SelectedText = year
    End Sub

    '月ComboBoxの設定
    Private Sub MonthComboBox_Update(month As Integer)
        ComboBox_month.SelectedText = month
    End Sub

    'DataGridView更新処理
    Private Sub Calendar_Update(ByVal year As Integer, ByVal month As Integer, ByVal day As Integer)
        Dim daysOfMonth As Integer = 0
        If month < 12 Then
            daysOfMonth = DateDiff("d", Str(year) + "-" & Str(month) + "-1", Str(year) + "-" + Str(month + 1) & "-1")
        Else
            daysOfMonth = DateDiff("d", Str(year) + "-" & Str(month) + "-1", Str(year + 1) + "-" + Str((month + 1) Mod 12) & "-1")
        End If

        Dim monthStartWeek As Integer = Weekday(Str(year) + "-" + Str(month) + "-1")

        For i = 1 To daysOfMonth
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

End Class
