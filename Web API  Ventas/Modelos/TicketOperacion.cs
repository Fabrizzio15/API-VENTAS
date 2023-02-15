namespace Web_API__Ventas.Modelos
{
    public class TicketOperacion
    {
        public string nProducto { get; set; }
        public float cantidad { get; set; }
        public float unitario { get; set; }
        public float totalUnidad { get; set; }
        public string fecha { get; set; }
        public float totalVenta { get; set; }
        public string nombres { get; set; }
        public string sucursal { get; set; }
        public string serie { get; set; }
    }
}
