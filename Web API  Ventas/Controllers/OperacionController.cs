using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;
using Web_API__Ventas.Servicios;

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
        public DTOPaginacion Listar(string fechaInicio, string fechaFin, string descripcion = "", int pagina = 0,int sucursal = 1)
        {
            descripcion = descripcion == null ? "" : descripcion;
            return operacion.ListarOperacionVentas(fechaInicio, fechaFin, descripcion, pagina, sucursal);
        }
        [HttpGet]
        [Route("ListarCompras")]
        public DTOPaginacion ListarCompras(string fechaInicio, string fechaFin, string descripcion = "", int pagina = 0,int sucursal = 1)
        {
            descripcion = descripcion == null ? "" : descripcion;
            return operacion.ListarOperacionCompras(fechaInicio, fechaFin, descripcion, pagina, sucursal);
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
        public string InsertarOperacion(/*string dFechaOperacion,*/int tipoOperacion, double dMontoTotal, int nIdVendedor, int nIdSucursal, string nIdPersona, string sSerie, string sCorrelativo, string nidSunat,string emision, List<DTOProductos> detalles)
        {
            string ok = operacion.InsertarOperacion(/*dFechaOperacion,*/ tipoOperacion, dMontoTotal, nIdVendedor, nIdSucursal, nIdPersona, sSerie, sCorrelativo, nidSunat, emision, detalles);
            return ok;
        }

        [HttpPost]
        [Route("Eliminar")]
        public string EliminarOperacion(string sSerie, string sCorrelativo, int nIdOperacion)
        {
            string ok = operacion.EliminarOperacion(sSerie,sCorrelativo,nIdOperacion);
            return ok;
        }   
        
        [HttpPost]
        [Route("EliminarCompras")]
        public string EliminarOperacionCompras(int nIdOperacion)
        {
            string ok = operacion.EliminarOperacionCompras(nIdOperacion);
            return ok;
        }

        [HttpGet]
        [Route("PDF")]
        public IActionResult TicketVenta(int id)
        {
            // Lógica para generar el ticket de venta
            byte[] ticketContent;
            string serie;
            (ticketContent, serie) = operacion.GenerateTicket(id);

            // Devolver el ticket como un archivo descargable
            return File(ticketContent, "application/pdf", serie + ".pdf");
        }

        [HttpGet]
        [Route("Reporte")]
        public IActionResult ReporteVentas(string fInicial, string fFinal, int op = 0)
        {
            // Lógica para generar el ticket de venta
            byte[] reporte = operacion.ReporteVentas(fInicial, fFinal, op);

            // Devolver el ticket como un archivo descargable
            return File(reporte, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
        }

    }
}
