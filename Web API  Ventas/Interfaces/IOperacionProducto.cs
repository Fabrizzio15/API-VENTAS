namespace Web_API__Ventas.Interfaces
{
    public interface IOperacionProducto
    {
        string InsertarDetalle(int nIdProducto, double nCantidad, double dPrecioVenta, int nIdOperacion);

    }
}
