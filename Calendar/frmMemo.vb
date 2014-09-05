'********************************************************************
'  システム            ：   カレンダーシステム
'  サブシステム名 　   ：   メモ表示ダイアログ
'  クラス名　　　      ：   frmMemo
'  機能概要　　　      ：   
'  作成日      　　　　：   2014/09/05
'  作成者      　　　　：   SKB 孫　氷
'  変更履歴    　　　　：   
'********************************************************************
Option Strict On
Option Explicit On
Option Compare Binary

Imports System.Windows.Forms
Imports System.Data.OleDb

''' <summary>
''' メモダイアログ
''' </summary>
''' <remarks></remarks>
Public Class frmMemo

    Dim mintYear As Integer = 0      '年
    Dim mintMonth As Integer = 0     '月
    Dim mintDay As Integer = 0       '日
    Dim mstrMemoDB As String = ""    'DBのメモ

    ''' <summary>
    ''' ダイアログの初期化
    ''' </summary>
    ''' <param name="pYear">年</param>
    ''' <param name="pMonth">月</param>
    ''' <param name="pDay">日</param>
    ''' <remarks></remarks>
    Public Sub subInitial(ByVal pYear As Integer, ByVal pMonth As Integer, ByVal pDay As Integer)

        Debug.WriteLine("メモダイアログ初期化開始：" & pYear & "年" & pMonth & "月" & pDay & "日")

        '年月日
        mintYear = pYear
        mintMonth = pMonth
        mintDay = pDay

        'ダイアログタイトルの設定
        Me.Text = pYear & "年" & pMonth & "月" & pDay & "日 -- メモ"
        rtbMemoContent.Text = ""
        mstrMemoDB = ""

        Try
            '当日のメモ内容を検索する
            godbcmdCommand = New OleDbCommand()
            godbcmdCommand.Connection = godbcnConnection

            Dim strDate As String = String.Format("{0:0000}", pYear) & _
                                    String.Format("{0:00}", pMonth) & _
                                    String.Format("{0:00}", pDay)
            godbcmdCommand.CommandText = "SELECT f_memo FROM tb_memo WHERE f_date =" & strDate

            'メモデータ取得()
            Dim dr As OleDbDataReader = godbcmdCommand.ExecuteReader

            '検索結果があれば
            If dr.HasRows Then
                dr.Read()
                'メモの表示
                mstrMemoDB = fncDecodeString(dr(0).ToString)
                rtbMemoContent.Text = mstrMemoDB

                Debug.WriteLine("メモ取得")

                dr.Close()
            End If

        Catch ex As Exception

            MsgBox("DBロードエラー。" & vbNewLine & "「" & grstrDBName & "」を確認してください。")

            Debug.WriteLine("DBロードエラー")

        Finally

            godbcmdCommand.Dispose()

            Debug.WriteLine("メモダイアログ初期化終了")

        End Try

    End Sub

    ''' <summary>
    ''' メモの保存処理
    ''' </summary>
    ''' <param name="sender">イベント発生源オブジェクト</param>
    ''' <param name="e">イベントに関連する補足情報</param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        Debug.WriteLine("メモダイアログ保存開始")

        Try
            'Transaction開始
            godbtTransaction = godbcnConnection.BeginTransaction()

            '当日の古い内容を削除する
            Dim tdate As String = String.Format("{0:0000}", mintYear) & _
                                  String.Format("{0:00}", mintMonth) & _
                                  String.Format("{0:00}", mintDay)

            godbcmdCommand = New OleDbCommand()
            godbcmdCommand.Connection = godbcnConnection
            godbcmdCommand.CommandText = "DELETE FROM tb_memo WHERE f_date =" & tdate
            godbcmdCommand.Transaction = godbtTransaction
            godbcmdCommand.ExecuteNonQuery()
            Debug.WriteLine("メモレコーダー削除")

            If Not rtbMemoContent.Text = "" Then

                '当日の新メモを追加する
                godbcmdCommand.CommandText = "INSERT INTO tb_memo VALUES('" & tdate & "','" & _
                                            fncEncodeString(rtbMemoContent.Text) & "')"
                godbcmdCommand.ExecuteNonQuery()
                Debug.WriteLine("メモレコーダー追加")

            End If

            godbtTransaction.Commit()

        Catch ex As Exception

            godbtTransaction.Rollback()

            MsgBox("DB書込エラー。" & vbNewLine & "「" & grstrDBName & "」を確認してください。")

            Debug.WriteLine("DB書込エラー")

        Finally

            godbcmdCommand.Dispose()

            Debug.WriteLine("メモダイアログ保存終了")

        End Try

        'カレンダー更新
        frmCalendar.subUpdateCalendar(mintYear, _
                                      mintMonth, _
                                      mintDay)

        'ダイアログ閉じる
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    ''' <summary>
    ''' キャンセル処理
    ''' </summary>
    ''' <param name="sender">イベント発生源オブジェクト</param>
    ''' <param name="e">イベントに関連する補足情報</param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If Not mstrMemoDB = rtbMemoContent.Text Then
            Dim drResult As DialogResult = MessageBox.Show("メモの編集がありますが、保存しますか？", _
                                             "質問", _
                                             MessageBoxButtons.YesNo, _
                                             MessageBoxIcon.Exclamation, _
                                             MessageBoxDefaultButton.Button1)

            '何が選択されたか調べる 
            If drResult = DialogResult.Yes Then
                '「はい」が選択された時 
                btnOK_Click(sender, New System.EventArgs())
            End If
        End If

        'ダイアログ閉じる
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>
    ''' エンコード
    ''' </summary>
    ''' <param name="input">エンコード前の文字列</param>
    ''' <returns>エンコード後の文字列</returns>
    ''' <remarks></remarks>
    Private Function fncEncodeString(input As String) As String

        Debug.WriteLine("メモ内容のエンコード")

        Return input.Replace("'", "['']")

    End Function

    ''' <summary>
    ''' ディコード
    ''' </summary>
    ''' <param name="input">ディコード前の文字列</param>
    ''' <returns>ディコード後の文字列</returns>
    ''' <remarks></remarks>
    Private Function fncDecodeString(input As String) As String

        Debug.WriteLine("メモ内容のディコード")

        Return input.Replace("[']", "'")

    End Function

End Class
