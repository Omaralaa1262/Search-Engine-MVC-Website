//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IR_MVCwebsite
{
    using System;
    using System.Collections.Generic;
    
    public partial class tockenz
    {
        public int id { get; set; }
        public string sring { get; set; }
        public Nullable<int> doc_no { get; set; }
        public string position { get; set; }
        public Nullable<int> frequency { get; set; }
    
        public virtual crawler_data crawler_data { get; set; }
    }
}
