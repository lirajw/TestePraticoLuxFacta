using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LuxFactaTestePratico.Interfaces
{
    public interface IFuncoesBD
    {
        DataTable Consulta(SqlConnection conexaoDB, string sql, SqlTransaction tr = null, bool FecharConn = true);
        Object[] consultaRapidaSQL(SqlConnection pConexaoBD, string pSQL, bool FecharConn = true);
        Object[] consultaRapidaSQL(SqlConnection pConexaoBD, string pSQL, SqlTransaction pTran, bool FecharConn = true);
        int Exec(SqlConnection conexaoDb, string sql, SqlTransaction tr = null, bool FecharConn = true);
    }
}