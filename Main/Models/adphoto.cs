using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sql4.Models
{
    public class adphoto
    {
       

        public IEnumerable<sql4.Models.ad> ads{get;set;}
        public IEnumerable<sql4.Models.report> reports { get; set; }

    }
}