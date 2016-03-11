Imports System.Text
Public Class frmOTReport
    Friend SQL As StringBuilder
    Private Query As CPerformQuery

    Private Sub frmOTReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo xError
        Dim ListItemVar As ListViewItem

        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(SQL.ToString, oConn2)
        r = 0
        RowCount = dt.Rows.Count
        Do While r < RowCount
            ListItemVar = ListView1.Items.Add(r + 1)
            ListItemVar.SubItems.Add(dt.Rows(r).Item(0).ToString)
            ListItemVar.SubItems.Add(dt.Rows(r).Item(1).ToString)
            r += 1
        Loop

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Me.Close()
    End Sub
End Class