//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hospital
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lit()
        {
            this.DossierAdmission = new HashSet<DossierAdmission>();
        }
    
        public string Numero_lit { get; set; }
        public Nullable<decimal> ID_Chambre { get; set; }
        public Nullable<decimal> ID_TypeLit { get; set; }
        public decimal Occupe { get; set; }
    
        public virtual Chambre Chambre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DossierAdmission> DossierAdmission { get; set; }
        public virtual TypeLit TypeLit { get; set; }
    }
}
