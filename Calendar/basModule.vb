Imports System.Data.OleDb

Module basModule

    'データベースアクセス関連
    Public odbcnConnection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=memo.mdb;")
    Public odbcmdCommand As New OleDbCommand

End Module
