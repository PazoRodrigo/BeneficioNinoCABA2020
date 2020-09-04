Imports System.Net
Imports System.Web.Http
Imports System.Web.Http.Description
Imports Clases

Namespace Controllers
    <RoutePrefix("api/Familiare")>
    Public Class FamiliaresController
        Inherits ApiController

        <HttpGet>
        <ActionName("TraerTodosXTitular")>
        <ResponseType(GetType(List(Of DTO.DTO_Familiar)))>
        Public Function TraerTodosXTitular(IdAfiliado As Integer) As IHttpActionResult
            Try
                Dim result As List(Of DTO.DTO_Familiar) = Entidad.Familiar.TraerTodosXTitular_DTO(IdAfiliado)
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
    End Class
End Namespace