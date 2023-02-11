using System.Data;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Servicios
{
    public class ProductoService : ServiceBase, IDisposable, IProducto
    {
        public ProductoService(ICConexion pConexion) : base(pConexion)
        {
        }

        public List<Producto> ListarProductos(string descripcion, int nIdSucursal, int tipoBusqueda)
        {
            List<Producto>? listaList = new List<Producto>();
            try
            {
                DataTable lista = new DataTable();


                lista = this.conexion.TraerDataTable("prc_Producto_Listar", descripcion, nIdSucursal, tipoBusqueda);
                foreach (DataRow row in lista.Rows) {
                    Producto producto = new Producto();
                    producto.nIdProducto = int.Parse(row["id"].ToString());
                    producto.sDescripcion = row["descripcion"].ToString();
                    producto.sCodBarras = row["codBarras"].ToString();
                    producto.nIdCategoria = int.Parse(row["categoria"].ToString());
                    producto.nIdSucursal = int.Parse(row["sucursalId"].ToString());
                    producto.dPrecioVenta = double.Parse(row["precioVenta"].ToString());
                    producto.bEstado = int.Parse(row["sucursalId"].ToString());
                    listaList.Add(producto);
                }
                this.conexion.Dispose();
                return listaList;
            }
            catch (Exception e)
            {
                return null;
            }

        }


        public List<Producto> RecuperarProducto(int nIdProducto)
        {
            List<Producto>? listaList = new List<Producto>();
            try
            {
                DataTable lista = new DataTable();

                Producto producto = new Producto();
                lista = this.conexion.TraerDataTable("prc_Producto_Recuperar", nIdProducto);
                foreach (DataRow row in lista.Rows)
                {
                    producto.nIdProducto = int.Parse(row["id"].ToString());
                    producto.sDescripcion = row["descripcion"].ToString();
                    producto.sCodBarras = row["codBarras"].ToString();
                    producto.nIdCategoria = int.Parse(row["categoria"].ToString());
                    producto.nIdSucursal = int.Parse(row["sucursalId"].ToString());
                    producto.dPrecioVenta = double.Parse(row["precioVenta"].ToString());
                    producto.bEstado = int.Parse(row["sucursalId"].ToString());
                    listaList.Add(producto);
                }
                this.conexion.Dispose();
                return listaList;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public int ActualizarProductos(int nIdProductos,string sDescripcion, string sCodBarras,double dPrecioVenta, int nIdCategoria, int nIdSucursal, bool bEstado)
        {
            List<Producto>? listaList = new List<Producto>();
            try
            {
                int Respuesta = 0;
                Respuesta = int.Parse(this.conexion.TraerValor("prc_Producto_Actualizar", nIdProductos,sDescripcion,sCodBarras,dPrecioVenta,nIdCategoria,nIdSucursal,bEstado));
                this.conexion.Dispose();
                return Respuesta;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

            public int AgregarProductos(string sDescripcion, string sCodBarras, double dPrecioVenta, int nIdCategoria, int nIdSucursal)
            {
                List<Producto>? listaList = new List<Producto>();
                try
                {
                    int Respuesta = 0;
                    Respuesta = int.Parse(this.conexion.TraerValor("prc_Producto_Insertar", sDescripcion, sCodBarras, dPrecioVenta, nIdCategoria, nIdSucursal));
                    this.conexion.Dispose();
                    return Respuesta;
                }
                catch (Exception e)
                {
                    return 0;
                }

            }

        public int EliminarProducto(int nIdProductos)
        {
            List<Producto>? listaList = new List<Producto>();
            try
            {
                int Respuesta = 0;
                Respuesta = int.Parse(this.conexion.TraerValor("prc_Producto_Eliminar", nIdProductos));
                this.conexion.Dispose();
                return Respuesta;
            }
            catch (Exception e)
            {
                return 0;
            }

        }


    }
}
