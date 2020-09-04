Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net.Http.Formatting
Imports System.Web.Http


Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)
        config.EnableCors
        config.MapHttpAttributeRoutes()
        'config.MessageHandlers.Add(New TokenValidationHandler())
        config.Routes.MapHttpRoute(name:="WithActionApi", routeTemplate:="api/{controller}/{action}/{id}", defaults:=New With {Key .id = RouteParameter.[Optional]})
        Dim jsonp As New JsonMediaTypeFormatter()
        config.Formatters.Insert(0, jsonp)
        'config.Routes.MapHttpRoute(name:="DefaultApi", routeTemplate:="api/{controller}/{id}", defaults:=New With {Key .id = RouteParameter.[Optional]})
    End Sub
End Module

