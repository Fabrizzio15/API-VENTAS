using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Controladores
{
    [Route("Categoria")]
    [ApiController]
    public class CategoriaController : Controller
    {
        ICategoria _categoria;

        public CategoriaController(ICategoria categoria)
        {
            _categoria = categoria;
        }

        [HttpGet]
        [Route("Recuperar")]
        public List<Categoria> Recuperar(int nIdCategoria)
        {
            return _categoria.RecuperarCategoria(nIdCategoria);
        }

        [HttpGet]
        [Route("")]
        public List<Categoria> listarCategorias()
        {
            return _categoria.ListarCategoria();
        }

    }
}
