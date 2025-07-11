﻿Imports System.Data.SQLite
Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class VentaN
    Private Subtotal As Decimal = 0
    Private ProductosReservados As New Dictionary(Of String, Integer)

    Private Sub VentasForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Forzar cultura a México para formato de moneda en pesos
        Thread.CurrentThread.CurrentCulture = New CultureInfo("es-MX")

        CargarProductos()
    End Sub

    Private Function ObtenerUltimoIDVenta() As Integer
        Using conn As SQLiteConnection = DBConnection.GetConnection()
            conn.Open()
            Dim cmd As New SQLiteCommand("SELECT MAX(id) FROM ventas", conn)
            Dim resultado = cmd.ExecuteScalar()
            If resultado IsNot Nothing AndAlso Not IsDBNull(resultado) Then
                Return Convert.ToInt32(resultado)
            Else
                Return 0 ' En caso de que no haya ventas aún
            End If
        End Using
    End Function


    Private Sub CargarProductos()
        Dim query As String = "SELECT nombre FROM PRODUCTOS"

        Using connection As SQLiteConnection = DBConnection.GetConnection()
            Using command As New SQLiteCommand(query, connection)
                connection.Open()
                Using reader As SQLiteDataReader = command.ExecuteReader()
                    While reader.Read()
                        CmbProducto.Items.Add(reader("nombre").ToString())
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click

        Dim producto As String = CmbProducto.Text
        Dim piezas As Integer
        Dim precio As Decimal
        Dim existencias As Integer

        If String.IsNullOrEmpty(producto) Then
            MsgBox("Seleccione un producto.", vbExclamation)
            Return
        End If

        If Not Integer.TryParse(TxtPZ.Text, piezas) OrElse piezas <= 0 Then
            MsgBox("Ingrese una cantidad válida.", vbExclamation)
            Return
        End If

        ' Obtener el precio y existencias del producto seleccionado
        Using connection As SQLiteConnection = DBConnection.GetConnection()
            Dim query As String = "SELECT precio, existencia FROM productos WHERE nombre = @nombre"
            Using command As New SQLiteCommand(query, connection)
                command.Parameters.AddWithValue("@nombre", producto)
                connection.Open()
                Using reader As SQLiteDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        precio = Convert.ToDecimal(reader("precio"))
                        existencias = Convert.ToInt32(reader("existencia"))
                    Else
                        MsgBox("El producto no existe.", vbExclamation)
                        Return
                    End If
                End Using
            End Using
        End Using

        Dim existenciasReservadas = If(ProductosReservados.ContainsKey(producto), ProductosReservados(producto), 0)
        If piezas > existencias - existenciasReservadas Then
            MsgBox("No hay suficientes existencias.", vbExclamation)
            Return
        End If

        If ProductosReservados.ContainsKey(producto) Then
            ProductosReservados(producto) += piezas
        Else
            ProductosReservados(producto) = piezas
        End If

        ' Actualizar el DataGridView, verificando si el producto ya está en la lista.
        Dim found As Boolean = False
        For Each row As DataGridViewRow In DgvProductos.Rows
            If row.IsNewRow Then Continue For

            ' Si el producto ya está en el DataGridView, actualiza la cantidad
            If row.Cells(0).Value.ToString() = producto Then
                Dim cantidadActual As Integer = Convert.ToInt32(row.Cells(1).Value)
                row.Cells(1).Value = cantidadActual + piezas
                row.Cells(3).Value = "$" & (cantidadActual + piezas) * precio
                found = True
                Exit For
            End If
        Next

        ' Si el producto no existe en el DataGridView, agregarlo como una nueva fila
        If Not found Then
            Dim total As Decimal = piezas * precio
            DgvProductos.Rows.Add(producto, piezas, precio.ToString("C2"), total.ToString("C2"))
        End If

        ' Actualizar el subtotal
        Subtotal += piezas * precio
        TxtTotal.Text = Subtotal.ToString("C2")
    End Sub

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        If DgvProductos.SelectedRows.Count > 0 Then
            For Each row As DataGridViewRow In DgvProductos.SelectedRows
                If Not row.IsNewRow Then
                    Dim producto As String = row.Cells(0).Value.ToString()
                    Dim cantidad As Integer = Convert.ToInt32(row.Cells(1).Value)

                    If ProductosReservados.ContainsKey(producto) Then
                        ProductosReservados(producto) -= cantidad
                        If ProductosReservados(producto) <= 0 Then
                            ProductosReservados.Remove(producto)
                        End If
                    End If

                    ' Quitar símbolo moneda, espacios y formato para decimal
                    Dim totalStr = row.Cells(3).Value.ToString().Replace("$", "").Replace(" ", "").Replace("€", "").Trim()
                    Dim total As Decimal = 0
                    Decimal.TryParse(totalStr, total)

                    Subtotal -= total
                    DgvProductos.Rows.Remove(row)

                End If
            Next
            TxtTotal.Text = Subtotal.ToString("C2")
        Else
            MsgBox("Seleccione un producto para eliminar.", vbExclamation)
        End If
    End Sub

    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click
        Me.Close()
    End Sub

    Private Sub BtnFinalizar_Click(sender As Object, e As EventArgs) Handles BtnFinalizar.Click
        session.userid = 0
        If DgvProductos.Rows.Count = 0 Then
            MsgBox("No hay productos en el carrito.", vbExclamation)
            Return
        End If

        Dim confirmacionVenta = MsgBox("¿Está seguro de terminar la venta? No se podrá revertir.", vbYesNo + vbQuestion, "Finalizar Venta")
        If confirmacionVenta = vbYes Then
            Dim facturar = MsgBox("¿Se desea facturar? Esta Acción es Irreversible", vbYesNo + vbExclamation, "Facturación")
            Dim facturacionFlag As Integer = If(facturar = vbYes, 1, 0)
            RegistrarVenta(facturacionFlag, session.userid)


            If facturacionFlag = 1 Then
                ' Obtener ID de la venta recién registrada
                Dim ultimoID As Integer = ObtenerUltimoIDVenta()
                Dim frmFact As New Facturacion()
                frmFact.IDVenta = ultimoID
                frmFact.ShowDialog()
            ElseIf facturacionFlag = 0 Then
                MsgBox("Venta finalizada exitosamente.", vbInformation)
            End If
            LimpiarFormulario()
            ProductosReservados.Clear()
            Subtotal = 0
        End If
    End Sub



    Private Sub RegistrarVenta(facturacion As Integer, idCliente As Integer)
        Using connection As SQLiteConnection = DBConnection.GetConnection()
            connection.Open()

            Dim transaction As SQLiteTransaction = connection.BeginTransaction()

            Try
                Dim insertVenta As String = "INSERT INTO VENTAS (fecha, total, total_productos, facturacion, vendedor_id, cliente_id) 
VALUES (@fecha, @total, @total_productos, @facturacion, @vendedor_id, @cliente_id)"
                Using cmdVenta As New SQLiteCommand(insertVenta, connection)
                    cmdVenta.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    cmdVenta.Parameters.AddWithValue("@total", Subtotal)
                    ' Contar filas reales (no nueva) del DataGridView para total_productos
                    Dim totalProductos As Integer = DgvProductos.Rows.Cast(Of DataGridViewRow)().Count(Function(r) Not r.IsNewRow)
                    cmdVenta.Parameters.AddWithValue("@total_productos", totalProductos)
                    cmdVenta.Parameters.AddWithValue("@facturacion", facturacion)
                    cmdVenta.Parameters.AddWithValue("@vendedor_id", session.userid)
                    cmdVenta.Parameters.AddWithValue("@cliente_id", idCliente)
                    cmdVenta.ExecuteNonQuery()
                End Using

                Dim ventaId As Long
                Using cmdId As New SQLiteCommand("SELECT last_insert_rowid()", connection)
                    ventaId = CLng(cmdId.ExecuteScalar())
                End Using

                For Each row As DataGridViewRow In DgvProductos.Rows
                    If row.IsNewRow Then Continue For

                    Dim nombreProducto As String = row.Cells(0).Value.ToString()
                    Dim cantidad As Integer = Convert.ToInt32(row.Cells(1).Value)
                    Dim precioUnitarioStr As String = row.Cells(2).Value.ToString().Replace("$", "").Replace(" ", "").Replace("€", "").Trim()
                    Dim importeStr As String = row.Cells(3).Value.ToString().Replace("$", "").Replace(" ", "").Replace("€", "").Trim()

                    Dim precioUnitario As Decimal = 0
                    Decimal.TryParse(precioUnitarioStr, precioUnitario)
                    Dim importe As Decimal = 0
                    Decimal.TryParse(importeStr, importe)

                    Dim productoId As Integer
                    Using cmdProd As New SQLiteCommand("SELECT id FROM PRODUCTOS WHERE nombre = @nombre", connection)
                        cmdProd.Parameters.AddWithValue("@nombre", nombreProducto)
                        productoId = Convert.ToInt32(cmdProd.ExecuteScalar())
                    End Using

                    Dim insertDetalle As String = "INSERT INTO DVENTA (venta_id, producto_id, precio_unitario, cantidad, importe) 
VALUES (@venta_id, @producto_id, @precio_unitario, @cantidad, @importe)"
                    Using cmdDet As New SQLiteCommand(insertDetalle, connection)
                        cmdDet.Parameters.AddWithValue("@venta_id", ventaId)
                        cmdDet.Parameters.AddWithValue("@producto_id", productoId)
                        cmdDet.Parameters.AddWithValue("@precio_unitario", precioUnitario)
                        cmdDet.Parameters.AddWithValue("@cantidad", cantidad)
                        cmdDet.Parameters.AddWithValue("@importe", importe)
                        cmdDet.ExecuteNonQuery()
                    End Using

                    Dim updateExistencia As String = "UPDATE productos SET existencia = existencia - @cantidad WHERE id = @id"
                    Using cmdUpd As New SQLiteCommand(updateExistencia, connection)
                        cmdUpd.Parameters.AddWithValue("@cantidad", cantidad)
                        cmdUpd.Parameters.AddWithValue("@id", productoId)
                        cmdUpd.ExecuteNonQuery()
                    End Using
                Next

                transaction.Commit()
                GenerarTicketPDF(ventaId)
            Catch ex As Exception
                transaction.Rollback()
                MsgBox("Error al registrar la venta: " & ex.Message, vbCritical)
            End Try
        End Using
    End Sub

    Private Sub LimpiarFormulario()
        CmbProducto.SelectedIndex = -1
        TxtPZ.Clear()
        DgvProductos.Rows.Clear()
        Subtotal = 0
        TxtTotal.Text = "$0.00"
    End Sub

    Private Sub BtnRegresar_Click(sender As Object, e As EventArgs) Handles BtnRegresar.Click
        principal.Show()
        Me.Close()
    End Sub


    Private Function ConvertirNumeroALetra(ByVal numero As Decimal) As String
        Dim culture = New CultureInfo("es-MX")
        Dim texto = StrConv(numero.ToString("C", culture), VbStrConv.Uppercase)
        Return texto & " M.N."
    End Function

    Private Sub GenerarTicketPDF(ventaId As Long)
        Try
            ' Obtener datos del emisor desde la tabla FACTURACION_EMISORES
            Dim emisor As DataRow = Nothing
            Using conn As SQLiteConnection = DBConnection.GetConnection()
                conn.Open()
                Dim da As New SQLiteDataAdapter("SELECT * FROM FACTURACION_EMISORES LIMIT 1", conn)
                Dim dt As New DataTable()
                da.Fill(dt)
                If dt.Rows.Count = 0 Then
                    MsgBox("No hay configuración de emisor registrada.", vbExclamation)
                    Exit Sub
                End If
                emisor = dt.Rows(0)
            End Using

            ' Ruta del PDF a generar en C:\Tickets
            Dim rutaCarpeta As String = "C:\Tickets"
            If Not Directory.Exists(rutaCarpeta) Then
                Directory.CreateDirectory(rutaCarpeta)
            End If
            Dim rutaPDF As String = Path.Combine(rutaCarpeta, $"ticket_venta_{ventaId}.pdf")

            Dim doc As New Document(PageSize.A4, 30, 30, 20, 30)
            PdfWriter.GetInstance(doc, New FileStream(rutaPDF, FileMode.Create))
            doc.Open()

            ' Encabezado: Logo + Datos del negocio
            Dim tableEncabezado As New PdfPTable(2)
            tableEncabezado.WidthPercentage = 100
            tableEncabezado.SetWidths({30, 70})

            ' Logo desde la carpeta del proyecto
            Dim logoPath As String = Path.Combine(Application.StartupPath, "Resources\Logo.jpg")
            If Not File.Exists(logoPath) Then
                MsgBox("Logo no encontrado en: " & logoPath)
            Else
                MsgBox("Logo cargado correctamente.")
            End If
            If File.Exists(logoPath) Then
                Dim logo = Image.GetInstance(logoPath)
                logo.ScaleAbsolute(60, 60)
                Dim cellLogo As New PdfPCell(logo)
                cellLogo.Border = Rectangle.NO_BORDER
                tableEncabezado.AddCell(cellLogo)
            Else
                tableEncabezado.AddCell(New PdfPCell(New Phrase("")) With {.Border = Rectangle.NO_BORDER})
            End If

            ' Datos del negocio
            Dim datos = $"{emisor("NOMBRE")}" & vbCrLf &
                $"{emisor("CALLE")} {emisor("NOEXTERIOR")}" & vbCrLf &
                $"{emisor("COLONIA")}, {emisor("MUNICIPIO")}" & vbCrLf &
                $"{emisor("ESTADO")}, CP {emisor("CODIGOPOSTAL")}" & vbCrLf &
                $"RFC: {emisor("RFC")}" & vbCrLf &
                $"Email: {emisor("EMAIL")}"
            Dim cellInfo As New PdfPCell(New Phrase(datos, FontFactory.GetFont(FontFactory.HELVETICA, 10)))
            cellInfo.Border = Rectangle.NO_BORDER
            tableEncabezado.AddCell(cellInfo)
            doc.Add(tableEncabezado)

            ' Título
            doc.Add(New Paragraph("NOTA DE VENTA", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14)))
            doc.Add(New Paragraph("Fecha: " & Now.ToString("dd/MM/yyyy HH:mm:ss")))
            doc.Add(New Paragraph(" "))

            ' Tabla de productos
            Dim tablaProductos As New PdfPTable(5)
            tablaProductos.WidthPercentage = 100
            tablaProductos.SetWidths({10, 10, 40, 20, 20})

            Dim bold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9)
            tablaProductos.AddCell(New Phrase("CANT", bold))
            tablaProductos.AddCell(New Phrase("UNI", bold))
            tablaProductos.AddCell(New Phrase("DESCRIPCIÓN", bold))
            tablaProductos.AddCell(New Phrase("P. UNIT", bold))
            tablaProductos.AddCell(New Phrase("IMPORTE", bold))

            Dim total As Decimal = 0

            ' Leer productos de la venta
            Using conn As SQLiteConnection = DBConnection.GetConnection()
                conn.Open()
                Dim cmd As New SQLiteCommand("
                SELECT P.nombre, D.cantidad, D.precio_unitario, D.importe
                FROM DVENTA D
                JOIN PRODUCTOS P ON P.id = D.producto_id
                WHERE D.venta_id = @ventaId", conn)
                cmd.Parameters.AddWithValue("@ventaId", ventaId)

                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        tablaProductos.AddCell(reader("cantidad").ToString())
                        tablaProductos.AddCell("PZA")
                        tablaProductos.AddCell(reader("nombre").ToString())
                        tablaProductos.AddCell(FormatCurrency(reader("precio_unitario")))
                        tablaProductos.AddCell(FormatCurrency(reader("importe")))

                        total += Convert.ToDecimal(reader("importe"))
                    End While
                End Using
            End Using

            doc.Add(tablaProductos)

            doc.Add(New Paragraph(" "))
            doc.Add(New Paragraph("CANTIDAD EN LETRA: " & ConvertirNumeroALetra(total)))
            doc.Add(New Paragraph("TOTAL: " & total.ToString("C2"), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11)))

            doc.Add(New Paragraph(" "))
            doc.Add(New Paragraph(emisor("LEYENDATICKET").ToString(), FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 9)))
            doc.Close()

            MsgBox("Ticket generado: " & rutaPDF, vbInformation)
        Catch ex As Exception
            MsgBox("Error al generar ticket: " & ex.Message, vbCritical)
        End Try
    End Sub

End Class