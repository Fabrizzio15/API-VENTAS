using System.Data;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Servicios
{
    public class InventarioService : ServiceBase, IDisposable, IInventario
    {
        public InventarioService(ICConexion pConexion) : base(pConexion)
        {
        }

        public int InsertarInventario(int nIdProducto,int nTipoOperacion,double nCantidad,double dPrecioCompraVenta,int nIdOperacion,string sSerie,string sCorrelativo)
        {
            try
            {
                int valor = int.Parse(this.conexion.TraerValor("prc_Inventario_Insertar_Movimiento", nIdProducto,nTipoOperacion,nCantidad,dPrecioCompraVenta, nIdOperacion, sSerie,sCorrelativo ));
                return valor;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public List<Inventario> ListarInventario(int nIdProducto ,string fInicial, string fFinal)
        {
            List<Inventario>? listaList = new List<Inventario>();
            try
            {
                DataTable lista = new DataTable();


                lista = this.conexion.TraerDataTable("prc_InventarioRecuperar", nIdProducto, fInicial, fFinal);
                foreach (DataRow row in lista.Rows)
                {
                    Inventario inventario = new Inventario();
                    inventario.nIdInventario = int.Parse(row["idInventario"].ToString());
                    inventario.nIdProducto = int.Parse(row["idProducto"].ToString());
                    inventario.nTipoOperacion = row["tOperacion"].ToString();
                    inventario.nCantidad = double.Parse(row["cantidad"].ToString());
                    inventario.dPrecioCompraVenta = double.Parse(row["pOperacion"].ToString());
                    inventario.sOperacionSerie =row["sserie"].ToString();
                    inventario.stock = double.Parse(row["stock"].ToString());
                    inventario.fechaFinal = row["fecha"].ToString();
                    inventario.dSaldoCuenta = double.Parse(row["dSaldoCuenta"].ToString());

                    listaList.Add(inventario);
                }
                this.conexion.Dispose();
                return listaList;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public List<object> InventarioResumen(int nIdProducto)
        {
            List<object>? listaList = new List<object>();
            try
            {
                DataSet lista = new DataSet();


                lista = this.conexion.TraerDataSet("prc_Inventario_Resumen", nIdProducto);

                object resumen = new
                {
                    vCantidad = double.Parse(lista.Tables[0].Rows[0]["vCantidad"].ToString()),
                    vTotal = double.Parse(lista.Tables[1].Rows[0]["vTotal"].ToString()),
                    cCantidad = double.Parse(lista.Tables[2].Rows[0]["cCantidad"].ToString()),
                    cTotal = double.Parse(lista.Tables[3].Rows[0]["cTotal"].ToString()),
                    
                };              
                listaList.Add(resumen);
                this.conexion.Dispose();
                return listaList;
            }
            catch (Exception e)
            {
                return null;
            }

        }


    }
}
