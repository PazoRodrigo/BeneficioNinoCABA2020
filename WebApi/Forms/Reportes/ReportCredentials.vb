﻿Public Class ReportCredentials
    Implements Microsoft.Reporting.WebForms.IReportServerCredentials

    Private _userName As String, _password As String, _domain As String
    Public Sub New(ByVal userName As String, ByVal password As String, ByVal domain As String)
        _userName = userName
        _password = password
        _domain = domain
    End Sub

    Public ReadOnly Property ImpersonationUser() As System.Security.Principal.WindowsIdentity Implements Microsoft.Reporting.WebForms.IReportServerCredentials.ImpersonationUser
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property NetworkCredentials() As System.Net.ICredentials Implements Microsoft.Reporting.WebForms.IReportServerCredentials.NetworkCredentials
        Get
            Return New System.Net.NetworkCredential(_userName, _password, _domain)
        End Get
    End Property


    Public Function GetFormsCredentials(ByRef authCoki As System.Net.Cookie, ByRef userName As String, ByRef password As String, ByRef authority As String) As Boolean Implements Microsoft.Reporting.WebForms.IReportServerCredentials.GetFormsCredentials
        userName = _userName
        password = _password
        authority = _domain
        authCoki = New System.Net.Cookie(".ASPXAUTH", ".ASPXAUTH", "/", "Domain")
        Return True
    End Function
End Class