using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Servicios
{
    public class DTOPaginacion
    {
        public int TotalPaginas { get; set; }
        public List<Operacion> operaciones { get; set; }

    }
}
