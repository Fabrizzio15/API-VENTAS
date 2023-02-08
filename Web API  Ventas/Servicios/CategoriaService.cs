using System.Data;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Servicios
{
    public class CategoriaService : ServiceBase,ICategoria, IDisposable

    {
        public CategoriaService(ICConexion pConexion) : base(pConexion)
        {
        }

        public List<Categoria> ListarCategoria()
        {
            List<Categoria>? listaList = new List<Categoria>();
            try
            {
                DataTable lista = new DataTable();


                lista = this.conexion.TraerDataTable("prc_Categoria_Listar");
                foreach (DataRow row in lista.Rows)
                {
                    Categoria categoria = new Categoria();
                    categoria.idCategoria = int.Parse(row["id"].ToString());
                    categoria.descripcion = row["descripcion"].ToString();
                    
                    listaList.Add(categoria);
                }
                this.conexion.Dispose();
                return listaList;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public List<Categoria> RecuperarCategoria(int nIdCategoria)
        {
            List<Categoria>? listaList = new List<Categoria>();
            try
            {
                DataTable lista = new DataTable();

                Categoria categoria = new Categoria();
                lista = this.conexion.TraerDataTable("prc_Categoria_Recuperar",nIdCategoria);
                foreach (DataRow row in lista.Rows)
                {
                    categoria.idCategoria = int.Parse(row["id"].ToString());
                    categoria.descripcion = row["descripcion"].ToString();

                    listaList.Add(categoria);
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
