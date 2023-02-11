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
//Se está listando el procedimiento almacenado

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
//te retorna la lista con el procedimiento
                return listaList;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        
        //método 
        public List<Sucursal> RecuperarSucursal(int nIdSucursal)
        {
            List<Sucursal>? listaList = new List<Sucursal>();
            try
            {
                DataTable lista = new DataTable();
                lista = this.conexion.TraerDataTable("prc_Sucursal_Recuperar", nIdSucursal );
                foreach (DataRow row in lista.Rows)
                {
                    //estas trayendo de la tupla a la variable sucursal uno por uno
                    Sucursal sucursal = new Sucursal();
                    sucursal.nIdSucursal = int.Parse(row["nIdSucursal"].ToString());
                    sucursal.nombre = row["sNombre"].ToString();
                    sucursal.telefono = row["telefono"].ToString();
                    sucursal.direccion = row["direccion"].ToString();
                    //a una lista se añade todos los datos que capturó sucursal
                    listaList.Add(sucursal);
                }
                //cierra la conexion
                this.conexion.Dispose();
                //retorna la lista
                return listaList;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}


