using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Interfaces
{
    public interface ISucursal
    {
        List<Sucursal> RecuperarSucursal(int nIdSucursal);
        List<Sucursal> ListarSucursal();

    }
}
