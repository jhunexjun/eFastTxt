<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInvoices
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInvoices))
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.colNo = New System.Windows.Forms.ColumnHeader
        Me.colInvoice = New System.Windows.Forms.ColumnHeader
        Me.colInvoiceDate = New System.Windows.Forms.ColumnHeader
        Me.colCustomer = New System.Windows.Forms.ColumnHeader
        Me.colSalesperson = New System.Windows.Forms.ColumnHeader
        Me.colPesoValue = New System.Windows.Forms.ColumnHeader
        Me.colStatus = New System.Windows.Forms.ColumnHeader
        Me.colDeliveryDate = New System.Windows.Forms.ColumnHeader
        Me.colDueDate = New System.Windows.Forms.ColumnHeader
        Me.colLeadTime = New System.Windows.Forms.ColumnHeader
        Me.colLogistic = New System.Windows.Forms.ColumnHeader
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ChangeStatusToToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LoadedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReturnedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeliveredToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PendingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.TSPrintPreview = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colNo, Me.colInvoice, Me.colInvoiceDate, Me.colCustomer, Me.colSalesperson, Me.colPesoValue, Me.colStatus, Me.colDeliveryDate, Me.colDueDate, Me.colLeadTime, Me.colLogistic})
        Me.ListView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(17, 12)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(990, 431)
        Me.ListView1.TabIndex = 73
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'colNo
        '
        Me.colNo.Text = "#"
        Me.colNo.Width = 35
        '
        'colInvoice
        '
        Me.colInvoice.Text = "Invoice"
        '
        'colInvoiceDate
        '
        Me.colInvoiceDate.Text = "Invoice Date"
        Me.colInvoiceDate.Width = 84
        '
        'colCustomer
        '
        Me.colCustomer.Text = "Customer"
        Me.colCustomer.Width = 211
        '
        'colSalesperson
        '
        Me.colSalesperson.Text = "SM"
        Me.colSalesperson.Width = 36
        '
        'colPesoValue
        '
        Me.colPesoValue.Text = "Peso Value"
        Me.colPesoValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colPesoValue.Width = 91
        '
        'colStatus
        '
        Me.colStatus.Text = "Status"
        Me.colStatus.Width = 90
        '
        'colDeliveryDate
        '
        Me.colDeliveryDate.Text = "Delivery"
        Me.colDeliveryDate.Width = 90
        '
        'colDueDate
        '
        Me.colDueDate.Text = "Due Date"
        Me.colDueDate.Width = 80
        '
        'colLeadTime
        '
        Me.colLeadTime.Text = "Lead Time"
        Me.colLeadTime.Width = 65
        '
        'colLogistic
        '
        Me.colLogistic.Text = "Logistic"
        Me.colLogistic.Width = 100
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChangeStatusToToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(161, 26)
        '
        'ChangeStatusToToolStripMenuItem
        '
        Me.ChangeStatusToToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CanceToolStripMenuItem, Me.LoadedToolStripMenuItem, Me.ReturnedToolStripMenuItem, Me.DeliveredToolStripMenuItem, Me.PendingToolStripMenuItem})
        Me.ChangeStatusToToolStripMenuItem.Name = "ChangeStatusToToolStripMenuItem"
        Me.ChangeStatusToToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.ChangeStatusToToolStripMenuItem.Text = "Change Status To"
        '
        'CanceToolStripMenuItem
        '
        Me.CanceToolStripMenuItem.Name = "CanceToolStripMenuItem"
        Me.CanceToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.CanceToolStripMenuItem.Text = "Canceled"
        '
        'LoadedToolStripMenuItem
        '
        Me.LoadedToolStripMenuItem.Name = "LoadedToolStripMenuItem"
        Me.LoadedToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.LoadedToolStripMenuItem.Text = "Loaded"
        '
        'ReturnedToolStripMenuItem
        '
        Me.ReturnedToolStripMenuItem.Name = "ReturnedToolStripMenuItem"
        Me.ReturnedToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.ReturnedToolStripMenuItem.Text = "Returned"
        '
        'DeliveredToolStripMenuItem
        '
        Me.DeliveredToolStripMenuItem.Name = "DeliveredToolStripMenuItem"
        Me.DeliveredToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.DeliveredToolStripMenuItem.Text = "Delivered"
        '
        'PendingToolStripMenuItem
        '
        Me.PendingToolStripMenuItem.Name = "PendingToolStripMenuItem"
        Me.PendingToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.PendingToolStripMenuItem.Text = "Pending"
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(932, 458)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 79
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1019, 24)
        Me.MenuStrip1.TabIndex = 80
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'FileToolStripMenuItem1
        '
        Me.FileToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.TSPrintPreview, Me.ToolStripMenuItem1, Me.ToolStripMenuItem6})
        Me.FileToolStripMenuItem1.MergeAction = System.Windows.Forms.MergeAction.MatchOnly
        Me.FileToolStripMenuItem1.Name = "FileToolStripMenuItem1"
        Me.FileToolStripMenuItem1.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem1.Text = "&File"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(149, 6)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripMenuItem1.Text = "E&xport"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Image = Global.eFastText.My.Resources.Resources.Quit
        Me.ToolStripMenuItem6.MergeAction = System.Windows.Forms.MergeAction.MatchOnly
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripMenuItem6.Text = "&Quit"
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRefresh.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdRefresh.Location = New System.Drawing.Point(851, 458)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(75, 23)
        Me.cmdRefresh.TabIndex = 81
        Me.cmdRefresh.Text = "&Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = True
        '
        'TSPrintPreview
        '
        Me.TSPrintPreview.Enabled = False
        Me.TSPrintPreview.Image = CType(resources.GetObject("TSPrintPreview.Image"), System.Drawing.Image)
        Me.TSPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSPrintPreview.Name = "TSPrintPreview"
        Me.TSPrintPreview.Size = New System.Drawing.Size(152, 22)
        Me.TSPrintPreview.Text = "Print Pre&view"
        '
        'frmInvoices
        '
        Me.AcceptButton = Me.cmdRefresh
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(1019, 493)
        Me.Controls.Add(Me.cmdRefresh)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmInvoices"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Invoice Status Monitoring"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents colNo As System.Windows.Forms.ColumnHeader
    Friend WithEvents colInvoice As System.Windows.Forms.ColumnHeader
    Friend WithEvents colInvoiceDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents colCustomer As System.Windows.Forms.ColumnHeader
    Friend WithEvents colSalesperson As System.Windows.Forms.ColumnHeader
    Friend WithEvents colPesoValue As System.Windows.Forms.ColumnHeader
    Friend WithEvents colStatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDeliveryDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents colLeadTime As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDueDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents colLogistic As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ChangeStatusToToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CanceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReturnedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeliveredToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents PendingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSPrintPreview As System.Windows.Forms.ToolStripMenuItem
End Class
