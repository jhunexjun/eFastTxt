Imports My
Public Class CExcelWriter
    Private xlApp As Excel9.Interop.Application
    Private xlBook As Excel9.Interop.Workbook
    Private xlSheet As Excel9.Interop.Worksheet
    Private xlRange As Excel9.Interop.Range

    Public Sub New()
    End Sub

    Public Sub OpenWorkSheet(ByVal SourceFile As String)
        xlApp = New Excel9.Interop.Application
        xlApp.Visible = False
        xlBook = xlApp.Workbooks.Open(SourceFile)
    End Sub

    Public Sub CreateNewSheet(ByVal SheetNameFrom As String, ByVal SheetNameTo As String)
        xlSheet = xlBook.Sheets(SheetNameFrom)
        xlSheet.Copy(After:=xlApp.Worksheets(SheetNameFrom))
        xlSheet = Nothing
        xlSheet = xlBook.Sheets("OR FORMAT (2)")
        xlSheet.Name = SheetNameTo
    End Sub

    Public Sub WriteToSheet(ByVal sSheetName As String, ByVal iRow As Integer, ByVal iColumn As Integer, ByVal objValue As Object, ByVal sType As Char)
        xlSheet = xlBook.Sheets(sSheetName)
        If sType = "N" Then
            xlSheet.Cells(iRow, iColumn) = CDec(objValue)
        ElseIf sType = "S" Then
            xlSheet.Cells(iRow, iColumn) = CStr(objValue)
        End If
    End Sub

    Public Sub HideSheet(ByVal SheetName As String)
        xlSheet = xlBook.Sheets(SheetName)
        xlSheet.Visible = Excel9.Interop.XlSheetVisibility.xlSheetVeryHidden
    End Sub

    Public Sub CloseWorkSheet(ByVal sFileDest As String)
        xlBook.SaveAs(sFileDest)
        xlBook.Close()
        xlApp = Nothing
    End Sub

    Public Sub OpenWorkSheetTest(ByVal sFileSource As String)
        xlApp = New Excel9.Interop.Application
        xlApp.Visible = True
        xlBook = xlApp.Workbooks.Open(sFileSource)
    End Sub

    Friend Sub SumColumn(ByVal Sheet As String, ByVal Row As Int32, ByVal Column As Int32)
        xlSheet = xlBook.Sheets(Sheet)
        'xlSheet.Range("D29").Subtotal(:=1,function:=

    End Sub

End Class
