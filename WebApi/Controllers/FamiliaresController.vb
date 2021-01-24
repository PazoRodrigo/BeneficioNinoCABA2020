Imports System.Net
Imports System.Web.Http
Imports System.Web.Http.Description
Imports Clases

Namespace Controllers
    <RoutePrefix("api/Familiares")>
    Public Class FamiliaresController
        Inherits ApiController



        <HttpGet>
        <ActionName("TraerUnoXDatos")>
        <ResponseType(GetType(List(Of DTO.DTO_Familiar)))>
        Public Function TraerUnoXDatos(NroDocumento As Integer, IdAfiliado As Integer) As IHttpActionResult
            Try
                Dim result As DTO.DTO_Familiar = Entidad.Familiar.TraerUno(IdAfiliado, NroDocumento).ToDTO
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
        <HttpGet>
        <ActionName("TraerUno")>
        <ResponseType(GetType(List(Of DTO.DTO_Familiar)))>
        Public Function TraerUno(IdFamiliar As Integer, IdAfiliado As Integer) As IHttpActionResult
            Try
                Dim result As List(Of DTO.DTO_Familiar) = Entidad.Familiar.TraerTodosXTitular_DTO(IdAfiliado)
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
        <HttpGet>
        <ActionName("TraerTodosXTitular")>
        <ResponseType(GetType(List(Of DTO.DTO_Familiar)))>
        Public Function TraerTodosXTitular(IdTitular As Integer) As IHttpActionResult
            Try
                Dim result As List(Of DTO.DTO_Familiar) = Entidad.Familiar.TraerTodosXTitular_DTO(IdTitular)
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
    End Class
End Namespace