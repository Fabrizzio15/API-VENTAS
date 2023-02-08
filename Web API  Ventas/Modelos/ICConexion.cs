using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_API__Ventas
{
    public interface ICConexion
    {
        string GetCadenaConexion();
        void SetCadenaConexion(string cadena);
        string GetBase();
        void SetBase(string pBase);
        int Ejecutar(string ProcedimientoAlmacenado);
        int Ejecutar(string ProcedimientoAlmacenado, params Object[] Parametros);
        void IniciarTransaccion();
        string GetServidor();
        void SetServidor(string servidor);
        void TerminarTransaccion();
        void AbortarTransaccion();
        DataRow TraerDataRow(String ProcedimientoAlmacenado);
        DataRow TraerDataRow(String ProcedimientoAlmacenado, params Object[] Parametros);
        DataSet TraerDataSet(String ProcedimientoAlmacenado);
        DataSet TraerDataSet(String ProcedimientoAlmacenado, params Object[] Parametros);
        DataSet TraerDataSet_Consulta(String Consulta);
        DataTable TraerDataTable(String ProcedimientoAlmacenado);
        DataTable TraerDataTable(String ProcedimientoAlmacenado, params Object[] Parametros);
        String TraerValor(String ProcedimientoAlmacenado);
        String TraerValor(String ProcedimientoAlmacenado, params Object[] Parametros);
        void GenerarConexion(long id);
        void Dispose();
        string GetUuid();
    }
}
