VERSION 5.00
Begin VB.Form DialogChangePassword 
   BackColor       =   &H00FFC0C0&
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Password"
   ClientHeight    =   3195
   ClientLeft      =   2760
   ClientTop       =   3750
   ClientWidth     =   5685
   Icon            =   "DialogChangePassword.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3195
   ScaleWidth      =   5685
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox Text1 
      Appearance      =   0  'Flat
      Height          =   285
      IMEMode         =   3  'DISABLE
      Left            =   1680
      MaxLength       =   20
      TabIndex        =   8
      Top             =   120
      Visible         =   0   'False
      Width           =   1575
   End
   Begin VB.TextBox txtConfirm 
      Appearance      =   0  'Flat
      Height          =   285
      IMEMode         =   3  'DISABLE
      Left            =   1680
      MaxLength       =   20
      PasswordChar    =   "*"
      TabIndex        =   2
      Top             =   1200
      Width           =   1575
   End
   Begin VB.TextBox txtOldpassword 
      Appearance      =   0  'Flat
      Height          =   285
      IMEMode         =   3  'DISABLE
      Left            =   1680
      MaxLength       =   20
      PasswordChar    =   "*"
      TabIndex        =   0
      Top             =   480
      Width           =   1575
   End
   Begin VB.TextBox txtPassword 
      Appearance      =   0  'Flat
      Height          =   285
      IMEMode         =   3  'DISABLE
      Left            =   1680
      MaxLength       =   20
      PasswordChar    =   "*"
      TabIndex        =   1
      Top             =   840
      Width           =   1575
   End
   Begin VB.CommandButton cmdCancel 
      BackColor       =   &H00FFC0C0&
      Caption         =   "&Cancel"
      Height          =   375
      Left            =   4080
      Style           =   1  'Graphical
      TabIndex        =   4
      Top             =   2520
      Width           =   1215
   End
   Begin VB.CommandButton cmdUpdate 
      BackColor       =   &H00FFC0C0&
      Caption         =   "&Update"
      Default         =   -1  'True
      Height          =   375
      Left            =   4080
      Style           =   1  'Graphical
      TabIndex        =   3
      Top             =   2040
      Width           =   1215
   End
   Begin VB.Label Label6 
      BackColor       =   &H00FFC0C0&
      Caption         =   "Confirm"
      Height          =   255
      Left            =   480
      TabIndex        =   7
      Top             =   1200
      Width           =   975
   End
   Begin VB.Label Label4 
      BackColor       =   &H00FFC0C0&
      Caption         =   "Old password"
      Height          =   255
      Left            =   480
      TabIndex        =   6
      Top             =   480
      Width           =   975
   End
   Begin VB.Label Label3 
      BackColor       =   &H00FFC0C0&
      Caption         =   "Password"
      Height          =   255
      Left            =   480
      TabIndex        =   5
      Top             =   840
      Width           =   1095
   End
End
Attribute VB_Name = "DialogChangePassword"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Option Explicit

Private Sub cmdCancel_Click()
Unload Me
End Sub

Private Sub cmdUpdate_Click()
Dim EncryptPasswordText As New CPasswordEncrypt, Str As String
Str = EncryptPasswordText.Encrypt(Text1.Text)
'MsgBox Str
'Exit Sub

If txtOldpassword.Visible = False Then
    If StrComp(txtPassword, txtConfirm) <> 0 Then
        MsgBox "Confirmation and new password doesn't match.", vbCritical
    Else
        MsgBox "Password check has been successful.", vbInformation
        Set EncryptPasswordText = New CPasswordEncrypt
        DialogUserName.txtPassword = EncryptPasswordText.Encrypt(Me.txtPassword)
        DialogUserName.cmdFind.Caption = "&Update"
        Unload Me
    End If
    Exit Sub
End If

If StrComp(Str, txtOldpassword) <> 0 Then
    MsgBox "Old password is invalid.", vbCritical
Else
    If StrComp(txtPassword, txtConfirm) <> 0 Then
        MsgBox "Confirmation and new password doesn't match.", vbCritical
    Else
        MsgBox "Password check has been successful.", vbInformation
        Set EncryptPasswordText = New CPasswordEncrypt
        DialogUserName.txtPassword = EncryptPasswordText.Encrypt(Me.txtPassword)
        DialogUserName.cmdFind.Caption = "&Update"
        Unload Me
    End If
End If
End Sub


Private Sub txtConfirm_GotFocus()
SendKeys "{HOME}+{END}"
End Sub

Private Sub txtOldpassword_GotFocus()
SendKeys "{HOME}+{END}"
End Sub

Private Sub txtPassword_GotFocus()
SendKeys "{HOME}+{END}"
End Sub
