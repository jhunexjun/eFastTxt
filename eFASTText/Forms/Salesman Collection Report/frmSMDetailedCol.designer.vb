<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSMDetailedColSummary
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSMDetailedColSummary))
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.colNo = New System.Windows.Forms.ColumnHeader
        Me.colSalesman = New System.Windows.Forms.ColumnHeader
        Me.colType = New System.Windows.Forms.ColumnHeader
        Me.colCustomer = New System.Windows.Forms.ColumnHeader
        Me.colBank = New System.Windows.Forms.ColumnHeader
        Me.colCheckDueDate = New System.Windows.Forms.ColumnHeader
        Me.colPayment = New System.Windows.Forms.ColumnHeader
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdFind = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtGrandTotal = New System.Windows.Forms.TextBox
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.TSSave = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.TSPrint = New System.Windows.Forms.ToolStripMenuItem
        Me.TSPrintPrevw = New System.Windows.Forms.ToolStripMenuItem
        Me.TSQuit = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colNo, Me.colSalesman, Me.colType, Me.colCustomer, Me.colBank, Me.colCheckDueDate, Me.colPayment})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(12, 12)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(697, 414)
        Me.ListView1.TabIndex = 4
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'colNo
        '
        Me.colNo.Text = "#"
        Me.colNo.Width = 30
        '
        'colSalesman
        '
        Me.colSalesman.Text = "SM"
        Me.colSalesman.Width = 40
        '
        'colType
        '
        Me.colType.Text = "Type"
        '
        'colCustomer
        '
        Me.colCustomer.Text = "Customer"
        Me.colCustomer.Width = 234
        '
        'colBank
        '
        Me.colBank.Text = "Bank"
        Me.colBank.Width = 114
        '
        'colCheckDueDate
        '
        Me.colCheckDueDate.Text = "Check Due Date"
        Me.colCheckDueDate.Width = 93
        '
        'colPayment
        '
        Me.colPayment.Text = "Payment"
        Me.colPayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colPayment.Width = 120
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(634, 454)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 8
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdFind
        '
        Me.cmdFind.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdFind.Location = New System.Drawing.Point(554, 454)
        Me.cmdFind.Name = "cmdFind"
        Me.cmdFind.Size = New System.Drawing.Size(75, 23)
        Me.cmdFind.TabIndex = 6
        Me.cmdFind.Text = "&Find"
        Me.cmdFind.UseVisualStyleBackColor = True
        Me.cmdFind.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(501, 435)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 43
        Me.Label3.Text = "Grand Total"
        Me.Label3.Visible = False
        '
        'txtGrandTotal
        '
        Me.txtGrandTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGrandTotal.Location = New System.Drawing.Point(570, 432)
        Me.txtGrandTotal.Name = "txtGrandTotal"
        Me.txtGrandTotal.ReadOnly = True
        Me.txtGrandTotal.Size = New System.Drawing.Size(140, 21)
        Me.txtGrandTotal.TabIndex = 5
        Me.txtGrandTotal.Text = "0.00"
        Me.txtGrandTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGrandTotal.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(663, 24)
        Me.MenuStrip1.TabIndex = 44
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'FileToolStripMenuItem1
        '
        Me.FileToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator2, Me.TSSave, Me.ToolStripSeparator7, Me.TSPrint, Me.TSPrintPrevw, Me.TSQuit})
        Me.FileToolStripMenuItem1.MergeAction = System.Windows.Forms.MergeAction.MatchOnly
        Me.FileToolStripMenuItem1.Name = "FileToolStripMenuItem1"
        Me.FileToolStripMenuItem1.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem1.Text = "&File"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(134, 6)
        '
        'TSSave
        '
        Me.TSSave.Enabled = False
        Me.TSSave.Image = CType(resources.GetObject("TSSave.Image"), System.Drawing.Image)
        Me.TSSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSSave.MergeIndex = 1
        Me.TSSave.Name = "TSSave"
        Me.TSSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.TSSave.Size = New System.Drawing.Size(137, 22)
        Me.TSSave.Text = "&Save"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(134, 6)
        '
        'TSPrint
        '
        Me.TSPrint.Enabled = False
        Me.TSPrint.Image = CType(resources.GetObject("TSPrint.Image"), System.Drawing.Image)
        Me.TSPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSPrint.Name = "TSPrint"
        Me.TSPrint.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.TSPrint.Size = New System.Drawing.Size(137, 22)
        Me.TSPrint.Text = "&Print"
        Me.TSPrint.Visible = False
        '
        'TSPrintPrevw
        '
        Me.TSPrintPrevw.Enabled = False
        Me.TSPrintPrevw.Image = CType(resources.GetObject("TSPrintPrevw.Image"), System.Drawing.Image)
        Me.TSPrintPrevw.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSPrintPrevw.Name = "TSPrintPrevw"
        Me.TSPrintPrevw.Size = New System.Drawing.Size(137, 22)
        Me.TSPrintPrevw.Text = "Print Pre&view"
        '
        'TSQuit
        '
        Me.TSQuit.Image = Global.eFastText.My.Resources.Resources.Quit
        Me.TSQuit.MergeAction = System.Windows.Forms.MergeAction.MatchOnly
        Me.TSQuit.Name = "TSQuit"
        Me.TSQuit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.TSQuit.Size = New System.Drawing.Size(137, 22)
        Me.TSQuit.Text = "&Quit"
        '
        'frmSMDetailedColSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(721, 489)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtGrandTotal)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdFind)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmSMDetailedColSummary"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Detailed Salesman Collection"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents colType As System.Windows.Forms.ColumnHeader
    Friend WithEvents colPayment As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdFind As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtGrandTotal As System.Windows.Forms.TextBox
    Friend WithEvents colBank As System.Windows.Forms.ColumnHeader
    Friend WithEvents colCustomer As System.Windows.Forms.ColumnHeader
    Friend WithEvents colCheckDueDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSPrint As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSPrintPrevw As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSQuit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents colNo As System.Windows.Forms.ColumnHeader
    Friend WithEvents colSalesman As System.Windows.Forms.ColumnHeader
End Class
