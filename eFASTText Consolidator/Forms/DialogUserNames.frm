VERSION 5.00
Begin VB.Form DialogUserName 
   BackColor       =   &H00FFC0C0&
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "System User"
   ClientHeight    =   4110
   ClientLeft      =   2760
   ClientTop       =   3750
   ClientWidth     =   7065
   Icon            =   "DialogUserNames.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   ScaleHeight     =   4110
   ScaleWidth      =   7065
   ShowInTaskbar   =   0   'False
   Begin VB.CommandButton cmdAdd 
      BackColor       =   &H00FFC0C0&
      Caption         =   "&Add"
      Height          =   375
      Left            =   5400
      Style           =   1  'Graphical
      TabIndex        =   16
      Top             =   2880
      Width           =   1095
   End
   Begin VB.ComboBox Combo1 
      Height          =   315
      ItemData        =   "DialogUserNames.frx":000C
      Left            =   2160
      List            =   "DialogUserNames.frx":0016
      Style           =   2  'Dropdown List
      TabIndex        =   4
      Top             =   1560
      Width           =   1575
   End
   Begin VB.TextBox txtLName 
      Appearance      =   0  'Flat
      Height          =   285
      Left            =   2160
      MaxLength       =   20
      TabIndex        =   7
      Top             =   2880
      Width           =   2055
   End
   Begin VB.TextBox txtFName 
      Appearance      =   0  'Flat
      Height          =   285
      Left            =   2160
      MaxLength       =   20
      TabIndex        =   5
      Top             =   2160
      Width           =   2055
   End
   Begin VB.TextBox txtMidName 
      Appearance      =   0  'Flat
      Height          =   285
      Left            =   2160
      MaxLength       =   20
      TabIndex        =   6
      Top             =   2520
      Width           =   2055
   End
   Begin VB.CheckBox Check1 
      BackColor       =   &H00FFC0C0&
      Caption         =   "Active"
      Height          =   255
      Left            =   2160
      TabIndex        =   3
      Top             =   1200
      Width           =   975
   End
   Begin VB.CommandButton cmdPassword 
      BackColor       =   &H00FFC0C0&
      Caption         =   "..."
      Enabled         =   0   'False
      Height          =   255
      Left            =   3840
      Style           =   1  'Graphical
      TabIndex        =   2
      Top             =   720
      Width           =   375
   End
   Begin VB.TextBox txtPassword 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      Enabled         =   0   'False
      Height          =   285
      IMEMode         =   3  'DISABLE
      Left            =   2160
      MaxLength       =   20
      PasswordChar    =   "*"
      TabIndex        =   1
      Top             =   720
      Width           =   1575
   End
   Begin VB.TextBox txtUserName 
      Appearance      =   0  'Flat
      Height          =   285
      Left            =   2160
      MaxLength       =   10
      TabIndex        =   0
      Top             =   360
      Width           =   1575
   End
   Begin VB.CommandButton cmdCancel 
      BackColor       =   &H00FFC0C0&
      Caption         =   "&Cancel"
      Height          =   375
      Left            =   5400
      Style           =   1  'Graphical
      TabIndex        =   9
      Top             =   3360
      Width           =   1095
   End
   Begin VB.CommandButton cmdFind 
      BackColor       =   &H00FFC0C0&
      Caption         =   "&Find"
      Default         =   -1  'True
      Height          =   375
      Left            =   5400
      Style           =   1  'Graphical
      TabIndex        =   8
      Top             =   2400
      Width           =   1095
   End
   Begin VB.Label Label5 
      BackColor       =   &H00FFC0C0&
      Caption         =   "User Level"
      Height          =   255
      Left            =   960
      TabIndex        =   15
      Top             =   1560
      Width           =   975
   End
   Begin VB.Label Label6 
      BackColor       =   &H00FFC0C0&
      Caption         =   "Last"
      Height          =   255
      Left            =   960
      TabIndex        =   14
      Top             =   2880
      Width           =   975
   End
   Begin VB.Label Label4 
      BackColor       =   &H00FFC0C0&
      Caption         =   "First"
      Height          =   255
      Left            =   960
      TabIndex        =   13
      Top             =   2160
      Width           =   975
   End
   Begin VB.Label Label3 
      BackColor       =   &H00FFC0C0&
      Caption         =   "Middle"
      Height          =   255
      Left            =   960
      TabIndex        =   12
      Top             =   2520
      Width           =   975
   End
   Begin VB.Label Label2 
      BackColor       =   &H00FFC0C0&
      Caption         =   "Password"
      Height          =   255
      Left            =   960
      TabIndex        =   11
      Top             =   720
      Width           =   975
   End
   Begin VB.Label Label1 
      BackColor       =   &H00FFC0C0&
      Caption         =   "User name"
      Height          =   255
      Left            =   960
      TabIndex        =   10
      Top             =   360
      Width           =   975
   End
End
Attribute VB_Name = "DialogUserName"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Option Explicit

Sub cmdFindCheck()
If cmdFind.Caption = "&Ok" Then
    If cmdPassword.Enabled = False Then
        
    Else
        txtUserName.Locked = True
        cmdFind.Caption = "&Update"
        cmdAdd.Enabled = 0
    End If
End If
End Sub

Private Sub Check1_Click()
Call cmdFindCheck
End Sub

Private Sub cmdAdd_Click()
txtUserName = ""
txtPassword.Enabled = True
txtPassword = ""
cmdPassword.Enabled = False
Check1.Value = 1
Combo1.ListIndex = -1
txtFName = ""
txtMidName = ""
txtLName = ""
cmdAdd.Enabled = False
cmdFind.Caption = "&Add"
txtUserName.Locked = 0
End Sub

Sub ClearValues()

    txtUserName = ""
    txtPassword = ""
    cmdPassword.Enabled = False
    Check1.Value = 0
    Combo1.ListIndex = -1
    txtFName = ""
    txtMidName = ""
    txtLName = ""
    txtUserName.Locked = 0
    txtPassword.Enabled = False
End Sub

Private Sub cmdCancel_Click()
If cmdFind.Caption = "&Update" Or cmdFind.Caption = "&Add" Or cmdFind.Caption = "&Ok" Then
    cmdFind.Caption = "&Find"
    Call ClearValues
    If UserLevel = False Then
        cmdAdd.Enabled = False
    End If
    'cmdAdd.Enabled = True
Else
    Unload Me
End If
End Sub

Private Sub cmdPassword_Click()
Set rs = New ADODB.Recordset
rs.Open "SELECT UserLevel From SystemUsers Where UserName='" & UserName & "'", cn, adOpenKeyset, adLockReadOnly
If rs!UserLevel = True And UserName <> txtUserName.Text Then
    DialogChangePassword.txtOldpassword.Visible = False
    DialogChangePassword.Label4.Visible = False
End If

DialogChangePassword.Text1.Text = txtPassword.Text
DialogChangePassword.Show 1
End Sub

Private Sub cmdFind_Click()
On Error GoTo xError

Dim EncryptedPassword As CPasswordEncrypt
Dim EncryptedPasswordStr As String

Set rs = New ADODB.Recordset

If cmdFind.Caption = "&Find" Then

    Dim xs As Control, AsteriskChecker As Boolean, Str As String
    AsteriskChecker = False
    
    Str = "SELECT a.UserName[User Name],a.FName[First],a.MidName[Middle],a.LName[Last],(CASE WHEN a.Active=1 THEN 'True' ELSE 'False' END)[Active],(CASE WHEN a.UserLevel=1 THEN 'Administrator' ELSE 'User' END)[User Level] From SystemUsers a Where"
    
    For Each xs In Me
        If TypeOf xs Is TextBox Then
            If InStr(1, xs, "*") <> 0 Then
                If xs.Name = txtUserName.Name Then
                    Str = Str & " a.UserName Like '" & Replace(txtUserName, "*", "%") & "' And "
                ElseIf xs.Name = txtFName.Name Then
                    Str = Str & " a.FName Like '" & Replace(txtFName, "*", "%") & "' And "
                ElseIf xs.Name = txtMidName.Name Then
                    Str = Str & " a.MidName Like '" & Replace(txtMidName, "*", "%") & "' And "
                ElseIf xs.Name = txtLName.Name Then
                    Str = Str & " a.LName Like '" & Replace(txtLName, "*", "%") & "' And "
                End If
                    AsteriskChecker = True
            End If
        End If
    Next xs
        If AsteriskChecker = False Then
            Str = Str & " a.UserName='" & Trim(txtUserName) & "'"
        Else
            Str = Mid$(Str, 1, Len(Str) - 5)
        End If
        
        Set rs = New ADODB.Recordset
        rs.Open Str, cn, adOpenKeyset, adLockReadOnly
        If rs.RecordCount <= 0 Then
            MsgBox "No matching record found.", vbInformation
            txtUserName.SetFocus
            SendKeys "{HOME}+{END}"
        ElseIf rs.RecordCount = 1 Then
            txtUserName = rs![User Name]
            Check1.Value = IIf(rs!Active = "True", 1, 0)
            Combo1.Text = rs![User Level]
            txtFName = rs![First]
            txtMidName = IIf(IsNull(rs!Middle), "", rs!Middle)
            txtLName = rs!Last
            cmdPassword.Enabled = True
            
            Set rs = New ADODB.Recordset
            rs.Open "SELECT a.Psswrd From SystemUsers a Where a.UserName='" & Trim(txtUserName) & "'", cn, adOpenKeyset, adLockReadOnly
            
            txtPassword = rs!Psswrd
            
            If UCase$(UserName) <> UCase$(txtUserName) Then
                If UserLevel = False Then
                    cmdPassword.Enabled = False
                Else
                    cmdPassword.Enabled = True
                End If
            End If
            
            cmdFind.Caption = "&Ok"
            
        ElseIf rs.RecordCount > 1 Then
            
            CallingForm = Me.Name
            Unload Dialog
            Dialog.Show 1
        End If
    
ElseIf cmdFind.Caption = "&Add" Then
    
    If CheckValues = True Then
        Exit Sub
    End If
    
    Set rs = New ADODB.Recordset
    rs.Open "SELECT UserName From SystemUsers Where UserName='" & Trim$(txtUserName) & "'", cn, adOpenKeyset, adLockReadOnly
    If Not rs.BOF And Not rs.EOF Then
        MsgBox "Username already exist.", vbCritical
    Else
        Set EncryptedPassword = New CPasswordEncrypt
        EncryptedPasswordStr = EncryptedPassword.Encrypt(txtPassword)
        
        Set rs = New ADODB.Recordset
        rs.Open "Insert Into SystemUsers(UserName,Psswrd,FName,MidName,LName,UserLevel,Active,UpdateDate,DateCreated) " & _
                "Values('" & Trim$(txtUserName) & "','" & Trim(EncryptedPasswordStr) & "','" & Trim(Replace(txtFName, "'", "''")) & "','" & Trim(Replace(txtMidName, "'", "''")) & "','" & Trim(Replace(txtLName, "'", "''")) & "'," & IIf(Combo1.ListIndex = 0, 1, 0) & "," & IIf(Check1.Value = 1, 1, 0) & ",GetDate(),GetDate())", cn, adOpenKeyset, adLockPessimistic
        MsgBox "Adding has been successful.", vbInformation
        cmdAdd.Enabled = True
        Call ClearValues
        cmdFind.Caption = "&Find"
    End If
    
ElseIf cmdFind.Caption = "&Ok" Then
    Unload Me
ElseIf cmdFind.Caption = "&Update" Then
    If CheckValues = True Then
        Exit Sub
    End If
    
    rs.Open "Update SystemUsers Set Psswrd='" & Trim(txtPassword) & "',UpdateDate=GetDate(),FName='" & Trim(Replace(txtFName, "'", "''")) & "',MidName='" & Trim(Replace(txtMidName, "'", "''")) & "',LName='" & Trim(Replace(txtLName, "'", "''")) & "',UserLevel=" & IIf(Combo1.ListIndex = 0, 1, 0) & ",Active='" & IIf(Check1.Value = 1, 1, 0) & "' Where UserName='" & Trim(txtUserName) & "'", cn, adOpenKeyset, adLockPessimistic
    ErrorString = "Log: " & Me.Name & ": " & rs.Source
    ErrorWriter (ErrorString)
    
    MsgBox "Updating has been successful.", vbInformation
    
    ErrorString = "Log: " & Me.Name & ": Updating has been successful."
    ErrorWriter (ErrorString)
    
    If UCase$(UserName) <> UCase$(txtUserName) Then
        If UserLevel = False Then
            cmdPassword.Enabled = False
            cmdAdd.Enabled = False
        Else
            cmdPassword.Enabled = True
            cmdAdd.Enabled = True
        End If
    End If
    
    cmdFind.Caption = "&Find"
    cmdPassword.Enabled = False
End If

Exit Sub
xError:
ErrorString = "Error: " & Me.Name & ": " & Err.Number & ":" & Err.Description
ErrorWriter (ErrorString)
MsgBox ErrorString, vbCritical

End Sub

Function CheckValues() As Boolean
On Error GoTo xError
    Dim Str As String
    CheckValues = False
    Str = "must at least 3 characters."
    If Len(Trim(txtUserName)) < 3 Then
        MsgBox "Username " & Str, vbCritical
        CheckValues = True
        Exit Function
    End If
    
    If Len(Trim(txtPassword)) < 3 Then
        MsgBox "Password " & Str, vbCritical
        CheckValues = True
        Exit Function
    End If
    
    If Combo1.ListIndex = -1 Then
        MsgBox "Please choose user level.", vbCritical
        CheckValues = True
        Exit Function
    End If
    
    If InStr(1, txtUserName.Text, "'") <> 0 Then
        MsgBox "Invalid username character.", vbCritical
        CheckValues = True
        Exit Function
    End If
    
    If InStr(1, txtPassword, "'") <> 0 Then
        MsgBox "Invalid password character.", vbCritical
        CheckValues = True
        Exit Function
    End If
    
    'If Len(Trim(txtFName)) < 3 Then
    '    MsgBox "First " & sTr, vbCritical
    '    CheckValues = True
    '    Exit Function
    'End If
    
    'If Len(Trim(txtLName)) < 3 Then
    '     MsgBox "Last name " & sTr, vbCritical
    '     CheckValues = True
    '     Exit Function
    'End If

Exit Function
xError:
ErrorString = "Error: " & Err.Number & ":" & Err.Description
ErrorWriter (ErrorString)
MsgBox ErrorString
End Function

Private Sub Combo1_Click()
Call cmdFindCheck
End Sub

Private Sub Form_Load()
Me.Left = (MDIForm1.Width - Me.Width - 200) / 2
Me.Top = (MDIForm1.Height - Me.Height - 1700) / 2

If UserLevel = False Then
    cmdAdd.Enabled = False
End If
End Sub

Private Sub txtFName_Change()
Call cmdFindCheck
End Sub

Private Sub txtFName_GotFocus()
SendKeys "{Home}+{End}"
End Sub

Private Sub txtLName_Change()
Call cmdFindCheck
End Sub

Private Sub txtLName_GotFocus()
SendKeys "{Home}+{End}"
End Sub

Private Sub txtMidName_Change()
Call cmdFindCheck
End Sub

Private Sub txtMidName_GotFocus()
SendKeys "{Home}+{End}"
End Sub
