﻿using System.ComponentModel.DataAnnotations;

namespace OMSproject.DTO
{
    public class ItemDTO
    {
        [Key]
        public int? OrderId { get; set; }

        public int? Product_Id { get; set; }

        public string ColorName { get; set; } = String.Empty;

        public int Quantity { get; set; }

        public List<ProductDTO>? Products { get; set; } = new List<ProductDTO>();

        public List<ColorDTO>? Colors { get; set; } = new List<ColorDTO>();


    }
}
