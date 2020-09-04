Imports System.Net
Imports System.Web.Http
Imports System.Web.Http.Description
Imports Clases

Namespace Controllers.Domicilio
    <RoutePrefix("api/Localidades")>
    Public Class LocalidadesController
        Inherits ApiController

        <HttpGet>
        <ActionName("TraerTodosXCodigoPostal")>
        <ResponseType(GetType(List(Of DTO.DTO_Localidad)))>
        Public Function TraerTodosXCodigoPostal(CodigoPostal As Integer) As IHttpActionResult
            Try
                Dim result As List(Of DTO.DTO_Localidad) = Entidad.Localidad.TraerTodosXCodigoPostal_DTO(CodigoPostal)
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
    End Class
End Namespace