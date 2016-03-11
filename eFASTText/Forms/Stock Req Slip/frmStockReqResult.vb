Imports System.Text
Public Class frmStockReqResult

    Friend vBranch As String
    Friend vSellDays As Int16
    Friend vProvision As Int16
    Friend vcboWHouse As String

    Private SQL As StringBuilder
    Private Query As CPerformQuery
    Dim r As Integer, rCount As Integer

    Private Sub frmStockReqResult_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo xError
        DataGridView1.Rows.Clear()
        SQL = New StringBuilder
        SQL.Append("exec sp_StockReqSlip " & vSellDays & "," & vProvision & "," & "'" & vcboWHouse & "'")
        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(SQL.ToString, oConn)
        r = 0
        rCount = dt.Rows.Count

        Do While r < rCount
            With DataGridView1
                .Rows.Add()
                .Item(0, r).Value = r + 1
                .Item(1, r).Value = dt.Rows(r).Item("StockCode").ToString.Trim
                .Item(2, r).Value = dt.Rows(r).Item("Description").ToString.Trim
                .Item(3, r).Value = dt.Rows(r).Item("SLoading").ToString.Trim
                .Item(4, r).Value = dt.Rows(r).Item("QtyOnHand").ToString.Trim
            End With

            r += 1
        Loop

        If rCount <= 0 Then
        Else
        End If

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub cmdCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopy.Click
        On Error GoTo xError

        Dim f As New frmStocksRequisitionSlip
        With f
            .MdiParent = frmMain
            .Show()
            .txtBranch.Text = vBranch
            .txtProvision.Text = vProvision
            .txtSellingDays.Text = vSellDays
            .cboWhouse.Text = vcboWHouse
            .GroupBox1.Enabled = False

            .cmdFind.Text = "&Save"
            .TSSave.Enabled = True

            Me.r = 0 : Me.rCount = Me.DataGridView1.Rows.Count - 1

            Do While Me.r < Me.rCount
                With .DataGridView1
                    .Rows.Add()
                    .Item(0, Me.r).Value = Me.DataGridView1.Item(0, r).Value
                    .Item(1, Me.r).Value = Me.DataGridView1.Item(1, r).Value
                    .Item(2, Me.r).Value = Me.DataGridView1.Item(2, r).Value
                    .Item(3, Me.r).Value = Me.DataGridView1.Item(3, r).Value
                    .Item(4, Me.r).Value = Me.DataGridView1.Item(4, r).Value

                    .Item(0, Me.r).ReadOnly = True
                    .Item(1, Me.r).ReadOnly = True
                    .Item(2, Me.r).ReadOnly = True
                    .Item(3, Me.r).ReadOnly = True
                    .Item(4, Me.r).ReadOnly = True

                    .Item(0, Me.r).Style.BackColor = Color.LightGray
                    .Item(1, Me.r).Style.BackColor = Color.LightGray
                    .Item(2, Me.r).Style.BackColor = Color.LightGray
                    .Item(3, Me.r).Style.BackColor = Color.LightGray
                    .Item(4, Me.r).Style.BackColor = Color.LightGray
                End With

                Me.r += 1
            Loop

            SQL = New StringBuilder
            SQL.Append("SELECT (CASE WHEN CAST(MAX(CAST(SUBSTRING(DocNum,CHARINDEX('-',DocNum)+1,LEN(DocNum)) as numeric)) as nvarchar) is NULL then 1 ELSE CAST(MAX(CAST(SUBSTRING(DocNum,CHARINDEX('-',DocNum)+1,LEN(DocNum)) as numeric)+1) as nvarchar) END)[DocNumber] FROM StockReqHeader WHERE WHouse Like substring('" & vcboWHouse & "',0,CHARINDEX('-',DocNum)) + '%' ")
            Query = New CPerformQuery
            dt = Query.PerformSelectQuery(SQL.ToString, oConn2)
            .txtDocNum.Text = vcboWHouse & "-" & dt.Rows(0).Item("DocNumber")
            .txtDocNum.ReadOnly = True

            DialogResult = Windows.Forms.DialogResult.Yes
        End With

        Me.Close()

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class