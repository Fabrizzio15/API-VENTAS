using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;
using Web_API__Ventas.Servicios;

namespace Web_API__Ventas.Controladores
{
    [Route("Persona")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        IPersona persona;

        public PersonaController(IPersona persona)
        {
            this.persona = persona;
        }

        [HttpGet]
        [Route("Autocomplete")]
        public List<PersonaAutocomplete> Autocomplete(string descripcion)
        {
            descripcion = descripcion == null ? "" : descripcion;
            return persona.PersonaAutocomplete(descripcion);
        }

        [HttpPost]
        [Route("Insertar")]
        public string InsertarProducto(string sTipoDocumento, string sRucPersona, string sRazonSocial, string sTelefono = "", string scorreo = "")
        {
            string ok = persona.Persona_Insertar(sTipoDocumento,sRucPersona,sRazonSocial,sTelefono,scorreo);
            return ok;
        }
    }
}
