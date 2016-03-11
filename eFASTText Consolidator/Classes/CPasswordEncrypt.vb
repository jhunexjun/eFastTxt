Option Explicit On
Public Class CPasswordEncrypt
    Public Function Encrypt(ByVal StringText As String) As String
        Dim i As Integer            'Loop counter
        Dim intKeyChar As Integer   'Character within the key that we'll use to encrypt
        Dim strTemp As String = ""       'Store the encrypted string as it grows
        Dim strText As String       'The initial text to be encrypted
        Dim strKey As String        'The encryption key
        Dim strChar1 As String   'The first character to XOR
        Dim strChar2 As String   'The second character to XOR

        'Get the text and key from the textboxes
        'If strCurrent = "" Then
        '    strText = txtText.Text
        'Else
        '    strText = strCurrent
        'End If
        'strKey = txtKey.Text

        strText = StringText

        strKey = 4

        'Loop through each character in the text
        For i = 1 To Len(strText)
            'Get the next character from the text
            strChar1 = Mid(strText, i, 1)

            'Find the current "frame" within the key
            intKeyChar = ((i - 1) Mod Len(strKey)) + 1

            'Get the next character from the key
            strChar2 = Mid(strKey, intKeyChar, 1)

            'Convert the charaters to ASCII, XOR them, and convert to a character again
            strTemp = strTemp & Chr(Asc(strChar1) Xor Asc(strChar2))

        Next i

        'Display the resultant encrypted string
        Encrypt = strTemp
    End Function

End Class
