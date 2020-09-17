Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports LUM
Imports Clases.Entidad
Imports Connection
Imports System.IO

Namespace Entidad
    Public Class Familiar
        Inherits DBE

        'Private Shared _Todos As List(Of Familiar)
        'Public Shared Property Todos() As List(Of Familiar)
        '    Get
        '        If _Todos Is Nothing Then
        '            _Todos = DAL_Familiar.TraerTodos
        '        End If
        '        Return _Todos
        '    End Get
        '    Set(ByVal value As List(Of Familiar))
        '        _Todos = value
        '    End Set
        'End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdAfiliadoTitular() As Integer = 0
        Public Property IdParentesco() As Integer = 0
        Public Property NroAfiliado() As Integer = 0
        Public Property ApellidoNombre() As String = ""
        Public Property NroDocumento() As Integer = 0
        Public Property Edad() As Integer = 0
        Public Property FechaNacimiento() As Date?

#End Region
#Region " Lazy Load "
        'Public ReadOnly Property Edad() As Integer
        '    Get
        '        Dim Result As Integer = 0
        '        If FechaNacimiento.HasValue Then
        '            'Result = LUM.LUM.Reto
        '        End If
        '        Return Result
        '    End Get
        'End Property
        Public ReadOnly Property LngFechaNacimiento() As Long
            Get
                Dim result As Long = 0
                If FechaNacimiento.HasValue Then
                    result = CLng(Year(FechaNacimiento.Value).ToString & Right("00" & Month(FechaNacimiento.Value).ToString, 2) & Right("00" & Day(FechaNacimiento.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Familiar = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            IdAfiliadoTitular = objImportar.IdAfiliadoTitular
            ApellidoNombre = objImportar.ApellidoNombre
            NroDocumento = objImportar.NroDocumento
            FechaNacimiento = objImportar.FechaNacimiento
            Edad = objImportar.Edad
            IdParentesco = objImportar.IdParentesco
        End Sub
        Sub New(ByVal ObjDTO As DTO.DTO_Familiar)
            ' Entidad
            IdEntidad = ObjDTO.IdEntidad
            IdAfiliadoTitular = ObjDTO.IdAfiliadoTitular
            ApellidoNombre = ObjDTO.ApellidoNombre
            NroDocumento = ObjDTO.NroDocumento
            Edad = ObjDTO.Edad
            IdParentesco = ObjDTO.IdParentesco
            If ObjDTO.FechaNacimiento > 0 Then
                Dim TempFecha As String = Right(ObjDTO.FechaNacimiento.ToString, 2) + "/" + Left(Right(ObjDTO.FechaNacimiento.ToString, 4), 2) + "/" + Left(ObjDTO.FechaNacimiento.ToString, 4)
                FechaNacimiento = CDate(TempFecha)
            End If
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        'Public Shared Function TraerUno(ByVal Id As Integer) As Familiar
        '    Dim result As Familiar = Todos.Find(Function(x) x.IdEntidad = Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Familiar)
        '    Return Todos
        'End Function
        Public Shared Function TraerUno(ByVal Id As Integer) As Familiar
            Dim result As Familiar = DAL_Familiar.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Familiar)
            Dim result As List(Of Familiar) = DAL_Familiar.TraerTodos()
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXTitular(idTitular As Integer) As List(Of Familiar)
            'Con filtro menores por parentesco 3,5,7,8,9,10 y de alta
            Dim result As List(Of Familiar) = DAL_Familiar.TraerTodosXTitular(idTitular)
            result = result.Where(Function(s) (s.IdParentesco = 3 Or s.IdParentesco = 5 Or s.IdParentesco = 7 Or s.IdParentesco = 8 Or s.IdParentesco = 9 Or s.IdParentesco = 10) And s.FechaBaja Is Nothing And s.Edad <= 12).ToList
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXTitular_DTO(idTitular As Integer) As List(Of DTO.DTO_Familiar)
            'filtro menores por parentesco 3,5,7,8,9,10 y de alta
            'Dim listafamiliares As List(Of Familiar) = TraerTodosXTitular(idTitular).Where(Function(s) (s.IdParentesco = 3 Or s.IdParentesco = 5 Or s.IdParentesco = 7 Or s.IdParentesco = 8 Or s.IdParentesco = 9 Or s.IdParentesco = 10) And s.FechaBaja Is Nothing).ToList
            Dim listafamiliares As List(Of Familiar) = TraerTodosXTitular(idTitular)
            Return ToListDTO(listafamiliares)
        End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Familiar.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Familiar.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Familiar.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Familiar
            Dim result As New DTO.DTO_Familiar
            result.IdEntidad = IdEntidad
            result.IdAfiliadoTitular = IdAfiliadoTitular
            result.NroAfiliado = NroAfiliado
            result.ApellidoNombre = ApellidoNombre
            result.NroDocumento = NroDocumento
            result.FechaNacimiento = LngFechaNacimiento
            result.Edad = Edad
            result.IdParentesco = IdParentesco
            Return result
        End Function
        Private Shared Function ToListDTO(ListaOriginal As List(Of Familiar)) As List(Of DTO.DTO_Familiar)
            Dim ListaResult As New List(Of DTO.DTO_Familiar)
            If ListaOriginal IsNot Nothing AndAlso ListaOriginal.Count > 0 Then
                For Each item As Familiar In ListaOriginal
                    ListaResult.Add(item.ToDTO)
                Next
            End If
            Return ListaResult
        End Function
        'Public Shared Sub refresh()
        '    _Todos = DAL_Familiar.TraerTodos
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
            'Dim cantidad As Integer = DAL_Familiar.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Familiar
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Familiar

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdAfiliadoTitular() As Integer = 0
        Public Property NroAfiliado() As Integer = 0
        Public Property ApellidoNombre() As String = ""
        Public Property NroDocumento() As Integer = 0
        Public Property FechaNacimiento() As Long = 0
        Public Property Edad() As Integer = 0
        Public Property IdParentesco() As Integer = 0
#End Region
    End Class ' DTO_Familiar
End Namespace ' DTO



Namespace DataAccessLibrary
    Public Class DAL_Familiar

#Region " Stored "
        Const storeAlta As String = "p_Familiar_Alta"
        Const storeBaja As String = "p_Familiar_Baja"
        Const storeModifica As String = "p_Familiar_Modifica"
        Const storeTraerUnoXId As String = "p_Familiar_TraerUno"
        Const storeTraerTodos As String = "p_Familiar_TraerTodos"
        Const storeTraerTodosActivos As String = "p_Familiar_TraerTodosActivos"
        Const storeTraerTodosXTitular As String = "p_Familiar_getAllByid_afiliado"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Familiar)
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
        Public Shared Sub Baja(ByVal entidad As Familiar)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa, "strConn_UTEDyC")
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Familiar)
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
        Public Shared Function TraerUno(ByVal id As Integer) As Familiar
            Dim store As String = storeTraerUnoXId
            Dim result As New Familiar
            Dim pa As New parametrosArray
            pa.add("@id_Familiar", id)
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
        Public Shared Function TraerTodos() As List(Of Familiar)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Familiar)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_UTEDyC")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Friend Shared Function TraerTodosXTitular(idTitular As Integer) As List(Of Familiar)
            Dim store As String = storeTraerTodosXTitular
            Dim pa As New parametrosArray
            pa.add("@id_Afiliado", idTitular)
            Dim listaResult As New List(Of Familiar)
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
        Public Shared Function LlenarEntidad(ByVal dr As DataRow) As Familiar
            Dim entidad As New Familiar
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
            If dr.Table.Columns.Contains("fec_baja") Then
                If dr.Item("fec_baja") IsNot DBNull.Value Then
                    entidad.FechaBaja = CDate(dr.Item("fec_baja"))
                End If
            End If
            ' Entidad
            If dr.Table.Columns.Contains("id_familiar") Then
                If dr.Item("id_familiar") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id_familiar"))
                End If
            End If
            If dr.Table.Columns.Contains("id_afiliado") Then
                If dr.Item("id_afiliado") IsNot DBNull.Value Then
                    entidad.IdAfiliadoTitular = CInt(dr.Item("id_afiliado"))
                End If
            End If
            If dr.Table.Columns.Contains("parentesco") Then
                If dr.Item("parentesco") IsNot DBNull.Value Then
                    entidad.IdParentesco = CInt(dr.Item("parentesco"))
                End If
            End If
            If dr.Table.Columns.Contains("id_afiliado") Then
                If dr.Item("id_afiliado") IsNot DBNull.Value Then
                    entidad.NroAfiliado = CInt(dr.Item("id_afiliado"))
                End If
            End If
            If dr.Table.Columns.Contains("ape_nom") Then
                If dr.Item("ape_nom") IsNot DBNull.Value Then
                    entidad.ApellidoNombre = dr.Item("ape_nom").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("nro_doc") Then
                If dr.Item("nro_doc") IsNot DBNull.Value Then
                    entidad.NroDocumento = CInt(dr.Item("nro_doc"))
                End If
            End If
            If dr.Table.Columns.Contains("edad") Then
                If dr.Item("edad") IsNot DBNull.Value Then
                    entidad.Edad = CInt(dr.Item("edad"))
                End If
            End If
            If dr.Table.Columns.Contains("fec_nac") Then
                If dr.Item("fec_nac") IsNot DBNull.Value Then
                    entidad.FechaNacimiento = CDate(dr.Item("fec_nac"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Familiar
End Namespace ' DataAccessLibrary