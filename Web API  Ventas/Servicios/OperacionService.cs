using System.Data;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Servicios
{
    public class OperacionService : ServiceBase, IOperacion, IDisposable
    {
        public OperacionService(ICConexion pConexion) : base(pConexion)
        {
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
    }
}
