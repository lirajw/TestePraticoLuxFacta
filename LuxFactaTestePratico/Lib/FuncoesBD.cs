using LuxFactaTestePratico.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LuxFactaTestePratico.Lib
{
    public class FuncoesBD : IFuncoesBD
    {
        public DataTable Consulta(SqlConnection pConexaoDB, string pSql, SqlTransaction pTran = null, bool FecharConn = true)
        {
            try
            {
                DataTable dtConsulta = new DataTable();
                SqlDataAdapter daConsulta = new SqlDataAdapter(pSql, pConexaoDB);
                daConsulta.SelectCommand.Transaction = pTran;
                daConsulta.Fill(dtConsulta);

                return dtConsulta;
            }
            finally
            {
                if (FecharConn)
                    pConexaoDB.Close();
            }

        }

        public Object[] consultaRapidaSQL(SqlConnection pConexaoBD, string pSQL, bool FecharConn = true)
        {
            return consultaRapidaSQL(pConexaoBD, pSQL, null, FecharConn);
        }

        public  Object[] consultaRapidaSQL(SqlConnection pConexaoBD, string pSQL, SqlTransaction pTran, bool FecharConn = true)
        {
            try
            {
                DataTable dtConsulta = new DataTable();

                using (SqlDataAdapter daConsulta = new SqlDataAdapter(pSQL, pConexaoBD))
                {
                    if (pTran != null)
                    {
                        daConsulta.SelectCommand.Transaction = pTran;
                    }

                    // Preenche o DataTable
                    daConsulta.Fill(dtConsulta);

                    if (dtConsulta.Rows.Count != 0)
                    {
                        // Retornado DataTable
                        return dtConsulta.Rows[0].ItemArray;
                    }
                    else
                    {
                        // Retornado DataTable
                        return null;
                    }
                }
            }
            finally
            {
                if (FecharConn)
                    pConexaoBD.Close();
            }

            
        }

        public int Exec(SqlConnection pConexaoBD, string pSql, SqlTransaction pTran = null, bool FecharConn = true)
        {
            try
            {
                if (pConexaoBD.State != ConnectionState.Open)
                    pConexaoBD.Open();

                SqlCommand sqlComando = new SqlCommand(pSql, pConexaoBD) { Transaction = pTran };
                sqlComando.CommandTimeout = 600;
                return sqlComando.ExecuteNonQuery();
            }
            finally
            {

                if (FecharConn)
                    pConexaoBD.Close();
            }

        }
    }
}