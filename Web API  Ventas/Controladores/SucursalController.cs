using Microsoft.AspNetCore.Mvc;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Controladores
{
    [Route("Sucursal")]
    [ApiController]
    public class SucursalController : Controller
    {
        ISucursal _sucursal;

        public SucursalController(ISucursal sucursal)
        {
            _sucursal = sucursal;
        }

        [HttpGet]
        [Route("Recuperar")]
        public List<Sucursal> RecuperarSucursal(int nIdSucursal)
        {
            return _sucursal.RecuperarSucursal(nIdSucursal);
        }

        [HttpGet]
        [Route("")]
        public List<Sucursal> listarSucursal()
        {
            return _sucursal.ListarSucursal();
        }
    }
}
