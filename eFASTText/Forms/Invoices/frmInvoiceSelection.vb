Imports System.Text
Public Class frmInvoiceSelection
    Private SQL As StringBuilder
    Private Query As CPerformQuery

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub frmInvoiceSelection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboStatus.SelectedIndex = 0
        txtYear.Text = Date.Today.Year
        txtMonth.Text = Date.Today.Month
        Radio1.Checked = True
    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError

        If CheckValues() = False Then
            SQL = New StringBuilder
            If Radio1.Checked = True Then   'For select within select
                SQL.Append("SELECT * FROM (")
            End If
            SQL.Append("SELECT DISTINCT Invoice,InvoiceDate, ")
            SQL.Append("(SELECT (rtrim(a.Name) + ' - ' + a.Customer) FROM [" & oConn.Database & "].[dbo].[ArCustomer] a WHERE a.Customer = ArTrnDetail.Customer) AS Customer, Salesperson, SUM(NetSalesValue) AS [Peso Sales],ISNULL(InvoiceStat,'n/a') AS InvoiceStat, ")
            SQL.Append("DeliveryDate,[DueDate]=DATEADD(d,(Select (Select x.DueDays1 from [" & oConn.Database & "].dbo.TblArTerms x Where x.TermsCode = y.TermsCode) ")
            SQL.Append("FROM [" & oConn.Database & "].dbo.ArCustomer y Where y.Customer = dbo.ArTrnDetail.Customer),isnull(DeliveryDate,DeliveryDate)),Logistics ")
            SQL.Append(",(CASE WHEN InvoiceStat='PENDING' THEN DATEDIFF(dd,InvoiceDate,isnull(DeliveryDate,GetDate())) WHEN InvoiceStat='DELIVERED' THEN DATEDIFF(dd,InvoiceDate,isnull(DeliveryDate,GetDate())) ELSE '' END)[LeadTime] ")
            SQL.Append("FROM dbo.ArTrnDetail ")
            SQL.Append("WHERE (TrnYear = " & txtYear.Text.Trim & ") AND (TrnMonth = " & txtMonth.Text.Trim & ") AND (OrderType = 'B') ")
            SQL.Append("AND NetSalesValue > 0 ")
            If RadioCode.Checked = True Then
                SQL.Append("AND Salesperson='" & txtSalesmanCode.Text & "' ")
            End If
            If cboStatus.SelectedIndex > 0 Then
                SQL.Append("AND ISNULL(InvoiceStat,'n/a')='" & cboStatus.SelectedItem & "' ")
            End If
            SQL.Append("GROUP BY TrnYear, TrnMonth, Invoice,InvoiceDate, Customer, Salesperson, InvoiceStat, DeliveryDate,Logistics ")
            If Radio1.Checked = True Then   'For select within select
                SQL.Append(")[TableA] WHERE InvoiceDate>='" & DTFrom.Value & "' AND InvoiceDate<='" & DTTo.Value & "'")
            End If
            SQL.Append("ORDER BY InvoiceStat,Invoice,InvoiceDate,Salesperson,Customer ")

            Dim f As New frmInvoices
            f.SQL = SQL
            f.MdiParent = frmMain
            f.Show()
        End If

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Function CheckValues() As Boolean
        CheckValues = True

        If txtYear.Text.Trim = "" Then
            MsgBox("Please enter year.", MsgBoxStyle.Exclamation)
            txtYear.Focus()
            Exit Function
        ElseIf txtMonth.Text.Trim = "" Then
            MsgBox("Please enter month code.", MsgBoxStyle.Exclamation)
            txtMonth.Focus()
            Exit Function
        ElseIf RadioCode.Checked = True Then
            If txtSalesmanCode.Text.Trim = "" Then
                MsgBox("Please enter salesman code.", MsgBoxStyle.Exclamation)
                txtSalesmanCode.Focus()
                Exit Function
            End If
        End If

        CheckValues = False
        Return CheckValues
    End Function

    Private Sub txtSalesmanCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSalesmanCode.GotFocus
        RadioCode.Checked = True
    End Sub

    Private Sub DTFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTFrom.GotFocus
        Radio1.Checked = True
    End Sub

    Private Sub DTFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTFrom.ValueChanged
        Radio1.Checked = True
    End Sub

    Private Sub DTTo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTTo.GotFocus
        Radio1.Checked = True
    End Sub

    Private Sub DTTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTTo.ValueChanged
        Radio1.Checked = True
    End Sub

    Private Sub txtYear_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtYear.GotFocus
        Radio2.Checked = True
    End Sub

    Private Sub txtMonth_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMonth.GotFocus
        Radio2.Checked = True
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStatus.SelectedIndexChanged
        Radio2.Checked = True
    End Sub

    Private Sub RadioButton1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton1.GotFocus
        Radio2.Checked = True
    End Sub

    Private Sub RadioCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioCode.GotFocus
        Radio2.Checked = True
    End Sub
End Class