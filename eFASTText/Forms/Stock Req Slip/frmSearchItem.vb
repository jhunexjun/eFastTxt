Public Class frmSearchItem
    Friend vC As Int16, vItem As String, vSearch As Boolean = False

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        vSearch = False
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSearchBy.SelectedIndexChanged
        lblSearchby.Text = cboSearchBy.SelectedItem
    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError

        If cboSearchBy.SelectedItem = "Stock Code" Then
            vC = 1
        ElseIf cboSearchBy.SelectedItem = "Description" Then
            vC = 2
        End If

        vItem = txtSearchItem.Text
        vSearch = True
        Me.Close()

        Exit Sub

xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub frmSearchItem_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cboSearchBy.SelectedIndex = 0
        txtSearchItem.Focus()
        SendKeys.Send("{Home}+{End}")
    End Sub

End Class