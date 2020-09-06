Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports LUM
Imports Clases.Entidad
Imports Connection

Namespace Entidad
    Public Class Provincia
        Inherits DBE

        Private Shared _Todos As List(Of Provincia)
        Public Shared Property Todos() As List(Of Provincia)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Provincia.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Provincia))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad As Integer = 0
        Public Property Nombre As String = ""
        Public Property Letra As String = ""
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
            Dim objImportar As Provincia = TraerUno(id)
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Nombre = objImportar.Nombre
            Letra = objImportar.Letra
        End Sub
        Sub New(ByVal ObjDTO As DTO.DTO_Provincia)
            ' Entidad
            IdEntidad = ObjDTO.IdEntidad
            Nombre = ObjDTO.Nombre
            Letra = ObjDTO.Letra
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Provincia
            Dim result As Provincia = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Provincia)
            Return Todos
        End Function
        Public Shared Function TraerTodos_DTO() As List(Of DTO.DTO_Provincia)
            Return ToListDTO(TraerTodos())
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As Provincia
        '    Dim result As Provincia= DAL_Provincia.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Provincia)
        '    Dim result As List(Of Provincia) = DAL_Provincia.TraerTodos()
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' Otros
        Public Function ToDTO() As DTO.DTO_Provincia
            Dim result As New DTO.DTO_Provincia With {
                .IdEntidad = IdEntidad,
                .Nombre = Nombre,
                .Letra = Letra
            }
            Return result
        End Function
        Private Shared Function ToListDTO(ListaOriginal As List(Of Provincia)) As List(Of DTO.DTO_Provincia)
            Dim ListaResult As New List(Of DTO.DTO_Provincia)
            If ListaOriginal IsNot Nothing AndAlso ListaOriginal.Count > 0 Then
                For Each item As Provincia In ListaOriginal
                    ListaResult.Add(item.ToDTO)
                Next
            End If
            Return ListaResult
        End Function
        Public Shared Sub refresh()
            _Todos = DAL_Provincia.TraerTodos
        End Sub
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
            'Dim cantidad As Integer = DAL_Provincia.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Provincia
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Provincia

#Region " Atributos / Propiedades"
        Public Property IdEntidad As Integer = 0
        Public Property Nombre As String = ""
        Public Property Letra As String = ""
#End Region
    End Class ' DTO_Provincia
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_Provincia

#Region " Stored "
        Const storeTraerUnoXId As String = "p_Provincia_TraerUnoXId"
        Const storeTraerTodos As String = "DIM.usp_Provincia_TraerTodos"
#End Region
#Region " Métodos Públicos "
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Provincia
            Dim store As String = storeTraerUnoXId
            Dim result As New Provincia
            Dim pa As New parametrosArray
            pa.add("@id", id)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_SIGES")
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
        Public Shared Function TraerTodos() As List(Of Provincia)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Provincia)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_SIGES")
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Provincia
            Dim entidad As New Provincia
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
            If dr.Table.Columns.Contains("Id_Provincia") Then
                If dr.Item("Id_Provincia") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("Id_Provincia"))
                End If
            End If
            If dr.Table.Columns.Contains("Provincia") Then
                If dr.Item("Provincia") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("Provincia").ToString.Trim
                End If
            End If
            If dr.Table.Columns.Contains("Letra") Then
                If dr.Item("Letra") IsNot DBNull.Value Then
                    entidad.Letra = dr.Item("Letra").ToString.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Provincia
End Namespace ' DataAccessLibrary