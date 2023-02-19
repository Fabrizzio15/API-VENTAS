using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Interfaces
{
    public interface IPersona
    {
        string Persona_Insertar(string sTipoDocumento, string sRucPersona, string sRazonSocial, string sTelefono = "", string scorreo = "");
        List<PersonaAutocomplete> PersonaAutocomplete(string descripcion);
    }
}
