using LuxFactaTestePratico.Interfaces;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace LuxFactaTestePratico.Lib
{
    public class Conexao : IConexao
    {
        private int timeOut { get; set; }
        protected SqlConnection SqlConnection;

        //Retorna Conexao Aberta
        public SqlConnection GetConexao
        {
            get {
                    if (SqlConnection != null && SqlConnection.State == System.Data.ConnectionState.Open)
                        return SqlConnection;

                    SqlConnection = new SqlConnection(StringConexao + $" Connection Timeout={timeOut};");
                    SqlConnection.Open();

                    return SqlConnection;
            }
        }

        //Retorna string de conexao
        public string StringConexao
        {
            get
            {
                var conn = ConfigurationManager.ConnectionStrings["Luxfacta"];
                var connString = new SqlConnectionStringBuilder(conn.ConnectionString);

                return connString.ConnectionString;
            }
        }

        public  IFuncoesBD Funcoes { get; set;}

        public Conexao(IFuncoesBD funcoesBD, int timeOut = 60)
        {
            Funcoes = funcoesBD;
            this.timeOut = timeOut;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                SqlConnection.Dispose();
            }
        }

        public void CloseConection()
        {
            if (SqlConnection.State == System.Data.ConnectionState.Open)
                SqlConnection.Close();
        }
    }
}