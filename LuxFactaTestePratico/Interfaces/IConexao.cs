using LuxFactaTestePratico.Lib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LuxFactaTestePratico.Interfaces
{
    public interface IConexao : IDisposable
    {
         string StringConexao { get; }
        SqlConnection GetConexao { get; }
        IFuncoesBD Funcoes { get; set; }

        void CloseConection();

    }
}