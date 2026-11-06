Imports MySql.Data.MySqlClient


Module OperacionesB

    Public Function GetDs(query As String, Optional dataMember As String = "") As DataSet
        Dim ds As New DataSet
        Try
            Dim da As New MySqlDataAdapter(query, cadena_diesel)
            If dataMember <> "" Then
                da.Fill(ds, dataMember)
            Else
                da.Fill(ds)
            End If
            Return ds
        Catch ex As Exception
            MsgBox("GetDs Error --> " & ex.ToString)
        End Try
        Return ds
    End Function

    Public Function FechaCorregida(fecha As String) As String
        Dim fech As DateTime = fecha
        Return fech.ToString("yyyy-MM-dd HH:mm:ss")
    End Function
    Public Function FechaCorregidaCorta(fecha As String) As String
        Dim fech As DateTime = fecha
        Return fech.ToString("yyyy-MM-dd")
    End Function

    Public Function GetDato(query As String) As String
        Dim dato As String = ""
        Try
            conexion_diesel.Close()
            conexion_diesel.Open()

            Dim comm As New MySqlCommand(query, conexion_diesel)

            If IsDBNull(comm.ExecuteScalar) Then
                dato = ""
            Else
                dato = comm.ExecuteScalar()
            End If

        Catch ex As Exception
            MsgBox("GetDato Error --> " & ex.ToString)
        End Try
        Return dato
    End Function

    Public Sub GenerarExcel(ByVal dgv As DataGridView, titulo1 As String, titulo2 As String, nombre As String, letra_ultima_colm As String)
        Dim exApp As New Microsoft.Office.Interop.Excel.Application
        Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
        Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet

        Try
            'anadimos libro y hojas
            exLibro = exApp.Workbooks.Add
            exHoja = exLibro.Worksheets(1)
            exHoja.Name = nombre
            'numero de columnas y numero de filas
            Dim Ncol As Integer = dgv.ColumnCount
            Dim Nrow As Integer = dgv.RowCount

            'recorremos todas las filas y por cada fila todas las columnas
            For i As Integer = 1 To Ncol
                exHoja.Cells.Item(6, i) = dgv.Columns(i - 1).HeaderText
            Next

            For fila As Integer = 0 To Nrow - 1
                For col As Integer = 0 To Ncol - 1
                    exHoja.Cells.Item(fila + 7, col + 1) = dgv.Item(col, fila).Value '.ToString
                Next
            Next

            With exHoja
                .Cells(1, 1) = titulo1
                .Cells(2, 1) = titulo2
                .Cells(3, 1) = "Generado: " & DateTime.Now.ToString("dd-MM-yyyy HH:mm")
                .Range("A1:" & letra_ultima_colm & "1").MergeCells = True
                .Range("A2:" & letra_ultima_colm & "2").MergeCells = True
                .Range("A3:" & letra_ultima_colm & "3").MergeCells = True
                .Range("A1:" & letra_ultima_colm & "5").Font.Bold = True
                .Range("A1:" & letra_ultima_colm & "5").Font.Size = 15
                .Range("A1:" & letra_ultima_colm & "5").HorizontalAlignment = 3
                .Rows.Item(6).Font.Bold = 1
                .Rows.Item(6).Font.Size = 13
                .Rows.Item(6).HorizontalAlignment = 3
                .Range("A6:" & letra_ultima_colm & Nrow + 6).Borders.LineStyle = 1
                .Range("A6:" & letra_ultima_colm & Nrow + 6).Borders.Color = Color.Gray
                .Range("A" & (Nrow + 6) & ":" & letra_ultima_colm & Nrow + 6).Font.Bold = True
                .Columns.AutoFit()
            End With

            exApp.Application.Visible = True
            exHoja = Nothing
            exLibro = Nothing
            exApp = Nothing

        Catch ex As Exception
            MsgBox("Error Generar Excel --> " & ex.ToString)
        End Try
    End Sub

    Public Sub MostrarReporteServicios(fechaD As String, fechaH As String)
        Try
            Dim query As String = "select fecha_salida as FECHA, proveedor as PROVEEDOR, galones_pedido as PEDIDO, Round( galones_peso,2) as RECIBIDO, densidad as DENSIDAD from entregas_diesel where date(fecha_salida) between '" & fechaD & "' and '" & fechaH & "'"
            Dim ds As DataSet = GetDs(query, "despachos")
            Dim pedido, recibido, promDensidad As Double

            For i = 0 To ds.Tables(0).Rows.Count() - 1
                pedido += Val(ds.Tables(0).Rows(i)(2).ToString)
                recibido += Val(ds.Tables(0).Rows(i)(3).ToString)
                promDensidad += Val(ds.Tables(0).Rows(i)(4).ToString)
            Next

            With Form1
                .txtGlPedidos.Text = pedido
                .txtGlPedidos.TextAlign = ContentAlignment.MiddleRight
                .txtGlRecibido.Text = recibido
                .txtGlRecibido.TextAlign = ContentAlignment.MiddleRight
                .txtPromDensi.Text = promDensidad / ds.Tables(0).Rows.Count()
                .txtPromDensi.TextAlign = ContentAlignment.MiddleRight
                .dgvEntregas.DataSource = ds
                .dgvEntregas.DataMember = "despachos"
                .dgvEntregas.ClearSelection()
            End With
        Catch ex As Exception

        End Try
    End Sub

End Module
