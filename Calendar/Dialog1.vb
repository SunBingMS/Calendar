Imports System.Windows.Forms
Imports System.Data.OleDb

Public Class Dialog1

    ' Connection string for ADO.NET via OleDB
    Dim cn As OleDbConnection =
        New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=memo.accdb;")
    Dim cmd As OleDbCommand
    Dim intYear As Integer
    Dim intMonth As Integer
    Dim intDay As Integer

    Public Sub Initial(ByVal year As Integer, ByVal month As Integer, ByVal day As Integer)
        intYear = year
        intMonth = month
        intDay = day

        Me.Text = year & "年" & month & "月" & day & "日 -- メモ"
        MemoContent.Text = ""
        cn.Open()
        cmd = New OleDbCommand()
        cmd.Connection = cn

        Dim tdate As String = String.Format("{0:0000}", year) & String.Format("{0:00}", month) & String.Format("{0:00}", day)
        cmd.CommandText = "SELECT f_memo FROM tb_memo WHERE f_date =" & tdate
        Dim dr As OleDbDataReader = cmd.ExecuteReader

        If dr.HasRows Then
            dr.Read()
            MemoContent.Text = dr(0)
            dr.Close()
        End If
        cn.Close()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        cn.Open()
        Dim tdate As String = String.Format("{0:0000}", intYear) & String.Format("{0:00}", intMonth) & String.Format("{0:00}", intDay)
        cmd.CommandText = "DELETE FROM tb_memo WHERE f_date =" & tdate
        'Dim dr As OleDbDataReader = cmd.ExecuteReader
        cmd.ExecuteNonQuery()
        cmd.CommandText = "INSERT INTO tb_memo VALUES('" & tdate & "','" & Me.MemoContent.Text & "')"
        cmd.ExecuteNonQuery()
        cn.Close()
        Calendar.Calendar_Update(intYear, intMonth, intDay)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
