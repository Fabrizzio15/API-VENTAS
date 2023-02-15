using System.Data;
using System.Data.SqlClient;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;
namespace Web_API__Ventas.Modelos
{
    public class Producto
    {
        public int nIdProducto { get; set; }
        public string sDescripcion { get; set; }
        public string sCodBarras { get; set; }
        public int nIdCategoria { get; set; }
        public int nIdSucursal { get; set; }
        public double dPrecioVenta { get; set; }
        public int bEstado { get; set; }
        public double stock { get; set; }



    }
}
