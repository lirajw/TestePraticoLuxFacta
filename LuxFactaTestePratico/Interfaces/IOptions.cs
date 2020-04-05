using LuxFactaTestePratico.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LuxFactaTestePratico.Interfaces
{
    public interface IOptions : IDisposable
    {
        void InsertOptions(SqlConnection conn, string opt, int IdPoll, SqlTransaction transac = null, bool FecharConn = true);
        void ImplementVote(int Poll_Id, int Opt_Id);

    }
}