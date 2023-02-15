using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        [HttpGet]
        [Route("ListarCompras")]
        public List<Operacion> ListarCompras(string fechaInicio, string fechaFin, string descripcion = "")
        {
            descripcion = descripcion == null ? "" : descripcion;
            return operacion.ListarOperacionCompras(fechaInicio, fechaFin, descripcion);
        }

        [HttpGet]
        [Route("Correlativo")]
        public int Listar(string serie)
        {
            serie = serie == null ? "NV01" : serie;
            return operacion.ObtenerCorrelativo(serie);
        }

        [HttpPost]
        [Route("Insertar")]
        public string InsertarOperacion(/*string dFechaOperacion,*/int tipoOperacion, double dMontoTotal, int nIdVendedor, int nIdSucursal, string nIdPersona, string sSerie, string sCorrelativo, List<DTOProductos> detalles)
        {
            string ok = operacion.InsertarOperacion(/*dFechaOperacion,*/ tipoOperacion, dMontoTotal, nIdVendedor, nIdSucursal, nIdPersona, sSerie, sCorrelativo, detalles);
            return ok;
        }

    }
}
