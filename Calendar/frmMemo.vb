Imports System.Windows.Forms
Imports System.Data.OleDb


Public Class frmMemo

    ' Connection string for ADO.NET via OleDB
    Dim odbcnConnection As OleDbConnection =
        New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=memo.mdb;")
    Dim odbcmdCommand As OleDbCommand
    '年月日
    Dim intYear As Integer
    Dim intMonth As Integer
    Dim intDay As Integer
    'DBのメモ
    Dim strMemoDB As String = ""

    'ダイアログの初期化

    Public Sub Initial(ByVal year As Integer, ByVal month As Integer, ByVal day As Integer)
        '年月日
        intYear = year
        intMonth = month
        intDay = day

        '当日のメモ内容を検索する
        Me.Text = year & "年" & month & "月" & day & "日 -- メモ"
        MemoContent.Text = ""
        strMemoDB = ""
        odbcnConnection.Open()
        odbcmdCommand = New OleDbCommand()
        odbcmdCommand.Connection = odbcnConnection

        Dim tdate As String = String.Format("{0:0000}", year) & String.Format("{0:00}", month) & String.Format("{0:00}", day)
        odbcmdCommand.CommandText = "SELECT f_memo FROM tb_memo WHERE f_date =" & tdate
        Dim dr As OleDbDataReader = odbcmdCommand.ExecuteReader

        '検索結果があれば
        If dr.HasRows Then
            dr.Read()
            'メモの表示
            strMemoDB = String_Decode(dr(0))
            MemoContent.Text = strMemoDB

            dr.Close()
        End If
        odbcnConnection.Close()
    End Sub

    'メモの保存処理
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        odbcnConnection.Open()
        '当日の古い内容を削除する
        Dim tdate As String = String.Format("{0:0000}", intYear) & String.Format("{0:00}", intMonth) & String.Format("{0:00}", intDay)
        odbcmdCommand.CommandText = "DELETE FROM tb_memo WHERE f_date =" & tdate
        odbcmdCommand.ExecuteNonQuery()
        If Not MemoContent.Text = "" Then
            '当日の新メモを追加する
            odbcmdCommand.CommandText = "INSERT INTO tb_memo VALUES('" & tdate & "','" & String_Encode(MemoContent.Text) & "')"
            odbcmdCommand.ExecuteNonQuery()
        End If
        odbcnConnection.Close()
        frmCalendar.Calendar_Update(intYear, intMonth, intDay)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    'キャンセル処理
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        If Not strMemoDB = MemoContent.Text Then
            Dim result As DialogResult = MessageBox.Show("メモの編集がありますが、保存しますか？", _
                                             "質問", _
                                             MessageBoxButtons.YesNo, _
                                             MessageBoxIcon.Exclamation, _
                                             MessageBoxDefaultButton.Button1)
            '何が選択されたか調べる 
            If result = DialogResult.Yes Then
                '「はい」が選択された時 
                OK_Button_Click(sender, New System.EventArgs())
            End If
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    'エンコード
    Private Function String_Encode(input As String) As String
        Return input.Replace("'", "['']")
    End Function

    'ディコード
    Private Function String_Decode(input As String) As String
        Return input.Replace("[']", "'")
    End Function

End Class
