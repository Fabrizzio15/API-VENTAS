using System;

namespace Web_API__Ventas.Modelos
{
    public class Operacion
    {
        public int idOperacion { get; set; }
        public string fechaOperacion { get; set; }    
        public double monto { get; set; }
        public string usuario { get; set; }
        public string nombreSucursal { get; set; }
        public string RazonSocial { get; set; }
        public string Correlativo { get; set; }

    }
}
