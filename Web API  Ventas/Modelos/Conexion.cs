using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using Web_API__Ventas.Models;

namespace Web_API__Ventas.Modelos
{
    public class Conexion : ConexionDB, ICConexion
    {
        private string uuid;
        public Conexion()
        {
            GenerarConexion();
        }
        private void GenerarConexion()
        {
            string bd = "PuntoVenta";
            //var cadena = "Data Source=" + "DESKTOP-DPTJEJC" + ";Initial Catalog=" + bd + ";Integrated Security=True";
            //var cadena = "Server=tcp:comercialayrcalca.database.windows.net,1433;Initial Catalog=PuntoVenta;Persist Security Info=False;User ID=fsmr159;Password=LaPurf666;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var cadena = "Data Source=SQL8001.site4now.net;Initial Catalog=db_a95d3b_puntoventa;User Id=db_a95d3b_puntoventa_admin;Password=LaPurf666";
            CadenaConexion = cadena;
        }


        public void DisposeCommand()
        {
        }

        public void DisposeConexion()
        {
        }

        public int Ejecutar(string ProcedimientoAlmacenado)
        {
            int n = 0;
            if (!enTransaccion)
            {
                using (SqlConnection con = Conectar)
                {
                    try
                    {
                        using (IDbCommand command = new SqlCommand(ProcedimientoAlmacenado, con))
                        {
                            con.Open();
                            command.CommandType = CommandType.StoredProcedure;
                            n = command.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }
                }
            }
            else
            {
                try
                {
                    using (IDbCommand command = new SqlCommand(ProcedimientoAlmacenado, Conectar))
                    {
                        command.Transaction = transaccion;
                        command.CommandType = CommandType.StoredProcedure;
                        n = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    AbortarTransaccion();
                    throw new Exception("Se aborto la Transaccion", ex);
                }
            }

            return n;
        }


        public int Ejecutar(string ProcedimientoAlmacenado, params object[] Parametros)
        {
            int n = 0;
            if (!enTransaccion)
            {
                using (SqlConnection con = Conectar)
                {
                    try
                    {
                        using (IDbCommand command = Comando(ProcedimientoAlmacenado))
                        {
                            command.Connection = con;
                            command.CommandType = CommandType.StoredProcedure;
                            CargarParametros(command, Parametros);
                            con.Open();
                            n = command.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }
                }
            }
            else
            {
                try
                {
                    using (IDbCommand command = Comando(ProcedimientoAlmacenado))
                    {
                        command.Connection = Conectar;
                        command.Transaction = transaccion;
                        command.CommandType = CommandType.StoredProcedure;
                        CargarParametros(command, Parametros);
                        n = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    AbortarTransaccion();
                    throw new Exception("Se aborto la Transaccion", ex);
                }
            }
            return n;
        }

        


        public string GetBase()
        {
            return "";
        }

        public string GetCadenaConexion()
        {
            return CadenaConexion;
        }

        public string GetServidor()
        {
            return "";
        }

        public void SetBase(string pBase)
        {
        }

        public void SetCadenaConexion(string cadena)
        {
            CadenaConexion = cadena;
        }

        public void SetServidor(string servidor)
        {
        }

        public byte[] TraerByteArray(string ProcedimientoAlmacenado, params object[] Parametros)
        {
            DataRow r = TraerDataRow(ProcedimientoAlmacenado, Parametros);
            return (byte[])r[0];
        }

        public DataRow TraerDataRow(string ProcedimientoAlmacenado)
        {
            DataSet data = TraerDataSet(ProcedimientoAlmacenado);
            return data.Tables[0].Rows[0];
        }

        public DataRow TraerDataRow(string ProcedimientoAlmacenado, params object[] Parametros)
        {
            DataSet data = TraerDataSet(ProcedimientoAlmacenado, Parametros);
            return data.Tables[0].Rows[0];
        }

        public DataSet TraerDataSet(string ProcedimientoAlmacenado)
        {
            DataSet mDataSet = new DataSet();
            if (!enTransaccion)
            {
                using (SqlConnection con = Conectar)
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand(ProcedimientoAlmacenado, con))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                con.Open();
                                adapter.Fill(mDataSet);
                                con.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }
                }
            }
            else
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(ProcedimientoAlmacenado, Conectar))
                    {
                        command.Transaction = transaccion;
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(mDataSet);
                        }
                    }
                }
                catch (Exception ex)
                {
                    AbortarTransaccion();
                    throw new Exception("Se aborto la transaccion", ex);
                }
            }
            return mDataSet;
        }

        public DataSet TraerDataSet(string ProcedimientoAlmacenado, params object[] Parametros)
        {
            DataSet mDataSet = new DataSet();
            if (!enTransaccion)
            {
                using (SqlConnection con = Conectar)
                {
                    try
                    {
                        using (SqlCommand command = Comando(ProcedimientoAlmacenado))
                        {
                            command.Connection = con;
                            command.CommandType = CommandType.StoredProcedure;
                            CargarParametros(command, Parametros);
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                con.Open();
                                adapter.Fill(mDataSet);
                                con.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }
                }
            }
            else
            {
                try
                {
                    using (SqlCommand command = Comando(ProcedimientoAlmacenado))
                    {
                        command.Connection = Conectar;
                        command.Transaction = transaccion;
                        command.CommandType = CommandType.StoredProcedure;
                        CargarParametros(command, Parametros);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(mDataSet);
                        }
                    }
                }
                catch (Exception ex)
                {
                    AbortarTransaccion();
                    throw new Exception("Se aborto la transaccion", ex);
                }
            }
            return mDataSet;
        }

        public DataSet TraerDataSet_Consulta(string Consulta)
        {
            DataSet mDataSet = new DataSet();
            if (!enTransaccion)
            {
                using (SqlConnection con = Conectar)
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand(Consulta, con))
                        {
                            command.CommandType = CommandType.Text;
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                con.Open();
                                adapter.Fill(mDataSet);
                                con.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }
                }
            }
            else
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(Consulta, Conectar))
                    {
                        command.Transaction = transaccion;
                        command.CommandType = CommandType.Text;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(mDataSet);
                        }
                    }
                }
                catch (Exception ex)
                {
                    AbortarTransaccion();
                    throw new Exception("Se aborto la transaccion", ex);
                }
            }
            return mDataSet;
        }

        public DataTable TraerDataTable(string ProcedimientoAlmacenado)
        {
            DataSet dataSet = TraerDataSet(ProcedimientoAlmacenado);
            return dataSet.Tables[0].Copy();
        }

        public DataTable TraerDataTable(string ProcedimientoAlmacenado, params object[] Parametros)
        {
            DataSet dataSet = TraerDataSet(ProcedimientoAlmacenado, Parametros);
            return dataSet.Tables[0].Copy();
        }

        public string TraerValor(string ProcedimientoAlmacenado)
        {
            DataSet data = TraerDataSet(ProcedimientoAlmacenado);
            return data.Tables[0].Rows[0][0].ToString();
        }

        public string TraerValor(string ProcedimientoAlmacenado, params object[] Parametros)
        {
            DataSet data = TraerDataSet(ProcedimientoAlmacenado, Parametros);
            return data.Tables[0].Rows[0][0].ToString();
        }

        protected void CargarParametros(IDbCommand oComando, Object[] Args)
        {

            int Limite = oComando.Parameters.Count;
            for (int i = 1; i < oComando.Parameters.Count; i++)
            {
                SqlParameter P = (SqlParameter)oComando.Parameters[i];
                if (i <= Args.Length)
                {
                    P.Value = Args[i - 1];
                }
                else
                    P.Value = null;
            }
        }

        protected SqlCommand Comando(string ProcedimientoAlmacenado)
        {
            SqlCommand oComando = new SqlCommand(ProcedimientoAlmacenado);
            if (!enTransaccion)
            {
                using (SqlConnection con = Conectar)
                {
                    try
                    {
                        con.Open();
                        oComando = new SqlCommand(ProcedimientoAlmacenado, con);
                        oComando.CommandType = CommandType.StoredProcedure;
                        oComando.CommandTimeout = 100000;
                        SqlCommandBuilder.DeriveParameters(oComando);
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }
                }
            }
            else
            {
                try
                {
                    oComando = new SqlCommand(ProcedimientoAlmacenado, Conectar);
                    oComando.Transaction = transaccion;
                    oComando.CommandType = CommandType.StoredProcedure;
                    oComando.CommandTimeout = 100000;
                    SqlCommandBuilder.DeriveParameters(oComando);
                }
                catch (Exception ex)
                {
                    AbortarTransaccion();
                    throw new Exception("Se aborto la transaccion", ex);
                }
            }
            return oComando;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public string GetUuid()
        {
            throw new NotImplementedException();
        }

        public void GenerarConexion(long id)
        {
            throw new NotImplementedException();
        }
    }
}
