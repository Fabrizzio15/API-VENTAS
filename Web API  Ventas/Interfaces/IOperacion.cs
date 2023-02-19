using Web_API__Ventas.Modelos;
using Web_API__Ventas.Servicios;

namespace Web_API__Ventas.Interfaces
{
    public interface IOperacion
    {
        DTOPaginacion ListarOperacionVentas(string fechaInicio, string fechaFin, string sDescripcion, int pagina);
        int ObtenerCorrelativo(string serie);
        (byte[], string serie) GenerateTicket(int id = 10);
        string EliminarOperacion(string sSerie, string sCorrelativo, int nIdOperacion);
        byte[] ReporteVentas(string fechaInicio, string fechaFin);
        List<TicketOperacion> TicketOperacion(int nIdOperacion);
        List<Operacion> ListarOperacionCompras(string fechaInicio, string fechaFin, string sDescripcion);
        string InsertarOperacion(/*string dFechaOperacion,*/ int tipoOperacion, double dMontoTotal, int nIdVendedor, int nIdSucursal, string nIdPersona, string sSerie, string sCorrelativo, List<DTOProductos> detalles);
    }
}
