Imports System.Text
Public Class frmStockReqSelection
    Private Query As CPerformQuery
    Private SQL As StringBuilder
    Dim r, rCount As Int16

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGenerate.Click
        Me.Cursor = Cursors.WaitCursor
        If CheckValues() = False Then
            With frmStockReqResult
                .vBranch = txtBranch.Text
                .vSellDays = txtSellingDays.Text
                .vProvision = txtProvision.Text
                .vcboWHouse = cboWhouse.Text
            End With

            If frmStockReqResult.ShowDialog(frmMain) = Windows.Forms.DialogResult.Yes Then
                Me.Close()
            End If

        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Function CheckValues() As Boolean
        CheckValues = True

        If txtBranch.TextLength <= 0 Then
            MessageBox.Show("Please indicate branch name.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtBranch.Focus()
            Exit Function
        End If

        If txtSellingDays.TextLength <= 0 Then
            MessageBox.Show("Please indicate number of days.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtSellingDays.Focus()
            Exit Function
        End If

        If Not IsNumeric(txtSellingDays.Text) Then
            MessageBox.Show("Invalid number value.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtSellingDays.Focus()
            Exit Function
        End If
        If Not IsNumeric(txtProvision.Text) Then
            MessageBox.Show("Please indicate provision in percent.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtProvision.Focus()
            Exit Function
        End If
        If cboWhouse.SelectedIndex = -1 Then
            MessageBox.Show("Please choose warehouse.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboWhouse.Focus()
            Exit Function
        End If

        CheckValues = False
        Return CheckValues
    End Function

    Private Sub frmStockReqSelection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo xError
        Query = New CPerformQuery
        txtBranch.Text = Query.PerformSelectQuery("SELECT top 1 Branch FROM OADM", oConn2).Rows(0).Item(0).ToString


        Sql = New StringBuilder
        SQL.Append("Select Warehouse from FDCWarehouse Group by Warehouse Order by Warehouse")
        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(Sql.ToString, oConn)
        r = 0
        RCount = dt.Rows.Count
        Do While r < rCount
            cboWhouse.Items.Add(dt.Rows(r).Item("Warehouse").ToString)
            r += 1
        Loop

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

End Class