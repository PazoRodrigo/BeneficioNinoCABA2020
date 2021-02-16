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
        <HttpGet>
        <ResponseType(GetType(DTO.DTO_EntregaBeneficio))>
        Public Function TraerTodos() As IHttpActionResult
            Try
                Dim result As New List(Of DTO.DTO_EntregaBeneficio)
                result = Entidad.EntregaBeneficio.TraerTodos()
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
        <HttpGet>
        <ResponseType(GetType(Entidad.EntregaBeneficio_ReporteEntregas))>
        Public Function TraerTodosReporte_Entregas() As IHttpActionResult
            Try
                Dim result As New List(Of Entidad.EntregaBeneficio_ReporteEntregas)
                result = Entidad.EntregaBeneficio.TraerTodosReporte_Entregas()
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
        <HttpGet>
        <ResponseType(GetType(Entidad.EntregaBeneficio_ReporteSolicitados))>
        Public Function TraerTodosReporte_Solicitados() As IHttpActionResult
            Try
                Dim result As New List(Of Entidad.EntregaBeneficio_ReporteSolicitados)
                result = Entidad.EntregaBeneficio.TraerTodosReporte_Solicitados()
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
        '<HttpGet>
        '<ResponseType(GetType(DTO.DTO_EntregaBeneficio_Solicitudes))>
        'Public Function TraerTodosReporte_Solicitudes() As IHttpActionResult
        '    Try
        '        Dim result As New List(Of DTO.DTO_EntregaBeneficio_Solicitudes)
        '        result = Entidad.EntregaBeneficio.TraerTodos()
        '        Return Ok(result)
        '    Catch ex As Exception
        '        Return Content(HttpStatusCode.InternalServerError, ex.Message)
        '    End Try
        'End Function



    End Class
End Namespace