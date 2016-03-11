Public Class CExportToText
    Private CExportedFile As System.IO.StreamWriter

    Friend Sub CreateFile(ByVal sPath As String)
        CExportedFile = New System.IO.StreamWriter(sPath)
    End Sub
    
    Friend Sub WriteText(ByVal sString As String)
        CExportedFile.WriteLine(sString)
    End Sub

    Friend Sub CloseFile()
        CExportedFile.Close()
    End Sub

End Class
