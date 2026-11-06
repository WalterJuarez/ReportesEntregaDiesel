Imports MySql.Data.MySqlClient
Module ConexionDB

    '------- Conexion a base de datos local ------
    'Public cadena_diesel As String = "database=inventarios; server=localhost; port=3306; user=root; password=gasautoremoto;"
    Public cadena_diesel As String = "Database = zetatanques; Port = 3306; Server = 10.24.10.210; User Id = granel_fel; Password = FEL.2019;"
    Public conexion_diesel As New MySqlConnection(cadena_diesel)

End Module
