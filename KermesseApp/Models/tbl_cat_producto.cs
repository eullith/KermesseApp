//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KermesseApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class tbl_cat_producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_cat_producto()
        {
            this.tbl_productos = new HashSet<tbl_productos>();
        }
    
        public int id_cat_producto { get; set; }
        [Display(Name = "Nombre de la categor�a")]
        [Required(ErrorMessage = "Escriba el nombre de la categor�a")]
        [StringLength(50, ErrorMessage = "El valor m�ximo de caracteres permitida es de 50")]
        public string nombre { get; set; }
        [Display(Name = "Descripci�n de la categor�a")]
        [Required(ErrorMessage = "Escriba la descripci�n de la categor�a")]
        [StringLength(100, ErrorMessage = "El valor m�ximo de caracteres permitida es de 100")]
        public string descripcion { get; set; }
        
        public int estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_productos> tbl_productos { get; set; }
    }
}
