Imports System.Text
Public Class frmPDCListings
    Dim Query As CPerformQuery
    Private SQL As StringBuilder

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        cmdFind.Enabled = False

        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor
        ListView1.Items.Clear()

        Dim ListItemVar As ListViewItem
        Query = New CPerformQuery

        dt = Query.PerformSelectQuery(GetSQL(), oConn2)

        Dim c As Int16 = 0
        txtGrandTotal.Text = "0.00"

        Do While c < dt.Rows.Count
            ListItemVar = ListView1.Items.Add(c + 1)
            With ListItemVar
                ListItemVar.SubItems.Add(dt.Rows(c).Item("Origin"))
                ListItemVar.SubItems.Add(dt.Rows(c).Item("CheckDate"))
                ListItemVar.SubItems.Add(dt.Rows(c).Item("ORNo").ToString)
                ListItemVar.SubItems.Add(dt.Rows(c).Item("CustomerName").ToString)
                ListItemVar.SubItems.Add(Format(dt.Rows(c).Item("CheckAmount"), "standard"))
                ListItemVar.SubItems.Add(dt.Rows(c).Item("Narration").ToString)
            End With

            txtGrandTotal.Text = CDbl(txtGrandTotal.Text) + CDbl(dt.Rows(c).Item("CheckAmount"))

            c += 1
        Loop

        txtGrandTotal.Text = Format(txtGrandTotal.Text, "Standard")

        If dt.Rows.Count <= 0 Then
            MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
        Else
            TSExport.Enabled = True
        End If

        cmdFind.Enabled = True
        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        cmdFind.Enabled = True
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Function GetSQL() As String
        On Error GoTo xError

        SQL = New StringBuilder

        SQL.Append("Declare @From as DateTime, @To as DateTime ")
        SQL.Append("SET @From='" & DateTimePicker1.Value & "' ")
        SQL.Append("SET @To='" & DateTimePicker2.Value & "' ")
        SQL.Append("SET @From=CONVERT(nvarchar,@From,101) ")
        SQL.Append("SET @To=CONVERT(nvarchar,@To,101) ")
        SQL.Append("SELECT (CASE WHEN dbo.fn_GetOR(CommandUsed) IS NULL THEN 'Syspro' ELSE 'eFastText' END )[Origin],CONVERT(nvarchar,dbo.fn_GetCheckDueDate(CommandUsed),101)[CheckDate],dbo.fn_GetOR(CommandUsed)[ORNo],oRegName collate Database_default as CustomerName,dbo.fn_GetCheckPayment(CommandUsed)[CheckAmount],b.Narration ")
        SQL.Append("FROM [" & oConn2.Database & "].[dbo].[tblsmsOut] a RIGHT OUTER JOIN [" & oConn.Database & "].[dbo].[ArPostDatedCh] b ON [" & oConn2.Database & "].[dbo].fn_GetOR(CommandUsed)=substring(b.Cheque,CHARINDEX('-',b.Cheque,0)+1,len(b.Cheque)) ")
        SQL.Append("WHERE CommandUsed Like 'PAYCK %' AND oRepliedMSG Like '%successful%' AND oVoid=0 ")
        If CheckBox1.Checked = True Then
            SQL.Append("AND CONVERT(nvarchar,dbo.fn_GetCheckDueDate(CommandUsed),101)>=@From AND CONVERT(nvarchar,dbo.fn_GetCheckDueDate(CommandUsed),101)<=@To ")
        End If

        Return SQL.ToString

        Exit Function
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Function

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            GroupBox1.Visible = True
        Else
            CheckBox1.Checked = False
            GroupBox1.Visible = False
        End If

        ListView1.Items.Clear()
        TSExport.Enabled = False
        txtGrandTotal.Text = "0.00"

    End Sub

    Private Sub ExportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSExport.Click
        On Error GoTo xError

        SaveFileDialog1.Filter = "Text File(*.txt)|*.txt"
        SaveFileDialog1.ShowDialog()

        If SaveFileDialog1.FileName <> "" Then
            Dim ExportToText As New CExportToText

            Dim c = 0, RowText As String = ""
            r = 0
            Dim LVColumnCnt As Int16 = ListView1.Columns.Count, LVRCnt As Integer = ListView1.Items.Count

            Do While c < LVColumnCnt
                RowText &= ListView1.Columns(c).Text & IIf(c = ListView1.Columns.Count - 1, "", vbTab)
                c += 1
            Loop

            ExportToText.CreateFile(SaveFileDialog1.FileName)
            ExportToText.WriteText(RowText)

            c = 0
            RowText = ""

            Do While r < LVRCnt
                Do While c < LVColumnCnt
                    RowText &= ListView1.Items(r).SubItems(c).Text & IIf(c = LVColumnCnt - 1, "", vbTab)
                    c += 1
                Loop

                ExportToText.WriteText(RowText)

                c = 0
                r += 1
                RowText = ""
            Loop

            ExportToText.CloseFile()
            SaveFileDialog1.FileName = ""

            MessageBox.Show("Export has been successful!", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub
End Class