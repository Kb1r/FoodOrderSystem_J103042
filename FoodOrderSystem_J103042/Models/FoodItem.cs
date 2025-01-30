﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderSystem.Models
{
    public class FoodItem
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Item_Name { get; set; }
        [StringLength(255)]
        public string Item_Desc { get; set; }

        public bool? Available { get; set; }
        public bool Vegetarian { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal? Price { get; set; }
    }
}
