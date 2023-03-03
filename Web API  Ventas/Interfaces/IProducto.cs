using System.Data;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Interfaces
{
    public interface IProducto
    {
        ProductoDTO ListarProductos(string descripcion, int nIdSucursal, int tipoBusqueda, int nroPagina);
        int ActualizarProductos(int nIdProductos, string sDescripcion, string sCodBarras, double dPrecioVenta, int nIdCategoria, int nIdSucursal, bool bEstado);
        int AgregarProductos(string sDescripcion, string sCodBarras, double dPrecioVenta, int nIdCategoria, int nIdSucursal, string unidadMedida, double igv, double gravada); 
        List<Producto> RecuperarProducto(int nIdProducto);
        int EliminarProducto(int nIdProductos);
        List<Producto> BuscarProductoCodBarras(string cod);
        int VerificarCodigoBarras(string sDescripcion);
    }
}
