using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tt.Models;

namespace tt.ViewModels
{
    public class TTVTVM
    {      
        public string selected { get; set; }
        public List<TTVT> ttvts {get; set;}
    }
}