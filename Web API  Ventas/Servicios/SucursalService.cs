using System.Data;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Servicios
{
    public class SucursalService : ServiceBase, ISucursal, IDisposable
    {
        public SucursalService(ICConexion pConexion) : base(pConexion)
        {
        }

        public List<Sucursal> ListarSucursal()
        {
            List<Sucursal>? listaList = new List<Sucursal>();
            try
            {
                DataTable lista = new DataTable();


                lista = this.conexion.TraerDataTable("prc_Sucursal_Listar");
                foreach (DataRow row in lista.Rows)
                {
                    Sucursal sucursal = new Sucursal();
                    sucursal.nIdSucursal = int.Parse(row["nIdSucursal"].ToString());
                    sucursal.nombre = row["sNombre"].ToString();
                    sucursal.telefono = row["telefono"].ToString();
                    sucursal.direccion = row["direccion"].ToString();

                    listaList.Add(sucursal);
                }
                this.conexion.Dispose();
                return listaList;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public List<Sucursal> RecuperarSucursal(int nIdSucursal)
        {
            List<Sucursal>? listaList = new List<Sucursal>();
            try
            {
                DataTable lista = new DataTable();


                lista = this.conexion.TraerDataTable("prc_Sucursal_Recuperar", nIdSucursal );
                foreach (DataRow row in lista.Rows)
                {
                    Sucursal sucursal = new Sucursal();
                    sucursal.nIdSucursal = int.Parse(row["nIdSucursal"].ToString());
                    sucursal.nombre = row["sNombre"].ToString();
                    sucursal.telefono = row["telefono"].ToString();
                    sucursal.direccion = row["direccion"].ToString();

                    listaList.Add(sucursal);
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


