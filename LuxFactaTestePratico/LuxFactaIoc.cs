using LuxFactaTestePratico.ADO;
using LuxFactaTestePratico.Interfaces;
using LuxFactaTestePratico.Lib;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuxFactaTestePratico
{
    public class LuxFactaIoc : NinjectModule
    {
        //Metodo configura qual objeto sera instanciado pelo injetor de dependencia.
        public override void Load()
        {
            Bind<IFuncoesBD>().To<FuncoesBD>();
            Bind<IConexao>().To<Conexao>();
            Bind<IPoll>().To<PollADO>();
            Bind<IOptions>().To<OptionsADO>();
        }
    }

}