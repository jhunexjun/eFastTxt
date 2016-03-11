Imports System.Text
Public Class frmInvoices
    Private Query As CPerformQuery
    Friend SQL As StringBuilder
    Dim SQL2 As StringBuilder
    Dim r, rCount As Int16

    Private Sub frmInvoices_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo xError

        cmdRefresh.Enabled = False
        TSPrintPreview.Enabled = False

        Me.Cursor = Cursors.WaitCursor

        Me.Cursor = Cursors.WaitCursor
        ListView1.Items.Clear()

        Dim ListItemVar As ListViewItem
        Query = New CPerformQuery

        dt = Query.PerformSelectQuery(SQL.ToString, oConn3)
        Dim c As Int16 = 0, PesoValueTotal As Double
        rCount = dt.Rows.Count

        Do While c < rCount
            ListItemVar = ListView1.Items.Add(c + 1)
            With ListItemVar
                .SubItems.Add(dt.Rows(c).Item("Invoice"))
                .SubItems.Add(dt.Rows(c).Item("InvoiceDate"))
                .SubItems.Add(dt.Rows(c).Item("Customer").ToString)
                .SubItems.Add(dt.Rows(c).Item("Salesperson").ToString)
                .SubItems.Add(Format(dt.Rows(c).Item("Peso Sales"), "#,##0.00"))
                .SubItems.Add(dt.Rows(c).Item("InvoiceStat").ToString)
                .SubItems.Add(dt.Rows(c).Item("DeliveryDate").ToString)
                .SubItems.Add(dt.Rows(c).Item("DueDate").ToString)
                .SubItems.Add(dt.Rows(c).Item("LeadTime").ToString)
                .SubItems.Add(dt.Rows(c).Item("Logistics").ToString)
            End With
            PesoValueTotal += CDbl(Format(dt.Rows(c).Item("Peso Sales"), "#,##0.00"))
            c += 1
        Loop

        ListItemVar = ListView1.Items.Add("")
        With ListItemVar
            .SubItems.Add("")
            .SubItems.Add("")
            .SubItems.Add("")
            .SubItems.Add("Total")
            .SubItems.Add(Format(PesoValueTotal, "#,##0.00"))
            .SubItems.Add("")
            .SubItems.Add("")
            .SubItems.Add("")
            .SubItems.Add("")
            .SubItems.Add("")
        End With

        If c > 0 Then
            TSPrintPreview.Enabled = True
        End If

        Me.Cursor = Cursors.Default

        cmdRefresh.Enabled = True
        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub CanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CanceToolStripMenuItem.Click
        On Error GoTo xError
        If MsgBox("Are you sure you want to cancel this item?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            SQL2 = New StringBuilder
            SQL2.Append("UPDATE ArTrnDetail SET InvoiceStat='CANCELED' WHERE Invoice='" & ListView1.FocusedItem.SubItems(1).Text & "' ")
            Query = New CPerformQuery
            If Query.PerformUpdateQuery(SQL2.ToString, oConn3) > 0 Then
                MsgBox("Updating has been successful.", MsgBoxStyle.Information)
                ListView1.FocusedItem.SubItems(6).Text = "CANCELED"
            End If

            frmInvoices_Load(sender, e)

        End If

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub ContextMenuStrip1_Opened(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenuStrip1.Opened
        ContextMenuStrip1.Enabled = True
        If ListView1.FocusedItem.SubItems(6).Text.Trim.ToUpper = "DELIVERED".ToUpper Then
            ContextMenuStrip1.Enabled = False
        End If
    End Sub

    Private Sub LoadedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadedToolStripMenuItem.Click
        On Error GoTo xError
        If MsgBox("Are you sure you want to set this as 'LOADED'?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            SQL2 = New StringBuilder
            SQL2.Append("UPDATE ArTrnDetail SET InvoiceStat='LOADED' WHERE Invoice='" & ListView1.FocusedItem.SubItems(1).Text & "' ")
            Query = New CPerformQuery
            If Query.PerformUpdateQuery(SQL2.ToString, oConn3) > 0 Then
                MsgBox("Update has been successful.", MsgBoxStyle.Information)
                ListView1.FocusedItem.SubItems(6).Text = "LOADED"
            End If

            frmInvoices_Load(sender, e)

        End If
        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub ReturnedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnedToolStripMenuItem.Click
        On Error GoTo xError
        If MsgBox("Are you sure you want to set this as 'RETURNED'?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            SQL2 = New StringBuilder
            SQL2.Append("UPDATE ArTrnDetail SET InvoiceStat='RETURNED' WHERE Invoice='" & ListView1.FocusedItem.SubItems(1).Text & "' ")
            Query = New CPerformQuery
            If Query.PerformUpdateQuery(SQL2.ToString, oConn3) > 0 Then
                MsgBox("Update has been successful.", MsgBoxStyle.Information)
                ListView1.FocusedItem.SubItems(6).Text = "RETURNED"
            End If

            frmInvoices_Load(sender, e)

        End If
        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        On Error GoTo xError

        SaveFileDialog1.Filter = "Text File(*.txt)|*.txt"
        SaveFileDialog1.ShowDialog()

        If SaveFileDialog1.FileName <> "" Then
            Dim ExportToText As New CExportToText

            Dim c = 0, RowText As String = ""
            r = 0
            Dim LVColumnCnt As Int16 = ListView1.Columns.Count, LVRCnt As Integer = ListView1.Items.Count

            Do While c < LVColumnCnt
                RowText &= ListView1.Columns(c).Text & IIf(c = ListView1.Columns.Count - 1, "", vbTab)
                c += 1
            Loop

            ExportToText.CreateFile(SaveFileDialog1.FileName)
            ExportToText.WriteText(RowText)

            c = 0
            RowText = ""

            Do While r < LVRCnt
                Do While c < LVColumnCnt
                    RowText &= ListView1.Items(r).SubItems(c).Text & IIf(c = LVColumnCnt - 1, "", vbTab)
                    c += 1
                Loop

                ExportToText.WriteText(RowText)

                c = 0
                r += 1
                RowText = ""
            Loop

            ExportToText.CloseFile()
            SaveFileDialog1.FileName = ""

            MessageBox.Show("Export has been successful!", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub DeliveredToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeliveredToolStripMenuItem.Click
        On Error GoTo xError
        If MsgBox("Are you sure you want to set this as 'DELIVERED'?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            SQL2 = New StringBuilder
            SQL2.Append("UPDATE ArTrnDetail SET InvoiceStat='DELIVERED',DeliveryDate=GetDate() WHERE Invoice='" & ListView1.FocusedItem.SubItems(1).Text & "' ")
            Query = New CPerformQuery
            If Query.PerformUpdateQuery(SQL2.ToString, oConn3) > 0 Then
                MsgBox("Update has been successful.", MsgBoxStyle.Information)
                ListView1.FocusedItem.SubItems(6).Text = "DELIVERED"
            End If

            frmInvoices_Load(sender, e)

        End If

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        frmInvoices_Load(sender, e)
    End Sub

    Private Sub PendingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PendingToolStripMenuItem.Click
        On Error GoTo xError
        If MsgBox("Are you sure you want to set this as 'PENDING'?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            SQL2 = New StringBuilder
            SQL2.Append("UPDATE ArTrnDetail SET InvoiceStat='PENDING' WHERE Invoice='" & ListView1.FocusedItem.SubItems(1).Text & "' ")
            Query = New CPerformQuery
            If Query.PerformUpdateQuery(SQL2.ToString, oConn3) > 0 Then
                MsgBox("Update has been successful.", MsgBoxStyle.Information)
                ListView1.FocusedItem.SubItems(6).Text = "PENDING"
            End If

            frmInvoices_Load(sender, e)

        End If

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub TSPrintPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSPrintPreview.Click

        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor
        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(SQL.ToString, oConn3)

        Dim Report As New crDelReport

        With Report
            .SetDataSource(dt)

            .PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
            .PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter
        End With


        With frmCrystalViewer
            .CrystalReportViewer1.ReportSource = Report
            .Show()
            .MdiParent = frmMain
            .Text = "Delivery Monitoring Report"
        End With

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub ContextMenuStrip1_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        On Error GoTo xError

        If ListView1.FocusedItem.Text = "" Then
            ContextMenuStrip1.Items(0).Enabled = False
        Else
            ContextMenuStrip1.Items(0).Enabled = True
        End If

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub
End Class