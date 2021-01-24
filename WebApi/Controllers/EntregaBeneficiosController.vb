Imports System.Net
Imports System.Web.Http
Imports System.Web.Http.Description
Imports Clases

Namespace Controllers.Domicilio
    <RoutePrefix("api/Localidades")>
    Public Class EntregaBeneficiosController
        Inherits ApiController
        <HttpPost>
        <ActionName("Alta")>
        <ResponseType(GetType(DTO.DTO_EntregaBeneficio))>
        Public Function Alta(<FromBody()> entidad As DTO.DTO_EntregaBeneficio) As IHttpActionResult
            Try
                Dim EB As New Entidad.EntregaBeneficio(entidad)
                EB.Alta()
                Return Ok()
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function


    End Class
End Namespace