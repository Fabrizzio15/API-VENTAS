using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;

namespace Web_API__Ventas.Servicios
{
    public class PersonaService : ServiceBase, IPersona, IDisposable
    {
        public PersonaService(ICConexion pConexion) : base(pConexion)
        {
        }

        public List<PersonaAutocomplete> PersonaAutocomplete(string descripcion)
        {
            List<PersonaAutocomplete>? listaList = new List<PersonaAutocomplete>();
            try
            {
                DataTable lista = new DataTable();

                lista = this.conexion.TraerDataTable("prc_PersonaAutocomplete", descripcion);
                foreach (DataRow row in lista.Rows)
                {
                    PersonaAutocomplete persona = new PersonaAutocomplete();
                    persona.sTipoDocumento = row["sTipoDocumento"].ToString();
                    persona.nRucPersona = row["nRucPersona"].ToString();
                    persona.srazonSocial = row["sRazonSocial"].ToString();
                    listaList.Add(persona);
                }
                this.conexion.Dispose();
                return listaList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string Persona_Insertar(string sTipoDocumento, string sRucPersona,string sRazonSocial, string sTelefono = "",string scorreo = "")
        {
            List<Producto>? listaList = new List<Producto>();
            try
            {
                string Respuesta = "";
                Respuesta = this.conexion.TraerValor("prc_PersonaInsertar", sTipoDocumento,sRucPersona, sRazonSocial, sTelefono, scorreo);
                this.conexion.Dispose();
                return Respuesta;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}
