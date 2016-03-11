Imports System.Text
Public Class frmDepositSummary
    Dim Query As CPerformQuery
    Dim Onloading As Boolean = True
    Private SQL As New StringBuilder
    Dim c, rCount As Integer
    Private HT As Hashtable

    Private Sub frmDetailedCollectionSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo xError
        HT = New Hashtable

        DTFrom.Value = Today
        DTTo.Value = Today

        Query = New CPerformQuery
        dt = Query.PerformSelectQuery("SELECT BankCode,BankName FROM Bank WHERE Active='Y' Order By BankCode", oConn2)
        c = 0
        rCount = dt.Rows.Count

        Do While c < rCount
            cboBanks.Items.Add(dt.Rows(c).Item("BankCode"))
            HT.Add(dt.Rows(c).Item("BankCode"), dt.Rows(c).Item("BankName"))
            c += 1
        Loop

        Exit Sub

xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Friend Sub ShowDetailes()
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        ListView1.Items.Clear()
        dt.Rows.Clear()

        If cmdFind.Text = "&Find" Then

            Reset()

            Dim ListItemVar As ListViewItem
            Query = New CPerformQuery

            dt = Query.PerformSelectQuery(GetSQL(), oConn2)


            c = 0

            Do While c < dt.Rows.Count

                ListItemVar = ListView1.Items.Add(c + 1)
                ListItemVar.SubItems.Add(dt.Rows(c).Item("oDateTimeIn"))
                ListItemVar.SubItems.Add(dt.Rows(c).Item("Type"))
                ListItemVar.SubItems.Add(dt.Rows(c).Item("oRegName").ToString)
                ListItemVar.SubItems.Add(dt.Rows(c).Item("Bank").ToString)
                ListItemVar.SubItems.Add(dt.Rows(c).Item("DueDate").ToString)
                ListItemVar.SubItems.Add(Format(dt.Rows(c).Item("Payment").ToString, "Standard"))
                ListItemVar.SubItems.Add(dt.Rows(c).Item("DateRemitted"))
                ListItemVar.SubItems.Add(IIf(dt.Rows(c).Item("OnlinePymnt").ToString = "Y", "True", "False"))

                txtCash.Text = CDbl(txtCash.Text) + IIf(dt.Rows(c).Item("Type") = "Cash" AndAlso dt.Rows(c).Item("OnlinePymnt").ToString = "N", dt.Rows(c).Item("Payment"), "0")
                txtCheck.Text = CDbl(txtCheck.Text) + IIf(dt.Rows(c).Item("Type") = "Check" AndAlso dt.Rows(c).Item("OnlinePymnt").ToString = "N", dt.Rows(c).Item("Payment"), "0")
                txtTotalForDeposit.Text = CDbl(txtTotalForDeposit.Text) + IIf(dt.Rows(c).Item("OnlinePymnt").ToString = "N", dt.Rows(c).Item("Payment"), 0)

                txtOnlinePay.Text = CDbl(txtOnlinePay.Text) + IIf(dt.Rows(c).Item("OnlinePymnt").ToString = "Y", dt.Rows(c).Item("Payment"), "0")

                c += 1
            Loop

            txtOnlinePay.Text = Format(txtOnlinePay.Text, "standard")
            txtCash.Text = Format(txtCash.Text, "standard")
            txtCheck.Text = Format(txtCheck.Text, "standard")
            txtTotalForDeposit.Text = Format(txtTotalForDeposit.Text, "Standard")

            If dt.Rows.Count <= 0 Then
                MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
            Else
                ToolStripMenuItem5.Enabled = True
            End If
        End If
        Me.Cursor = Cursors.Default

        Exit Sub

xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError

        cmdFind.Enabled = False

        If InStr(txtSalesperson.Text, "*") > 0 Then
            SQL = New StringBuilder
            SQL.Append("SELECT Salesperson,cFirstName,cFamilyName,cMobile FROM tblClients WHERE Salesperson Like '" & txtSalesperson.Text.Replace("*", "%").Trim & "' AND cApprover=7")

            Dim f As New frmDialog
            f.DatabaseConnection = 3
            f.strToPerform = SQL.ToString
            f.ShowDialog(Me)

            If f.Sel(0).ToString.Length <> 0 Then
                txtSalesperson.Text = f.Sel(0).ToString
            End If
        Else
            ShowDetailes()
        End If

        cmdFind.Enabled = True
        cmdFind.Focus()

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)

    End Sub

    Private Function GetSQL() As String
        On Error GoTo xError
        SQL = New StringBuilder
        SQL.Append("Declare @DateForCashFrom as DateTime,@DateForCashTo as DateTime,@DateForCheckFrom as DateTime,@DateForCheckTo as DateTime,@DateWeekName as nvarchar(15), @Salesperson as nvarchar(20) ")
        SQL.Append("SET @DateForCashFrom='" & DTFrom.Value & "' ")
        SQL.Append("SET @DateForCashTo='" & DTTo.Value & "' ")
        SQL.Append("SET @DateForCheckFrom=@DateForCashFrom ")
        SQL.Append("SET @DateForCheckTo=@DateForCashTo ")
        SQL.Append("SET @Salesperson='" & txtSalesperson.Text & "' ")
        SQL.Append("SET @DateForCashFrom=CONVERT(nvarchar(30),@DateForCashFrom,101) ")
        SQL.Append("SET @DateForCashTo=CONVERT(nvarchar(30),@DateForCashTo,101) ")
        SQL.Append("SET @DateForCheckFrom=CONVERT(nvarchar(30),@DateForCheckFrom,101) ")
        SQL.Append("SET @DateForCheckTo=CONVERT(nvarchar(30),@DateForCheckTo,101) ")

        SQL.Append("SELECT DISTINCT oID,* FROM (SELECT oID,oDateTimeIn,'Cash'[Type],oRegName,''[Bank],''[DueDate],dbo.fn_GetCashPayment(CommandUsed)[Payment],DateRemitted,(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt],(CASE WHEN BankCode is null then '' ELSE BankCode END)[DepositTo] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID Left Outer Join BP_Bank c ON dbo.fn_GetCustCode(oRegName)=c.BPCode ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCH %') AND Remitted=1 ")
        SQL.Append("AND DateRemitted IS NOT NULL ")
        If ChckFilter.Checked = True Then
            SQL.Append("AND a.Salesperson=@Salesperson ")
        End If
        If RadioBank.Checked = True Then
            SQL.Append("AND c.BankCode='" & cboBanks.Text & "' ")
        End If
        SQL.Append("AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)>=@DateForCashFrom AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)<=@DateForCashTo AND oRepliedMSG Like '%successful%' ")
        SQL.Append("AND dbo.fn_GetCashPayment(CommandUsed)>0 ")
        SQL.Append("AND (OnlinePymnt IS NULL OR OnlinePymnt='N') ")

        If RadioBank.Checked = True Then
            SQL.Append("UNION ALL ")
            SQL.Append("SELECT oID,oDateTimeIn,'Cash'[Type],oRegName,''[Bank],''[DueDate],dbo.fn_GetCashPayment(CommandUsed)[Payment],DateRemitted,(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt],(CASE WHEN BankCode is null then '' ELSE BankCode END)[DepositTo] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID Left Outer Join BP_Bank c ON a.Salesperson=c.BPCode ")
            SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCH %') AND Remitted=1 ")
            SQL.Append("AND DateRemitted IS NOT NULL ")
            If ChckFilter.Checked = True Then
                SQL.Append("AND a.Salesperson=@Salesperson ")
            End If
            SQL.Append("AND c.BankCode='" & cboBanks.Text & "' ")
            SQL.Append("AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)>=@DateForCashFrom AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)<=@DateForCashTo AND oRepliedMSG Like '%successful%' ")
            SQL.Append("AND dbo.fn_GetCashPayment(CommandUsed)>0 ")
            SQL.Append("AND (OnlinePymnt IS NULL OR OnlinePymnt='N') ")
        End If
        SQL.Append("UNION ALL ")
        SQL.Append("SELECT oID,oDateTimeIn,'Check'[Type],oRegName,dbo.fn_GetBankName(CommandUsed)[Bank],convert(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101)[DueDate],dbo.fn_GetCheckPayment(CommandUsed)[Payment],DateRemitted,(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt],(CASE WHEN BankCode is null then '' ELSE BankCode END)[DepositTo] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID  Left Outer Join BP_Bank c ON dbo.fn_GetCustCode(oRegName)=c.BPCode ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCK %') AND Remitted=1 ")
        SQL.Append("AND DateRemitted IS NOT NULL ")
        If ChckFilter.Checked = True Then
            SQL.Append(" AND a.Salesperson=@Salesperson ")
        End If
        If RadioBank.Checked = True Then
            SQL.Append("AND c.BankCode='" & cboBanks.Text & "' ")
        End If
        SQL.Append("AND oRepliedMSG Like '%successful%' ")
        If ChckDueCheckOnly.Checked = True Then
            SQL.Append("AND dbo.fn_DepSumCheckDue(convert(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101), CONVERT(nvarchar(30),DateRemitted,101))>=@DateForCheckFrom AND dbo.fn_DepSumCheckDue(convert(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101), CONVERT(nvarchar(30),DateRemitted,101))<=@DateForCheckTo ")
        End If
        SQL.Append("AND (OnlinePymnt IS NULL OR OnlinePymnt='N') ")

        SQL.Append("UNION ALL ")
        If RadioBank.Checked = True Then
            SQL.Append("SELECT oID,oDateTimeIn,'Cash Online Pay'[Type],oRegName,''[Bank],''[DueDate],dbo.fn_GetCashPayment(CommandUsed)[Payment],DateRemitted,(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt],(CASE WHEN BankCode is null then '' ELSE BankCode END)[DepositTo] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID Left Outer Join BP_Bank c ON a.Salesperson=c.BPCode ")
            SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCH %') AND Remitted=1 ")
            SQL.Append("AND DateRemitted IS NOT NULL ")
            If ChckFilter.Checked = True Then
                SQL.Append("AND a.Salesperson=@Salesperson ")
            End If
            SQL.Append("AND c.BankCode='" & cboBanks.Text & "' ")
            SQL.Append("AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)>=@DateForCashFrom AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)<=@DateForCashTo AND oRepliedMSG Like '%successful%' ")
            SQL.Append("AND dbo.fn_GetCashPayment(CommandUsed)>0 ")
            SQL.Append("AND (OnlinePymnt='Y') ")
        Else
            SQL.Append("SELECT oID,oDateTimeIn,'Cash Online Pay'[Type],oRegName,''[Bank],''[DueDate],dbo.fn_GetCashPayment(CommandUsed)[Payment],DateRemitted,(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt],(CASE WHEN BankCode is null then '' ELSE BankCode END)[DepositTo] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID Left Outer Join BP_Bank c ON dbo.fn_GetCustCode(oRegName)=c.BPCode ")
            SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCH %') AND Remitted=1 ")
            SQL.Append("AND DateRemitted IS NOT NULL ")
            If ChckFilter.Checked = True Then
                SQL.Append("AND a.Salesperson=@Salesperson ")
            End If
            If RadioBank.Checked = True Then
                SQL.Append("AND c.BankCode='" & cboBanks.Text & "' ")
            End If
            SQL.Append("AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)>=@DateForCashFrom AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)<=@DateForCashTo AND oRepliedMSG Like '%successful%' ")
            SQL.Append("AND dbo.fn_GetCashPayment(CommandUsed)>0 ")
            SQL.Append("AND (OnlinePymnt='Y') ")
        End If
        SQL.Append("UNION ALL ")
        SQL.Append("SELECT oID,oDateTimeIn,'Check Online Pay'[Type],oRegName,dbo.fn_GetBankName(CommandUsed)[Bank],convert(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101)[DueDate],dbo.fn_GetCheckPayment(CommandUsed)[Payment],DateRemitted,(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt],(CASE WHEN BankCode is null then '' ELSE BankCode END)[DepositTo] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID  Left Outer Join BP_Bank c ON dbo.fn_GetCustCode(oRegName)=c.BPCode ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCK %') AND Remitted=1 ")
        SQL.Append("AND DateRemitted IS NOT NULL ")
        If ChckFilter.Checked = True Then
            SQL.Append(" AND a.Salesperson=@Salesperson ")
        End If
        If RadioBank.Checked = True Then
            SQL.Append("AND c.BankCode='" & cboBanks.Text & "' ")
        End If
        SQL.Append("AND oRepliedMSG Like '%successful%' ")
        If ChckDueCheckOnly.Checked = True Then
            SQL.Append("AND CONVERT(nvarchar(30),DateRemitted,101)>=@DateForCheckFrom AND CONVERT(nvarchar(30),DateRemitted,101)<=@DateForCheckTo ")
        End If
        SQL.Append("AND (OnlinePymnt='Y'))[TableA] ")
        SQL.Append("ORDER BY OnlinePymnt,[Type],oRegName")

        Return SQL.ToString

        Exit Function
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Function

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChckDueCheckOnly.CheckedChanged
        If Onloading = False Then
            ShowDetailes()
        End If

        Onloading = False
    End Sub

    Private Sub txtSalesperson_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalesperson.TextChanged
        ListView1.Items.Clear()
        Reset()
    End Sub

    Private Sub DTTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTFrom.ValueChanged
        ListView1.Items.Clear()
        Reset()
    End Sub

    Private Sub Reset()
        txtOnlinePay.Text = "0.00"
        txtCash.Text = "0.00"
        txtCheck.Text = "0.00"
        txtTotalForDeposit.Text = "0.00"
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChckFilter.CheckedChanged
        If ChckFilter.Checked = True Then
            Label1.Visible = True
            txtSalesperson.Visible = True
            ChckDueCheckOnly.Visible = True
        Else
            Label1.Visible = False
            txtSalesperson.Visible = 0
            ChckDueCheckOnly.Visible = 0
        End If
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        Dim Report As New crDepositSummary, SubReport As New crDepositSummarySubReport, SubSQL As StringBuilder
        Dim Subdt As New DataTable

        ''''''''''''''''''''''''
        SubSQL = New StringBuilder
        SubSQL.Append("Declare @DateForCashFrom as DateTime,@DateForCashTo as DateTime,@DateForCheckFrom as DateTime,@DateForCheckTo as DateTime,@DateWeekName as nvarchar(15), @Salesperson as nvarchar(20) ")
        SubSQL.Append("SET @DateForCashFrom='" & DTFrom.Value & "' ")
        SubSQL.Append("SET @DateForCashTo='" & DTTo.Value & "' ")
        SubSQL.Append("SET @DateForCheckFrom=@DateForCashFrom ")
        SubSQL.Append("SET @DateForCheckTo=@DateForCashTo ")
        SubSQL.Append("SET @Salesperson='" & txtSalesperson.Text.Trim & "' ")
        SubSQL.Append("SET @DateForCashFrom=CONVERT(nvarchar(30),@DateForCashFrom,101) ")
        SubSQL.Append("SET @DateForCashTo=CONVERT(nvarchar(30),@DateForCashTo,101) ")
        SubSQL.Append("SET @DateForCheckFrom=CONVERT(nvarchar(30),@DateForCheckFrom,101) ")
        SubSQL.Append("SET @DateForCheckTo=CONVERT(nvarchar(30),@DateForCheckTo,101) ")

        SubSQL.Append("SELECT DISTINCT oID,* FROM (")
        If RadioBank.Checked = True Then
            SubSQL.Append("SELECT oID,oDateTimeIn,'Cash Online Pay'[Type],oRegName,''[Bank],''[DueDate],dbo.fn_GetCashPayment(CommandUsed)[Payment],DateRemitted,(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt],(CASE WHEN BankCode is null then '' ELSE BankCode END)[DepositTo] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID Left Outer Join BP_Bank c ON a.Salesperson=c.BPCode ")
            SubSQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCH %') AND Remitted=1 ")
            SubSQL.Append("AND DateRemitted IS NOT NULL ")

            If ChckFilter.Checked = True Then
                SQL.Append("AND a.Salesperson=@Salesperson ")
            End If

            SubSQL.Append("AND c.BankCode='" & cboBanks.Text & "' ")
            SubSQL.Append("AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)>=@DateForCashFrom AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)<=@DateForCashTo AND oRepliedMSG Like '%successful%' ")
            SubSQL.Append("AND dbo.fn_GetCashPayment(CommandUsed)>0 ")

            SubSQL.Append("AND (OnlinePymnt='Y') ")
        Else
            SubSQL.Append("SELECT oID,oDateTimeIn,'Cash Online Pay'[Type],oRegName,''[Bank],''[DueDate],dbo.fn_GetCashPayment(CommandUsed)[Payment],DateRemitted,(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt],(CASE WHEN BankCode is null then '' ELSE BankCode END)[DepositTo] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID Left Outer Join BP_Bank c ON dbo.fn_GetCustCode(oRegName)=c.BPCode ")
            SubSQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCH %') AND Remitted=1 ")
            SubSQL.Append("AND DateRemitted IS NOT NULL ")

            If ChckFilter.Checked = True Then
                SubSQL.Append("AND a.Salesperson=@Salesperson ")
            End If

            If RadioBank.Checked = True Then
                SubSQL.Append("AND c.BankCode='" & cboBanks.Text & "' ")
            End If

            SubSQL.Append("AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)>=@DateForCashFrom AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)<=@DateForCashTo AND oRepliedMSG Like '%successful%' ")
            SubSQL.Append("AND dbo.fn_GetCashPayment(CommandUsed)>0 ")

            SubSQL.Append("AND (OnlinePymnt='Y') ")

        End If
        SubSQL.Append("UNION ALL ")
        SubSQL.Append("SELECT oID,oDateTimeIn,'Check Online Pay'[Type],oRegName,dbo.fn_GetBankName(CommandUsed)[Bank],convert(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101)[DueDate],dbo.fn_GetCheckPayment(CommandUsed)[Payment],DateRemitted,(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt],(CASE WHEN BankCode is null then '' ELSE BankCode END)[DepositTo] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID  Left Outer Join BP_Bank c ON dbo.fn_GetCustCode(oRegName)=c.BPCode ")
        SubSQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCK %') AND Remitted=1 ")
        SubSQL.Append("AND DateRemitted IS NOT NULL ")
        If ChckFilter.Checked = True Then
            SubSQL.Append(" AND a.Salesperson=@Salesperson ")
        End If
        If RadioBank.Checked = True Then
            SubSQL.Append("AND c.BankCode='" & cboBanks.Text & "' ")
        End If
        SubSQL.Append("AND oRepliedMSG Like '%successful%' ")
        If ChckDueCheckOnly.Checked = True Then
            SubSQL.Append("AND CONVERT(nvarchar(30),DateRemitted,101)>=@DateForCheckFrom AND CONVERT(nvarchar(30),DateRemitted,101)<=@DateForCheckTo ")
        End If
        SubSQL.Append("AND (OnlinePymnt='Y'))[TableA] ")


        Dim SubQuery As New CPerformQuery
        Subdt = SubQuery.PerformSelectQuery(SubSQL.ToString, oConn2)
        Report.Subreports(0).SetDataSource(Subdt)
        '''''''''''''''''''''''

        SQL = New StringBuilder
        SQL.Append("Declare @DateForCashFrom as DateTime,@DateForCashTo as DateTime,@DateForCheckFrom as DateTime,@DateForCheckTo as DateTime,@DateWeekName as nvarchar(15), @Salesperson as nvarchar(20) ")
        SQL.Append("SET @DateForCashFrom='" & DTFrom.Value & "' ")
        SQL.Append("SET @DateForCashTo='" & DTTo.Value & "' ")
        SQL.Append("SET @DateForCheckFrom=@DateForCashFrom ")
        SQL.Append("SET @DateForCheckTo=@DateForCashTo ")
        SQL.Append("SET @Salesperson='" & txtSalesperson.Text & "' ")
        SQL.Append("SET @DateForCashFrom=CONVERT(nvarchar(30),@DateForCashFrom,101) ")
        SQL.Append("SET @DateForCashTo=CONVERT(nvarchar(30),@DateForCashTo,101) ")
        SQL.Append("SET @DateForCheckFrom=CONVERT(nvarchar(30),@DateForCheckFrom,101) ")
        SQL.Append("SET @DateForCheckTo=CONVERT(nvarchar(30),@DateForCheckTo,101) ")

        SQL.Append("SELECT DISTINCT oID,* FROM (SELECT oID,oDateTimeIn,'Cash'[Type],oRegName,''[Bank],''[DueDate],dbo.fn_GetCashPayment(CommandUsed)[Payment],DateRemitted,(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt],(CASE WHEN BankCode is null then '' ELSE BankCode END)[DepositTo] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID Left Outer Join BP_Bank c ON dbo.fn_GetCustCode(oRegName)=c.BPCode ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCH %') AND Remitted=1 ")
        SQL.Append("AND DateRemitted IS NOT NULL ")
        If ChckFilter.Checked = True Then
            SQL.Append("AND a.Salesperson=@Salesperson ")
        End If
        If RadioBank.Checked = True Then
            SQL.Append("AND c.BankCode='" & cboBanks.Text & "' ")
        End If
        SQL.Append("AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)>=@DateForCashFrom AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)<=@DateForCashTo AND oRepliedMSG Like '%successful%' ")
        SQL.Append("AND dbo.fn_GetCashPayment(CommandUsed)>0 ")
        SQL.Append("AND (OnlinePymnt IS NULL OR OnlinePymnt='N') ")

        If RadioBank.Checked = True Then
            SQL.Append("UNION ALL ")
            SQL.Append("SELECT oID,oDateTimeIn,'Cash'[Type],oRegName,''[Bank],''[DueDate],dbo.fn_GetCashPayment(CommandUsed)[Payment],DateRemitted,(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt],(CASE WHEN BankCode is null then '' ELSE BankCode END)[DepositTo] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID Left Outer Join BP_Bank c ON a.Salesperson=c.BPCode ")
            SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCH %') AND Remitted=1 ")
            SQL.Append("AND DateRemitted IS NOT NULL ")
            If ChckFilter.Checked = True Then
                SQL.Append("AND a.Salesperson=@Salesperson ")
            End If
            SQL.Append("AND c.BankCode='" & cboBanks.Text & "' ")
            SQL.Append("AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)>=@DateForCashFrom AND dbo.fn_DepSummaryCash(CONVERT(nvarchar(30),DateRemitted,101),OnLinePymnt)<=@DateForCashTo AND oRepliedMSG Like '%successful%' ")
            SQL.Append("AND dbo.fn_GetCashPayment(CommandUsed)>0 ")
            SQL.Append("AND (OnlinePymnt IS NULL OR OnlinePymnt='N') ")
        End If

        SQL.Append("UNION ALL ")
        SQL.Append("SELECT oID,oDateTimeIn,'Check'[Type],oRegName,dbo.fn_GetBankName(CommandUsed)[Bank],convert(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101)[DueDate],dbo.fn_GetCheckPayment(CommandUsed)[Payment],DateRemitted,(CASE WHEN OnlinePymnt IS NULL THEN 'N' ELSE OnlinePymnt END)[OnlinePymnt],(CASE WHEN BankCode is null then '' ELSE BankCode END)[DepositTo] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID  Left Outer Join BP_Bank c ON dbo.fn_GetCustCode(oRegName)=c.BPCode ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCK %') AND Remitted=1 ")
        SQL.Append("AND DateRemitted IS NOT NULL ")
        If ChckFilter.Checked = True Then
            SQL.Append(" AND a.Salesperson=@Salesperson ")
        End If
        If RadioBank.Checked = True Then
            SQL.Append("AND c.BankCode='" & cboBanks.Text & "' ")
        End If
        SQL.Append("AND oRepliedMSG Like '%successful%' ")
        If ChckDueCheckOnly.Checked = True Then
            SQL.Append("AND dbo.fn_DepSumCheckDue(convert(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101), CONVERT(nvarchar(30),DateRemitted,101))>=@DateForCheckFrom AND dbo.fn_DepSumCheckDue(convert(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101), CONVERT(nvarchar(30),DateRemitted,101))<=@DateForCheckTo ")
        End If
        SQL.Append("AND (OnlinePymnt IS NULL OR OnlinePymnt='N'))[TableA] ")
        SQL.Append("ORDER BY OnlinePymnt,[Type],oRegName")

        dt = Query.PerformSelectQuery(SQL.ToString, oConn2)
        With Report
            .SetDataSource(dt)
            .SetParameterValue(0, ChckDueCheckOnly.Checked)
            .SetParameterValue(1, DTFrom.Value)
            .SetParameterValue(2, UserName.ToString)
            .SetParameterValue(3, txtSalesperson.Text)
            .SetParameterValue(4, DTTo.Value)
            .SetParameterValue(5, IIf(RadioBank.Checked = True, lblBankName.Text, ""))

            .PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            .PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter

        End With

        With frmCrystalViewer
            .CrystalReportViewer1.ReportSource = Report
            .Show()
            .MdiParent = frmMain
            .Text = "Deposit Summary Report"
        End With
        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        Me.Cursor = Cursors.Default

        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub


    Private Sub RadioBank_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioBank.CheckedChanged
        If RadioBank.Checked = True Then
            cboBanks.Visible = True
            lblBankName.Visible = True
        Else
            cboBanks.Visible = False
            lblBankName.Visible = False
        End If
    End Sub

    Private Sub cboBanks_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBanks.SelectedIndexChanged
        lblBankName.Text = HT.Item(cboBanks.SelectedItem)
    End Sub

    Private Sub DTTo_ValueChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTTo.ValueChanged
        Reset()
    End Sub
End Class