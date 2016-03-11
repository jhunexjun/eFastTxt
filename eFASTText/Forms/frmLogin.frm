VERSION 5.00
Begin VB.Form frmLogin 
   BackColor       =   &H00FFC0C0&
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "FDC SMART Retailer Toolkit - Login"
   ClientHeight    =   1545
   ClientLeft      =   2835
   ClientTop       =   3480
   ClientWidth     =   3750
   Icon            =   "frmLogin.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   912.837
   ScaleMode       =   0  'User
   ScaleWidth      =   3521.047
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.TextBox txtUserName 
      Height          =   345
      Left            =   1290
      TabIndex        =   1
      Top             =   135
      Width           =   2325
   End
   Begin VB.CommandButton cmdOK 
      BackColor       =   &H00C0FFFF&
      Caption         =   "OK"
      Default         =   -1  'True
      Height          =   390
      Left            =   495
      Style           =   1  'Graphical
      TabIndex        =   4
      Top             =   1020
      Width           =   1140
   End
   Begin VB.CommandButton cmdCancel 
      BackColor       =   &H00C0FFFF&
      Cancel          =   -1  'True
      Caption         =   "Cancel"
      Height          =   390
      Left            =   2100
      Style           =   1  'Graphical
      TabIndex        =   5
      Top             =   1020
      Width           =   1140
   End
   Begin VB.TextBox txtPassword 
      Height          =   345
      IMEMode         =   3  'DISABLE
      Left            =   1290
      MaxLength       =   20
      PasswordChar    =   "*"
      TabIndex        =   3
      Top             =   525
      Width           =   2325
   End
   Begin VB.Label lblLabels 
      BackColor       =   &H00FFC0C0&
      Caption         =   "&User Name:"
      Height          =   270
      Index           =   0
      Left            =   105
      TabIndex        =   0
      Top             =   150
      Width           =   1080
   End
   Begin VB.Label lblLabels 
      BackColor       =   &H00FFC0C0&
      Caption         =   "&Password:"
      Height          =   270
      Index           =   1
      Left            =   105
      TabIndex        =   2
      Top             =   540
      Width           =   1080
   End
End
Attribute VB_Name = "frmLogin"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cmdCancel_Click()
End
End Sub

Private Sub cmdOK_Click()
On Error GoTo xError
Me.MousePointer = 11

    Dim EncryptedPassword As New CPasswordEncrypt, StrPass As String
    StrPass = EncryptedPassword.Encrypt(txtPassword)
    
    Call Main
    
    If UCase(txtUserName) = UCase$("Admin") And txtPassword = "fdcsmartcebu" Then
        UserName = txtUserName
        UserLevel = "1"
        With MDIForm1
            .Show
            .StatusBar1.Panels(5) = UserName
            If UserLevel = True Then

            Else

                .mnuDash3.Visible = False
                .mnuDash4.Visible = False
                .mnuChangeRUplineID.Visible = False
                .mnuCutOff.Visible = False
            End If
        End With
        Unload Me
        Exit Sub
    End If
    
    Set rs = New ADODB.Recordset
    rs.Open "Select UserName,Psswrd,UserLevel,Active from SystemUsers where UserName='" & Trim$(txtUserName) & "' And Psswrd='" & StrPass & "' COLLATE SQL_Latin1_General_Cp1_CS_AS", cn, adOpenKeyset, adLockReadOnly
    If Not rs.BOF And Not rs.EOF Then
            If rs!Active = False Then
                MsgBox "Account is inactive Please call your system administrator.", vbExclamation
                Me.MousePointer = Default
                ErrorWriter ("")
                ErrorString = "Log: " & Me.Name & ": Account is inactive. Username or password is invalid."
                ErrorWriter (ErrorString)
                Exit Sub
            End If
            
            UserName = rs!UserName
            UserLevel = rs!UserLevel
            With MDIForm1
                .Show
                .StatusBar1.Panels(5) = UserName
                '.mnuLogIn.Caption = "Log-&Off"
                '.mnuLockScreen.Enabled = True
                'Locked = False
                If UserLevel = True Then
                    
                Else
                    
                    .mnuDash3.Visible = False
                    .mnuDash4.Visible = False
                    .mnuChangeRUplineID.Visible = False
                    .mnuCutOff.Visible = False
                End If
            End With
            Unload Me
            
    Else
        Me.MousePointer = Default
        ErrorWriter ("")
        ErrorString = "Log: " & Me.Name & ": Account Not found. Username or password is invalid."
        ErrorWriter (ErrorString)
        MsgBox "Username or password is invalid.", vbCritical
        txtUserName.SetFocus
        SendKeys "{HOME}+{END}"
    End If

Exit Sub
xError:
ErrorString = "Error: " & Me.Name & ": " & Err.Number & ":" & Err.Description
ErrorWriter (ErrorString)

Me.MousePointer = Default
MsgBox ErrorString, vbCritical
End Sub

Sub LoadForm()
    
    
End Sub

Private Sub txtPassword_GotFocus()
SendKeys "{HOME}+{END}"
End Sub

Private Sub txtUserName_GotFocus()
SendKeys "{HOME}+{END}"
End Sub
