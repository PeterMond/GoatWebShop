using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GoatWebShop.Models;
using Newtonsoft.Json;

namespace GoatWebShop.Models
{
    [MetadataType(typeof(OrderRowMetaData))]
    public partial class OrderRow
    {
    }

    public class OrderRowMetaData
    {
        [Required]
        [Display(Name = "Order")]
        public int Order_ID { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int Product_ID { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public decimal Price { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }

        //[JsonIgnore]
        public virtual Product Product { get; set; }

    }
}