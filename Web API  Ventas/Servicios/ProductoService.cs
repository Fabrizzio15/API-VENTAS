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

        public List<Producto> BuscarProductoCodBarras(string cod)
        {
            List<Producto>? listaList = new List<Producto>();
            try
            {
                DataTable lista = new DataTable();


                lista = this.conexion.TraerDataTable("prc_Producto_BuscarCodigo", cod);
                foreach (DataRow row in lista.Rows)
                {
                    Producto producto = new Producto();
                    producto.nIdProducto = int.Parse(row["id"].ToString());
                    producto.sDescripcion = row["descripcion"].ToString();
                    producto.sCodBarras = row["codBarras"].ToString();
                    producto.nIdCategoria = int.Parse(row["categoria"].ToString());
                    producto.nIdSucursal = int.Parse(row["sucursalId"].ToString());
                    producto.stock = double.Parse(row["stock"].ToString());
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
        public ProductoDTO ListarProductos(string descripcion, int nIdSucursal, int tipoBusqueda,int nroPagina)
        {
            List<Producto>? listaList = new List<Producto>();
            ProductoDTO dto = new ProductoDTO();
            try
            {
                DataSet lista = new DataSet();


                lista = this.conexion.TraerDataSet("prc_Producto_Listar", descripcion, nIdSucursal, tipoBusqueda, nroPagina, nroPagina);
                foreach (DataRow row in lista.Tables[0].Rows)
                {
                    Producto producto = new Producto();
                    producto.nIdProducto = int.Parse(row["id"].ToString());
                    producto.sDescripcion = row["descripcion"].ToString();
                    producto.sCodBarras = row["codBarras"].ToString();
                    producto.nIdCategoria = int.Parse(row["categoria"].ToString());
                    producto.nIdSucursal = int.Parse(row["sucursalId"].ToString());
                    producto.dPrecioVenta = double.Parse(row["precioVenta"].ToString());
                    producto.bEstado = int.Parse(row["sucursalId"].ToString());
                    producto.gravada = double.Parse(row["gravado"].ToString());
                    producto.igv = double.Parse(row["igv"].ToString());
                    listaList.Add(producto);
                }

                dto.producto = listaList;
                dto.totalItems = int.Parse(lista.Tables[1].Rows[0]["cantidad"].ToString());
                this.conexion.Dispose();
                return dto;
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
                    producto.igv = double.Parse(row["montoIGV"].ToString());
                    producto.gravada = double.Parse(row["gravado"].ToString());
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

        public int VerificarCodigoBarras(string sDescripcion)
        {
            List<Producto>? listaList = new List<Producto>();
            try
            {
                int Respuesta = 0;
                Respuesta = int.Parse(this.conexion.TraerValor("prc_VerificarCodBarras", sDescripcion));
                this.conexion.Dispose();
                return Respuesta;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public int AgregarProductos(string sDescripcion, string sCodBarras, double dPrecioVenta, int nIdCategoria, int nIdSucursal, string unidadMedida, double igv, double gravada)
        {
            List<Producto>? listaList = new List<Producto>();
            try
            {
                int Respuesta = 0;
                Respuesta = int.Parse(this.conexion.TraerValor("prc_Producto_Insertar", sDescripcion, sCodBarras, dPrecioVenta, nIdCategoria, nIdSucursal, unidadMedida, gravada, igv));
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
