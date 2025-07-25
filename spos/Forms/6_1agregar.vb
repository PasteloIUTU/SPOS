﻿Imports System.Data.SQLite

Public Class agregar
    Dim tipo As String = "ALTA"
    Private Sub agregar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cargarDatos()
    End Sub

    ' Método para cargar los productos en el ComboBox
    Private Sub cargarDatos()
        cmbProducto.Items.Clear()

        Using connection As SQLiteConnection = DBConnection.GetConnection()
            Try
                connection.Open()
                Dim query As String = "SELECT nombre FROM PRODUCTOS"
                Dim command As New SQLiteCommand(query, connection)

                Using reader As SQLiteDataReader = command.ExecuteReader()
                    While reader.Read()
                        cmbProducto.Items.Add(reader("nombre").ToString())
                    End While
                End Using
            Catch ex As Exception
                MsgBox("Error al cargar los productos." & vbCrLf & ex.Message)
            End Try
        End Using
    End Sub

    ' Validación para aceptar solo números en el campo de cantidad
    Private Sub txtCantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtObservacion.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    ' Método para agregar los productos
    Public Sub agregar()
        If txtObservacion.Text <> "" AndAlso CInt(txtObservacion.Text) > 0 Then
            If cmbProducto.SelectedIndex <> -1 Then
                Dim producto As String = cmbProducto.SelectedItem.ToString()
                Dim cantidad As Integer = CInt(txtObservacion.Text)
                Dim existencias As Integer = obtenerExistencias(producto)

                If existencias >= 0 Then
                    Dim nuevasExistencias As Integer = existencias + cantidad
                    actualizarExistencias(producto, nuevasExistencias)
                    registrarHistorial(tipo, producto, cantidad, userid, txtObservacion.Text.Trim())
                    MsgBox("Producto: " & producto & vbCrLf & "Cantidad agregada: " & cantidad & vbCrLf & "Existencias actuales: " & nuevasExistencias, vbInformation, "EXISTENCIAS ACTUALIZADAS")
                End If
            Else
                MsgBox("Por favor, selecciona un producto.")
            End If
        Else
            MsgBox("Por favor, ingresa una cantidad válida.")
        End If
    End Sub

    ' Función para obtener las existencias actuales de un producto
    Private Function obtenerExistencias(producto As String) As Integer
        Dim existencias As Integer = -1

        Using connection As SQLiteConnection = DBConnection.GetConnection()
            Try
                connection.Open()
                Dim query As String = "SELECT existencia FROM PRODUCTOS WHERE nombre = @producto"
                Dim command As New SQLiteCommand(query, connection)
                command.Parameters.AddWithValue("@producto", producto)

                Using reader As SQLiteDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        If Not IsDBNull(reader("existencia")) Then
                            existencias = Convert.ToInt32(reader("existencia"))
                        End If
                    End If
                End Using
            Catch ex As Exception
                MsgBox("Error al obtener las existencias: " & ex.Message)
            End Try
        End Using

        Return existencias
    End Function

    ' Función para actualizar las existencias de un producto
    Private Sub actualizarExistencias(producto As String, nuevasExistencias As Integer)
        Using connection As SQLiteConnection = DBConnection.GetConnection()
            Try
                connection.Open()
                Dim query As String = "UPDATE PRODUCTOS SET existencia = @nuevasExistencias WHERE nombre = @producto"
                Dim command As New SQLiteCommand(query, connection)
                command.Parameters.AddWithValue("@nuevasExistencias", nuevasExistencias)
                command.Parameters.AddWithValue("@producto", producto)
                command.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("Error al actualizar las existencias: " & ex.Message)
            End Try
        End Using
    End Sub




    Private Sub registrarHistorial(tipo As String, nombreProducto As String, cantidad As Integer, idVendedor As Integer, observacion As String)
        Using connection As SQLiteConnection = DBConnection.GetConnection()
            Try
                connection.Open()

                ' Obtener ID del producto
                Dim queryId As String = "SELECT id FROM PRODUCTOS WHERE nombre = @nombre"
                Dim cmdId As New SQLiteCommand(queryId, connection)
                cmdId.Parameters.AddWithValue("@nombre", nombreProducto)

                Dim idProducto As Integer = -1
                Using reader = cmdId.ExecuteReader()
                    If reader.Read() Then
                        idProducto = Convert.ToInt32(reader("id"))
                    End If
                End Using

                If idProducto = -1 Then
                    MsgBox("No se encontró el producto en la base de datos.")
                    Return
                End If

                ' Insertar en historial
                Dim currentDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
                Dim insertQuery As String = "INSERT INTO HISTORIAL (ID_PRODUCTO,
A_B, CANTIDAD, ID_VENDEDOR, OBSERVACIONES, FECHA) 
VALUES (@id_Producto, @tipo, @cantidad, @idVendedor, @observaciones, @fecha)"
                Dim cmdInsert As New SQLiteCommand(insertQuery, connection)
                cmdInsert.Parameters.AddWithValue("@id_Producto", idProducto)
                cmdInsert.Parameters.AddWithValue("@tipo", tipo)
                cmdInsert.Parameters.AddWithValue("@cantidad", cantidad)
                cmdInsert.Parameters.AddWithValue("@idVendedor", userid)
                cmdInsert.Parameters.AddWithValue("@observaciones", observacion)
                cmdInsert.Parameters.AddWithValue("@fecha", currentDate)
                cmdInsert.ExecuteNonQuery()

            Catch ex As Exception
                MsgBox("Error al registrar historial: " & ex.Message)
            End Try
        End Using
    End Sub
    ' Método para sumar la cantidad cuando se presiona el botón "+"
    Public Function sumar(valor As String) As Integer
        Dim cantidad As Integer
        If String.IsNullOrEmpty(valor) OrElse valor = "0" Then
            cantidad = 1
        Else
            cantidad = CInt(valor)
            cantidad += 1
        End If
        Return cantidad
    End Function

    ' Método para restar la cantidad cuando se presiona el botón "-"
    Public Function restar(valor As String) As Integer
        Dim cantidad As Integer
        If Not String.IsNullOrEmpty(valor) AndAlso valor <> "0" Then
            cantidad = CInt(valor)
            If cantidad > 1 Then
                cantidad -= 1
            End If
        Else
            cantidad = 1
        End If
        Return cantidad
    End Function

    ' Eventos de los botones de suma y resta de cantidad
    Private Sub btnMas_Click(sender As Object, e As EventArgs) Handles btnMas.Click
        Dim mas = sumar(txtObservacion.Text)
        txtObservacion.Text = mas.ToString()
    End Sub

    Private Sub btnMenos_Click(sender As Object, e As EventArgs) Handles btnMenos.Click
        Dim menos = restar(txtObservacion.Text)
        txtObservacion.Text = menos.ToString()
    End Sub

    ' Evento del botón "Agregar"
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        agregar()
        cmbProducto.Text = ""
        txtObservacion.Text = ""
        txtObservacion.Text = ""
    End Sub

    ' Evento del botón "Cerrar sesión"
    Private Sub BtnSesion_Click(sender As Object, e As EventArgs) Handles BtnSesion.Click
        principal.Show()
        Me.Close()
    End Sub
End Class

