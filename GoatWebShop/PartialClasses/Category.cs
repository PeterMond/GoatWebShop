using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GoatWebShop.Models;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }

    }
}