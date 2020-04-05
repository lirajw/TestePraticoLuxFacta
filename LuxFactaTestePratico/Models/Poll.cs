using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuxFactaTestePratico.Models
{
    public class Poll
    {
      public int poll_id { get; set; }
      public string poll_description { get; set; }
      public List<Option> options { get; set; }
    }
}