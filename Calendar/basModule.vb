'********************************************************************
'  システム            ：   カレンダーシステム
'  サブシステム名 　   ：   DB共通
'  クラス名　　　      ：   basModule
'  機能概要　　　      ：   
'  作成日      　　　　：   2014/09/05
'  作成者      　　　　：   SKB 孫　氷
'  変更履歴    　　　　：   
'********************************************************************
Option Strict On
Option Explicit On
Option Compare Binary

Imports System.Data.OleDb

Module basModule

    Public ReadOnly grstrDBName As String = "memo.mdb"

    'データベースアクセス関連
    Public godbcnConnection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _
                                                  grstrDBName & ";")
    Public godbcmdCommand As OleDbCommand

    Public godbtTransaction As OleDbTransaction

End Module
