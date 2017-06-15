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
        //[RegularExpression(@"\d+(\.|,\d{1,2})?", ErrorMessage = "Invalid price")]
        //[DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Product")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public byte[] Photo { get; set; }

        [Required]
        [Display(Name = "Available")]
        public int AmountInStock { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int Category_ID { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public int Supplier_ID { get; set; }

        public virtual Supplier Supplier { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime Deleted { get; set; }

    }


}