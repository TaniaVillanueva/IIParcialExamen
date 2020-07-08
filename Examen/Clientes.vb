Imports System.ComponentModel

Public Class Clientes
    Private dtclientes As New dsTiendaCliente.clienteDataTable
    Private taclientes As New dsTiendaClienteTableAdapters.clienteTableAdapter
    Private campollave As String
    Private editar As Boolean = False
    Private numeroRegistro As String
    Private registro As dsTiendaCliente.clienteRow
    Private Sub Clientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.dtclientes = Me.taclientes.GetData
        dtgClientes.DataSource = Me.dtclientes

    End Sub

    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click


        If Me.editar Then
            Me.registro = dtclientes.FindByidCliente("campoLlave")
            Me.registro.idCliente = txtIdentidad.Text
            Me.registro.nombre = txtNombre.Text
            Me.registro.apellido = txtApellido.Text
            Me.registro.direccion = txtDireccion.Text


            Try
                taclientes.Update(dtclientes)
                MsgBox("La base de datos ha sido actualizada")
            Catch ex As Exception
                MsgBox("Hubo un error al actualizar la base de datos")
            End Try
            Me.editar = False
            Limpiar()
        Else
            Me.registro = dtclientes.NewclienteRow()
            Me.registro.idCliente = txtIdentidad.Text
            Me.registro.nombre = txtNombre.Text
            Me.registro.apellido = txtApellido.Text
            Me.registro.direccion = txtDireccion.Text



            dtclientes.AddclienteRow(Me.registro)
            Try
                taclientes.Update(dtclientes)
                MsgBox("Dato Ingresado exitosamente")
            Catch ex As Exception
                MsgBox("Error al ingresar el dato")
            End Try
            Limpiar()
        End If

    End Sub

    Private Sub enviarDatosACajas()
        Me.campollave = Me.dtgClientes.CurrentRow.Cells.Item(0).Value.ToString()
        txtNombre.Text = Me.dtgClientes.CurrentRow.Cells.Item(1).Value.ToString()
        txtApellido.Text = Me.dtgClientes.CurrentRow.Cells.Item(2).Value.ToString()
        txtDireccion.Text = Me.dtgClientes.CurrentRow.Cells.Item(3).Value.ToString()
        txtIdentidad.Text = Me.dtgClientes.CurrentRow.Cells.Item(0).Value.ToString()
        txtIdentidad.Enabled = False
    End Sub
    Public Sub Limpiar()
        txtIdentidad.Clear()
        txtNombre.Clear()
        txtApellido.Clear()
        txtDireccion.Clear()
    End Sub

    Private Sub btnmodificar_Click(sender As Object, e As EventArgs) Handles btnmodificar.Click
        enviarDatosACajas()
        editar = True
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Me.registro = dtclientes.Rows(numeroRegistro)
        registro.Delete()

        Try
            taclientes.Update(dtclientes)
            MsgBox("Los datos han sido eliminados")
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al eliminar los datos")
        End Try
    End Sub

    Private Sub dtgClientes_SelectionChanged(sender As Object, e As EventArgs) Handles dtgClientes.SelectionChanged
        Me.numeroRegistro = dtgClientes.CurrentRow.Index.ToString()
    End Sub

    Private Sub txtBuscar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBuscar.KeyPress
        dtclientes = taclientes.GetDataBy(txtBuscar.Text)

        dtgClientes.DataSource = dtclientes
    End Sub

    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub txtIdentidad_TextChanged(sender As Object, e As EventArgs) Handles txtIdentidad.TextChanged

    End Sub

    Private Sub txtIdentidad_Validating(sender As Object, e As CancelEventArgs) Handles txtIdentidad.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "Ingrese un valor")
        End If
    End Sub

    Private Sub txtNombre_TextChanged(sender As Object, e As EventArgs) Handles txtNombre.TextChanged

    End Sub

    Private Sub txtApellido_TextChanged(sender As Object, e As EventArgs) Handles txtApellido.TextChanged

    End Sub

    Private Sub txtNombre_Validating(sender As Object, e As CancelEventArgs) Handles txtNombre.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "Ingrese un valor")
        End If
    End Sub

    Private Sub txtApellido_Validating(sender As Object, e As CancelEventArgs) Handles txtApellido.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "Ingrese un valor")
        End If
    End Sub

    Private Sub txtDireccion_TextChanged(sender As Object, e As EventArgs) Handles txtDireccion.TextChanged

    End Sub

    Private Sub txtDireccion_Validating(sender As Object, e As CancelEventArgs) Handles txtDireccion.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "Ingrese un valor")
        End If
    End Sub
End Class