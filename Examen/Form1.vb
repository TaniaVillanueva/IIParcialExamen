Imports System.ComponentModel
Imports System.Data.SqlClient
Public Class Form1
    Dim conexion As conexion = New conexion()
    Dim dt As New DataTable
    Private editar As Boolean = False
    Private campoLlave As String
    Private numeroRegistro As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mostrarDatos()
        btnEliminar.Enabled = False
    End Sub

    Private Sub mostrarDatos()
        Try
            'asigna a la variable datatable la consulta realizada a la base de datos y si existen registros los asigna al datagrid'
            'caso contrario no muestra nada en el datagrid
            dt = conexion.consulta
            If dt.Rows.Count <> 0 Then
                dtgFactura.DataSource = dt
                conexion.conexion.Close()
            Else
                dtgFactura.DataSource = Nothing
                conexion.conexion.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnFacturar_Click(sender As Object, e As EventArgs) Handles btnFacturar.Click
        Dim factura As Integer = Val(txtfactura.Text)
        Dim idCliente As String = txtCliente.Text
        Dim producto As String = txtProducto.Text
        Dim precio As Integer = Val(txtPrecio.Text)
        Dim cantidad As Integer = Val(txtCantidad.Text)
        Dim fecha As Date = txtFecha.Text

        If Me.editar Then
            Try
                If conexion.modificar(factura, cantidad, precio) Then
                    MsgBox("La Base de datos se modificó exitosamente")
                Else
                    MsgBox("Hubo un error al modificar los datos")
                End If
            Catch ex As Exception
                MsgBox("Ha sucedido un error", ex.Message)
            End Try
            mostrarDatos()
            Limpiar()
            Me.editar = False

        Else

            Try
                If conexion.insertarVenta(factura, fecha, precio, cantidad, idCliente, producto) Then
                    MsgBox("Se ingresaron los datos exitosamente")
                Else
                    MsgBox("Hubo un error al ingresar los datos")
                End If
            Catch ex As Exception
                MsgBox("Ha sucedido un error", ex.Message)
            End Try
            mostrarDatos()
            Limpiar()

        End If
    End Sub

    Private Sub enviarDatosATablas()
        Me.campoLlave = Me.dtgFactura.CurrentRow.Cells.Item(0).Value.ToString()
        txtfactura.Text = Me.dtgFactura.CurrentRow.Cells.Item(0).Value.ToString()
        txtCliente.Text = Me.dtgFactura.CurrentRow.Cells.Item(1).Value.ToString()
        txtProducto.Text = Me.dtgFactura.CurrentRow.Cells.Item(2).Value.ToString()
        txtCantidad.Text = Me.dtgFactura.CurrentRow.Cells.Item(4).Value.ToString()
        txtPrecio.Text = Me.dtgFactura.CurrentRow.Cells.Item(5).Value.ToString()
        txtfactura.Enabled = False
        txtCliente.Enabled = False
        txtProducto.Enabled = False


    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Me.editar = True
        enviarDatosATablas()
        btnEliminar.Enabled = True
    End Sub

    Public Sub Limpiar()
        txtfactura.Clear()
        txtCliente.Clear()
        txtProducto.Clear()
        txtCantidad.Clear()
        txtPrecio.Clear()
        txtFecha.Clear()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim factura As Integer = Val(txtfactura.Text)

        Try
            If conexion.EliminarVenta(factura) Then
                MsgBox("Ha eliminado una fila")
                mostrarDatos()
            Else
                MsgBox("Ha ocurrido un error")
            End If
        Catch ex As Exception
            MsgBox("Error al eliminar los datos")
        End Try
    End Sub

    Private Sub btnClientes_Click(sender As Object, e As EventArgs) Handles btnClientes.Click
        Clientes.Show()
        Me.Hide()
    End Sub

    Private Sub btnProducto_Click(sender As Object, e As EventArgs) Handles btnProducto.Click
        producto.Show()
        Me.Hide()
    End Sub

    Private Sub txtfactura_TextChanged(sender As Object, e As EventArgs) Handles txtfactura.TextChanged

    End Sub

    Private Sub txtfactura_Validating(sender As Object, e As CancelEventArgs) Handles txtfactura.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "Ingrese un valor")
        End If
    End Sub

    Private Sub txtCliente_TextChanged(sender As Object, e As EventArgs) Handles txtCliente.TextChanged

    End Sub

    Private Sub txtCliente_Validating(sender As Object, e As CancelEventArgs) Handles txtCliente.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "Ingrese un valor")
        End If
    End Sub

    Private Sub txtProducto_TextChanged(sender As Object, e As EventArgs) Handles txtProducto.TextChanged

    End Sub

    Private Sub txtProducto_Validating(sender As Object, e As CancelEventArgs) Handles txtProducto.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "Ingrese un valor")
        End If
    End Sub

    Private Sub txtCantidad_TextChanged(sender As Object, e As EventArgs) Handles txtCantidad.TextChanged

    End Sub

    Private Sub txtCantidad_Validating(sender As Object, e As CancelEventArgs) Handles txtCantidad.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "Ingrese un valor")
        End If
    End Sub

    Private Sub txtPrecio_TextChanged(sender As Object, e As EventArgs) Handles txtPrecio.TextChanged

    End Sub

    Private Sub txtPrecio_Validating(sender As Object, e As CancelEventArgs) Handles txtPrecio.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "Ingrese un valor")
        End If
    End Sub

    Private Sub txtFecha_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles txtFecha.MaskInputRejected

    End Sub

    Private Sub txtFecha_Validating(sender As Object, e As CancelEventArgs) Handles txtFecha.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.errorValidacion.SetError(sender, "")
        Else
            Me.errorValidacion.SetError(sender, "Ingrese un valor")
        End If
    End Sub
End Class
