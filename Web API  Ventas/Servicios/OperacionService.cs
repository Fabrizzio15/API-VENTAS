using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Reflection.Metadata;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;
using static iTextSharp.text.pdf.AcroFields;

namespace Web_API__Ventas.Servicios
{
    public class OperacionService : ServiceBase, IOperacion, IDisposable
    {
        IOperacionProducto producto;
        IInventario inventario;
        public OperacionService(ICConexion pConexion, IOperacionProducto producto, IInventario inventario) : base(pConexion)
        {
            this.producto = producto;
            this.inventario = inventario;
        }
        public List<Operacion> ListarOperacionVentas(string fechaInicio, string fechaFin, string sDescripcion)
        {
            List<Operacion>? listaList = new List<Operacion>();
            try
            {
                DataTable lista = new DataTable();


                lista = this.conexion.TraerDataTable("prc_OperacionVentasListar", sDescripcion);
                foreach (DataRow row in lista.Rows)
                {
                    Operacion operacion = new Operacion();
                    operacion.idOperacion = int.Parse(row["idOperacion"].ToString());
                    operacion.fechaOperacion = row["fechaOperacion"].ToString();
                    operacion.monto = double.Parse(row["monto"].ToString());
                    operacion.usuario = row["usuario"].ToString();
                    operacion.nombreSucursal = row["nombreSucursal"].ToString();
                    operacion.RazonSocial = row["RazonSocial"].ToString();
                    operacion.Correlativo = row["Correlativo"].ToString();
                    listaList.Add(operacion);
                }
                this.conexion.Dispose();
                return listaList;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public List<Operacion> ListarOperacionCompras(string fechaInicio, string fechaFin, string sDescripcion)
        {
            List<Operacion>? listaList = new List<Operacion>();
            try
            {
                DataTable lista = new DataTable();


                lista = this.conexion.TraerDataTable("prc_OperacionComprasListar", sDescripcion);
                foreach (DataRow row in lista.Rows)
                {
                    Operacion operacion = new Operacion();
                    operacion.idOperacion = int.Parse(row["idOperacion"].ToString());
                    operacion.fechaOperacion = row["fechaOperacion"].ToString();
                    operacion.monto = double.Parse(row["monto"].ToString());
                    operacion.usuario = row["usuario"].ToString();
                    operacion.nombreSucursal = row["nombreSucursal"].ToString();
                    operacion.RazonSocial = row["RazonSocial"].ToString();
                    operacion.Correlativo = row["Correlativo"].ToString();
                    listaList.Add(operacion);
                }
                this.conexion.Dispose();
                return listaList;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public int ObtenerCorrelativo(string serie)
        {
            try
            {
             
                int correlativo = int.Parse(this.conexion.TraerValor("prc_Operacion_Correlativo", serie));
                this.conexion.Dispose();
                return correlativo;
            }
            catch (Exception e)
            {
                return 0;
            }

        }
 
        public string InsertarOperacion(/*string dFechaOperacion,*/int tipoOperacion,double dMontoTotal,int nIdVendedor,int nIdSucursal,string nIdPersona,string sSerie,string sCorrelativo,List<DTOProductos> detalles)
        {
           
            try
            {
                int nIdOperacion = int.Parse(this.conexion.TraerValor("prc_Operacion_Insertar", /*dFechaOperacion,*/ tipoOperacion,dMontoTotal, nIdVendedor, nIdSucursal, nIdPersona, sSerie, sCorrelativo));
                foreach (DTOProductos op in detalles) 
                {
                    string value = producto.InsertarDetalle(op.nIdProducto, op.cantidad, op.precioVenta, nIdOperacion);
                    int inventarioInsertar = inventario.InsertarInventario(op.nIdProducto, tipoOperacion, op.cantidad, op.precioVenta, nIdOperacion, sSerie, sCorrelativo);
                }
                return "ok";
            }
            catch (Exception e)
            {
                return "no";
            }

        }

        public List<TicketOperacion> TicketOperacion(int nIdOperacion)
        {
            List<TicketOperacion>? listaList = new List<TicketOperacion>();
            try
            {
                DataTable lista = new DataTable();


                lista = this.conexion.TraerDataTable("prc_ProductoDetalle", nIdOperacion);

                foreach (DataRow row in lista.Rows)
                {
                    TicketOperacion operacionProducto = new TicketOperacion();

                    operacionProducto.nProducto = row["nProducto"].ToString();
                    operacionProducto.cantidad = float.Parse(row["cantidad"].ToString());
                    operacionProducto.unitario = float.Parse(row["unitario"].ToString());
                    operacionProducto.totalUnidad = float.Parse(row["totalUnidad"].ToString());
                    operacionProducto.fecha = row["fecha"].ToString();
                    operacionProducto.totalVenta = float.Parse(row["totalVenta"].ToString());
                    operacionProducto.nombres = row["nombres"].ToString();
                    operacionProducto.sucursal = row["sucursal"].ToString();
                    operacionProducto.serie = row["serie"].ToString();
                    
                    listaList.Add(operacionProducto);
                }
                this.conexion.Dispose();
                return listaList;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public (byte[],string serie) GenerateTicket(int id = 10)
        {

            // Crear un documento PDF con iTextSharp
            var ticketSize = new Rectangle(200, 500);
            iTextSharp.text.Document documento = new iTextSharp.text.Document(ticketSize, 4, 4, 0, 0);
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(documento, memoryStream);

            // Abrir el documento
            documento.Open();

            // Agregar el logo
            Image logo = Image.GetInstance("Servicios/logoTienda.jpeg");
            logo.ScaleAbsolute(200f, 200f);
            documento.Add(logo);

            // Agregar la descripción de la tienda
            Paragraph descripcionTienda = new Paragraph("DistribuidoraA&R", FontFactory.GetFont(FontFactory.HELVETICA, 12f));
            descripcionTienda.Alignment = Element.ALIGN_CENTER;
            descripcionTienda.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f);
            documento.Add(descripcionTienda);
            //DIRECCION 
            Paragraph DireccionTienda = new Paragraph("Calle bolivar s/n Calca, mercado modelo", FontFactory.GetFont(FontFactory.HELVETICA, 9f));
            DireccionTienda.Alignment = Element.ALIGN_CENTER;
            DireccionTienda.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f);
            documento.Add(DireccionTienda);
            //RUC
            Paragraph RUCTienda = new Paragraph("R.U.C. 10700995564", FontFactory.GetFont(FontFactory.HELVETICA, 9f));
            RUCTienda.Alignment = Element.ALIGN_CENTER;
            RUCTienda.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f);
            documento.Add(RUCTienda);
            // Agregar la línea divisoria
            Paragraph lineaDivisoria = new Paragraph("------------------------------------------------");
            lineaDivisoria.Alignment = Element.ALIGN_CENTER;
            lineaDivisoria.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f);
            documento.Add(lineaDivisoria);
            //TITULO (TIPO DE COMPROBANTE)
            Paragraph tDocumento = new Paragraph("NOTA DE VENTA ELECTRONICA", FontFactory.GetFont(FontFactory.HELVETICA,8f));
            tDocumento.Alignment = Element.ALIGN_CENTER;
            tDocumento.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f);
            documento.Add(tDocumento);
            //TITULO (TIPO DE COMPROBANTE)
            List<TicketOperacion> list = TicketOperacion(id);

            Paragraph sSerieCorrelativo = new Paragraph(list[0].serie, FontFactory.GetFont(FontFactory.HELVETICA, 9f));
            sSerieCorrelativo.Alignment = Element.ALIGN_CENTER;
            sSerieCorrelativo.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f);
            documento.Add(sSerieCorrelativo);
            // Agregar la línea divisoria
            documento.Add(lineaDivisoria);

            // Agregar los ítems
            PdfPTable tablaItems = new PdfPTable(4);
            tablaItems.WidthPercentage = 100;
            tablaItems.SetWidths(new float[] { 1f, 0.3f, 0.4f, 0.3f });

            // Cabecera de la tabla
            PdfPCell celdaDescripcion = new PdfPCell(new Phrase("Descripción", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f)));
            celdaDescripcion.HorizontalAlignment = Element.ALIGN_LEFT;
            celdaDescripcion.Border = 0;
            tablaItems.AddCell(celdaDescripcion);

            PdfPCell celdaCantidad = new PdfPCell(new Phrase("Cant.", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f)));
            celdaCantidad.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaCantidad.Border = 0;
            tablaItems.AddCell(celdaCantidad);

            PdfPCell celdaPrecioUnitario = new PdfPCell(new Phrase("Precio", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f)));
            celdaPrecioUnitario.HorizontalAlignment = Element.ALIGN_RIGHT;
            celdaPrecioUnitario.Border = 0;
            tablaItems.AddCell(celdaPrecioUnitario);

            PdfPCell celdaTotal = new PdfPCell(new Phrase("Total", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f)));
            celdaTotal.HorizontalAlignment = Element.ALIGN_RIGHT;
            celdaTotal.Border = 0;
            tablaItems.AddCell(celdaTotal);
            //LDIVISORIA3


            foreach (TicketOperacion ti in list)
            {
                PdfPCell celdaItemDescripcion = new PdfPCell(new Phrase(ti.nProducto , FontFactory.GetFont(FontFactory.HELVETICA, 8f)));
                celdaItemDescripcion.HorizontalAlignment = Element.ALIGN_LEFT;
                celdaItemDescripcion.Border = 0;
                tablaItems.AddCell(celdaItemDescripcion);

                PdfPCell celdaItemCantidad = new PdfPCell(new Phrase(ti.cantidad.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8f)));
                celdaItemCantidad.HorizontalAlignment = Element.ALIGN_CENTER;
                celdaItemCantidad.Border = 0;
                tablaItems.AddCell(celdaItemCantidad);

                // Devolver el contenido del archivo PDF como un arreglo de bytes
                PdfPCell celdaItemPrecioUnitario = new PdfPCell(new Phrase(ti.unitario.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8f)));

                celdaItemPrecioUnitario.HorizontalAlignment = Element.ALIGN_RIGHT;
                celdaItemPrecioUnitario.Border = 0;
                tablaItems.AddCell(celdaItemPrecioUnitario);

                PdfPCell celdaItemTotal = new PdfPCell(new Phrase(ti.totalUnidad.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8f)));
                celdaItemTotal.HorizontalAlignment = Element.ALIGN_RIGHT;
                celdaItemTotal.Border = 0;
                tablaItems.AddCell(celdaItemTotal);

            }

            documento.Add(tablaItems);

            // Agregar la línea divisoria
            documento.Add(lineaDivisoria);

            // Agregar el total

            Paragraph totalTexto = new Paragraph($"Total: {list[0].totalVenta}", FontFactory.GetFont(FontFactory.HELVETICA, 8f));
            totalTexto.Alignment = Element.ALIGN_RIGHT;
            totalTexto.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f);
            documento.Add(totalTexto);

            // Cerrar el documento
            documento.Close();

            return (memoryStream.ToArray(), list[0].serie);

        }


    }
}
