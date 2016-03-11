<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerConcern
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomerConcern))
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.colNo = New System.Windows.Forms.ColumnHeader
        Me.colID = New System.Windows.Forms.ColumnHeader
        Me.colStatus = New System.Windows.Forms.ColumnHeader
        Me.colDateTime = New System.Windows.Forms.ColumnHeader
        Me.colNumber = New System.Windows.Forms.ColumnHeader
        Me.colFrom = New System.Windows.Forms.ColumnHeader
        Me.colMessage = New System.Windows.Forms.ColumnHeader
        Me.colReply = New System.Windows.Forms.ColumnHeader
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OpenMessageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ViewReplyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdOpen = New System.Windows.Forms.Button
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.DTFrom = New System.Windows.Forms.DateTimePicker
        Me.DTTo = New System.Windows.Forms.DateTimePicker
        Me.Checkbox1 = New System.Windows.Forms.CheckBox
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colNo, Me.colID, Me.colStatus, Me.colDateTime, Me.colNumber, Me.colFrom, Me.colMessage, Me.colReply})
        Me.ListView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListView1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.FullRowSelect = True
        Me.ListView1.Location = New System.Drawing.Point(15, 90)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(850, 298)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'colNo
        '
        Me.colNo.Text = "#"
        Me.colNo.Width = 33
        '
        'colID
        '
        Me.colID.Text = "ID"
        Me.colID.Width = 58
        '
        'colStatus
        '
        Me.colStatus.Text = "Status"
        '
        'colDateTime
        '
        Me.colDateTime.Text = "Date/Time"
        Me.colDateTime.Width = 110
        '
        'colNumber
        '
        Me.colNumber.Text = "Number"
        Me.colNumber.Width = 97
        '
        'colFrom
        '
        Me.colFrom.Text = "From"
        Me.colFrom.Width = 140
        '
        'colMessage
        '
        Me.colMessage.Text = "Message"
        Me.colMessage.Width = 284
        '
        'colReply
        '
        Me.colReply.Text = "Reply"
        Me.colReply.Width = 120
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenMessageToolStripMenuItem, Me.ViewReplyToolStripMenuItem, Me.StatusToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(157, 70)
        '
        'OpenMessageToolStripMenuItem
        '
        Me.OpenMessageToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OpenMessageToolStripMenuItem.Name = "OpenMessageToolStripMenuItem"
        Me.OpenMessageToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.OpenMessageToolStripMenuItem.Text = "Open Message"
        '
        'ViewReplyToolStripMenuItem
        '
        Me.ViewReplyToolStripMenuItem.Name = "ViewReplyToolStripMenuItem"
        Me.ViewReplyToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.ViewReplyToolStripMenuItem.Text = "View Reply"
        '
        'StatusToolStripMenuItem
        '
        Me.StatusToolStripMenuItem.Name = "StatusToolStripMenuItem"
        Me.StatusToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.StatusToolStripMenuItem.Text = "Close"
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(790, 405)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOpen
        '
        Me.cmdOpen.Location = New System.Drawing.Point(709, 405)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(75, 25)
        Me.cmdOpen.TabIndex = 1
        Me.cmdOpen.Text = "&Open"
        Me.cmdOpen.UseVisualStyleBackColor = True
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Location = New System.Drawing.Point(628, 405)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(75, 25)
        Me.cmdRefresh.TabIndex = 2
        Me.cmdRefresh.Text = "&Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = True
        '
        'DTFrom
        '
        Me.DTFrom.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTFrom.Location = New System.Drawing.Point(38, 33)
        Me.DTFrom.Name = "DTFrom"
        Me.DTFrom.Size = New System.Drawing.Size(107, 21)
        Me.DTFrom.TabIndex = 5
        '
        'DTTo
        '
        Me.DTTo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTTo.Location = New System.Drawing.Point(38, 57)
        Me.DTTo.Name = "DTTo"
        Me.DTTo.Size = New System.Drawing.Size(107, 21)
        Me.DTTo.TabIndex = 6
        '
        'Checkbox1
        '
        Me.Checkbox1.AutoSize = True
        Me.Checkbox1.Checked = True
        Me.Checkbox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Checkbox1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Checkbox1.Location = New System.Drawing.Point(15, 11)
        Me.Checkbox1.Name = "Checkbox1"
        Me.Checkbox1.Size = New System.Drawing.Size(50, 17)
        Me.Checkbox1.TabIndex = 4
        Me.Checkbox1.Text = "Filter"
        Me.Checkbox1.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(877, 24)
        Me.MenuStrip1.TabIndex = 84
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'FileToolStripMenuItem1
        '
        Me.FileToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.ToolStripMenuItem4, Me.ToolStripMenuItem5, Me.ToolStripMenuItem6})
        Me.FileToolStripMenuItem1.MergeAction = System.Windows.Forms.MergeAction.MatchOnly
        Me.FileToolStripMenuItem1.Name = "FileToolStripMenuItem1"
        Me.FileToolStripMenuItem1.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem1.Text = "&File"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(134, 6)
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Enabled = False
        Me.ToolStripMenuItem4.Image = CType(resources.GetObject("ToolStripMenuItem4.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(137, 22)
        Me.ToolStripMenuItem4.Text = "&Print"
        Me.ToolStripMenuItem4.Visible = False
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Enabled = False
        Me.ToolStripMenuItem5.Image = CType(resources.GetObject("ToolStripMenuItem5.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(137, 22)
        Me.ToolStripMenuItem5.Text = "Print Pre&view"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Image = Global.eFastText.My.Resources.Resources.Quit
        Me.ToolStripMenuItem6.MergeAction = System.Windows.Forms.MergeAction.MatchOnly
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(137, 22)
        Me.ToolStripMenuItem6.Text = "&Quit"
        '
        'frmCustomerConcern
        '
        Me.AcceptButton = Me.cmdRefresh
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(877, 442)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Checkbox1)
        Me.Controls.Add(Me.DTTo)
        Me.Controls.Add(Me.DTFrom)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.cmdOpen)
        Me.Controls.Add(Me.cmdRefresh)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmCustomerConcern"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Customer Concern"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents colID As System.Windows.Forms.ColumnHeader
    Friend WithEvents colNumber As System.Windows.Forms.ColumnHeader
    Friend WithEvents colFrom As System.Windows.Forms.ColumnHeader
    Friend WithEvents colMessage As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDateTime As System.Windows.Forms.ColumnHeader
    Friend WithEvents DTFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Checkbox1 As System.Windows.Forms.CheckBox
    Friend WithEvents colReply As System.Windows.Forms.ColumnHeader
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents OpenMessageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewReplyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents colStatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents StatusToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents colNo As System.Windows.Forms.ColumnHeader
End Class
