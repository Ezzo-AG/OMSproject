using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSproject.Models
{
    [Table("colors")]
    public class Color
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product_Id { get; set; }

        [Key]
        [Column(TypeName = "varchar(150)")]
        public string ColorName { get; set; } = String.Empty;
        
        [Required]
        public int Quantity { get; set; }

       
        [ForeignKey("Product_Id")]
        public virtual Product? Product { get; private set; }

        [NotMapped]
        public bool IsDeleted { get; set; } = false;
    }
}
