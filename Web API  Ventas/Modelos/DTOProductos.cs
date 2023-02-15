namespace Web_API__Ventas.Modelos
{
    public class DTOProductos
    {
        public int nIdProducto { get; set; }
        public string sDescripcion { get; set; }
        public double stock { get; set; }
        public double cantidad { get; set; }
        public double precioVenta { get; set; }
        public double total { get; set; }
    }
}
