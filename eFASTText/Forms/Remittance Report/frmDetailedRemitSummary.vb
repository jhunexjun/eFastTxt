Imports System.Text
Public Class frmDetailedRemitSummary
    Dim Query As CPerformQuery

    Friend owned As Boolean

    Private Sub frmDetailedCollectionSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If owned = True Then
        Else
            DTFrom.Value = Today
            DTTo.Value = Today
        End If

    End Sub

    Private Sub frmDetailedCollectionSummary_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Width = 665
        Me.Height = 556
    End Sub

    Friend Sub ShowDetailes()
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        ListView1.Items.Clear()
        If cmdFind.Text = "&Find" Then

            txtGrandTotal.Text = "0.00"

            Dim ListItemVar As ListViewItem
            Query = New CPerformQuery

            dt = Query.PerformSelectQuery(GetSQL(), oConn2)

            Dim c As Int16 = 0

            Do While c < dt.Rows.Count

                ListItemVar = ListView1.Items.Add(c + 1)
                ListItemVar.SubItems.Add(dt.Rows(c).Item("Type"))
                ListItemVar.SubItems.Add(dt.Rows(c).Item("oRegName").ToString)
                ListItemVar.SubItems.Add(dt.Rows(c).Item("Bank").ToString)
                ListItemVar.SubItems.Add(dt.Rows(c).Item("DueDate").ToString)
                ListItemVar.SubItems.Add(Format(dt.Rows(c).Item("Payment").ToString, "Standard"))

                txtGrandTotal.Text = CDbl(txtGrandTotal.Text) + CDbl(dt.Rows(c).Item("Payment"))

                c += 1
            Loop

            txtGrandTotal.Text = Format(txtGrandTotal.Text, "Standard")

            'Label2.Text = ListView1.Items.Count & " record(s) found."
            If dt.Rows.Count <= 0 Then
                MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
            Else
                cmdPrint.Enabled = True
            End If
        End If
        Me.Cursor = Cursors.Default
        cmdFind.Enabled = True

        Exit Sub
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & Me.ToString & vbTab & Err.Number & vbTab & Err.Description)
        Me.Cursor = Cursors.Default
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        cmdFind.Enabled = False
        If owned = True Then

        Else
            'If txtSalesperson.TextLength <= 0 Then
            'MsgBox("Please enter Salesperson code.", MsgBoxStyle.Critical)
            'txtSalesperson.Focus()
            'Exit Sub
            'End If
        End If

        ShowDetailes()
    End Sub

    Private Function GetSQL() As String
        On Error GoTo xError

        Dim SQL As New StringBuilder
        SQL.Append("Declare @DateFrom as DateTime,@DateTo as DateTime,@Salesperson as nvarchar(20) ")
        SQL.Append("SET @DateFrom='" & DTFrom.Value & "' ")
        SQL.Append("SET @DateTo='" & DTTo.Value & "' ")
        SQL.Append("SET @DateFrom=CONVERT(nvarchar(30),@DateFrom,101) ")
        SQL.Append("SET @DateTo=CONVERT(nvarchar(30),@DateTo,101) ")
        SQL.Append("SET @Salesperson='" & txtSalesperson.Text & "' ")
        SQL.Append("SELECT 'Cash'[Type],oRegName,''[Bank],''[DueDate],dbo.fn_GetCashPayment(CommandUsed)[Payment] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCH %') AND Remitted=1")

        If Trim(txtSalesperson.TextLength) > 0 Then
            SQL.Append("AND a.Salesperson=@Salesperson ")
        End If

        SQL.Append("AND CONVERT(nvarchar(30),DateRemitted,101)>=@DateFrom AND CONVERT(nvarchar(30),DateRemitted,101)<=@DateTo AND oRepliedMSG Like '%successful%' ")
        SQL.Append("Union ALL ")
        SQL.Append("SELECT 'Check'[Type],oRegName,dbo.fn_GetBankName(CommandUsed)[Bank],convert(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101)[DueDate],dbo.fn_GetCheckPayment(CommandUsed)[Payment] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCK %') AND Remitted=1 ")
        If Trim(txtSalesperson.TextLength) > 0 Then
            SQL.Append(" AND a.Salesperson=@Salesperson ")
        End If

        SQL.Append("AND CONVERT(nvarchar(30),DateRemitted,101)>=@DateFrom AND CONVERT(nvarchar(30),DateRemitted,101)<=@DateTo ")

        SQL.Append("AND oRepliedMSG Like '%successful%' ")

        Return SQL.ToString

        Exit Function
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & Me.ToString & vbTab & Err.Number & vbTab & Err.Description)
        Me.Cursor = Cursors.Default
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Function

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        Dim Report As New crDetailedSalesmanCollection

        dt = Query.PerformSelectQuery(GetSQL(), oConn2)

        With Report
            .SetDataSource(dt)
            .SetParameterValue(1, DTFrom.Value)
            .SetParameterValue(2, DTTo.Value)
            .SetParameterValue(3, UserName.ToString)
            .SetParameterValue(4, txtSalesperson.Text)

            .PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            .PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter

        End With

        With frmCrystalViewer
            .CrystalReportViewer1.ReportSource = Report
            .Owner = Me
            .Show()
            '.MdiParent = frmMain
            .Text = "Detailed Collection Report"
        End With

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        Me.Cursor = Cursors.Default

        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        Me.Cursor = Cursors.Default
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtSalesperson_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalesperson.TextChanged
        ClearValues()
    End Sub

    Private Sub DTFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTFrom.ValueChanged
        ClearValues()
    End Sub

    Private Sub DTTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTTo.ValueChanged
        ClearValues()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub ClearValues()
        ListView1.Items.Clear()
        'Label2.Text = "0 record(s) found."
        txtGrandTotal.Text = "0.00"
    End Sub
End Class