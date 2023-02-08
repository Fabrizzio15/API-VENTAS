using System.Data;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Interfaces
{
    public interface IProducto
    {
        List<Producto> ListarProductos(string descripcion, int nIdSucursal, int tipoBusqueda);
        int ActualizarProductos(int nIdProductos, string sDescripcion, string sCodBarras, double dPrecioVenta, int nIdCategoria, int nIdSucursal, bool bEstado);
        int AgregarProductos(string sDescripcion, string sCodBarras, double dPrecioVenta, int nIdCategoria, int nIdSucursal);
        List<Producto> RecuperarProducto(int nIdProducto);
        int EliminarProducto(int nIdProductos);


    }
}
