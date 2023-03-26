using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.Drawing;
using System.Reflection.Metadata;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;
using static iTextSharp.text.pdf.AcroFields;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;

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
        public DTOPaginacion ListarOperacionVentas(string fechaInicio, string fechaFin, string sDescripcion, int pagina, int sucursal)
        {
            DTOPaginacion paginacion = new DTOPaginacion();
            List<Operacion>? listaList = new List<Operacion>();
            try
            {
                DataSet resultado = new DataSet();

                resultado = this.conexion.TraerDataSet("prc_OperacionVentasListar", fechaInicio, fechaFin, sDescripcion, pagina, sucursal);
                foreach (DataRow row in resultado.Tables[0].Rows)
                {
                    Operacion operacion = new Operacion();
                    operacion.idOperacion = int.Parse(row["idOperacion"].ToString());
                    operacion.fechaOperacion = row["fechaOperacion"].ToString();
                    operacion.monto = double.Parse(row["monto"].ToString());
                    operacion.usuario = row["sNombres"].ToString();
                    operacion.nombreSucursal = row["nombreSucursal"].ToString();
                    operacion.RazonSocial = row["RazonSocial"].ToString();
                    operacion.Correlativo = row["Correlativo"].ToString();
                    operacion.nIdSunat = row["idSunat"].ToString();
                    operacion.tipoDoc = row["tDocumento"].ToString();
                    int estado = row["bEstado"].ToString() == "True" ? 1 : 0;
                    operacion.bEstado = estado;
                    listaList.Add(operacion);
                }
                paginacion.TotalPaginas= int.Parse(resultado.Tables[1].Rows[0]["total"].ToString());
                paginacion.operaciones = listaList;
                this.conexion.Dispose();

                return paginacion;
            }
            catch (Exception e)
            {
                return null;
            }

        }        
        public DTOPaginacion ListarOperacionCompras(string fechaInicio, string fechaFin, string sDescripcion, int pagina, int sucursal)
        {
            DTOPaginacion paginacion = new DTOPaginacion();
            List<Operacion>? listaList = new List<Operacion>();
            try
            {
                DataSet resultado = new DataSet();

                resultado = this.conexion.TraerDataSet("prc_OperacionComprasListar", fechaInicio, fechaFin, sDescripcion, pagina, sucursal);
                foreach (DataRow row in resultado.Tables[0].Rows)
                {
                    Operacion operacion = new Operacion();
                    operacion.idOperacion = int.Parse(row["idOperacion"].ToString());
                    operacion.fechaOperacion = row["fechaOperacion"].ToString();
                    operacion.monto = double.Parse(row["monto"].ToString());
                    operacion.usuario = row["sNombres"].ToString();
                    operacion.nombreSucursal = row["nombreSucursal"].ToString();
                    operacion.RazonSocial = row["RazonSocial"].ToString();
                    operacion.Correlativo = row["Correlativo"].ToString();
                    operacion.nIdSunat = row["idSunat"].ToString();
                    operacion.tipoDoc = row["tDocumento"].ToString();
                    int estado = row["bEstado"].ToString() == "True" ? 1 : 0;
                    operacion.bEstado = estado;
                    listaList.Add(operacion);
                }
                paginacion.TotalPaginas= int.Parse(resultado.Tables[1].Rows[0]["total"].ToString());
                paginacion.operaciones = listaList;
                this.conexion.Dispose();

                return paginacion;
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
 
        public string InsertarOperacion(int tipoOperacion,double dMontoTotal,int nIdVendedor,int nIdSucursal,string nIdPersona,string sSerie,string sCorrelativo,string nidSunat,string fechaEmision,List<DTOProductos> detalles)
        {
           
            try
            {
                int nIdOperacion = int.Parse(this.conexion.TraerValor("prc_Operacion_Insertar", tipoOperacion,dMontoTotal, nIdVendedor, nIdSucursal, nIdPersona, sSerie, sCorrelativo, nidSunat==null ? "" : nidSunat, fechaEmision));
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
        public string EliminarOperacion( string sSerie, string sCorrelativo, int nIdOperacion)
        {

            try
            {
                DataTable productos = this.conexion.TraerDataTable("prc_Operacion_Eliminar", nIdOperacion);
                foreach (DataRow op in productos.Rows)
                {
                    int inventarioInsertar = inventario.InsertarInventario(
                        int.Parse(op["nIdProducto"].ToString()),
                        2,
                        double.Parse(op["nCantidad"].ToString()),
                        double.Parse(op["dPrecioVenta"].ToString()),
                        int.Parse(op["nIdOperacion"].ToString()),
                        sSerie,
                        sCorrelativo);
                }
                return "ok";
            }
            catch (Exception e)
            {
                return "no";
            }

        }
        public string EliminarOperacionCompras(int nIdOperacion)
        {

            try
            {
                DataTable productos = this.conexion.TraerDataTable("prc_Operacion_Eliminar", nIdOperacion);
                foreach (DataRow op in productos.Rows)
                {
                    int inventarioInsertar = inventario.InsertarInventario(
                        int.Parse(op["nIdProducto"].ToString()),
                        3,
                        double.Parse(op["nCantidad"].ToString()),
                        double.Parse(op["dPrecioVenta"].ToString()),
                        int.Parse(op["nIdOperacion"].ToString()),
                        op["serie"].ToString(),
                        op["correlativo"].ToString()
                        );
                }
                return "ok";
            }
            catch (Exception e)
            {
                return "no";
            }

        }

        public (byte[],string serie) GenerateTicket(int id = 10)
        {

            // Crear un documento PDF con iTextSharp
            var ticketSize = new Rectangle(200, 350);
            iTextSharp.text.Document documento = new iTextSharp.text.Document(ticketSize, 4, 4, 0, 0);
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(documento, memoryStream);

            // Abrir el documento
            documento.Open();

            // Agregar el logo
            //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("Servicios/Comercialgrande.jpg");
            //logo.ScaleAbsolute(150f, 70f);
            //logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER | iTextSharp.text.Image.ALIGN_MIDDLE;
            //
            //documento.Add(logo);

            // Agregar la descripción de la tienda
            Font fuente = FontFactory.GetFont(FontFactory.HELVETICA, 12f);
            fuente.SetStyle(Font.BOLD);


            Paragraph descripcionTienda = new Paragraph("DISTRIBUCIONES A & R E.I.R.L.", fuente);
            descripcionTienda.Alignment = Element.ALIGN_CENTER;
            descripcionTienda.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f);
            documento.Add(descripcionTienda);
            //DIRECCION
            Paragraph DireccionTienda = new Paragraph("CAL. MARIANO DE LOS SANTOS  KM. 658, CALCA - CUSCO", FontFactory.GetFont(FontFactory.HELVETICA, 9f));
            DireccionTienda.Alignment = Element.ALIGN_CENTER;
            DireccionTienda.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f);
            documento.Add(DireccionTienda);
            //RUC
            Paragraph RUCTienda = new Paragraph("R.U.C. 20610442553", FontFactory.GetFont(FontFactory.HELVETICA, 9f));
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
            //OBTENCION DE DATOS
            List<TicketOperacion> list = TicketOperacion(id);
            //FECHA 
            Paragraph tFecha = new Paragraph(("EMISIÓN: "+ list[0].fecha), FontFactory.GetFont(FontFactory.HELVETICA, 8f));
            tFecha.Alignment = Element.ALIGN_CENTER;
            tFecha.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f);
            documento.Add(tFecha);

            //TITULO (TIPO DE COMPROBANTE)

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
            documento.Add(lineaDivisoria);

            Paragraph gracias = new Paragraph("GRACIAS POR SU COMPRA!", FontFactory.GetFont(FontFactory.HELVETICA, 8f));
            gracias.Alignment = Element.ALIGN_CENTER;
            gracias.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f);
            documento.Add(gracias);
            // Cerrar el documento
            Paragraph espacio = new Paragraph("");
            lineaDivisoria.Alignment = Element.ALIGN_CENTER;
            lineaDivisoria.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f);
            documento.Add(espacio);
            documento.Add(espacio);
            documento.Add(espacio);
            documento.Add(espacio);
            documento.Add(espacio);
            documento.Add(espacio);

            documento.Close();

            return (memoryStream.ToArray(), list[0].serie);

        }

        public byte[] ReporteVentas(string fechaInicio, string fechaFin, int operacion)
        {
            DataTable lista = new DataTable();
            try
            {
                lista = this.conexion.TraerDataTable("prc_Operacion_Exportar", fechaInicio, fechaFin, operacion);
                this.conexion.Dispose();
            }
            catch (Exception e)
            {
                return null;
            }
            int row = 3;
            double total = 0;
            var stream = new MemoryStream();
            Color primary = ColorTranslator.FromHtml("#00b19d");
            using (var package = new ExcelPackage(stream))
            {
                var ews = package.Workbook.Worksheets.Add("Report");
                ews.Cells[1, 1].Value = "REPORTE DE VENTAS";
                ews.Cells[1,1,1,6].Merge = true;
                ews.Cells[1,1,1,6].Style.Font.Bold = true;
                ews.Cells[1,1,1,6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ews.Cells[1, 1, 1, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ews.Cells[1, 1, 1, 6].Style.Fill.BackgroundColor.SetColor(primary);
                ews.Cells[row, 1].Value = "SERIE";
                ews.Cells[row, 2].Value = "FECHA";
                ews.Cells[row, 3].Value = "CLIENTE";
                ews.Cells[row, 4].Value = "SUCURSAL";
                ews.Cells[row, 5].Value = "VENDEDOR";
                ews.Cells[row, 6].Value = "TOTAL";
                ews.Cells[row, 6].Style.Font.Bold = true;
                ews.Cells[row, 1, row, 6].Style.Font.Bold = true;
                ews.Cells[row, 1, row, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ews.Cells[row, 1, row, 6].Style.Fill.BackgroundColor.SetColor(primary);

                row++;
                // Crear un libro de trabajo de Excel

                foreach (DataRow data in lista.Rows)
                {
                    // Agregar datos al archivo de Excel
                    ews.Cells[row, 1].Value = data["serie"].ToString();
                    ews.Cells[row, 2].Value = data["fecha"].ToString();
                    ews.Cells[row, 3].Value = data["cliente"].ToString();
                    ews.Cells[row, 4].Value = data["sucursal"].ToString();
                    ews.Cells[row, 5].Value = data["vendedor"].ToString();
                    ews.Cells[row, 6].Value = double.Parse(data["total"].ToString());
                    ews.Cells[row, 6].Style.Numberformat.Format = "#,##0.00";

                    total = total + double.Parse(data["total"].ToString());
                    row++;
                }
                ews.Cells[row, 5].Style.Font.Bold = true;
                ews.Cells[row, 5].Value = "TOTAL";  
                ews.Cells[row, 6].Style.Font.Bold = true;
                ews.Cells[row, 6].Style.Numberformat.Format = "#,##0.00";
                ews.Cells[row, 6].Value = total;
                ews.Cells[row, 5,row, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ews.Cells[row, 5,row, 6].Style.Fill.BackgroundColor.SetColor(primary);
                ews.Column(1).AutoFit();
                ews.Column(2).AutoFit();
                ews.Column(3).AutoFit();
                ews.Column(4).AutoFit();
                ews.Column(5).AutoFit();
                ews.Column(6).AutoFit();

                // Guardar el archivo de Excel en el flujo de memoria
                package.Save();
            }

            // Devolver el archivo de Excel como una respuesta HTTP
            return stream.ToArray();
            //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
        }


    }
}
