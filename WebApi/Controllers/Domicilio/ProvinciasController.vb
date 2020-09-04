Imports System.Net
Imports System.Web.Http
Imports System.Web.Http.Description
Imports Clases

Namespace Controllers.Domicilio
    <RoutePrefix("api/Provincia")>
    Public Class ProvinciasController
        Inherits ApiController

        <HttpGet>
        <ActionName("TraerTodos")>
        <ResponseType(GetType(List(Of DTO.DTO_Provincia)))>
        Public Function TraerTodos() As IHttpActionResult
            Try
                Dim result As List(Of DTO.DTO_Provincia) = Entidad.Provincia.TraerTodos_DTO()
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
    End Class
End Namespace