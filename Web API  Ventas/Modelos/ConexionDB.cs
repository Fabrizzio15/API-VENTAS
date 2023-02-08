using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace appApiRestAsistencia.Models
{
    public class ConexionDB
    {
        private SqlConnection conexion;
        private SqlTransaction currentTransaction;
        private string cadena;

        //Recupera la una conexion si esta en trasaccion la anterior o sino una nueva
        protected SqlConnection Conectar
        {
            get
            {
                if (currentTransaction != null && conexion != null)
                {
                    return conexion;
                }
                else
                {
                    return new SqlConnection(cadena);
                }
            }
        }

        public void IniciarTransaccion()
        {
            conexion = new SqlConnection(cadena);
            conexion.Open();
            currentTransaction = conexion.BeginTransaction();
        }

        public void TerminarTransaccion()
        {
            if (currentTransaction != null)
            {
                currentTransaction.Commit();
                currentTransaction.Dispose();
                currentTransaction = null;
            }
            if (conexion.State == ConnectionState.Open)
            {
                conexion.Close();
            }
            conexion.Dispose();
            conexion = null;
        }

        public void AbortarTransaccion()
        {
            if (currentTransaction != null)
            {
                currentTransaction.Rollback();
                currentTransaction.Dispose();
                currentTransaction = null;
            }
            if (conexion != null)
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
                conexion.Dispose();
            }
            conexion = null;
        }

        public virtual void Dispose()
        {
            if (currentTransaction != null)
            {
                currentTransaction.Rollback();
                currentTransaction.Dispose();
            }
            if (conexion != null)
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
                conexion.Dispose();
            }
            currentTransaction = null;
            conexion = null;
        }

        protected bool enTransaccion
        {
            get
            {
                return currentTransaction != null;
            }
        }

        protected SqlTransaction transaccion
        {
            get { return currentTransaction; }
        }

        protected string CadenaConexion
        {
            get { return cadena; }
            set
            {
                if (enTransaccion)
                {
                    AbortarTransaccion();
                }
                if (conexion != null)
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                    conexion.Dispose();
                    conexion = null;
                }
                cadena = value;
            }
        }
    }
}
