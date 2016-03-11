Imports System.Data.SqlClient

Module ModulePublicVariables
    Friend oConn(19) As SqlConnection
    Friend dt As DataTable

    Friend UserMobile, UserName As String, UserLevel As Boolean
    'UserLevel = 1 'Admin else user e.g. 0

    Friend ErrorFound As Boolean
    Friend Dir As String = My.Application.Info.DirectoryPath
    Friend File As String = "Branches.csv"
    Friend SiteCount As Int16
    Friend x As Int16
    Friend conn(19) As Connection

    Structure Connection
        Friend server As String
        Friend database As String
        Friend username As String
        Friend password As String

        Friend BranchName As String
    End Structure

    Structure Update_Settings
        Friend Application_Update_Location As String
        Friend EXE_Updater_Location
        Friend Auto_Update_On_Startup
        Friend EXE_Name
    End Structure
End Module

