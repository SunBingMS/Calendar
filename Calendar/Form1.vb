Public Class Calendar

    'Window Load処理
    Private Sub Calendar_Load(sender As Object, e As EventArgs) Handles Me.Load
        DataGridView1.RowTemplate.Height = 50

        DataGridView1.Rows.Add("-", "-", "-", "-", "-", "-", "-")
        DataGridView1.Rows.Add("-", "-", "-", "-", "-", "-", "-")
        DataGridView1.Rows.Add("-", "-", "-", "-", "-", "-", "-")
        DataGridView1.Rows.Add("-", "-", "-", "-", "-", "-", "-")
        DataGridView1.Rows.Add("-", "-", "-", "-", "-", "-", "-")
        DataGridView1.Rows.Add("-", "-", "-", "-", "-", "-", "-")

        Calendar_Update(2014, 8, 29)
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
