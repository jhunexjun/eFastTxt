Imports System.Text
Public Class frmDeposit_DPS
    Private SQL As StringBuilder
    Private Query As CPerformQuery
    Private ListItemVar As ListViewItem

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError
        Dim Cash, Cheq, DPS As Double
        ListView1.Items.Clear()
        cmdFind.Enabled = False
        Cursor = Cursors.WaitCursor
        ToolStripMenuItem5.Enabled = False

        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(GetSQL.ToString, oConn2)
        r = 0
        RowCount = dt.Rows.Count

        Do While r < RowCount
            ListItemVar = ListView1.Items.Add(r + 1)
            ListItemVar.SubItems.Add(dt.Rows(r).Item("DateField"))
            ListItemVar.SubItems.Add(Format(dt.Rows(r).Item("Cash"), "standard"))
            ListItemVar.SubItems.Add(Format(dt.Rows(r).Item("CheckP"), "standard"))
            ListItemVar.SubItems.Add(Format(dt.Rows(r).Item("TotalDep"), "standard"))
            ListItemVar.SubItems.Add(Format(dt.Rows(r).Item("DPSNetTotal"), "standard"))

            Cash += dt.Rows(r).Item("Cash")
            Cheq += dt.Rows(r).Item("CheckP")
            DPS += dt.Rows(r).Item("DPSNetTotal")

            ToolStripMenuItem5.Enabled = True
            r += 1
        Loop

        txtCash.Text = Format(Cash, "standard")
        txtCheck.Text = Format(Cheq, "standard")
        txtTotalForDeposit.Text = Format(Cash + Cheq, "standard")
        txtTotalDPS.Text = Format(DPS, "standard")

        cmdFind.Enabled = True
        Cursor = Cursors.Default
        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
        Cursor = Cursors.Default
        cmdFind.Enabled = False
    End Sub

    Private Function GetSQL() As String
        On Error GoTo xError
        Sql = New StringBuilder
        SQL.Append("Declare @DateForCashFrom as DateTime,@DateForCashTo as DateTime,@DateForCheckFrom as DateTime,@DateForCheckTo as DateTime,@DateWeekName as nvarchar(15) ")
        SQL.Append("SET @DateForCashFrom='" & DTFrom.Value & "' ")
        SQL.Append("SET @DateForCashTo='" & DTTo.Value & "' ")
        SQL.Append("SET @DateForCheckFrom=@DateForCashFrom ")
        SQL.Append("SET @DateForCheckTo=@DateForCashTo ")
        SQL.Append("SET @DateForCashFrom=CONVERT(nvarchar(30),@DateForCashFrom,101) ")
        SQL.Append("SET @DateForCashTo=CONVERT(nvarchar(30),@DateForCashTo,101) ")
        SQL.Append("SET @DateForCheckFrom=CONVERT(nvarchar(30),@DateForCheckFrom,101) ")
        SQL.Append("SET @DateForCheckTo=CONVERT(nvarchar(30),@DateForCheckTo,101) ")
        SQL.Append("Declare @Days Table (DateField datetime) ")
        SQL.Append("Declare @CurrentDate datetime ")
        SQL.Append("Declare @EndDate datetime ")
        SQL.Append("Set @CurrentDate=@DateForCashFrom ")
        SQL.Append("Set @EndDate=@DateForCashTo ")
        SQL.Append("While @CurrentDate <= @EndDate ")
        SQL.Append("Begin ")
        SQL.Append("    Insert Into @Days Values(@CurrentDate) ")
        SQL.Append("    Set @CurrentDate = DateAdd(d,1,@CurrentDate) ")
        SQL.Append("End ")
        SQL.Append("Select CONVERT(nvarchar,DateField,101)[DateField],ISNULL(Cash,0)[Cash],ISNULL(CheckP,0)[CheckP],ISNULL(TotalDep,0)[TotalDep],ISNULL(DPSNetTotal,0)[DPSNetTotal] From @Days As DaysTable LEFT JOIN ")
        SQL.Append("( ")
        SQL.Append("SELECT DepositDate,Cash=sum(CASE WHEN Type='Cash' THEN Payment ELSE 0 END),CheckP=SUM(CASE WHEN Type='Check' THEN Payment ELSE 0 END),(sum(CASE WHEN Type='Cash' THEN Payment ELSE 0 END)+SUM(CASE WHEN Type='Check' THEN Payment ELSE 0 END))[TotalDep] ")
        SQL.Append("FROM (")
        SQL.Append("Select DISTINCT * FROM ")
        SQL.Append("(SELECT oID,CONVERT(nvarchar,oDateTimeIn,101)[oDateTimeIn],dbo.fn_DepSummaryCash(DateRemitted,(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END))[DepositDate],'Cash'[Type],dbo.fn_GetCashPayment(CommandUsed)[Payment],(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID Left Outer Join BP_Bank c ON dbo.fn_GetCustCode(oRegName)=c.BPCode ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCH %') AND Remitted=1 AND DateRemitted IS NOT NULL AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)>=@DateForCashFrom AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)<=@DateForCashTo AND oRepliedMSG Like '%successful%' AND dbo.fn_GetCashPayment(CommandUsed)>0 AND (OnlinePymnt IS NULL OR OnlinePymnt='N') ")
        SQL.Append("UNION ALL ")
        SQL.Append("SELECT oID,CONVERT(nvarchar,oDateTimeIn,101)[oDateTimeIn],dbo.fn_DepSumCheckDue(dbo.fn_GetCheckDueDate(CommandUsed),DateRemitted),'Check'[Type],dbo.fn_GetCheckPayment(CommandUsed)[Payment],(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID  Left Outer Join BP_Bank c ON dbo.fn_GetCustCode(oRegName)=c.BPCode ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCK %') AND Remitted=1 ")
        SQL.Append("AND DateRemitted IS NOT NULL AND oRepliedMSG Like '%successful%' AND dbo.fn_DepSumCheckDue(convert(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101), CONVERT(nvarchar(30),DateRemitted,101))>=@DateForCheckFrom AND dbo.fn_DepSumCheckDue(convert(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101), CONVERT(nvarchar(30),DateRemitted,101))<=@DateForCheckTo AND (OnlinePymnt IS NULL OR OnlinePymnt='N') ")
        SQL.Append("UNION ALL ")
        SQL.Append("SELECT oID,CONVERT(nvarchar,oDateTimeIn,101)[oDateTimeIn],dbo.fn_DepSummaryCash(DateRemitted,(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)),'Cash'[Type],dbo.fn_GetCashPayment(CommandUsed)[Payment],(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID Left Outer Join BP_Bank c ON dbo.fn_GetCustCode(oRegName)=c.BPCode ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCH %') AND Remitted=1 AND DateRemitted IS NOT NULL AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)>=@DateForCashFrom AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)<=@DateForCashTo AND oRepliedMSG Like '%successful%' AND dbo.fn_GetCashPayment(CommandUsed)>0 AND (OnlinePymnt='Y') ")
        SQL.Append("UNION ALL ")
        SQL.Append("SELECT oID,CONVERT(nvarchar,oDateTimeIn,101)[oDateTimeIn],dbo.fn_DepSumCheckDue(dbo.fn_GetCheckDueDate(CommandUsed),DateRemitted),'Check'[Type],dbo.fn_GetCheckPayment(CommandUsed)[Payment],(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID  Left Outer Join BP_Bank c ON dbo.fn_GetCustCode(oRegName)=c.BPCode ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCK %') AND Remitted=1 AND DateRemitted IS NOT NULL AND oRepliedMSG Like '%successful%'     AND CONVERT(nvarchar(30),DateRemitted,101)>=@DateForCheckFrom AND CONVERT(nvarchar(30),DateRemitted,101)<=@DateForCheckTo AND (OnlinePymnt='Y') ")
        SQL.Append(")[TableA])[TableB] ")
        SQL.Append("GROUP BY DepositDate")
        SQL.Append(") as B ")
        SQL.Append("ON DaysTable.DateField=B.DepositDate ")
        SQL.Append("Left JOIN ")
        SQL.Append("(SELECT sum(InvAmt)-sum(EWTAmt) as DPSNetTotal,DocDate ")
        SQL.Append("FROM ODPS ")
        SQL.Append("WHERE CONVERT(nvarchar,DocDate,101)>=@DateForCashFrom AND CONVERT(nvarchar,DocDate,101)<=@DateForCashTo AND (Void=0 OR Void IS NULL) ")
        SQL.Append("GROUP BY DocDate ")
        SQL.Append(") AS ODPS ON DaysTable.DateField=ODPS.DocDate ORDER BY DateField ")



        Return SQL.ToString

        Exit Function
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Function

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor
        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(GetSQL, oConn2)

        Dim Report As New crDepDPSMonitor

        With Report
            .SetDataSource(dt)

            .PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            .PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter
        End With


        With frmCrystalViewer
            .CrystalReportViewer1.ReportSource = Report
            .Show()
            .MdiParent = frmMain
            .Text = "DEPOSIT & DPS MONITORING"
        End With

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub
End Class