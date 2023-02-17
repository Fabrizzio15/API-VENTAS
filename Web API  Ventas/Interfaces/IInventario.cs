using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Interfaces
{
    public interface IInventario
    {
        List<Inventario> ListarInventario(int nIdProducto, string fInicial, string fFinal);
        List<object> InventarioResumen(int nIdProducto);
        int InsertarInventario(int nIdProducto, int nTipoOperacion, double nCantidad, double dPrecioCompraVenta, int nIdOperacion, string sSerie, string sCorrelativo);
    }
}
