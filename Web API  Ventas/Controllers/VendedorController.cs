using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Controladores
{
    [ApiController]
    [Route("Vendedor")]
    public class VendedorController : ControllerBase
    {
        IVendedor _iVendedor;

        public VendedorController(IVendedor vendedor)
        {
            _iVendedor = vendedor;
        }

            [HttpPost]
            [Route("")]
            public object Recuperar(string usuario, string contrasena)
            {
                return _iVendedor.Login(usuario, contrasena);
            }

    }
}
