﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class gastos
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DgvGasto = New System.Windows.Forms.DataGridView()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtImporte = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtConcepto = New System.Windows.Forms.TextBox()
        Me.BtnCerrar = New System.Windows.Forms.Button()
        Me.LblUser = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCategoria = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMetodoPago = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtObservacion = New System.Windows.Forms.TextBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpGastos = New System.Windows.Forms.DateTimePicker()
        CType(Me.DgvGasto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DgvGasto
        '
        Me.DgvGasto.AllowUserToAddRows = False
        Me.DgvGasto.AllowUserToDeleteRows = False
        Me.DgvGasto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DgvGasto.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.DgvGasto.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenVertical
        Me.DgvGasto.ColumnHeadersHeight = 29
        Me.DgvGasto.Location = New System.Drawing.Point(-5, 232)
        Me.DgvGasto.Name = "DgvGasto"
        Me.DgvGasto.ReadOnly = True
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ButtonFace
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvGasto.RowHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvGasto.RowHeadersWidth = 51
        Me.DgvGasto.Size = New System.Drawing.Size(1061, 410)
        Me.DgvGasto.TabIndex = 13
        '
        'btnAgregar
        '
        Me.btnAgregar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAgregar.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregar.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAgregar.Location = New System.Drawing.Point(858, 115)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(147, 47)
        Me.btnAgregar.TabIndex = 18
        Me.btnAgregar.Text = "AGREGAR"
        Me.btnAgregar.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label4.Location = New System.Drawing.Point(446, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 24)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "IMPORTE"
        '
        'TxtImporte
        '
        Me.TxtImporte.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtImporte.Location = New System.Drawing.Point(554, 72)
        Me.TxtImporte.Name = "TxtImporte"
        Me.TxtImporte.Size = New System.Drawing.Size(105, 29)
        Me.TxtImporte.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label2.Location = New System.Drawing.Point(124, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 24)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "CONCEPTO"
        '
        'TxtConcepto
        '
        Me.TxtConcepto.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtConcepto.Location = New System.Drawing.Point(255, 72)
        Me.TxtConcepto.Name = "TxtConcepto"
        Me.TxtConcepto.Size = New System.Drawing.Size(176, 29)
        Me.TxtConcepto.TabIndex = 14
        '
        'BtnCerrar
        '
        Me.BtnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCerrar.ForeColor = System.Drawing.Color.Transparent
        Me.BtnCerrar.Location = New System.Drawing.Point(1008, 15)
        Me.BtnCerrar.Name = "BtnCerrar"
        Me.BtnCerrar.Size = New System.Drawing.Size(30, 32)
        Me.BtnCerrar.TabIndex = 19
        Me.BtnCerrar.Text = "X"
        Me.BtnCerrar.UseVisualStyleBackColor = True
        '
        'LblUser
        '
        Me.LblUser.CausesValidation = False
        Me.LblUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUser.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.LblUser.Location = New System.Drawing.Point(424, 15)
        Me.LblUser.Name = "LblUser"
        Me.LblUser.Size = New System.Drawing.Size(234, 32)
        Me.LblUser.TabIndex = 20
        Me.LblUser.Text = "GASTOS"
        Me.LblUser.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label1.Location = New System.Drawing.Point(675, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 24)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "CATEGORIA"
        '
        'txtCategoria
        '
        Me.txtCategoria.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoria.Location = New System.Drawing.Point(806, 72)
        Me.txtCategoria.Name = "txtCategoria"
        Me.txtCategoria.Size = New System.Drawing.Size(164, 29)
        Me.txtCategoria.TabIndex = 21
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label3.Location = New System.Drawing.Point(60, 134)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(198, 24)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "METODO DE PAGO"
        '
        'txtMetodoPago
        '
        Me.txtMetodoPago.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMetodoPago.Location = New System.Drawing.Point(257, 133)
        Me.txtMetodoPago.Name = "txtMetodoPago"
        Me.txtMetodoPago.Size = New System.Drawing.Size(159, 29)
        Me.txtMetodoPago.TabIndex = 23
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label5.Location = New System.Drawing.Point(515, 133)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(158, 24)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "OBSERVACION"
        '
        'txtObservacion
        '
        Me.txtObservacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservacion.Location = New System.Drawing.Point(679, 130)
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.Size = New System.Drawing.Size(159, 29)
        Me.txtObservacion.TabIndex = 25
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = Global.spos.My.Resources.Resources.pago
        Me.PictureBox6.Location = New System.Drawing.Point(382, 0)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(83, 56)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox6.TabIndex = 27
        Me.PictureBox6.TabStop = False
        Me.PictureBox6.Tag = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label6.Location = New System.Drawing.Point(29, 187)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(585, 24)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "SELECCIONA UNA FECHA PARA VISUALIZAR LOS GASTOS"
        '
        'dtpGastos
        '
        Me.dtpGastos.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpGastos.Location = New System.Drawing.Point(620, 183)
        Me.dtpGastos.MaxDate = New Date(2025, 6, 24, 23, 59, 59, 0)
        Me.dtpGastos.MinDate = New Date(2024, 1, 1, 0, 0, 0, 0)
        Me.dtpGastos.Name = "dtpGastos"
        Me.dtpGastos.Size = New System.Drawing.Size(385, 29)
        Me.dtpGastos.TabIndex = 29
        Me.dtpGastos.Value = New Date(2025, 6, 19, 0, 0, 0, 0)
        '
        'gastos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1059, 653)
        Me.Controls.Add(Me.dtpGastos)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtObservacion)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtMetodoPago)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCategoria)
        Me.Controls.Add(Me.LblUser)
        Me.Controls.Add(Me.BtnCerrar)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtImporte)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtConcepto)
        Me.Controls.Add(Me.DgvGasto)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "gastos"
        Me.Text = "gastos"
        CType(Me.DgvGasto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DgvGasto As DataGridView
    Friend WithEvents btnAgregar As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtImporte As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtConcepto As TextBox
    Friend WithEvents BtnCerrar As Button
    Friend WithEvents LblUser As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtCategoria As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtMetodoPago As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtObservacion As TextBox
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents Label6 As Label
    Friend WithEvents dtpGastos As DateTimePicker
End Class
