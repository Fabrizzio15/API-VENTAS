using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Controladores
{
    [Route("Producto")]
    [ApiController]
    public class ProductoController : ControllerBase 
    {
        IProducto producto;

        public ProductoController(IProducto producto)
        {
            this.producto = producto;
        }

        [HttpGet]
        [Route("")]
        public List<Producto> Listar(int nIdSucursal, int tipoBusqueda, string descripcion = "")
        {
            descripcion = descripcion == null ? "" : descripcion;
            return producto.ListarProductos(descripcion, nIdSucursal, tipoBusqueda);
        }

        [HttpGet]
        [Route("Recuperar")]
        public List<Producto> Recuperar(int nIdProducto)
        {
            return producto.RecuperarProducto(nIdProducto);
        }

        [HttpGet]
        [Route("Eliminar")]
        public int Eliminar(int nIdProducto)
        {
            return producto.EliminarProducto(nIdProducto);
        }

        [HttpPut]
        [Route("Agregar")]
        public int AgregarProducto(string sDescripcion, string sCodBarras, double dPrecioVenta, int nIdCategoria, int nIdSucursal)
        {
            return producto.AgregarProductos(sDescripcion,sCodBarras,dPrecioVenta,nIdCategoria,nIdSucursal);
        }

        [HttpPut]
        [Route("Actualizar")]
        public int ActualizarProducto(int nIdProductos, string sDescripcion, string sCodBarras, double dPrecioVenta, int nIdCategoria, int nIdSucursal, bool bEstado)
        {
            return producto.ActualizarProductos(nIdProductos, sDescripcion, sCodBarras, dPrecioVenta, nIdCategoria, nIdSucursal, bEstado);
        }
    }
}
