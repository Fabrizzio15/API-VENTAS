using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Interfaces
{
    public interface IOperacion
    {
        List<Operacion> ListarOperacionVentas(string fechaInicio, string fechaFin, string sDescripcion);

    }
}
