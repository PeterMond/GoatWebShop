using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GoatWebShop.Models;
using Newtonsoft.Json;

namespace GoatWebShop.Models
{
    [MetadataType(typeof(SupplierMetaData))]
    public partial class Supplier
    {
    }

    public class SupplierMetaData
    {
        [Required]
        [Display(Name = "Supplier")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Address")]
        public int Adress { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }

    }
}