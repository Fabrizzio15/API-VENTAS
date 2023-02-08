using appApiRestAsistencia.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Web_API__Ventas.Modelos
{ 
    public class ServiceBase
    {
        private string uuid;
        private ICConexion _conexion;

        public ServiceBase(ICConexion pConexion)
        {
            uuid = Guid.NewGuid().ToString("N");
            Debug.WriteLine("Creada instacia de servicio" + uuid);
            _conexion = pConexion;
        }

        public ICConexion conexion
        {
            get{ return _conexion; }
        }

        public ICConexion GetConexion()
        {
            return this._conexion;
        }

        public void SetConexion(ref ICConexion conexion)
        {
            this._conexion = conexion;
        }

        public void SetCadenaConexion(string cadena)
        {
            this._conexion.SetCadenaConexion(cadena);
        }



        public virtual void Dispose()
        {
            //if (_conexion != null)
            //{
                _conexion.Dispose();
            Debug.WriteLine($"Servicio {uuid} eliminado");
            //}
        }
    }
}