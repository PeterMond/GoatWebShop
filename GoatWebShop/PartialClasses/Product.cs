using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoatWebShop.Models
{
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }

    public class ProductMetaData
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int AmountInStock { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int Category_ID { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public int Supplier_ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime Deleted { get; set; }

    }


}