using Web_API__Ventas.Interfaces;

namespace Web_API__Ventas.Modelos
{
    public class Inventario
    {
        public int nIdInventario { get; set; }
        public int nIdProducto { get; set; }
        public string nTipoOperacion { get; set; } 
        public double nCantidad { get; set; }
        public double dPrecioCompraVenta { get; set; }
        public int nIdOperacion { get; set; }
        public double dSaldoCuenta { get; set; }
        public int bEstadoInventario { get; set; }
        public string sOperacionSerie { get; set; }
        public string sOperacionCorrelativo { get; set; }
        public string fechaFinal { get; set; }
        public double stock { get; set; }
    }
}
