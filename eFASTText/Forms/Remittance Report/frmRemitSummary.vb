Imports System.Text
Imports System.Windows.Forms

Public Class frmRemitSummary
    Dim Query As New CPerformQuery

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError
        cmdFind.Enabled = False

        Me.Cursor = Cursors.WaitCursor

        Dim ListItemVar As ListViewItem

        If cmdFind.Text = "&Find" Then
            ListView1.Items.Clear()
            ClearValues()

            dt = Query.PerformSelectQuery(GetSQL(), oConn2)

            Dim c As Int16 = 0, Salesperson As String = ""

            Do While c < dt.Rows.Count
                If dt.Rows(c).Item("Salesperson").ToString = "" Then
                    Salesperson = dt.Rows(c).Item("Salesperson2").ToString
                Else
                    Salesperson = dt.Rows(c).Item("Salesperson").ToString
                End If


                ListItemVar = ListView1.Items.Add(c + 1)
                ListItemVar.SubItems.Add(Salesperson)
                ListItemVar.SubItems.Add(Format(dt.Rows(c).Item("CashPayment").ToString, "Standard"))
                ListItemVar.SubItems.Add(Format(dt.Rows(c).Item("CheckPayment").ToString, "Standard"))
                ListItemVar.SubItems.Add(Format(dt.Rows(c).Item("Total").ToString, "Standard"))
                txtGrandTotal.Text = CDbl(txtGrandTotal.Text) + CDbl(dt.Rows(c).Item("Total").ToString)

                c += 1
            Loop

            txtGrandTotal.Text = Format(txtGrandTotal.Text, "Standard")

            'Label1.Text = ListView1.Items.Count & " record(s) found."
            If dt.Rows.Count <= 0 Then
                MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
            Else
                'cmdPrint.Enabled = True
                ToolStripMenuItem2.Enabled = True
            End If

        End If

        Me.Cursor = Cursors.Default
        cmdFind.Enabled = True
        cmdFind.Focus()

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub ClearValues()
        ListView1.Items.Clear()
        'Label1.Text = "0 record(s) found."
        'cmdPrint.Enabled = False
        ToolStripMenuItem2.Enabled = False
        txtGrandTotal.Text = "0.00"
    End Sub

    Private Function CheckValues()
        CheckValues = True


        Return CheckValues
    End Function

    Private Function GetSQL() As String
        On Error GoTo xError

        Dim SQL As New StringBuilder

        SQL.Append("Declare @DateFrom as DateTime,@DateTo as DateTime ")
        SQL.Append("SET @DateFrom='" & DTFrom.Value & "' ")
        SQL.Append("SET @DateTo='" & DTTo.Value & "' ")
        SQL.Append("SET @DateFrom=CONVERT(nvarchar(30),@DateFrom,101) ")
        SQL.Append("SET @DateTo=CONVERT(nvarchar(30),@DateTo,101) ")
        SQL.Append("SELECT a.Salesperson,sum(dbo.fn_GetCashPayment(CommandUsed))[CashPayment],sum(dbo.fn_GetCheckPayment(CommandUsed))[CheckPayment],(sum(dbo.fn_GetCashPayment(CommandUsed))+sum(dbo.fn_GetCheckPayment(CommandUsed)))[Total] ")
        SQL.Append("FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCK%' or b.CommandUsed Like 'PAYCH%') AND Remitted=1 AND ")
        SQL.Append("CONVERT(nvarchar(30),DateRemitted,101)>=@DateFrom AND CONVERT(nvarchar(30),DateRemitted,101)<=@DateTo AND oRepliedMSG Like '%successful%' ")
        SQL.Append("Group by a.Salesperson")
        Return SQL.ToString

        Exit Function
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & Me.ToString & vbTab & Err.Number & vbTab & Err.Description)
        Me.Cursor = Cursors.Default
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)

    End Function


    Private Sub frmRemitSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DTFrom.Value = Today
        DTTo.Value = Today
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cmdFind_Click(sender, e)
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        If ListView1.Items.Count > 0 Then
            ClearValues()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub frmCollectionSummary_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Width = 611
        Me.Height = 566
    End Sub

    Private Sub DTFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTFrom.ValueChanged
        'cmdPrint.Enabled = False
        ToolStripMenuItem2.Enabled = False
        ListView1.Items.Clear()
        txtGrandTotal.Text = "0.00"
    End Sub

    Private Sub DTTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTTo.ValueChanged
        'cmdPrint.Enabled = False
        ToolStripMenuItem2.Enabled = False
        ListView1.Items.Clear()
        txtGrandTotal.Text = "0.00"
    End Sub

    Private Sub ListView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseDoubleClick
        Call ShowDetails()
    End Sub

    Private Sub ListView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseUp
        If ListView1.SelectedItems.Count <= 0 Then
            ContextMenuStrip1.Items(0).Enabled = False
        Else
            ContextMenuStrip1.Items(0).Enabled = True
        End If
    End Sub

    Private Sub ShowDetails()
        Dim f As New frmDetailedRemitSummary
        f.txtSalesperson.ReadOnly = True
        f.DTFrom.Enabled = False
        f.DTTo.Enabled = False

        f.txtSalesperson.Text = ListView1.FocusedItem.SubItems(1).Text
        f.DTFrom.Value = DTFrom.Value
        f.DTTo.Value = DTTo.Value

        f.owned = True

        f.ShowInTaskbar = False
        f.MdiParent = frmMain
        f.Show()
        f.ShowDetailes()
    End Sub

    Private Sub ViewDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewDetailToolStripMenuItem.Click
        Call ShowDetails()
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        Dim Report As New crRemittanceSummary

        dt = Query.PerformSelectQuery(GetSQL(), oConn2)

        With Report
            .SetDataSource(dt)
            .SetParameterValue(0, DTFrom.Value)
            .SetParameterValue(1, DTTo.Value)
            .SetParameterValue(2, UserName.ToString)

            .PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            .PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter

        End With

        With frmCrystalViewer
            .CrystalReportViewer1.ReportSource = Report
            .MdiParent = frmMain
            .Show()
            .Text = "Remittance Summary Report"
        End With

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub
End Class