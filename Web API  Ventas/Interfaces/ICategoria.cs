using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Interfaces
{
    public interface ICategoria
    {
        List<Categoria> RecuperarCategoria(int nIdCategoria);
        List<Categoria> ListarCategoria();

    }
}
