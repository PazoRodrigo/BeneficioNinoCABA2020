Imports System.Net
Imports System.Web.Http
Imports System.Web.Http.Description
Imports Clases

Namespace Controllers
    <RoutePrefix("api/Familiare")>
    Public Class FamiliaresController
        Inherits ApiController

        <HttpGet>
        <ActionName("TraerUnoXNroDocumento")>
        <ResponseType(GetType(List(Of DTO.DTO_Titular)))>
        Public Function TraerUnoXNroDocumento(NroDocumento As Integer) As IHttpActionResult
            Try
                Dim result As List(Of DTO.DTO_Titular) = Entidad.Titular.TraerUnoXNroDocumento_DTO(NroDocumento)
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
    End Class
End Namespace