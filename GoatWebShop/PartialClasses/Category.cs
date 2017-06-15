using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GoatWebShop.Models;

namespace GoatWebShop.Models

{
    [MetadataType(typeof(CategoryMetaData))]
    public partial class Category
    {
    }

    public class CategoryMetaData
    {
        [Required]
        [Display(Name = "Category")]
        public string Category1 { get; set; }

        [Required]
        public string Description { get; set; }

    }
}