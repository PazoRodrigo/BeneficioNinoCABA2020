Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports LUM
Imports Clases.Entidad
Imports Connection

Namespace Entidad
    Public Class Domicilio
        Inherits DBE

        'Private Shared _Todos As List(Of Domicilio)
        'Public Shared Property Todos() As List(Of Domicilio)
        '    Get
        '        If _Todos Is Nothing Then
        '            _Todos = DAL_Domicilio.TraerTodos
        '        End If
        '        Return _Todos
        '    End Get
        '    Set(ByVal value As List(Of Domicilio))
        '        _Todos = value
        '    End Set
        'End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Domicilio() As String = ""
        Public Property CodigoPostal() As Integer = 0
        Public Property IdLocalidad() As Integer = 0
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
        'Sub New(ByVal id As Integer)
        '    Dim objImportar As Domicilio = TraerUno(id)
        '    ' DBE
        '    IdUsuarioAlta = objImportar.IdUsuarioAlta
        '    IdUsuarioBaja = objImportar.IdUsuarioBaja
        '    IdUsuarioModifica = objImportar.IdUsuarioModifica
        '    IdMotivoBaja = objImportar.IdMotivoBaja
        '    FechaAlta = objImportar.FechaAlta
        '    FechaBaja = objImportar.FechaBaja
        '    ' Entidad
        '    IdEntidad = objImportar.IdEntidad
        '    Domicilio = objImportar.Domicilio
        '    CodigoPostal = objImportar.CodigoPostal
        '    IdLocalidad = objImportar.IdLocalidad
        'End Sub
        Sub New(ByVal ObjDTO As DTO.DTO_Domicilio)
            ' DBE
            IdUsuarioAlta = ObjDTO.IdUsuarioAlta
            IdUsuarioBaja = ObjDTO.IdUsuarioBaja
            IdUsuarioModifica = ObjDTO.IdUsuarioModifica
            IdMotivoBaja = ObjDTO.IdMotivoBaja
            If ObjDTO.FechaAlta > 0 Then
                Dim TempFecha As String = Right(ObjDTO.FechaAlta.ToString, 2) + "/" + Left(Right(ObjDTO.FechaAlta.ToString, 4), 2) + "/" + Left(ObjDTO.FechaAlta.ToString, 4)
                FechaAlta = CDate(TempFecha)
            End If
            If ObjDTO.FechaBaja > 0 Then
                Dim TempFecha As String = Right(ObjDTO.FechaBaja.ToString, 2) + "/" + Left(Right(ObjDTO.FechaBaja.ToString, 4), 2) + "/" + Left(ObjDTO.FechaBaja.ToString, 4)
                FechaBaja = CDate(TempFecha)
            End If
            ' Entidad
            IdEntidad = ObjDTO.IdEntidad
            Domicilio = ObjDTO.Domicilio
            CodigoPostal = ObjDTO.CodigoPostal
            IdLocalidad = ObjDTO.IdLocalidad
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        'Public Shared Function TraerUno(ByVal Id As Integer) As Domicilio
        '    Dim result As Domicilio = Todos.Find(Function(x) x.IdEntidad = Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Domicilio)
        '    Return Todos
        'End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As Domicilio
        '    Dim result As Domicilio= DAL_Domicilio.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Domicilio.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Domicilio.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Domicilio.Modifica(Me)
        End Sub
        ' Otros
        Public Overridable Function ToDTO() As DTO.DTO_Domicilio
            Dim result As New DTO.DTO_Domicilio
            If CodigoPostal > 0 Then
                result.IdEntidad = IdEntidad
                result.Domicilio = Domicilio
                result.CodigoPostal = CodigoPostal
                result.IdLocalidad = IdLocalidad
            End If
            result.Domicilio = Domicilio.ToUpper.Trim
            Return result
        End Function
        Private Shared Function ToListDTO(ListaOriginal As List(Of Domicilio)) As List(Of DTO.DTO_Domicilio)
            Dim ListaResult As New List(Of DTO.DTO_Domicilio)
            If ListaOriginal IsNot Nothing AndAlso ListaOriginal.Count > 0 Then
                For Each item As Domicilio In ListaOriginal
                    ListaResult.Add(item.ToDTO)
                Next
            End If
            Return ListaResult
        End Function
        'Public Shared Sub refresh()
        '    _Todos = DAL_Domicilio.TraerTodos
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
            'Dim cantidad As Integer = DAL_Domicilio.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Domicilio
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Domicilio

#Region " Atributos / Propiedades"
        Public Property IdUsuarioAlta As Integer = 0
        Public Property IdUsuarioBaja As Integer = 0
        Public Property IdUsuarioModifica As Integer = 0
        Public Property FechaAlta As Long = 0
        Public Property FechaBaja As Long = 0
        Public Property FechaModifica As Long = 0
        Public Property IdMotivoBaja As Integer = 0

        Public Property IdEntidad() As Integer = 0
        Public Property Domicilio() As String = ""
        Public Property CodigoPostal() As Integer = 0
        Public Property IdLocalidad() As Integer = 0
#End Region
    End Class ' DTO_Domicilio
End Namespace ' DTO



Namespace DataAccessLibrary
    Public Class DAL_Domicilio

#Region " Stored "
        Const storeAlta As String = "p_Domicilio_Alta"
        Const storeBaja As String = "p_Domicilio_Baja"
        Const storeModifica As String = "p_Domicilio_Modifica"
        Const storeTraerUnoXId As String = "p_Domicilio_TraerUnoXId"
        Const storeTraerTodos As String = "p_Domicilio_TraerTodos"
        Const storeTraerTodosActivos As String = "p_Domicilio_TraerTodosActivos"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Domicilio)
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
        Public Shared Sub Baja(ByVal entidad As Domicilio)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa, "strConn_UTEDyC")
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Domicilio)
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
        Public Shared Function TraerUno(ByVal id As Integer) As Domicilio
            Dim store As String = storeTraerUnoXId
            Dim result As New Domicilio
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
        Public Shared Function TraerTodos() As List(Of Domicilio)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Domicilio)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_UTEDyC")
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
        Public Shared Function LlenarEntidad(ByVal dr As DataRow) As Domicilio
            Dim entidad As New Domicilio
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
            If dr.Table.Columns.Contains("fechaBaja") Then
                If dr.Item("fechaBaja") IsNot DBNull.Value Then
                    entidad.FechaBaja = CDate(dr.Item("fechaBaja"))
                End If
            End If
            ' Entidad
            If dr.Table.Columns.Contains("id") Then
                If dr.Item("id") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id"))
                End If
            End If
            If dr.Table.Columns.Contains("CALLE") Then
                If dr.Item("CALLE") IsNot DBNull.Value Then
                    entidad.Domicilio = dr.Item("CALLE").ToString.Trim.ToUpper
                End If
            Else
                If dr.Item("Domicilio") IsNot DBNull.Value Then
                    entidad.Domicilio = dr.Item("Domicilio").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("CP") Then
                If dr.Item("CP") IsNot DBNull.Value Then
                    entidad.CodigoPostal = CInt(dr.Item("CP"))
                End If
            End If
            If dr.Table.Columns.Contains("IdLocalidad") Then
                If dr.Item("IdLocalidad") IsNot DBNull.Value Then
                    entidad.IdLocalidad = CInt(dr.Item("IdLocalidad"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Domicilio
End Namespace ' DataAccessLibrary