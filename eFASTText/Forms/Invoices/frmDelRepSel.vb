Imports System.Text

Public Class frmDelRepSel
    Private SQL As StringBuilder
    Private Query As CPerformQuery

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub frmDelRepSel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboStatus.SelectedIndex = 0

        SQL = New StringBuilder
        SQL.Append("SELECT 'All'[Logistics] ")
        SQL.Append("UNION ALL ")
        SQL.Append("SELECT Logistics FROM ArTrnDetail GROUP BY Logistics ")

        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(SQL.ToString, oConn3)
        r = 0
        RowCount = dt.Rows.Count
        Do While r < RowCount
            cboLogistic.Items.Add(dt.Rows(r).Item(0).ToString)
            r += 1
        Loop

    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError

        If CheckValues() = False Then
            SQL = New StringBuilder
            SQL.Append("DECLARE @From DATETIME,@To DATETIME ")
            SQL.Append("SET @From='" & DTFrom.Value & "' ")
            SQL.Append("SET @To='" & DTTo.Value & "' ")
            SQL.Append("SET @From=CONVERT(nvarchar(30),@From,101) ")
            SQL.Append("SET @To=CONVERT(nvarchar(30),@To,101) ")
            SQL.Append("SELECT DISTINCT Invoice,InvoiceDate, ")
            SQL.Append("(SELECT (rtrim(a.Name) + ' - ' + a.Customer) FROM [" & oConn.Database & "].[dbo].[ArCustomer] a WHERE a.Customer = ArTrnDetail.Customer) AS Customer, Salesperson, SUM(NetSalesValue) AS [Peso Sales],ISNULL(InvoiceStat,'n/a') AS InvoiceStat, ")
            SQL.Append("DeliveryDate,[DueDate]=DATEADD(d,(Select (Select x.DueDays1 from [" & oConn.Database & "].dbo.TblArTerms x Where x.TermsCode = y.TermsCode) ")
            SQL.Append("FROM [" & oConn.Database & "].dbo.ArCustomer y Where y.Customer = dbo.ArTrnDetail.Customer),isnull(DeliveryDate,DeliveryDate)),Logistics ")
            SQL.Append(",(CASE WHEN InvoiceStat='PENDING' THEN DATEDIFF(dd,InvoiceDate,isnull(DeliveryDate,GetDate())) WHEN InvoiceStat='DELIVERED' THEN DATEDIFF(dd,InvoiceDate,isnull(DeliveryDate,GetDate())) ELSE '' END)[LeadTime] ")
            SQL.Append("FROM dbo.ArTrnDetail ")
            SQL.Append("WHERE InvoiceDate>=@From AND InvoiceDate<=@To ")
            SQL.Append("AND (OrderType = 'B') ")
            SQL.Append("AND NetSalesValue > 0 ")
            If RadioCode.Checked = True Then
                SQL.Append("AND Salesperson='" & txtSalesmanCode.Text & "' ")
            End If
            If cboStatus.SelectedIndex > 0 Then
                SQL.Append("AND ISNULL(InvoiceStat,'n/a')='" & cboStatus.SelectedItem & "' ")
            End If
            If cboLogistic.SelectedIndex > 0 Then
                If cboLogistic.SelectedItem = "" Then
                    SQL.Append("AND Logistics IS NULL ")
                Else
                    SQL.Append("AND Logistics='" & cboLogistic.SelectedItem & "' ")
                End If
            End If
            SQL.Append("GROUP BY TrnYear, TrnMonth, Invoice,InvoiceDate, Customer, Salesperson, InvoiceStat, DeliveryDate,Logistics ")
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

        If RadioCode.Checked = True Then
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
End Class