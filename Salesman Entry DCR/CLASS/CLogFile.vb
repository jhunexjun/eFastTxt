Option Explicit On

Public Class CLogFile
    Public Sub LogWrite(ByVal Str As String)
        Dim LogWriter As System.IO.StreamWriter

        LogWriter = New System.IO.StreamWriter(Application.StartupPath & "\eFastTextSalesman_" & oConn.DataSource & ".txt", True)
        LogWriter.WriteLine(Date.Now & vbTab & Str)
        LogWriter.Close()

    End Sub

End Class
