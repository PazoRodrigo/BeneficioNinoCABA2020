Imports System.Web.Http
Imports System.Web.Optimization

Public Class WebApiApplication
    Inherits System.Web.HttpApplication
    Protected Sub Application_Start()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
    End Sub
    Protected Sub Application_BeginRequest()
        If Request.Headers.AllKeys.Contains("Origin") AndAlso Request.HttpMethod = "OPTIONS" Then
            Response.Flush()
            Response.End()
        End If
    End Sub
End Class
