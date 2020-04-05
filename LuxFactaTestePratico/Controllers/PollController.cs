using LuxFactaTestePratico.ADO;
using LuxFactaTestePratico.Interfaces;
using LuxFactaTestePratico.Models;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace LuxFactaTestePratico.Controllers
{        
    public class PollController : ApiController
    {
        // Propriedades retornam uma instancia do objeto configurado no container(LuxFactaIoc)
        public IConexao Conexao => WebApiConfig.Kernel.Get<IConexao>(new ConstructorArgument("funcoesBD",
                                                                                              WebApiConfig.Kernel.Get<IFuncoesBD>()));
        public IOptions Option => WebApiConfig.Kernel.Get<IOptions>(new ConstructorArgument("conexao", Conexao));
        public IPoll Poll => WebApiConfig.Kernel.Get<IPoll>(new ConstructorArgument("conexao", Conexao));                                                            

        public IPoll PollWithOptions => WebApiConfig.Kernel.Get<IPoll>(new ConstructorArgument("conexao", Conexao),
                                                                       new ConstructorArgument("options", Option));


        // GET api/<controller>/5
        [HttpGet]
        [Route("poll/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (IPoll poll = Poll)
                {
                    var retorno = poll.SelectPoll(id);

                    if (retorno == null)
                    {
                        return NotFound();
                    }

                    return Ok(retorno);
                }

 
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // POST api/<controller>
        [HttpPost]
        [Route("poll")]
        public HttpResponseMessage Post([FromBody]PollInsert poll)
        {
            try
            {
                using (IPoll pollADO = Poll)
                {
                    if (poll == null)                    
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Poll não informado!");

                    if (poll == null)
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Options não informado!");

                    var retorno = pollADO.InsertPoll(poll);

                    if (retorno == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, retorno);
                    }
                }
 
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
            }


        }

        // POST api/<controller>
        [HttpPost]
        [Route("poll/{id}/vote")]
        public IHttpActionResult PostVote(int id, [FromBody]Option opt)
        {

            try
            {
                using (IPoll poll = Poll)
                using (IOptions options = Option)
                {
                    if (poll.SelectPoll(id) == null)
                    {
                        return NotFound();
                    }

                    options.ImplementVote(opt.option_id, id);
                    return Ok();
                }


            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        // GET api/<controller>/5
        [HttpGet]
        [Route("poll/{id}/stats")]
        public IHttpActionResult GetStats(int id)
        {
            try
            {
                using (IPoll poll = Poll)
                {
                    var retorno = Poll.SelectStats(id);

                    if (retorno == null)
                    {
                        return NotFound();
                    }

                    return Ok(retorno);
                }

            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
