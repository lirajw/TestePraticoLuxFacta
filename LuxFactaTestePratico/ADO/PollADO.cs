using LuxFactaTestePratico.Interfaces;
using LuxFactaTestePratico.Lib;
using LuxFactaTestePratico.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LuxFactaTestePratico.ADO
{
    public class PollADO : IPoll
    {
        private IConexao Conexao { get; set; }
        private IOptions Options { get; set; }

        public PollADO(IConexao conexao)
        {
            Conexao = conexao;
        }

        public PollADO(IConexao conexao, IOptions options)
        {
            Conexao = conexao;
            Options = options;
        }

        private int GetSequence(bool FecharConn = true)
        {
        
            string vSql = "SELECT NEXT VALUE FOR Seq_Poll";

            var result = Conexao.Funcoes.consultaRapidaSQL(Conexao.GetConexao, vSql, FecharConn);

            return Convert.ToInt32(result[0]);            

        }

        private void ImplementPoll(int Poll_Id)
        {
             string vSQl = $@"UPDATE Poll SET Poll_Views = Poll_Views + 1
                           WHERE Poll_Id = {Poll_Id}";

            Conexao.Funcoes.Exec(Conexao.GetConexao, vSQl);
        }

        public PollReturn InsertPoll(PollInsert poll)
        {
            int? Sequence = null;

            SqlTransaction transaction = null;

            try
            {
                // Caso ocorra algum erro a sequencia é queimada.
                Sequence = GetSequence(false);

                transaction = Conexao.GetConexao.BeginTransaction();                

                string vSql = $@"INSERT INTO Poll (Poll_Id, Poll_Description)
                                VALUES ({Sequence}, '{poll.poll_description}')";

                Conexao.Funcoes.Exec(Conexao.GetConexao, vSql, transaction, false);
                
                foreach (var item in poll.options)
                {
                    Options.InsertOptions(Conexao.GetConexao, item, Sequence ?? 0, transaction, false);
                }                

                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction?.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                // Conexao manteve-se aberta por conta da transaction
                Conexao.CloseConection();
            }

            

            return new PollReturn(Sequence);

        }

        public Stats SelectStats(int Poll_Id)
        {
            string vSQL = $@"Select Poll_Id,Poll_Views, Opt_Id, Opt_Votes
                            FROM Poll p
                            LEFT JOIN Options o on o.Opt_Poll_Id = p.Poll_Id 
                            WHERE p.Poll_Id = {Poll_Id}";

            DataTable dtRetorno = Conexao.Funcoes.Consulta(Conexao.GetConexao, vSQL);

            var stats = from dt in dtRetorno.AsEnumerable()
                            group dt by dt.Field<int>("Poll_Id")
                            into gp
                            select new Stats()
                            {
                                views = gp.First().Field<int>("Poll_Views"),
                                votes = gp.Select(X => new Votes()
                                                            {
                                                                option_id = X.Field<int>("Opt_Id"),
                                                                qty = X.Field<int>("Opt_Votes"),

                                                            }
                                                    ).ToList()
                            };

            if (stats.Count() > 0)
            {
                return stats.First();
            }
            else
            {
                return null;
            }
           
        }

        public Poll SelectPoll(int Poll_Id)
        {

            string vSQL = $@"SELECT Poll_Id, Poll_Description, Poll_Views, Opt_Id, Opt_Poll_Id, Opt_Description, Opt_Votes
                            FROM Poll p
                            LEFT JOIN Options o on o.Opt_Poll_Id = p.Poll_Id 
                            WHERE p.Poll_Id = {Poll_Id}";

            DataTable dtRetorno = Conexao.Funcoes.Consulta(Conexao.GetConexao, vSQL);

            var poll = from dt in dtRetorno.AsEnumerable()
                        group dt by dt.Field<int>("Poll_Id")
                            into gp
                        select new Poll()
                        {
                            poll_id = gp.First().Field<int>("Poll_Id"),
                            poll_description = gp.First().Field<string>("Poll_Description"),
                            options = gp.Select(X => new Option()
                            {
                                option_id = X.Field<int>("Opt_Id"),
                                option_description = X.Field<string>("Opt_Description"),

                            }
                                                ).ToList()
                        };

            if (poll.Count() > 0)
            {
                // Implementa uma visualização na enquete.
                ImplementPoll(poll.First().poll_id);

                return poll.First();
            }
            else
            {
                return null;
            }
            
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}