//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sql4.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class area
    {
        public area()
        {
            this.ads = new HashSet<ad>();
            this.cities = new HashSet<city>();
        }
    
        public int area_id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<ad> ads { get; set; }
        public virtual ICollection<city> cities { get; set; }
    }
}