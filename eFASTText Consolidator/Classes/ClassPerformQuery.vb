Imports System.Data.SqlClient

Public Class ClassPerformQuery
    Private cmd As SqlCommand
    Private da As SqlDataAdapter
    Private ds As DataSet, c As Int16

    Friend Function PerformSelectQuery(ByVal strQuery As String, ByVal oCon As SqlConnection) As DataTable
        On Error GoTo xError

        cmd = New SqlCommand(strQuery, oCon)
        da = New SqlDataAdapter
        da.SelectCommand = cmd
        ds = New DataSet
        da.Fill(ds, 0)
        PerformSelectQuery = ds.Tables(0)
        Application.DoEvents()
        Exit Function
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & Me.ToString & vbTab & Err.Number & vbTab & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Function

    Friend Function PerformUpdateQuery(ByVal strQuery As String, ByVal oCon As SqlConnection) As Integer
        On Error GoTo xError

        cmd = New SqlCommand(strQuery, oCon)
        PerformUpdateQuery = cmd.ExecuteNonQuery
        Application.DoEvents()

        Exit Function
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & Me.ToString & vbTab & Err.Number & vbTab & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Function

End Class
