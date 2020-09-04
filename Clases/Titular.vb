﻿Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports LUM
Imports Clases.Entidad
Imports Connection

Namespace Entidad
    Public Class Titular
        Inherits DBE

        'Private Shared _Todos As List(Of Titular)
        'Public Shared Property Todos() As List(Of Titular)
        '    Get
        '        If _Todos Is Nothing Then
        '            _Todos = DAL_Titular.TraerTodos
        '        End If
        '        Return _Todos
        '    End Get
        '    Set(ByVal value As List(Of Titular))
        '        _Todos = value
        '    End Set
        'End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property NroAfiliado() As Integer = 0
        Public Property ApellidoNombre() As String = ""
        Public Property NroDocumento() As Integer = 0
        Public Property IdSeccional() As Integer = 0

        Public Property ObjDomicilio As Domicilio
#End Region
#Region " Lazy Load "
        'Public Property IdLazy() As Integer
        'Private _ObjLazy As Lazy
        'Public ReadOnly Property ObjLazy() As Lazy
        '    Get
        '        If _ObjLazy Is Nothing Then
        '            _ObjLazy = Lazy.TraerUno(IdLazy)
        '        End If
        '        Return _ObjLazy
        '    End Get
        'End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Titular = TraerUno(id)
            ' DBE
            idUsuarioAlta = objImportar.idUsuarioAlta
            idUsuarioBaja = objImportar.idUsuarioBaja
            idUsuarioModifica = objImportar.idUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            fechaAlta = objImportar.fechaAlta
            fechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            NroAfiliado = objImportar.NroAfiliado
            ApellidoNombre = objImportar.ApellidoNombre
            NroDocumento = objImportar.NroDocumento
            IdSeccional = objImportar.IdSeccional
            ObjDomicilio = objImportar.ObjDomicilio
        End Sub
        Sub New(ByVal ObjDTO As DTO.DTO_Titular)
            ' Entidad
            IdEntidad = ObjDTO.IdEntidad
            NroAfiliado = ObjDTO.NroAfiliado
            ApellidoNombre = ObjDTO.ApellidoNombre
            NroDocumento = ObjDTO.NroDocumento
            IdSeccional = ObjDTO.IdSeccional
            If Not ObjDTO.ObjDomicilio Is Nothing Then
                ObjDomicilio = New Domicilio(ObjDTO.ObjDomicilio)
            End If
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        'Public Shared Function TraerUno(ByVal Id As Integer) As Titular
        '    Dim result As Titular = Todos.Find(Function(x) x.IdEntidad = Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Titular)
        '    Return Todos
        'End Function
        Public Shared Function TraerUno(ByVal Id As Integer) As Titular
            Dim result As Titular = DAL_Titular.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerUnoXNroDocumento(ByVal NroDocumento As Integer) As Titular
            Dim result As Titular = DAL_Titular.TraerUnoXNroDocumento(NroDocumento)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function

        Public Shared Function TraerTodos() As List(Of Titular)
            Dim result As List(Of Titular) = DAL_Titular.TraerTodos()
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerUnoXNroDocumento_DTO(NroDocumento As Integer) As List(Of DTO.DTO_Titular)
            Dim Result As New List(Of DTO.DTO_Titular)
            Result.Add(TraerUnoXNroDocumento(NroDocumento).ToDTO)
            Return Result
        End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Titular.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Titular.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Titular.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Titular
            Dim result As New DTO.DTO_Titular
            result.IdEntidad = IdEntidad
            result.NroAfiliado = NroAfiliado
            result.ApellidoNombre = ApellidoNombre
            result.NroDocumento = NroDocumento
            result.IdSeccional = IdSeccional
            Return result
        End Function
        Private Shared Function ToListDTO(ListaOriginal As List(Of Titular)) As List(Of DTO.DTO_Titular)
            Dim ListaResult As New List(Of DTO.DTO_Titular)
            If ListaOriginal IsNot Nothing AndAlso ListaOriginal.Count > 0 Then
                For Each item As Titular In ListaOriginal
                    ListaResult.Add(item.ToDTO)
                Next
            End If
            Return ListaResult
        End Function
        'Public Shared Sub refresh()
        '    _Todos = DAL_Titular.TraerTodos
        'End Sub
        ' Nuevos
#End Region
#Region " Métodos Privados "
        ' ABM
        Private Sub ValidarAlta()
            ValidarUsuario(Me.idUsuarioAlta)
            ValidarCampos()
            ValidarNoDuplicados()
        End Sub
        Private Sub ValidarBaja()
            ValidarUsuario(Me.idUsuarioBaja)
        End Sub
        Private Sub ValidarModifica()
            ValidarUsuario(Me.idUsuarioModifica)
            ValidarCampos()
            ValidarNoDuplicados()
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
            'Dim cantidad As Integer = DAL_Titular.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Titular
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Titular

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property NroAfiliado() As Integer = 0
        Public Property ApellidoNombre() As String = ""
        Public Property NroDocumento() As Integer = 0
        Public Property IdSeccional() As Integer = 0

        Public Property ObjDomicilio As DTO.DTO_Domicilio
#End Region
    End Class ' DTO_Titular
End Namespace ' DTO



Namespace DataAccessLibrary
    Public Class DAL_Titular

#Region " Stored "
        Const storeAlta As String = "p_Titular_Alta"
        Const storeBaja As String = "p_Titular_Baja"
        Const storeModifica As String = "p_Titular_Modifica"
        Const storeTraerUnoXId As String = "p_Titular_TraerUnoXId"
        Const storeTraerTodos As String = "p_Titular_TraerTodos"
        Const storeTraerTodosActivos As String = "p_Titular_TraerTodosActivos"
        Const storeTraerUnoXNroDocumento As String = "p_Titular_TraerUnoXNroDocumento"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Titular)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.idUsuarioAlta)
            ' Variable Numérica
            '	If entidad.codPostal <> 0 Then
            '		pa.add("@VariableNumero", entidad.VariableNumero)
            '	Else
            '		pa.add("@codPostal", "borrarEntero")
            '	End If
            ' VariableFecha
            '	If entidad.fechaNacimiento.HasValue Then
            '		pa.add("@fechaNacimiento", entidad.fechaNacimiento)
            '	Else
            '		pa.add("@fechaNacimiento", "borrarFecha")
            '	End If
            ' VariableString
            '	pa.add("@VariableString", entidad.VariableString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_UTEDyC")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Titular)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.idUsuarioBaja)
            pa.add("@id", entidad.idEntidad)
            Connection.Connection.Ejecutar(store, pa, "strConn_UTEDyC")
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Titular)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.idUsuarioModifica)
            pa.add("@id", entidad.idEntidad)
            ' Variable Numérica
            '	If entidad.codPostal <> 0 Then
            '		pa.add("@VariableNumero", entidad.VariableNumero)
            '	Else
            '		pa.add("@codPostal", "borrarEntero")
            '	End If
            ' VariableFecha
            '	If entidad.fechaNacimiento.HasValue Then
            '		pa.add("@fechaNacimiento", entidad.fechaNacimiento)
            '	Else
            '		pa.add("@fechaNacimiento", "borrarFecha")
            '	End If
            ' VariableString
            '	pa.add("@VariableString", entidad.VariableString.ToUpper.Trim)
            Connection.Connection.Ejecutar(store, pa, "strConn_UTEDyC")
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Titular
            Dim store As String = storeTraerUnoXId
            Dim result As New Titular
            Dim pa As New parametrosArray
            pa.add("@id", id)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_UTEDyC")
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
        Public Shared Function TraerTodos() As List(Of Titular)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Titular)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_UTEDyC")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Friend Shared Function TraerUnoXNroDocumento(nroDocumento As Integer) As Titular
            Dim store As String = storeTraerUnoXNroDocumento
            Dim pa As New parametrosArray
            pa.add("@nroDocumento", nroDocumento)
            Dim result As New Titular
            Dim listaResult As New List(Of Titular)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_UTEDyC")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = LlenarEntidad(dt.Rows(0))
                    End If
                End If
            End Using
            Return result
        End Function
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Titular
            Dim entidad As New Titular
            ' DBE
            If dr.Table.Columns.Contains("idUsuarioAlta") Then
                If dr.Item("idUsuarioAlta") IsNot DBNull.Value Then
                    entidad.idUsuarioAlta = CInt(dr.Item("idUsuarioAlta"))
                End If
            End If
            If dr.Table.Columns.Contains("idUsuarioBaja") Then
                If dr.Item("idUsuarioBaja") IsNot DBNull.Value Then
                    entidad.idUsuarioBaja = CInt(dr.Item("idUsuarioBaja"))
                End If
            End If
            If dr.Table.Columns.Contains("idUsuarioModifica") Then
                If dr.Item("idUsuarioModifica") IsNot DBNull.Value Then
                    entidad.idUsuarioModifica = CInt(dr.Item("idUsuarioModifica"))
                End If
            End If
            If dr.Table.Columns.Contains("IdMotivoBaja") Then
                If dr.Item("IdMotivoBaja") IsNot DBNull.Value Then
                    entidad.IdMotivoBaja = CInt(dr.Item("IdMotivoBaja"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaAlta") Then
                If dr.Item("fechaAlta") IsNot DBNull.Value Then
                    entidad.fechaAlta = CDate(dr.Item("fechaAlta"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaBaja") Then
                If dr.Item("fechaBaja") IsNot DBNull.Value Then
                    entidad.fechaBaja = CDate(dr.Item("fechaBaja"))
                End If
            End If
            ' Entidad
            If dr.Table.Columns.Contains("id") Then
                If dr.Item("id") IsNot DBNull.Value Then
                    entidad.idEntidad = CInt(dr.Item("id"))
                End If
            End If
            If dr.Table.Columns.Contains("NroAfiliado") Then
                If dr.Item("NroAfiliado") IsNot DBNull.Value Then
                    entidad.NroAfiliado = CInt(dr.Item("NroAfiliado"))
                End If
            End If
            If dr.Table.Columns.Contains("ApellidoNombre") Then
                If dr.Item("ApellidoNombre") IsNot DBNull.Value Then
                    entidad.ApellidoNombre = dr.Item("ApellidoNombre").ToString.Trim
                End If
            End If
            If dr.Table.Columns.Contains("NroDocumento") Then
                If dr.Item("NroDocumento") IsNot DBNull.Value Then
                    entidad.NroDocumento = CInt(dr.Item("NroDocumento"))
                End If
            End If
            If dr.Table.Columns.Contains("IdSeccional") Then
                If dr.Item("IdSeccional") IsNot DBNull.Value Then
                    entidad.IdSeccional = CInt(dr.Item("IdSeccional"))
                End If
            End If
            Dim ObjDomicilio As New Domicilio
            ObjDomicilio = DAL_Domicilio.LlenarEntidad(dr)
            entidad.ObjDomicilio = ObjDomicilio
            Return entidad
        End Function
#End Region
    End Class ' DAL_Titular
End Namespace ' DataAccessLibrary