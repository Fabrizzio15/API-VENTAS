using System.Data;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Servicios
{
    public class VendedorService : ServiceBase, IVendedor
    {
        public VendedorService(ICConexion pConexion) : base(pConexion)
        {
        }

        public object Login(string usuario, string contrasena)
        {
            object respuesta = null;
            try
            {
                DataTable dtRespuesta =  this.conexion.TraerDataTable("prc_ValidarUsuario", usuario, contrasena);

                respuesta = new
                {
                    usuario = dtRespuesta.Rows[0]["usuario"].ToString(),
                    sucursal = dtRespuesta.Rows[0]["sucursal"].ToString()
                };
                return respuesta;   
            }
            catch (Exception e)
            {
                respuesta = new
                {
                    sucursal= e.Message,
                    usuario = e.StackTrace
                };
            return respuesta;
            }
        }
    }
}
