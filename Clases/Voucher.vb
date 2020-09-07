Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports LUM
Imports Clases.Entidad
Imports Connection
Imports LUM.DTO

Namespace Entidad
    Public Class Voucher
        'Inherits DBE
#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdTitular() As Integer = 0
        Public Property IdFamiliar() As Integer = 0
        Public Property Codigo() As String = ""
        Public Property Confirmado() As Integer = 0
        Public Property CodigoPostal() As Integer = 0
        Public Property IdLocalidad() As Integer = 0
        Public Property Domicilio() As String = ""
        Public Property Fecha() As Date?
        Public Property CorreoElectronico() As String = ""
        Public Property Telefono() As Long = 0
#End Region
#Region " Lazy Load "
        Public ReadOnly Property LngFecha() As Long
            Get
                Dim result As Long = 0
                If Fecha.HasValue Then
                    result = CLng(Year(Fecha.Value).ToString & Right("00" & Month(Fecha.Value).ToString, 2) & Right("00" & Day(Fecha.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Voucher = TraerUno(id)
            ' DBE
            'IdUsuarioAlta = objImportar.IdUsuarioAlta
            'IdUsuarioBaja = objImportar.IdUsuarioBaja
            'IdUsuarioModifica = objImportar.IdUsuarioModifica
            'IdMotivoBaja = objImportar.IdMotivoBaja
            'FechaAlta = objImportar.FechaAlta
            'FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Codigo = objImportar.Codigo
            Confirmado = objImportar.Confirmado
            Fecha = objImportar.Fecha
            IdTitular = objImportar.IdTitular
            IdFamiliar = objImportar.IdFamiliar
            CodigoPostal = objImportar.CodigoPostal
            IdLocalidad = objImportar.IdLocalidad
            Domicilio = objImportar.Domicilio
            CorreoElectronico = objImportar.CorreoElectronico
            Telefono = objImportar.Telefono
        End Sub
        Sub New(ByVal objDesdeDTOVoucher As DTO.DTO_Voucher)
            ' DBE
            'IdUsuarioBaja = objDesdeDTOVoucher.IdUsuarioBaja
            'IdUsuarioModifica = objDesdeDTOVoucher.IdUsuarioModifica
            'IdMotivoBaja = objDesdeDTOVoucher.IdMotivoBaja
            ' Entidad
            IdEntidad = objDesdeDTOVoucher.IdEntidad
            Codigo = objDesdeDTOVoucher.Codigo
            Confirmado = objDesdeDTOVoucher.Confirmado
            Fecha = LUM.LUM.Fecha_LngToDate(objDesdeDTOVoucher.Fecha)
            IdTitular = objDesdeDTOVoucher.IdTitular
            IdFamiliar = objDesdeDTOVoucher.IdFamiliar
            CodigoPostal = objDesdeDTOVoucher.CodigoPostal
            IdLocalidad = objDesdeDTOVoucher.IdLocalidad
            Domicilio = objDesdeDTOVoucher.Domicilio
            CorreoElectronico = objDesdeDTOVoucher.CorreoElectronico
            Telefono = objDesdeDTOVoucher.Telefono
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        'Public Shared Function TraerUno(ByVal Id As Integer) As Voucher
        '    Dim result As Voucher = Todos.Find(Function(x) x.IdEntidad = Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Voucher)
        '    Return Todos
        'End Function
        Public Shared Function TraerUno(ByVal Id As Integer) As Voucher
            Dim result As Voucher = DAL_Voucher.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("Voucher Generado Con Anterioridad para ese Familiar")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosPorTitular(idafiliado As Integer) As List(Of DTO.DTO_Voucher)
            Dim L As List(Of Voucher) = DAL_Voucher.TraerTodosPorTitular(idafiliado)
            Dim result As New List(Of DTO.DTO_Voucher)
            For Each item As Voucher In L
                result.Add(item.ToDTO)
            Next
            'If result Is Nothing Or result.Count = 0 Then
            '    Throw New Exception("No existen vouchers para el grupo")
            'End If
            Return result
        End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Voucher.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Voucher.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Voucher.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Voucher
            Dim result As New DTO.DTO_Voucher
            result.IdEntidad = IdEntidad
            result.IdFamiliar = IdFamiliar
            result.IdTitular = IdTitular
            result.Confirmado = Confirmado
            result.Codigo = Codigo
            result.Fecha = LngFecha
            'result.CodigoPostal = CodigoPostal
            'result.IdLocalidad = IdLocalidad
            'result.Domicilio = Domicilio
            Return result
        End Function
        'Public Shared Sub refresh()
        '    _Todos = DAL_Voucher.TraerTodos
        'End Sub
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
            'Dim cantidad As Integer = DAL_Voucher.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Voucher
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Voucher
        'Inherits DTO_DBE

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdTitular() As Integer = 0
        Public Property IdFamiliar() As Integer = 0
        Public Property Codigo() As String = ""
        Public Property Confirmado() As Integer = 0
        Public Property Fecha() As Long = 0
        Public Property CodigoPostal() As Integer = 0
        Public Property IdLocalidad() As Integer = 0
        Public Property Domicilio() As String = ""
        Public Property CorreoElectronico() As String = ""
        Public Property Telefono() As Long = 0
#End Region
    End Class ' DTO_Voucher
End Namespace ' DTO



Namespace DataAccessLibrary
    Public Class DAL_Voucher

#Region " Stored "
        Const storeAlta As String = "p_VoucherDiaDelNino2020_Alta"
        Const storeBaja As String = "p_VoucherDiaDelNino2020_Baja"
        Const storeModifica As String = "p_Voucher_Modifica"
        Const storeTraerUnoXId As String = "p_VoucherDiaDelNino2020_TraerUnoXId"
        Const storeTraerTodosxTitular As String = "p_VoucherDiaDelNino2020_TraerTodosxTitular"
        Const storeTraerTodosActivos As String = "p_Voucher_TraerTodosActivos"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Voucher)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idafiliado", entidad.IdTitular)
            pa.add("@idfamiliar", entidad.IdFamiliar)
            pa.add("@domicilio", entidad.Domicilio)
            pa.add("@cp", entidad.CodigoPostal)
            pa.add("@idlocalidad", entidad.IdLocalidad)
            pa.add("@CorreoElectronico", entidad.CorreoElectronico)
            pa.add("@Telefono", entidad.Telefono)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_CABA")
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Voucher)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@id", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa, "strConn_CABA")
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Voucher)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            'pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa, "strConn_CABA")
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Voucher
            Dim store As String = storeTraerUnoXId
            Dim result As New Voucher
            Dim pa As New parametrosArray
            pa.add("@id", id)
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
        Public Shared Function TraerTodosPorTitular(IdAfiliado As Integer) As List(Of Voucher)
            Dim store As String = storeTraerTodosxTitular
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Voucher)
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
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Voucher
            Dim entidad As New Voucher
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
            If dr.Table.Columns.Contains("id_voucher") Then
                If dr.Item("id_voucher") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id_voucher"))
                End If
            End If
            If dr.Table.Columns.Contains("id_afiliado") Then
                If dr.Item("id_afiliado") IsNot DBNull.Value Then
                    entidad.IdTitular = CInt(dr.Item("id_afiliado"))
                End If
            End If
            If dr.Table.Columns.Contains("id_familiar") Then
                If dr.Item("id_familiar") IsNot DBNull.Value Then
                    entidad.IdFamiliar = CInt(dr.Item("id_familiar"))
                End If
            End If
            If dr.Table.Columns.Contains("codigo") Then
                If dr.Item("codigo") IsNot DBNull.Value Then
                    entidad.Codigo = dr.Item("codigo").ToString.Trim.ToUpper
                End If
            End If
            If dr.Table.Columns.Contains("confirmado") Then
                If dr.Item("confirmado") IsNot DBNull.Value Then
                    entidad.Confirmado = CInt(dr.Item("confirmado"))
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
    End Class ' DAL_Voucher
End Namespace ' DataAccessLibrary