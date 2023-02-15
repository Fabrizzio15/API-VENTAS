using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : Controller 
    {
        IInventario _inventario;

        public InventarioController(IInventario inventario)
        {
            _inventario = inventario;
        }

        [HttpGet]
        [Route("")]
        public List<Inventario> Listar(int nIdProducto, string fInicial, string fFinal)
        {
            return _inventario.ListarInventario(nIdProducto, fInicial, fFinal);
        }
    }
}
