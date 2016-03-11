Imports System.Windows.Forms
Imports System.Text

Public Class Dialog1
    Dim SQL As StringBuilder
    Private Query As CPerformQuery
    Protected Friend x As String
    Friend ToDate As Date

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If MessageBox.Show("Confirm change date?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.ToDate = DTTo.Value
            Me.Close()
        End If


    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Dialog1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SQL = New StringBuilder
        SQL.Append("SELECT convert(nvarchar,oDateTimeIn,101) FROM tblsmsOUT WHERE oID=" & x)
        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(SQL.ToString, oConn2)
        DTFrom.Value = dt.Rows(0).Item(0).ToString

        DTTo.Value = Today
    End Sub
End Class
