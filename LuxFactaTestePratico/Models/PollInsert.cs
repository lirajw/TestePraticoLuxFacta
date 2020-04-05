using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuxFactaTestePratico.Models
{
    public class PollInsert
    {
        public string poll_description { get; set; }
        public string[] options { get; set; }
    }    
}