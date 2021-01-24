Option Explicit On
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

        Public Property CUIL() As Long = 0
        Public Property CUITEmpresa() As Long = 0
        Public Property RazonSocial() As String = ""
        Public Property Beneficios() As Integer = 0

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
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
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
                Throw New Exception("No existen Titulares para la búsqueda")
            Else
                If result.FechaBaja IsNot Nothing Then
                    Throw New Exception("EL Beneficio es solo para Titulares de Alta")
                End If
                If result.IdSeccional <> 1 Then
                    Throw New Exception("EL Beneficio es solo para Titulares de Seccional Capital Federal")
                End If
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
        Public Shared Function TraerTodosConVoucher() As List(Of DTO.DTO_Titular)
            Dim L As List(Of Titular) = DAL_Titular.TraerTodosConVoucher()
            Dim result As New List(Of DTO.DTO_Titular)
            For Each item As Titular In L
                result.Add(item.ToDTO)
            Next
            Return result
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
            result.CUIL = CUIL
            result.CUITEmpresa = CUITEmpresa
            result.RazonSocial = RazonSocial
            result.Beneficios = Beneficios
            If ObjDomicilio IsNot Nothing Then
                result.ObjDomicilio = ObjDomicilio.ToDTO
            End If
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
            ValidarUsuario(Me.IdUsuarioAlta)
            ValidarCampos()
            ValidarNoDuplicados()
        End Sub
        Private Sub ValidarBaja()
            ValidarUsuario(Me.IdUsuarioBaja)
        End Sub
        Private Sub ValidarModifica()
            ValidarUsuario(Me.IdUsuarioModifica)
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

        Public Property CUIL() As Long = 0
        Public Property CUITEmpresa() As Long = 0
        Public Property RazonSocial() As String = ""
        Public Property Beneficios() As Integer = 0

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
        Const storeTraerUnoXId As String = "p_Representado_TraerUnoXId"
        Const storeTraerTodos As String = "p_Titular_TraerTodos"
        Const storeTraerTodosActivos As String = "p_Titular_TraerTodosActivos"
        Const storeTraerUnoXNroDocumento As String = "p_Representado_TraerUnoXNroDocumento"
        Const storeTraerTodosConVoucher As String = "p_VoucherDiaDelNino2020_TraerTodosTitularesConVoucher"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Titular)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
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
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa, "strConn_UTEDyC")
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Titular)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
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
        Friend Shared Function TraerTodosConVoucher() As List(Of Titular)
            Dim store As String = storeTraerTodosConVoucher
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Titular)
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
        Public Shared Function LlenarEntidad(ByVal dr As DataRow) As Titular
            Dim entidad As New Titular
            ' DBE
            If dr.Table.Columns.Contains("idUsuarioAlta") Then
                If dr.Item("idUsuarioAlta") IsNot DBNull.Value Then
                    entidad.IdUsuarioAlta = CInt(dr.Item("idUsuarioAlta"))
                End If
            End If
            If dr.Table.Columns.Contains("idUsuarioBaja") Then
                If dr.Item("idUsuarioBaja") IsNot DBNull.Value Then
                    entidad.IdUsuarioBaja = CInt(dr.Item("idUsuarioBaja"))
                End If
            End If
            If dr.Table.Columns.Contains("idUsuarioModifica") Then
                If dr.Item("idUsuarioModifica") IsNot DBNull.Value Then
                    entidad.IdUsuarioModifica = CInt(dr.Item("idUsuarioModifica"))
                End If
            End If
            If dr.Table.Columns.Contains("IdMotivoBaja") Then
                If dr.Item("IdMotivoBaja") IsNot DBNull.Value Then
                    entidad.IdMotivoBaja = CInt(dr.Item("IdMotivoBaja"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaAlta") Then
                If dr.Item("fechaAlta") IsNot DBNull.Value Then
                    entidad.FechaAlta = CDate(dr.Item("fechaAlta"))
                End If
            End If
            If dr.Table.Columns.Contains("FEC_BAJA") Then
                If dr.Item("FEC_BAJA") IsNot DBNull.Value Then
                    entidad.FechaBaja = CDate(dr.Item("FEC_BAJA"))
                End If
            End If
            ' Entidad
            If dr.Table.Columns.Contains("id_afiliado") Then
                If dr.Item("id_afiliado") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id_afiliado"))
                End If
            End If
            If dr.Table.Columns.Contains("NRO_SIND") Then
                If dr.Item("NRO_SIND") IsNot DBNull.Value Then
                    entidad.NroAfiliado = CInt(dr.Item("NRO_SIND"))
                End If
            End If
            If dr.Table.Columns.Contains("APE_NOM") Then
                If dr.Item("APE_NOM") IsNot DBNull.Value Then
                    entidad.ApellidoNombre = dr.Item("APE_NOM").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("NRO_DOC") Then
                If dr.Item("NRO_DOC") IsNot DBNull.Value Then
                    entidad.NroDocumento = CInt(dr.Item("NRO_DOC"))
                End If
            End If
            If dr.Table.Columns.Contains("sec") Then
                If dr.Item("sec") IsNot DBNull.Value Then
                    entidad.IdSeccional = CInt(dr.Item("sec"))
                End If
            End If
            If dr.Table.Columns.Contains("CUIL") Then
                If dr.Item("CUIL") IsNot DBNull.Value Then
                    entidad.CUIL = CLng(dr.Item("CUIL"))
                End If
            End If
            If dr.Table.Columns.Contains("CUIT") Then
                If dr.Item("CUIT") IsNot DBNull.Value Then
                    entidad.CUITEmpresa = CLng(dr.Item("CUIT"))
                End If
            End If
            If dr.Table.Columns.Contains("Razon_Social") Then
                If dr.Item("Razon_Social") IsNot DBNull.Value Then
                    entidad.RazonSocial = dr.Item("Razon_Social").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("Beneficios") Then
                If dr.Item("Beneficios") IsNot DBNull.Value Then
                    entidad.Beneficios = CInt(dr.Item("Beneficios"))
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