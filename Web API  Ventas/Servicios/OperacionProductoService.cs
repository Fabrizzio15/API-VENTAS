using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Servicios
{
    public class OperacionProductoService : ServiceBase, IDisposable, IOperacionProducto
    {
        public OperacionProductoService(ICConexion pConexion) : base(pConexion)
        {
        }

        public string InsertarDetalle(int nIdProducto ,double nCantidad ,double dPrecioVenta ,int nIdOperacion)
        {
            try
            {
                string Respuesta = this.conexion.TraerValor("prc_OperacionProducto_Insertar", nIdProducto, nCantidad, dPrecioVenta, nIdOperacion);
                this.conexion.Dispose();
                return Respuesta;
            }
            catch (Exception e)
            {
                return "Ocurrio un error al insertar los datos";
            }
        }
    }
}
