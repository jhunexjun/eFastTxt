Imports System.Text
Public Class frmMain
    Private Query As CPerformQuery

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ToolStripStatusLabel6.Text = DateTime.Now
    End Sub

    Private Sub QuitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitToolStripMenuItem.Click
        If MsgBox("Are you sure you want to close the application?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            End
        End If

    End Sub

    Friend Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MsgBox("Are you sure you want to close the application?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = Application.ProductName & "   v." & Application.ProductVersion
        ToolStripStatusLabel4.Text = UserName
        ToolStripStatusLabelIPAdd.Text = oConn.DataSource
        ToolStripStatusLabelDatabase.Text = oConn.Database

        ToolStripeFastTextDatabase.Text = oConn2.Database


        If UserLevel = True Then
            UplineIDSetupToolStripMenuItem.Enabled = True
            PromoToolStripMenuItem.Enabled = True
            PromoItemsToolStripMenuItem.Enabled = True
            CustomerPromoRegistrationToolStripMenuItem.Enabled = True
        Else
            UplineIDSetupToolStripMenuItem.Enabled = False
            PromoToolStripMenuItem.Enabled = False
            PromoItemsToolStripMenuItem.Enabled = False
            CustomerPromoRegistrationToolStripMenuItem.Enabled = False
        End If

        Dim dtAuthorize As New DataTable
        Query = New CPerformQuery
        dtAuthorize = Query.PerformSelectQuery("SELECT FormModule FROM [Authorization] WHERE UserName='" & UserName & "' AND Visible='Y'", oConn2)
        r = 0 : RowCount = dtAuthorize.Rows.Count

        Dim c As Int32

        Do While r < RowCount
            c = 0
            Do While c < SetUpTSMnu.DropDownItems.Count
                If SPTStripMnu.Name = dtAuthorize.Rows(r).Item("FormModule") Then
                    SPTStripMnu.Visible = True
                End If

                c += 1
            Loop

            ' put other FormModules here.

            r += 1
        Loop
    End Sub

    Private Sub BusinessPartnersToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BusinessPartnersToolStripMenuItem1.Click
        Me.Cursor = Cursors.WaitCursor

        Dim f As New frmDialog
        f.strToPerform = "Select Customer,Name FROM ArCustomer Order by Name"
        f.DatabaseConnection = 1
        f.MdiParent = Me
        f.Show()

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub BusinessPartnerMasterDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BusinessPartnerMasterDataToolStripMenuItem.Click
        Dim f As New frmBusinessPartners
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub BroadcastToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BroadcastToolStripMenuItem1.Click
        Me.Cursor = Cursors.WaitCursor
        Dim f As New frmbroadcast
        f.MdiParent = Me
        f.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CalculatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalculatorToolStripMenuItem.Click
        Shell("calc.exe")
    End Sub

    Private Sub NotepadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotepadToolStripMenuItem.Click
        Shell("Notepad.exe")
    End Sub

    Private Sub DailyCollectionReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New frmDCR
        f.MdiParent = Me
        'f.Owner = Me
        f.Show()
    End Sub

    Private Sub cmdQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmMain_FormClosing(sender, e)
    End Sub

    Private Sub SOASummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SOASummaryToolStripMenuItem.Click
        'Dim frm As New frmSOASummary
        'frm.MdiParent = Me
        'frm.Show()

        Dim frm As New frmSOASelection
        frm.MdiParent = Me
        frm.Show()

    End Sub

    Private Sub UsersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsersToolStripMenuItem.Click
        Dim frm As New frmUserName
        frm = New frmUserName
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub UplineIDSetupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UplineIDSetupToolStripMenuItem.Click
        Dim f As New frmChangeUplineID
        Me.Cursor = Cursors.WaitCursor
        f.MdiParent = Me
        f.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub SMSInboxToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMSInboxToolStripMenuItem1.Click
        Me.Cursor = Cursors.WaitCursor
        Dim f As New frmSMSInbox
        f.MdiParent = Me
        f.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CustomerConcernToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomerConcernToolStripMenuItem1.Click
        Dim f As New frmCustomerConcern
        Me.Cursor = Cursors.WaitCursor
        f.MdiParent = Me
        f.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DepositSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New frmDepositSummary
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub ApprovalReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApprovalReportToolStripMenuItem.Click
        Dim f As New frmApprovedInvoices
        f.MdiParent = Me
        'f.Owner = Me
        f.Show()
    End Sub

    Private Sub SuccessfulPaymentReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SuccessfulPaymentReportToolStripMenuItem.Click
        Dim f As New frmSuccessfulPayment
        f.MdiParent = Me
        'f.Owner = Me
        f.Show()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim f As New frmAboutBox
        f.ShowDialog(Me)
    End Sub

    Private Sub PromoItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PromoItemsToolStripMenuItem.Click
        Dim f As New frmPromo
        f.MdiParent = Me
        'f.Owner = Me
        f.Show()
    End Sub

    Private Sub DocumentSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DocumentSettingsToolStripMenuItem.Click
        Dim f As New frmAdministration
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub StockRequisitionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockRequisitionToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Dim f As New frmStocksRequisitionSlip
        f.MdiParent = Me
        f.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ExtructPaymentRegistrationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExtructPaymentRegistrationToolStripMenuItem.Click
        Dim f As New frmMiscPayment
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub DailyCollectionReportDCRToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DailyCollectionReportDCRToolStripMenuItem.Click
        Dim f As New frmDCR
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub ForDepositToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForDepositToolStripMenuItem.Click
        Dim f As New frmTaggingOfRemittances
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub DepositSummaryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DepositSummaryToolStripMenuItem1.Click
        Dim f As New frmDepositSummary
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub DetailedCollectionReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub SalesmanEntryDCRToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesmanEntryDCRToolStripMenuItem.Click
        Dim f As New frmDCREntry
        f.MdiParent = Me
        f.Show()

    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New frmMiscPayment
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New frmDCR
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub ToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New frmTaggingOfRemittances
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub ToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New frmDepositSummary
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub ToolStripMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ToolStripMenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmSMDetailedColSummary
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ToolStripMenuItem12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmSMColSummary
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ToolStripMenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmSMDetailedColSummary
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub PDCForPostingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PDCForPostingToolStripMenuItem.Click
        Dim frm As New frmPaymentForPosting
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub PDCListingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PDCListingToolStripMenuItem.Click
        Dim frm As New frmPDCListings
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ToolStripMenuItem2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        
    End Sub

    Private Sub ToolStripMenuItem5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        Dim frm As New frmStockReqSelection
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub TSNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSNext.Click
        Dim ActiveChildForm As Form = ActiveMdiChild
    End Sub

    Private Sub RemittanceReportToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemittanceReportToolStripMenuItem.Click
        Dim frm As New frmRemitSummary
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ToolStripMenuItem6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
        Dim f As New frmPDCforPosting
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub BanksToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BanksToolStripMenuItem.Click
        Dim frm As New frmBanks
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub SetupCustomersBankToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetupCustomersBankToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor

        Dim frm As New frmSetupCustomerBank
        frm.MdiParent = Me
        frm.Show()

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ToolStripMenuItem12_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem12.Click
        Dim frm As New frmSMColSummary
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ToolStripMenuItem13_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem13.Click
        Dim frm As New frmSMDetailedColSelection
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub InvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InvoiceToolStripMenuItem.Click
        'Dim f As New frmInvoiceSelection
        Dim f As New frmDelRepSel
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub ToolStripMenuItem7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem7.Click
        Call Main()
        Call eFastTextDatabase()
        Call ConsolidatorDatabase()
    End Sub

    Private Sub SalespersonToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SPTStripMnu.Click
        Query = New CPerformQuery
        dt = Query.PerformSelectQuery("SELECT FormModule FROM [Authorization] WHERE UserName='" & UserName & "' AND Allowed='Y' AND FormModule='SPTStripMnu'", oConn2)
        If dt.Rows.Count > 0 Then
            Dim f As New frmSP
            f.MdiParent = Me
            f.Show()
        Else
            MsgBox("You are not permitted to perform this action. Access denied.", MsgBoxStyle.Critical)
        End If

    End Sub

    Private Sub DepositProcessingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DepositProcessingToolStripMenuItem.Click
        Dim frm As New frmDPSProcessing
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub DPSDToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DPSDToolStripMenuItem.Click
        Dim frm As New frmDeposit_DPS
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub ApprovedSysproOvertimeReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApprovedSysproOvertimeReportToolStripMenuItem.Click
        Dim frm As New frmOTSelect
        frm.MdiParent = Me
        frm.Show()
    End Sub
End Class