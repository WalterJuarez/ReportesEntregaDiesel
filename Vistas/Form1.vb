Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MostrarReporteServicios(FechaCorregidaCorta(dtpFechaDesde.Value), FechaCorregidaCorta(dtpFechaHasta.Value))
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        GenerarExcel(dgvEntregas, "Reporte de Entregas Diesel", "Reporte generado del: " & dtpFechaDesde.Value.ToShortDateString & " al " & dtpFechaHasta.Value.ToShortDateString, "Reporte Entregas Diesel", "E")
    End Sub
End Class
