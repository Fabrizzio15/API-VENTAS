namespace Web_API__Ventas.Modelos
{
    public class Vendedor
    {
        string? sDocumento { get; set; }
        string? sNombres { get; set; }
        string? sApellidos { get; set; }
        string? sUsuario { get; set; }
        string? sContraseña { get; set; }
        string? sTelefono { get; set; }
        string? sDireccion { get; set; }
        DateTime dFechaIngreso { get; set; }
        DateTime dFechaCese { get; set; }
        bool bEstado { get; set; }
        double dSueldo { get; set; }
        int nRollId { get; set; }
    }
}
