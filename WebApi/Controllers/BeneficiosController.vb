Imports System.Net
Imports System.Web.Http
Imports System.Web.Http.Description
Imports Clases
Imports Clases.Entidad

Namespace Controllers
    Public Class BeneficiosController
        Inherits ApiController
        <HttpGet>
        <ActionName("TraerTodosxAfiliado_BeneficioEntrega")>
        <ResponseType(GetType(List(Of DTO.DTO_Beneficio)))>
        Public Function TraerTodosxAfiliado_BeneficioEntrega(idAfiliado As Integer) As IHttpActionResult
            Try
                Dim result As List(Of DTO.DTO_Beneficio) = Entidad.Beneficio.TraerTodosxAfiliado_BeneficioEntrega(idAfiliado)
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
        '<HttpGet>
        '<ActionName("GuardarDomicilio")>
        '<ResponseType(GetType(List(Of DTO.DTO_Beneficio)))>
        'Public Function TraerTodosxAfiliado_BeneficioEntregda(idAfiliado As Integer) As IHttpActionResult
        '    Try
        '        Dim result As List(Of DTO.DTO_Beneficio) = Entidad.Beneficio.TraerTodosxAfiliado_BeneficioEntrega(idAfiliado)
        '        Return Ok(result)
        '    Catch ex As Exception
        '        Return Content(HttpStatusCode.InternalServerError, ex.Message)
        '    End Try
        'End Function

    End Class
End Namespace