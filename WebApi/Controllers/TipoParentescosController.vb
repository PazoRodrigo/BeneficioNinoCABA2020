Imports System.Net
Imports System.Web.Http
Imports System.Web.Http.Description
Imports Clases
Imports Clases.Entidad

Namespace Controllers
    Public Class TipoParentescosController
        Inherits ApiController
        <HttpGet>
        <ActionName("TraerTodos")>
        <ResponseType(GetType(List(Of DTO.DTO_TipoParentesco)))>
        Public Function TraerTodos() As IHttpActionResult
            Try
                Dim result As List(Of DTO.DTO_TipoParentesco) = Entidad.TipoParentesco.TraerTodos()
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function

    End Class
End Namespace