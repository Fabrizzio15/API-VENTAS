using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Controladores
{
    [Route("Operacion")]
    [ApiController]
    public class OperacionController : ControllerBase
    {

        IOperacion operacion;

        public OperacionController(IOperacion operacion)
        {
            this.operacion = operacion;
        }

        [HttpGet]
        [Route("")]
        public List<Operacion> Listar(string fechaInicio, string fechaFin, string descripcion = "")
        {
            descripcion = descripcion == null ? "" : descripcion;
            return operacion.ListarOperacionVentas(fechaInicio, fechaFin, descripcion);
        }
    }
}
