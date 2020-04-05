using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuxFactaTestePratico.Models
{
    public class PollReturn
    {
        public int? poll_id { get; set; } 

        public PollReturn(int? id)
        {
            poll_id = id;
        }
    }
}