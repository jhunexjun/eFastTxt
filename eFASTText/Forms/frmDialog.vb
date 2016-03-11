Option Explicit On

Imports System.Data.SqlClient
Public Class frmDialog
    Friend strToPerform As String

    Friend DatabaseConnection As Int16

    Friend Sel(100) As String
    Dim c As Int16

    Private Sub frmDialog_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Call SetArrayToNone()
        Call cmdCancel_Click(sender, e)
    End Sub

    Private Sub frmDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo xError
        Dim Query As CPerformQuery
        Dim dt As DataTable
        Dim ListItemVar As New System.Windows.Forms.ListViewItem
        Dim WriteLog As New CLogFile

        Query = New CPerformQuery
        dt = New DataTable
        If DatabaseConnection = 1 Then
            dt = Query.PerformSelectQuery(strToPerform, oConn)
        ElseIf DatabaseConnection = 2 Then
            'dt = Query.PerformSelectQuery(strToPerform, oConn2)
            WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
            MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
        ElseIf DatabaseConnection = 3 Then
            dt = Query.PerformSelectQuery(strToPerform, oConn2)
        End If

        If dt.Rows.Count() > 0 Then
            Dim ColumnsIndex, RowIndex As Int32
            Do While ColumnsIndex < dt.Columns.Count()
                ListView1.Columns.Add(dt.Columns.Item(ColumnsIndex).ToString)
                ColumnsIndex += 1
            Loop
            ColumnsIndex = 0
            Do While RowIndex < dt.Rows.Count()

                ListItemVar = ListView1.Items.Add(dt.Rows(RowIndex).Item(ColumnsIndex))
                ColumnsIndex += 1
                Do While ColumnsIndex < dt.Columns.Count
                    ListItemVar.SubItems.Add(IIf(IsDBNull(dt.Rows(RowIndex).Item(ColumnsIndex)), "", dt.Rows(RowIndex).Item(ColumnsIndex)))
                    ColumnsIndex += 1
                Loop

                ColumnsIndex = 0
                RowIndex += 1
            Loop

            cmdSelect.Enabled = True
            cmdExport.Enabled = True
            Label1.Text = Format(dt.Rows.Count, "#,##0") & " record(s) found."
        End If
        Exit Sub
xError:
        WriteLog = New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub frmDialog_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Width = 925
        Me.Height = 499
    End Sub

    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        On Error GoTo xError
        Dim ExportedFile As System.IO.StreamWriter

        SaveFileDialog1.Filter = "Text Files(*.txt)|*.txt"
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.CheckFileExists() = False Then
            'Kill(SaveFileDialog1.FileName)

            If SaveFileDialog1.FileName = Nothing Then
                Exit Sub
            Else
                ExportedFile = New System.IO.StreamWriter(SaveFileDialog1.FileName)
            End If

            Dim ColumnIndex, RowIndex As Int32, RowTEXT As String = "", IsFirstColumn As Boolean

            RowTEXT = ""
            Do While ColumnIndex < ListView1.Columns.Count
                RowTEXT = RowTEXT & ListView1.Columns(ColumnIndex).Text & vbTab
                ColumnIndex += 1
            Loop
            ExportedFile.WriteLine(RowTEXT)
            RowIndex = 0 : ColumnIndex = 0

            Do While RowIndex < ListView1.Items.Count
                RowTEXT = ""
                Do While ColumnIndex < ListView1.Columns.Count

                    If IsFirstColumn = True Then
                        RowTEXT = RowTEXT & ListView1.Items(RowIndex).Text & vbTab
                    Else
                        RowTEXT = RowTEXT & ListView1.Items(RowIndex).SubItems(ColumnIndex).Text & vbTab
                    End If

                    IsFirstColumn = False
                    ColumnIndex += 1
                Loop
                'Me()
                ExportedFile.WriteLine(RowTEXT)

                RowIndex += 1
                ColumnIndex = 0
                IsFirstColumn = True
            Loop

            ExportedFile.Close()
            MessageBox.Show("Export has been successful!", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Exit Sub
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub SetArrayToNone()
        Do While c <= 99
            Sel(c) = ""
            c += 1
        Loop
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        
        Do While c < ListView1.Columns.Count
            If c = 0 Then
                Sel(c) = ListView1.FocusedItem.Text
            Else
                Sel(c) = ListView1.FocusedItem.SubItems(c).Text
            End If

            c += 1
        Loop

        Me.Close()
    End Sub

    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        ListView1_DoubleClick(sender, e)
    End Sub
End Class