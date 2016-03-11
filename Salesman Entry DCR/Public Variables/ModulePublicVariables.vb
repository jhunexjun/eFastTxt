Imports System.Data.SqlClient

Module ModulePublicVariables
    Friend oConn As SqlConnection
    'Friend oConn2 As SqlConnection
    Friend oConn3 As SqlConnection

    Friend cmd As SqlCommand
    Friend da As SqlDataAdapter
    Friend ds As DataSet
    Friend dt As DataTable

    Friend UserMobile, UserName As String, UserLevel As Boolean
    'UserLevel = 1 'Admin else user e.g. 0

    Friend ErrorFound As Boolean

    Structure Connection
        Friend server As String
        Friend database As String
        Friend username As String
        Friend password As String
    End Structure
End Module

