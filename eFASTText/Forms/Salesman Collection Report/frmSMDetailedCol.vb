Imports System.Text
Public Class frmSMDetailedColSummary
    Friend vFrom, vTo As DateTime, vFilter, vCheckDueOnly As Boolean, vSalesperson As String

    Dim Query As CPerformQuery
    Dim c As Int16 = 0
    Private ListItemVar As ListViewItem
    Friend owned As Boolean

    Private Sub frmDetailedCollectionSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If owned = True Then
        Else
            vFrom = Today
            vTo = Today
        End If

    End Sub

    Friend Sub ShowDetailes()
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        ListView1.Items.Clear()
        If cmdFind.Text = "&Find" Then

            txtGrandTotal.Text = "0.00"

            Query = New CPerformQuery

            dt = Query.PerformSelectQuery(GetSQL(), oConn2)

            Do While c < dt.Rows.Count

                ListItemVar = ListView1.Items.Add(c + 1)
                ListItemVar.SubItems.Add(dt.Rows(c).Item("Salesperson"))
                ListItemVar.SubItems.Add(dt.Rows(c).Item("Type"))
                ListItemVar.SubItems.Add(dt.Rows(c).Item("oRegName").ToString)
                ListItemVar.SubItems.Add(dt.Rows(c).Item("Bank").ToString)
                ListItemVar.SubItems.Add(dt.Rows(c).Item("DueDate").ToString)
                ListItemVar.SubItems.Add(Format(dt.Rows(c).Item("Payment").ToString, "Standard"))

                txtGrandTotal.Text = CDbl(txtGrandTotal.Text) + CDbl(dt.Rows(c).Item("Payment"))

                c += 1
            Loop

            txtGrandTotal.Text = Format(txtGrandTotal.Text, "Standard")

            AddBlankLVItem(ListView1, ListView1.Columns.Count - 3)
            ListItemVar.SubItems.Add("Grand Total")
            ListItemVar.SubItems.Add(txtGrandTotal.Text)

            If dt.Rows.Count > 0 Then
                TSPrintPrevw.Enabled = True
            End If
        End If
        Me.Cursor = Cursors.Default
        cmdFind.Enabled = True

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub AddBlankLVItem(ByVal ListViewName As ListView, ByVal iCount As Int32)
        c = 0

        ListItemVar = ListViewName.Items.Add("")
        Do While c < iCount
            ListItemVar.SubItems.Add("")
            c += 1
        Loop
    End Sub

    Friend Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        cmdFind.Enabled = False
        If owned = True Then
        End If

        ShowDetailes()
    End Sub

    Private Function GetSQL() As String
        On Error GoTo xError

        Dim SQL As New StringBuilder
        SQL.Append("Declare @DateFrom as DateTime,@DateTo as DateTime,@Salesperson as nvarchar(20) ")
        SQL.Append("SET @DateFrom='" & vFrom & "' ")
        SQL.Append("SET @DateTo='" & vTo & "' ")
        SQL.Append("SET @DateFrom=CONVERT(nvarchar(30),@DateFrom,101) ")
        SQL.Append("SET @DateTo=CONVERT(nvarchar(30),@DateTo,101) ")
        SQL.Append("SET @Salesperson='" & vSalesperson & "' ")
        SQL.Append("SELECT * FROM (SELECT a.Salesperson,'Cash'[Type],oRegName,''[Bank],''[DueDate],dbo.fn_GetCashPayment(CommandUsed)[Payment] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCH %') ")

        If vFilter = True Then
            SQL.Append("AND a.Salesperson=@Salesperson ")
        End If

        SQL.Append("AND CONVERT(nvarchar(30),oDateTimeIn,101)>=@DateFrom AND CONVERT(nvarchar(30),oDateTimeIn,101)<=@DateTo AND oRepliedMSG Like '%successful%' ")
        SQL.Append("Union ALL ")
        SQL.Append("SELECT a.Salesperson,'Check'[Type],oRegName,dbo.fn_GetBankName(CommandUsed)[Bank],CONVERT(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101)[DueDate],dbo.fn_GetCheckPayment(CommandUsed)[Payment] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCK %') ")

        If vFilter = True Then
            SQL.Append(" AND a.Salesperson=@Salesperson ")
        End If

        If vFilter = True AndAlso vCheckDueOnly = True Then
            SQL.Append("AND CONVERT(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101)>=@DateFrom ")
            SQL.Append("AND CONVERT(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101)<=@DateTo ")
        Else
            SQL.Append("AND CONVERT(nvarchar(30),oDateTimeIn,101)>=@DateFrom AND CONVERT(nvarchar(30),oDateTimeIn,101)<=@DateTo ")
        End If

        SQL.Append("AND oRepliedMSG Like '%successful%')[TableA] ORDER BY TABLEA.Salesperson ")

        Return SQL.ToString

        Exit Function
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Function

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ShowDetailes()
    End Sub

    Private Sub txtSalesperson_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ClearValues()
    End Sub

    Private Sub DTFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ClearValues()
    End Sub

    Private Sub DTTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ClearValues()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ClearValues()
        ListView1.Items.Clear()
        txtGrandTotal.Text = "0.00"
    End Sub

    Private Sub TSPrintPrevw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSPrintPrevw.Click
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        Dim Report As New crDetailedSalesmanCollection

        dt = Query.PerformSelectQuery(GetSQL(), oConn2)

        With Report
            .SetDataSource(dt)
            .SetParameterValue(0, vCheckDueOnly)
            .SetParameterValue(1, vFrom)
            .SetParameterValue(2, vTo)
            .SetParameterValue(3, UserName)
            .SetParameterValue(4, vSalesperson)

            .PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            .PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter

        End With

        With frmCrystalViewer
            .CrystalReportViewer1.ReportSource = Report
            .MdiParent = frmMain
            .Show()
            .Text = "Detailed Salesman Collection Report"
        End With

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub cmdBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class