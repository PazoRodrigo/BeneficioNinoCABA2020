﻿Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports LUM
Imports Clases.Entidad
Imports Connection
Imports LUM.DTO
Imports System.Configuration
Imports System.Net.Mail

Namespace Entidad
    Public Class EntregaBeneficio_ReporteEntregas
        Public Property IdTitular() As Integer = 0
        Public Property Tit_Apellido() As String = ""
        Public Property Tit_Nombre() As String = ""
        Public Property Tit_NroDocumento() As Long = 0
        Public Property Tit_Ape_Sind() As String = ""
        Public Property Tit_NroSindical() As Long = 0
        Public Property Telefono() As Long = 0
        Public Property Fam_Apellido() As String = ""
        Public Property Fam_Nombre() As String = ""
        Public Property TipoBeneficio() As String = ""
        Public Property Domicilio() As String = ""
        Public Property CodigoPostal() As Integer = 0
        Public Property Localidad() As String = ""
        Public Property RazonSocial() As String = ""
        Public Property FechaSolicitudEntrega() As Long = 0
        Public Property CorreoElectronico() As String = ""
    End Class
    Public Class EntregaBeneficio_ReporteSolicitados
        Public Property IdTitular() As Integer = 0
        Public Property Tit_Apellido() As String = ""
        Public Property Tit_Nombre() As String = ""
        Public Property Tit_NroDocumento() As Long = 0
        Public Property Tit_NroSindical() As Long = 0
        Public Property TipoBeneficio() As String = ""
        Public Property RazonSocial() As String = ""
        Public Property FechaEntrega() As Long = 0
        Public Property NroRemito() As Long = 0
    End Class
    Public Class EntregaBeneficio
        'Inherits DBE
#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdTitular() As Integer = 0
        Public Property CodigoPostal() As Integer = 0
        Public Property IdLocalidad() As Integer = 0
        Public Property Domicilio() As String = ""
        Public Property CorreoElectronico() As String = ""
        Public Property Telefono() As Long = 0

#End Region
#Region " Lazy Load "

#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As EntregaBeneficio = TraerUno(id)
            ' DBE
            'IdUsuarioAlta = objImportar.IdUsuarioAlta
            'IdUsuarioBaja = objImportar.IdUsuarioBaja
            'IdUsuarioModifica = objImportar.IdUsuarioModifica
            'IdMotivoBaja = objImportar.IdMotivoBaja
            'FechaAlta = objImportar.FechaAlta
            'FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            IdTitular = objImportar.IdTitular
            CodigoPostal = objImportar.CodigoPostal
            IdLocalidad = objImportar.IdLocalidad
            Domicilio = objImportar.Domicilio
            CorreoElectronico = objImportar.CorreoElectronico
            Telefono = objImportar.Telefono
        End Sub
        Sub New(ByVal objDesdeDTOEntregaBeneficio As DTO.DTO_EntregaBeneficio)
            ' DBE
            'IdUsuarioBaja = objDesdeDTOEntregaBeneficio.IdUsuarioBaja
            'IdUsuarioModifica = objDesdeDTOEntregaBeneficio.IdUsuarioModifica
            'IdMotivoBaja = objDesdeDTOEntregaBeneficio.IdMotivoBaja
            ' Entidad
            IdEntidad = objDesdeDTOEntregaBeneficio.IdEntidad
            IdTitular = objDesdeDTOEntregaBeneficio.IdTitular
            CodigoPostal = objDesdeDTOEntregaBeneficio.CodigoPostal
            IdLocalidad = objDesdeDTOEntregaBeneficio.IdLocalidad
            Domicilio = objDesdeDTOEntregaBeneficio.Domicilio
            CorreoElectronico = objDesdeDTOEntregaBeneficio.CorreoElectronico
            Telefono = objDesdeDTOEntregaBeneficio.Telefono
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        'Public Shared Function TraerUno(ByVal Id As Integer) As EntregaBeneficio
        '    Dim result As EntregaBeneficio = Todos.Find(Function(x) x.IdEntidad = Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of EntregaBeneficio)
        '    Return Todos
        'End Function
        Public Shared Function TraerUno(ByVal Id As Integer) As EntregaBeneficio
            Dim result As EntregaBeneficio = DAL_EntregaBeneficio.TraerUnoXTitular(Id)
            If result Is Nothing Then
                Throw New Exception("EntregaBeneficio Generado Con Anterioridad para ese Familiar")
            End If
            Return result
        End Function
        'Public Shared Function TraerTodosPorTitular(idafiliado As Integer) As List(Of DTO.DTO_EntregaBeneficio)
        '    Dim L As List(Of EntregaBeneficio) = DAL_EntregaBeneficio.TraerTodosPorTitular(idafiliado)
        '    Dim result As New List(Of DTO.DTO_EntregaBeneficio)
        '    For Each item As EntregaBeneficio In L
        '        result.Add(item.ToDTO)
        '    Next
        '    Return result
        'End Function
        Public Shared Function TraerTodosPorTitular(idafiliado As Integer) As List(Of EntregaBeneficio)
            Return DAL_EntregaBeneficio.TraerTodosPorTitular(idafiliado)
        End Function
        Public Shared Function TraerTodos() As List(Of DTO.DTO_EntregaBeneficio)
            Dim L As List(Of EntregaBeneficio) = DAL_EntregaBeneficio.TraerTodos()
            Dim result As New List(Of DTO.DTO_EntregaBeneficio)
            For Each item As EntregaBeneficio In L
                result.Add(item.ToDTO)
            Next
            Return result
        End Function
        Public Shared Function TraerTodosReporte_Entregas() As List(Of EntregaBeneficio_ReporteEntregas)
            Return DAL_EntregaBeneficio.TraerTodosReporte_Entregas()
        End Function
        Public Shared Function TraerTodosReporte_Solicitados() As List(Of EntregaBeneficio_ReporteSolicitados)
            Return DAL_EntregaBeneficio.TraerTodosReporte_Solicitados()
        End Function

        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_EntregaBeneficio.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_EntregaBeneficio.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_EntregaBeneficio.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_EntregaBeneficio
            Dim result As New DTO.DTO_EntregaBeneficio
            result.IdEntidad = IdEntidad
            result.IdTitular = IdTitular
            result.CodigoPostal = CodigoPostal
            result.IdLocalidad = IdLocalidad
            result.Domicilio = Domicilio
            result.CorreoElectronico = CorreoElectronico
            result.Telefono = Telefono
            Return result
        End Function
        ' Nuevos
#End Region
#Region " Métodos Privados "
        ' ABM
        Private Sub ValidarAlta()
            ' ValidarUsuario(Me.IdUsuarioAlta)
            ' ValidarCampos()
            '  ValidarNoDuplicados()
        End Sub
        Private Sub ValidarBaja()
            '  ValidarUsuario(Me.IdUsuarioBaja)
        End Sub
        Private Sub ValidarModifica()
            '  ValidarUsuario(Me.IdUsuarioModifica)
            ' ValidarCampos()
            'ValidarNoDuplicados()
        End Sub
        ' Validaciones
        Private Sub ValidarUsuario(ByVal idUsuario As Integer)
            If idUsuario = 0 Then
                Throw New Exception("Debe ingresar al sistema")
            End If
        End Sub
        Private Sub ValidarCampos()
            Dim sError As String = ""
            ' Campo Integer/Decimal
            '	If Me.VariableNumero.toString = "" Then
            '   	sError &= "<b>VariableNumero</b> Debe ingresar VariableNumero. <br />"
            '	ElseIf Not isnumeric(Me.VariableNumero) Then
            '   	sError &= "<b>VariableNumero</b> Debe ser numérico. <br />"
            '	End If

            ' Campo String
            '	If Me.VariableString = "" Then
            '		sError &= "<b>VariableString</b> Debe ingresar VariableString. <br />"
            '	ElseIf Me.apellido.Length > 50 Then
            '		sError &= "<b>VariableString</b> El campo debe tener como máximo 50 caracteres. <br />"
            '	End If

            ' Campo Date
            '	If Not Me.VariableFecha.has value Then
            '		sError &= "<b>VariableFecha</b> Debe ingresar VariableFecha. <br />"
            '	End If
            If sError <> "" Then
                sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
                Throw New Exception(sError)
            End If
        End Sub
        Private Sub ValidarNoDuplicados()
            'Dim cantidad As Integer = DAL_EntregaBeneficio.TraerTodosXDenominacionCant(Me.denominacion)
            'If Me.idEntidad = 0 Then
            '    ' Alta
            '    If cantidad > 0 Then
            '        Throw New Exception("La denominación a ingresar ya existe")
            '    End If
            'Else
            '    ' Modifica
            '    If cantidad > 1 Then
            '        Throw New Exception("La denominación a ingresar ya existe")
            '    End If
            'End If
        End Sub
#End Region
    End Class ' EntregaBeneficio
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_EntregaBeneficio
        'Inherits DTO_DBE

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdTitular() As Integer = 0
        Public Property CodigoPostal() As Integer = 0
        Public Property IdLocalidad() As Integer = 0
        Public Property Domicilio() As String = ""
        Public Property CorreoElectronico() As String = ""
        Public Property Telefono() As Long = 0

#End Region
    End Class ' DTO_EntregaBeneficio
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_EntregaBeneficio

#Region " Stored "
        Const storeAlta As String = "p_EntregaBeneficio_Alta"
        'Const storeBaja As String = "p_EntregaBeneficioDiaDelNino2020_Baja"
        'Const storeModifica As String = "p_EntregaBeneficio_Modifica"
        Const storeTraerUnoXIdTitular As String = "p_EntregaBeneficio_TraerUnoXIdTitular"
        'Const storeTraerTodosxTitular As String = "p_EntregaBeneficioDiaDelNino2020_TraerTodosxTitular"
        Const storeTraerTodosxTitular As String = "p_EntregaBeneficioMochilas_TraerTodosxTitular"
        Const storeTraerTodos As String = "p_EntregaBeneficioMochilas_TraerTodos"
        Const storeTraerTodosReporte As String = "p_EntregaBeneficioMochilas_TraerTodosReporte"
        Const storeTraerTodosReporte_Entregas As String = "p_EntregaBeneficioMochilas_TraerTodosReporte_Entregas"
        Const storeTraerTodosReporte_Solicitados As String = "p_EntregaBeneficioMochilas_TraerTodosReporte_Solicitados"
        'Const storeTraerTodosActivos As String = "p_EntregaBeneficio_TraerTodosActivos"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As EntregaBeneficio)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@IdTitular", entidad.IdTitular)
            pa.add("@domicilio", entidad.Domicilio.ToUpper.Trim)
            pa.add("@CodigoPostal", entidad.CodigoPostal)
            pa.add("@idlocalidad", entidad.IdLocalidad)
            pa.add("@CorreoElectronico", entidad.CorreoElectronico.ToUpper.Trim)
            pa.add("@Telefono", entidad.Telefono)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_CABA")
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As EntregaBeneficio)
            'Dim store As String = storeBaja
            'Dim pa As New parametrosArray
            'pa.add("@id", entidad.IdEntidad)
            'Connection.Connection.Ejecutar(store, pa, "strConn_CABA")
        End Sub
        Public Shared Sub Modifica(ByVal entidad As EntregaBeneficio)
            'Dim store As String = storeModifica
            'Dim pa As New parametrosArray
            ''pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            'pa.add("@id", entidad.IdEntidad)
            'Connection.Connection.Ejecutar(store, pa, "strConn_CABA")
        End Sub
        ' Traer
        Public Shared Function TraerUnoXTitular(ByVal IdTitular As Integer) As EntregaBeneficio
            Dim store As String = storeTraerUnoXIdTitular
            Dim result As New EntregaBeneficio
            Dim pa As New parametrosArray
            pa.add("@IdTitular", IdTitular)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_CABA")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = LlenarEntidad(dt.Rows(0))
                    ElseIf dt.Rows.Count = 0 Then
                        result = Nothing
                    End If
                End If
            End Using
            Return result
        End Function
        Public Shared Function TraerTodosPorTitular(IdAfiliado As Integer) As List(Of EntregaBeneficio)
            Dim store As String = storeTraerTodosxTitular
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of EntregaBeneficio)
            pa.add("@IdAfiliado", IdAfiliado)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_CABA")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodos() As List(Of EntregaBeneficio)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of EntregaBeneficio)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_CABA")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function

        Public Shared Function TraerTodosReporte_Solicitados() As List(Of EntregaBeneficio_ReporteSolicitados)
            Dim store As String = storeTraerTodosReporte_Solicitados
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of EntregaBeneficio_ReporteSolicitados)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_CABA")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidadReporte_Solicitados(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosReporte_Entregas() As List(Of EntregaBeneficio_ReporteEntregas)
            Dim store As String = storeTraerTodosReporte_Entregas
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of EntregaBeneficio_ReporteEntregas)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_CABA")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidadReporte_Entregas(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosReporte() As List(Of EntregaBeneficio)
            Dim store As String = storeTraerTodosReporte
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of EntregaBeneficio)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_CABA")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function

#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidadReporte_Entregas(ByVal dr As DataRow) As EntregaBeneficio_ReporteEntregas
            Dim entidad As New EntregaBeneficio_ReporteEntregas

            ' Entidad
            If dr.Table.Columns.Contains("IdTitular") Then
                If dr.Item("IdTitular") IsNot DBNull.Value Then
                    entidad.IdTitular = CInt(dr.Item("IdTitular"))
                End If
            End If
            If dr.Table.Columns.Contains("Tit_Apellido") Then
                If dr.Item("Tit_Apellido") IsNot DBNull.Value Then
                    entidad.Tit_Apellido = dr.Item("Tit_Apellido").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("Tit_Nombre") Then
                If dr.Item("Tit_Nombre") IsNot DBNull.Value Then
                    entidad.Tit_Nombre = dr.Item("Tit_Nombre").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("Nro_Documento") Then
                If dr.Item("Nro_Documento") IsNot DBNull.Value Then
                    entidad.Tit_NroDocumento = CLng(dr.Item("Nro_Documento"))
                End If
            End If
            If dr.Table.Columns.Contains("Apo_Sin") Then
                If dr.Item("Apo_Sin") IsNot DBNull.Value Then
                    entidad.Tit_Ape_Sind = dr.Item("Apo_Sin").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("Nro_Sindical") Then
                If dr.Item("Nro_Sindical") IsNot DBNull.Value Then
                    entidad.Tit_NroSindical = CLng(dr.Item("Nro_Sindical"))
                End If
            End If
            If dr.Table.Columns.Contains("Telefono") Then
                If dr.Item("Telefono") IsNot DBNull.Value Then
                    entidad.Telefono = CLng(dr.Item("Telefono"))
                End If
            End If
            If dr.Table.Columns.Contains("Fam_Apellido") Then
                If dr.Item("Fam_Apellido") IsNot DBNull.Value Then
                    entidad.Fam_Apellido = dr.Item("Fam_Apellido").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("Fam_Nombre") Then
                If dr.Item("Fam_Nombre") IsNot DBNull.Value Then
                    entidad.Fam_Nombre = dr.Item("Fam_Nombre").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("TipoBeneficio") Then
                If dr.Item("TipoBeneficio") IsNot DBNull.Value Then
                    entidad.TipoBeneficio = dr.Item("TipoBeneficio").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("domicilio") Then
                If dr.Item("domicilio") IsNot DBNull.Value Then
                    entidad.Domicilio = dr.Item("domicilio").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("CodigoPostal") Then
                If dr.Item("CodigoPostal") IsNot DBNull.Value Then
                    entidad.CodigoPostal = CInt(dr.Item("CodigoPostal"))
                End If
            End If
            If dr.Table.Columns.Contains("Localidad") Then
                If dr.Item("Localidad") IsNot DBNull.Value Then
                    entidad.Localidad = dr.Item("Localidad").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("Razon_Social") Then
                If dr.Item("Razon_Social") IsNot DBNull.Value Then
                    entidad.RazonSocial = dr.Item("Razon_Social").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("FechaSolicitudEntrega") Then
                If dr.Item("FechaSolicitudEntrega") IsNot DBNull.Value Then
                    Dim FechaTemp As Date? = CDate(dr.Item("FechaSolicitudEntrega"))
                    If FechaTemp.HasValue Then
                        entidad.FechaSolicitudEntrega = CLng(Year(FechaTemp.Value).ToString & Right("00" & Month(FechaTemp.Value).ToString, 2) & Right("00" & Day(FechaTemp.Value).ToString, 2))
                    End If
                End If
            End If
            If dr.Table.Columns.Contains("CorreoElectronico") Then
                If dr.Item("CorreoElectronico") IsNot DBNull.Value Then
                    entidad.CorreoElectronico = dr.Item("CorreoElectronico").ToString.Trim.ToUpper
                End If
            End If
            Return entidad
        End Function
        Private Shared Function LlenarEntidadReporte_Solicitados(ByVal dr As DataRow) As EntregaBeneficio_ReporteSolicitados
            Dim entidad As New EntregaBeneficio_ReporteSolicitados
            ' Entidad
            If dr.Table.Columns.Contains("Id_representado") Then
                If dr.Item("Id_representado") IsNot DBNull.Value Then
                    entidad.IdTitular = CInt(dr.Item("Id_representado"))
                End If
            End If
            If dr.Table.Columns.Contains("Apellido") Then
                If dr.Item("Apellido") IsNot DBNull.Value Then
                    entidad.Tit_Apellido = dr.Item("Apellido").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("Nombres") Then
                If dr.Item("Nombres") IsNot DBNull.Value Then
                    entidad.Tit_Nombre = dr.Item("Nombres").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("Nro_Documento") Then
                If dr.Item("Nro_Documento") IsNot DBNull.Value Then
                    entidad.Tit_NroDocumento = CLng(dr.Item("Nro_Documento"))
                End If
            End If
            If dr.Table.Columns.Contains("Nro_Sindical") Then
                If dr.Item("Nro_Sindical") IsNot DBNull.Value Then
                    entidad.Tit_NroSindical = CLng(dr.Item("Nro_Sindical"))
                End If
            End If
            If dr.Table.Columns.Contains("TipoBeneficio") Then
                If dr.Item("TipoBeneficio") IsNot DBNull.Value Then
                    entidad.TipoBeneficio = dr.Item("TipoBeneficio").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("Razon_Social") Then
                If dr.Item("Razon_Social") IsNot DBNull.Value Then
                    entidad.RazonSocial = dr.Item("Razon_Social").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("FechaEntrega") Then
                If dr.Item("FechaEntrega") IsNot DBNull.Value Then
                    Dim FechaTemp As Date? = CDate(dr.Item("FechaEntrega"))
                    If FechaTemp.HasValue Then
                        entidad.FechaEntrega = CLng(Year(FechaTemp.Value).ToString & Right("00" & Month(FechaTemp.Value).ToString, 2) & Right("00" & Day(FechaTemp.Value).ToString, 2))
                    End If
                End If
            End If
            If dr.Table.Columns.Contains("NroRemito") Then
                If dr.Item("NroRemito") IsNot DBNull.Value Then
                    entidad.NroRemito = CInt(dr.Item("NroRemito"))
                End If
            End If
            Return entidad
        End Function
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As EntregaBeneficio
            Dim entidad As New EntregaBeneficio
            ' DBE
            'If dr.Table.Columns.Contains("idUsuarioAlta") Then
            '    If dr.Item("idUsuarioAlta") IsNot DBNull.Value Then
            '        entidad.IdUsuarioAlta = CInt(dr.Item("idUsuarioAlta"))
            '    End If
            'End If
            'If dr.Table.Columns.Contains("idUsuarioBaja") Then
            '    If dr.Item("idUsuarioBaja") IsNot DBNull.Value Then
            '        entidad.IdUsuarioBaja = CInt(dr.Item("idUsuarioBaja"))
            '    End If
            'End If
            'If dr.Table.Columns.Contains("idUsuarioModifica") Then
            '    If dr.Item("idUsuarioModifica") IsNot DBNull.Value Then
            '        entidad.IdUsuarioModifica = CInt(dr.Item("idUsuarioModifica"))
            '    End If
            ''End If
            'If dr.Table.Columns.Contains("IdMotivoBaja") Then
            '    If dr.Item("IdMotivoBaja") IsNot DBNull.Value Then
            '        entidad.IdMotivoBaja = CInt(dr.Item("IdMotivoBaja"))
            '    End If
            'End If
            ''If dr.Table.Columns.Contains("cp") Then
            '    If dr.Item("cp") IsNot DBNull.Value Then
            '        entidad.CodigoPostal = CInt(dr.Item("cp"))
            '    End If
            'End If
            'If dr.Table.Columns.Contains("id_localidad") Then
            '    If dr.Item("id_localidad") IsNot DBNull.Value Then
            '        entidad.IdLocalidad = CInt(dr.Item("id_localidad"))
            '    End If
            'End If
            'If dr.Table.Columns.Contains("domicilio") Then
            '    If dr.Item("domicilio") IsNot DBNull.Value Then
            '        entidad.Domicilio = CStr(dr.Item("domicilio"))
            '    End If
            'End If
            'If dr.Table.Columns.Contains("fechaAlta") Then
            '    If dr.Item("fechaAlta") IsNot DBNull.Value Then
            '        entidad.FechaAlta = CDate(dr.Item("fechaAlta"))
            '    End If
            'End If
            'If dr.Table.Columns.Contains("fechaBaja") Then
            '    If dr.Item("fechaBaja") IsNot DBNull.Value Then
            '        entidad.FechaBaja = CDate(dr.Item("fechaBaja"))
            '    End If
            'End If
            ' Entidad
            If dr.Table.Columns.Contains("id") Then
                If dr.Item("id") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id"))
                End If
            End If
            If dr.Table.Columns.Contains("IdTitular") Then
                If dr.Item("IdTitular") IsNot DBNull.Value Then
                    entidad.IdTitular = CInt(dr.Item("IdTitular"))
                End If
            End If
            If dr.Table.Columns.Contains("CodigoPostal") Then
                If dr.Item("CodigoPostal") IsNot DBNull.Value Then
                    entidad.CodigoPostal = CInt(dr.Item("CodigoPostal"))
                End If
            End If
            If dr.Table.Columns.Contains("domicilio") Then
                If dr.Item("domicilio") IsNot DBNull.Value Then
                    entidad.Domicilio = dr.Item("domicilio").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("IdLocalidad") Then
                If dr.Item("IdLocalidad") IsNot DBNull.Value Then
                    entidad.IdLocalidad = CInt(dr.Item("IdLocalidad"))
                End If
            End If

            If dr.Table.Columns.Contains("CorreoElectronico") Then
                If dr.Item("CorreoElectronico") IsNot DBNull.Value Then
                    entidad.CorreoElectronico = dr.Item("CorreoElectronico").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("Telefono") Then
                If dr.Item("Telefono") IsNot DBNull.Value Then
                    entidad.Telefono = CLng(dr.Item("Telefono"))
                End If
            End If






            Return entidad
        End Function
#End Region
    End Class ' DAL_EntregaBeneficio
End Namespace ' DataAccessLibrary