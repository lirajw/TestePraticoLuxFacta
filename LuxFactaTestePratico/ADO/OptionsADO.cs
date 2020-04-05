using LuxFactaTestePratico.Interfaces;
using LuxFactaTestePratico.Lib;
using LuxFactaTestePratico.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LuxFactaTestePratico.ADO
{
    public class OptionsADO : IOptions
    {
        IConexao Conexao { get; set; }

        public OptionsADO(IConexao conexao)
        {
            Conexao = conexao;
        }

        public void InsertOptions(SqlConnection conn, string opt, int IdPoll, SqlTransaction transac = null, bool FecharConn = true)
        {

            string vSql = $@"Insert Into Options(Opt_Id, Opt_Poll_Id, Opt_Description)
                             VALUES((SELECT ISNULL(MAX(Opt_Id), 0) + 1 FROM Options WHERE Opt_Poll_Id = {IdPoll}), {IdPoll}, '{opt}')";

            Conexao.Funcoes.Exec(conn, vSql, transac, FecharConn);

        }

        public  void ImplementVote(int Poll_Id, int Opt_Id)
        {
            string vSQl = $@"UPDATE Options SET Opt_Votes = Opt_Votes + 1
                                WHERE Opt_Id = {Poll_Id}
                                AND Opt_Poll_Id = {Opt_Id}";

            Conexao.Funcoes.Exec(Conexao.GetConexao, vSQl);            

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}