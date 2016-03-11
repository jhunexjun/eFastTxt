Imports System.Data.SqlClient

Module ModPublicVariables
    Friend oConn As SqlConnection
    Friend oConn2 As SqlConnection
    Friend oConn3 As SqlConnection

    Friend r, RowCount As Integer


    Friend dt As DataTable

    Friend UserMobile, UserName As String, UserLevel As Boolean, CheqClrDay As Int16
    'UserLevel = 1 'Admin else user e.g. 0

    Friend ErrorFound As Boolean

    Structure Connection
        Friend server As String
        Friend database As String
        Friend username As String
        Friend password As String
    End Structure

    Structure Update_Settings
        Friend Application_Update_Location As String
        Friend EXE_Updater_Location
        Friend Auto_Update_On_Startup
        Friend EXE_Name
    End Structure
End Module

