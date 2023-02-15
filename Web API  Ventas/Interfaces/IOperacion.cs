using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Interfaces
{
    public interface IOperacion
    {
        List<Operacion> ListarOperacionVentas(string fechaInicio, string fechaFin, string sDescripcion);
        int ObtenerCorrelativo(string serie);
        List<Operacion> ListarOperacionCompras(string fechaInicio, string fechaFin, string sDescripcion);
        string InsertarOperacion(/*string dFechaOperacion,*/ int tipoOperacion, double dMontoTotal, int nIdVendedor, int nIdSucursal, string nIdPersona, string sSerie, string sCorrelativo, List<DTOProductos> detalles);
    }
}
