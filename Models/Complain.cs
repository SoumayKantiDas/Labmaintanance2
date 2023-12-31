//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Labmaintanance2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Complain
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Complain()
        {
            this.Actions = new HashSet<Action>();
        }
    
        public int complain_id { get; set; }
        public int user_id { get; set; }
        public string Name_Of_the_Item { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public int PriorityId { get; set; }
        public string status { get; set; }
        public int Repaired_StausId { get; set; }
        public byte[] image_data { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Action> Actions { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Repaired_Staus Repaired_Staus { get; set; }
        public virtual User User { get; set; }
    }
}
