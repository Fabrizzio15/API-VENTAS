using System.Data;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

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
    }
}
