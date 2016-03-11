Module ModuleRelatedFunctions
    Public Function GetCustType(ByVal i As Int16) As String
        Dim str As String
        Select Case i
            Case 0
                str = "Customer"
            Case 1
                str = "Extruct"
            Case 2
                str = "Booking"
            Case 3
                str = "Salesman"
            Case Else
                str = ""
        End Select

        Return str

    End Function

End Module
