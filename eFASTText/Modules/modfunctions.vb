
Module modfunctions
    Friend Function modConvertNull(ByVal v As String) As String
        If v Is Nothing Then
            v = ""
        Else
            v = v.Replace("'", "''")
        End If

        Return v
    End Function

    Friend Sub modWriteLog(ByVal LogType As String, ByVal objType As Form, ByVal xErrNum As Integer, ByRef xErrDescription As String)
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite(LogType & ": " & objType.ToString & vbTab & xErrNum & vbTab & xErrDescription)
        objType.Cursor = Cursors.Default
        MsgBox(xErrNum & Chr(13) & xErrDescription, MsgBoxStyle.Critical)
    End Sub


End Module
