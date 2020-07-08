Imports System.ComponentModel

Public Class producto
    Private dtproducto As New dsTiendaProducto.productoDataTable
    Private taproducto As New dsTiendaProductoTableAdapters.productoTableAdapter
    Private campollave As String
    Private editar As Boolean = False
    Private numeroRegistro As String
    Private registro As dsTiendaProducto.productoRow
    Private Sub producto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.dtproducto = Me.taproducto.GetData
        dtgProducto.DataSource = Me.dtgProducto
    End Sub

    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click

        If Me.editar Then
            Me.registro = dtproducto.FindByidProducto(campollave)
            Me.registro.nombreProducto = txtNombre.Text
            Me.registro.descripcion = txtDireccion.Text



            Try
                taproducto.Update(dtproducto)
                MsgBox("La base de datos ha sido actualizada")
            Catch ex As Exception
                MsgBox("Hubo un error al actualizar la base de datos")
            End Try
            Me.editar = False
            Limpiar()
        Else
            Me.registro = dtproducto.NewproductoRow()
            Me.registro.idProducto = txtid.Text
            Me.registro.nombreProducto = txtNombre.Text
            Me.registro.descripcion = txtDireccion.Text


            dtproducto.AddproductoRow(Me.registro)
            Try
                taproducto.Update(dtproducto)
                MsgBox("Dato Ingresado exitosamente")
            Catch ex As Exception
                MsgBox("Error al ingresar el dato")
            End Try
            Limpiar()
        End If

    End Sub



    Private Sub btnmodificar_Click(sender As Object, e As EventArgs) Handles btnmodificar.Click
        enviarDatosACajas()
        editar = True
    End Sub
    Private Sub enviarDatosACajas()
        Me.campollave = Me.dtgProducto.CurrentRow.Cells.Item(0).Value.ToString()
        txtNombre.Text = Me.dtgProducto.CurrentRow.Cells.Item(1).Value.ToString()
        txtDireccion.Text = Me.dtgProducto.CurrentRow.Cells.Item(3).Value.ToString()
        txtid.Text = Me.dtgProducto.CurrentRow.Cells.Item(0).Value.ToString()
        txtid.Enabled = False
    End Sub
    Public Sub Limpiar()
        txtid.Clear()
        txtNombre.Clear()
        txtDireccion.Clear()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Me.registro = dtproducto.Rows(numeroRegistro)
        registro.Delete()

        Try
            taproducto.Update(dtproducto)
            MsgBox("Los datos han sido eliminados")
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al eliminar los datos")
        End Try
    End Sub

    Private Sub txtBuscar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBuscar.KeyPress
        dtproducto = taproducto.GetDataBy(txtBuscar.Text)

        dtgProducto.DataSource = dtproducto
    End Sub

    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub txtid_TextChanged(sender As Object, e As EventArgs) Handles txtid.TextChanged

    End Sub

    Private Sub txtid_Validating(sender As Object, e As CancelEventArgs) Handles txtid.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "Ingrese un valor")
        End If
    End Sub

    Private Sub txtNombre_TextChanged(sender As Object, e As EventArgs) Handles txtNombre.TextChanged

    End Sub

    Private Sub txtNombre_Validating(sender As Object, e As CancelEventArgs) Handles txtNombre.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "Ingrese un valor")
        End If
    End Sub

    Private Sub txtDireccion_TextChanged(sender As Object, e As EventArgs) Handles txtDireccion.TextChanged
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "Ingrese un valor")
        End If
    End Sub
End Class