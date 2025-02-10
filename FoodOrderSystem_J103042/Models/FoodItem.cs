using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderSystem_J103042.Models
{
    public class FoodItem
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Item_Name { get; set; }

        [StringLength(500)]
        public string Item_Desc { get; set; }

        public bool? Available { get; set; }
        public bool Vegetarian { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal? Price { get; set; }

        public string? ImageUrl { get; set; }

    }
}
