Imports System.Text
Public Class frmOTSelect
    Private SQL As StringBuilder
    Private Query As CPerformQuery

    Private Sub frmOTSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DTFrom.Value = Date.Today
        DTTo.Value = Date.Today
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGenerate.Click
        SQL = New StringBuilder
        SQL.Append("DECLARE @From as DateTime,@To as DateTime ")
        SQL.Append("SET @From = '" & DTFrom.Value & "' ")
        SQL.Append("SET @To = '" & DTTo.Value & "' ")
        SQL.Append("SET @From = CONVERT(nvarchar(20),@From,101) ")
        SQL.Append("SET @To = CONVERT(nvarchar(20),@To,101) ")
        SQL.Append("SELECT CommandUsed,oDateTimeIn FROM tblsmsOUT ")
        SQL.Append("WHERE CommandUsed Like 'OT %' AND oRepliedMSG Like '%Thank You%' ")
        SQL.Append("AND CONVERT(nvarchar(20),oDateTimeIn,101)>=@From AND CONVERT(nvarchar(20),oDateTimeIn,101)<=@To ")
        SQL.Append("ORDER BY oDateTimeIn")

        Dim f As New frmOTReport
        f.SQL = SQL
        f.ShowDialog()
    End Sub
End Class