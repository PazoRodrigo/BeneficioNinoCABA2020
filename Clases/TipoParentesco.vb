
Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports LUM
Imports Clases.Entidad
Imports Connection
Imports LUM.DTO
Imports System.Configuration
Imports System.Net.Mail

Namespace Entidad
    Public Class TipoParentesco
        'Inherits DBE
#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
#End Region
#Region " Lazy Load "

#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            'Dim lista As List(Of TipoParentesco =
            'Dim objImportar As TipoParentesco = TraerUno(id)
            '' DBE
            ''IdUsuarioAlta = objImportar.IdUsuarioAlta
            ''IdUsuarioBaja = objImportar.IdUsuarioBaja
            ''IdUsuarioModifica = objImportar.IdUsuarioModifica
            ''IdMotivoBaja = objImportar.IdMotivoBaja
            ''FechaAlta = objImportar.FechaAlta
            ''FechaBaja = objImportar.FechaBaja
            '' Entidad
            'IdEntidad = objImportar.IdEntidad
            'Nombre = objImportar.Nombre
        End Sub

        Sub New(ByVal objDesdeDTOTipoParentesco As DTO.DTO_TipoParentesco)
            ' Entidad
            IdEntidad = objDesdeDTOTipoParentesco.IdEntidad
            Nombre = objDesdeDTOTipoParentesco.Nombre
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        'Public Shared Function TraerUno(ByVal Id As Integer) As TipoParentesco
        '    Dim result As TipoParentesco = Todos.Find(Function(x) x.IdEntidad = Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of TipoParentesco)
        '    Return Todos
        'End Function


        Public Shared Function TraerTodos() As List(Of DTO.DTO_TipoParentesco)
            Dim L As List(Of TipoParentesco) = DAL_TipoParentesco.TraerTodos()
            Dim result As New List(Of DTO.DTO_TipoParentesco)
            For Each item As TipoParentesco In L
                result.Add(item.ToDTO)
            Next
            Return result
        End Function
        Public Shared Sub EnviarEMail(IdAfiliado As Integer)
            Try
                'Dim Lista As List(Of TipoParentesco) = DAL_TipoParentesco.TraerTodosPorTitular(IdAfiliado)
                'Dim ObjTitular As New Titular(IdAfiliado)
                'If Lista.Count > 0 Then
                '    Dim str As String = "Gracias por participar del evento del Día de la Niñez con UTEDyC Seccional Capital Federal" & vbCrLf
                '    str += "Te enviamos los comprobantes de participación que seleccionaste desde nuestra plataforma." & vbCrLf
                '    str += "No es necesario que los imprimas." & vbCrLf & vbCrLf & vbCrLf
                '    str += ObjTitular.ApellidoNombre & ". DNI: " & ObjTitular.NroDocumento & vbCrLf & vbCrLf
                '    Dim i As Integer = 0
                '    While i <= Lista.Count - 1
                '        Dim item As New Familiar(Lista(i).IdFamiliar)
                '        str += "Beneficiario " & i + 1 & ": " & item.ApellidoNombre & ". - " & Lista(i).Codigo.ToString & vbCrLf
                '        i += 1
                '    End While
                '    Dim DireccionEnvio As String = Lista(0).CorreoElectronico
                '    'Dim DireccionEnvio As String = "pazo.rodrigo@gmail.com"
                '    Dim DesdeCuenta As String = ConfigurationManager.AppSettings("smtpFrom").ToString
                '    Dim DesdePass As String = ConfigurationManager.AppSettings("smtpPassword").ToString
                '    Using Mail As New MailMessage()
                '        str += vbCrLf & vbCrLf & "Atentamente." & vbCrLf & "Equipo de Sistemas UTEDyC Capital."
                '        Dim Smtp = New SmtpClient
                '        Mail.From = New MailAddress(DesdeCuenta)
                '        Mail.To.Add(New MailAddress(DireccionEnvio))
                '        Mail.Subject = "TipoParentesco TipoParentesco Día de la Niñez !!!"
                '        Mail.Body = str
                '        Mail.IsBodyHtml = False
                '        Mail.Priority = MailPriority.Normal
                '        Smtp.Host = ConfigurationManager.AppSettings("smtpHost").ToString
                '        Smtp.Port = 587
                '        Smtp.UseDefaultCredentials = False
                '        Smtp.Credentials = New System.Net.NetworkCredential(DesdeCuenta, DesdePass)
                '        Smtp.EnableSsl = True
                '        Smtp.Send(Mail)
                '    End Using
                'End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_TipoParentesco.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_TipoParentesco.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_TipoParentesco.Modifica(Me)
        End Sub


        ' Otros
        Public Function ToDTO() As DTO.DTO_TipoParentesco
            Dim result As New DTO.DTO_TipoParentesco
            result.IdEntidad = IdEntidad
            result.Nombre = Nombre
            Return result
        End Function
        'Public Shared Sub refresh()
        '    _Todos = DAL_TipoParentesco.TraerTodos
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
            'Dim cantidad As Integer = DAL_TipoParentesco.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' TipoParentesco
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_TipoParentesco
        'Inherits DTO_DBE

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
#End Region
    End Class ' DTO_TipoParentesco
End Namespace ' DTO



Namespace DataAccessLibrary
    Public Class DAL_TipoParentesco

#Region " Stored "
        Const storeAlta As String = ""
        Const storeBaja As String = ""
        Const storeModifica As String = ""
        Const storeTraerUnoXId As String = ""
        Const storeTraerTodos As String = "[DIM].[usp_Parentesco_TraerTodos]"
        Const storeTraerTodosActivos As String = ""
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As TipoParentesco)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            'pa.add("@idafiliado", entidad.IdTitular)
            'pa.add("@idfamiliar", entidad.IdFamiliar)
            'pa.add("@domicilio", entidad.Domicilio.ToUpper.Trim)
            'pa.add("@cp", entidad.CodigoPostal)
            'pa.add("@idlocalidad", entidad.IdLocalidad)
            'pa.add("@CorreoElectronico", entidad.CorreoElectronico.ToUpper.Trim)
            'pa.add("@Telefono", entidad.Telefono)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa, "strConn_CABA")
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As TipoParentesco)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@id", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa, "strConn_CABA")
        End Sub
        Public Shared Sub Modifica(ByVal entidad As TipoParentesco)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            'pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa, "strConn_CABA")
        End Sub
        ' Traer

        Public Shared Function TraerTodos() As List(Of TipoParentesco)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of TipoParentesco)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As TipoParentesco
            Dim entidad As New TipoParentesco
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
            '/*
            '        Public Property IdEntidad() As Integer = 0
            'Public Property Periodo() As Integer = 0
            'Public Property IdRepresentado() As Integer = 0
            'Public Property NroDocumento() As Integer = 0
            'Public Property IdTipoTipoParentesco() As Integer = 0
            'Public Property FechaEntrega() As Date?
            'Public Property IdSeccional() As Long = 0

            '*/
            ' Entidad
            If dr.Table.Columns.Contains("Id_Parentesco") Then
                If dr.Item("Id_Parentesco") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("Id_Parentesco"))
                End If
            End If
            If dr.Table.Columns.Contains("Parentesco") Then
                If dr.Item("Parentesco") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("Parentesco").ToString.Trim.ToUpper
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_TipoParentesco
End Namespace ' DataAccessLibrary