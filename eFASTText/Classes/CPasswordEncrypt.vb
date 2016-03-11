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

        strText = StringText

        strKey = 4

        'Loop through each character in the text
        For i = 1 To Len(strText)
            strChar1 = Mid(strText, i, 1)

            intKeyChar = ((i - 1) Mod Len(strKey)) + 1

            strChar2 = Mid(strKey, intKeyChar, 1)

            strTemp = strTemp & Chr(Asc(strChar1) Xor Asc(strChar2))

        Next i

        'Display the resultant encrypted string
        Encrypt = strTemp
    End Function

End Class
