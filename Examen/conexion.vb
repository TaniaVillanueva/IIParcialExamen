Imports System.Data.SqlClient

Public Class conexion

    Public conexion As New SqlConnection("Data Source= DESKTOP-4AAROTR; Initial Catalog=Tienda Examen; Integrated Security=True")
    Private cmb As SqlCommandBuilder
    Public ds As DataSet = New DataSet()
    Public DataTabla As DataTable
    Public comando As SqlCommand
    Public da As SqlDataAdapter

    Public Sub conectar()
        Try
            conexion.Open()
            MessageBox.Show("Conectado...")
        Catch ex As Exception
            MessageBox.Show("Error al conectar...")
        Finally
            conexion.Close()
        End Try
    End Sub
    Public Function consulta() As DataTable
        Try

            conexion.Open()

            Dim cmd As New SqlCommand("consultarVenta", conexion)
            cmd.CommandType = CommandType.StoredProcedure
            If cmd.ExecuteNonQuery Then
                Dim dt As New DataTable
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dt)
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Function eliminar(ByVal tabla, ByVal condicion)
        conexion.Open()
        Dim eliminarE As String = "delete from " + tabla + " where " + condicion
        comando = New SqlCommand(eliminarE, conexion)
        Dim i As Integer = comando.ExecuteNonQuery()
        conexion.Close()
        If (i > 0) Then
            Return True
        Else
            Return False
        End If
    End Function


    Function insertarVenta(t1 As Integer, t2 As Date, t3 As Integer, t4 As Integer, t5 As String, t6 As String) As Boolean
        Try
            conexion.Open()
            Dim procedimiento As SqlCommand = conexion.CreateCommand()
            procedimiento.CommandText = "insertarVenta"
            procedimiento.CommandType = CommandType.StoredProcedure

            procedimiento.Parameters.AddWithValue("@idVenta", t1)
            procedimiento.Parameters.AddWithValue("@fechaVenta", t2)
            procedimiento.Parameters.AddWithValue("@Precio", t3)
            procedimiento.Parameters.AddWithValue("@Cantidad", t4)
            procedimiento.Parameters.AddWithValue("@idCliente", t5)
            procedimiento.Parameters.AddWithValue("@idProducto", t6)
            If procedimiento.ExecuteNonQuery Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            conexion.Close()
        End Try
    End Function

    Public Function modificar(idVenta As Integer, cantidad As Integer, precio As Integer) As Boolean
        Try
            conexion.Open()
            Dim procedimiento As SqlCommand = conexion.CreateCommand()
            procedimiento.CommandText = "modificarVenta"
            procedimiento.CommandType = CommandType.StoredProcedure

            procedimiento.Parameters.AddWithValue("@idVenta", idVenta)
            procedimiento.Parameters.AddWithValue("@Precio", precio)
            procedimiento.Parameters.AddWithValue("@Cantidad", cantidad)

            If procedimiento.ExecuteNonQuery Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            conexion.Close()
        End Try
    End Function

    Public Function EliminarVenta(idVenta As Integer) As Boolean
        Try
            conexion.Open()
            Dim cmd As New SqlCommand("eliminarVenta", conexion)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idVenta", idVenta)
            If cmd.ExecuteNonQuery Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            conexion.Close()

        End Try

    End Function


End Class
