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
    
    public partial class report
    {
        public int Id { get; set; }
        public Nullable<int> adId { get; set; }
        public string complain { get; set; }
    
        public virtual ad ad { get; set; }
    }
}
