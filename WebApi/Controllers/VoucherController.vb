Imports System.Net
Imports System.Web.Http
Imports System.Web.Http.Description
Imports Clases
Imports Clases.Entidad

Namespace Controllers
    Public Class VoucherController
        Inherits ApiController
        <HttpGet>
        <ActionName("TraerTodos")>
        <ResponseType(GetType(List(Of DTO.DTO_Voucher)))>
        Public Function TraerTodos() As IHttpActionResult
            Try
                Dim result As List(Of DTO.DTO_Voucher) = Entidad.Voucher.TraerTodos()
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
        <HttpGet>
        <ActionName("TraerTodosxAfiliado")>
        <ResponseType(GetType(List(Of DTO.DTO_Voucher)))>
        Public Function TraerTodosxAfiliado(idAfiliado As Integer) As IHttpActionResult
            Try
                Dim result As List(Of DTO.DTO_Voucher) = Entidad.Voucher.TraerTodosPorTitular(idAfiliado)
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
        <HttpGet>
        <ActionName("TraerUnoxId")>
        <ResponseType(GetType(DTO.DTO_Voucher))>
        Public Function TraerUnoxId(idVoucher As Integer) As IHttpActionResult
            Try
                Dim result As DTO.DTO_Voucher = Entidad.Voucher.TraerUno(idVoucher).ToDTO
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
        <HttpPost>
        <ActionName("Alta")>
        <ResponseType(GetType(DTO.DTO_Voucher))>
        Public Function Alta(<FromBody()> entidad As DTO.DTO_Voucher) As IHttpActionResult
            Try
                Dim V As New Voucher(entidad)
                V.Alta()
                Dim result As DTO.DTO_Voucher = Voucher.TraerUno(V.IdEntidad).ToDTO
                Return Ok(result)
            Catch ex As Exception
                Return Content(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
    End Class
End Namespace