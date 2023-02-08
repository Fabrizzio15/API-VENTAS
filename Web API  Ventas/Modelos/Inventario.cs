namespace Web_API__Ventas.Modelos
{
    public class Inventario
    {
        int idInventario { get; set; }
        int idProducto{ get; set; }
        int nTipoOperacion{ get; set; }
        int nCantidad{ get; set; }
        decimal dPrecioCompraVenta{ get; set; }
        int idOperacio{ get; set; }
        decimal dSaldoCuenta{ get; set; }
        DateTime dtFechaRegistro{ get; set; }
        bool estado{ get; set; }
    }
}
