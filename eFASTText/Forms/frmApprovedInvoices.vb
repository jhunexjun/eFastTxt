Imports System.Text
Imports System.Windows.Forms
Public Class frmApprovedInvoices
    Private SQL As StringBuilder
    Private Query As CPerformQuery, c As Int16

    Private Sub DTFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTFrom.ValueChanged
        ClearValues()
    End Sub

    Private Sub DTTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTTo.ValueChanged
        ClearValues()
    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError

        Dim ListItemVar As ListViewItem
        ClearValues()
        cmdFind.Enabled = False
        TSExport.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        If cmdFind.Text = "&Find" Then
            SQL = New StringBuilder
            SQL.Append("Declare @DateFrom as DateTime,@DateTo as DateTime ")
            SQL.Append("SET @DateFrom='" & DTFrom.Value & "' ")
            SQL.Append("SET @DateTo='" & DTTo.Value & "' ")
            SQL.Append("SET @DateFrom=convert(nvarchar(30),@DateFrom,101) ")
            SQL.Append("SET @DateTo=convert(nvarchar(30),@DateTo,101) ")
            SQL.Append("SELECT AA.[Name],BB.* FROM (select b.SalesOrder,a.[Name] FROM " & oConn.Database & ".dbo.ArCustomer a Inner Join " & oConn.Database & ".dbo.ArInvoice b ON a.Customer=b.Customer)[AA] ")
            SQL.Append("INNER JOIN ")
            SQL.Append("(SELECT (LTRIM(RTRIM(a.cFirstName))+' '+LTRIM(RTRIM(a.cFamilyName)))[ApproverName],a.cMobile,dbo.fn_GetAPRVInv(b.CommandUsed)[SalesOrder],convert(nvarchar(30),b.oDateTimeIn,101)[oDateTimeIn] FROM " & oConn2.Database & ".[dbo].[tblClients] a Inner Join " & oConn2.Database & ".dbo.tblsmsOUT b ON a.cMobile=b.oMobileNo WHERE b.oRepliedMSG Like '% was successfully approved. %' AND b.CommandUsed Like 'APRV %' AND convert(nvarchar(30),b.oDateTimeIn,101)>=@DateFrom AND convert(nvarchar(30),b.oDateTimeIn,101)<=@DateTo)[BB] ")
            SQL.Append("ON AA.SalesOrder=BB.SalesOrder ")

            Query = New CPerformQuery
            dt.Rows.Clear()
            dt = Query.PerformSelectQuery(SQL.ToString, oConn2)
            c = 0
            Do While c < dt.Rows.Count
                ListItemVar = ListView1.Items.Add(c + 1)
                ListItemVar.SubItems.Add(dt.Rows(c).Item("ApproverName"))
                ListItemVar.SubItems.Add(dt.Rows(c).Item("SalesOrder"))
                ListItemVar.SubItems.Add(dt.Rows(c).Item("Name"))
                ListItemVar.SubItems.Add(dt.Rows(c).Item("oDateTimeIn"))
                c += 1
            Loop

            If dt.Rows.Count <= 0 Then
                MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
            Else
                TSExport.Enabled = True
            End If
        End If

        Me.Cursor = Cursors.Default
        cmdFind.Enabled = True

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
        cmdFind.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ClearValues()
        ListView1.Items.Clear()
    End Sub

    Private Sub frmApprovalReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DTFrom.MaxDate = Today
        DTTo.MaxDate = Today
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub TSExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSExport.Click
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
End Class