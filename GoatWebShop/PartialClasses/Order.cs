using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GoatWebShop.Models;
using Newtonsoft.Json;

namespace GoatWebShop.Models
{
    [MetadataType(typeof(OrderMetaData))]
    public partial class Order
    {
    }

    public class OrderMetaData
    {
        [Required]
        public string OrderNumber { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime Created { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int OrderStatus_id { get; set; }

        //[Required]
        public string UserId { get; set; }

        [ScaffoldColumn(false)]
        public string SessionUserId { get; set; }

        [JsonIgnore]
        public virtual AspNetUser AspNetUser { get; set; }

        [JsonIgnore]
        public virtual OrderStatu OrderStatu { get; set; }

        //[JsonIgnore]
        public virtual ICollection<OrderRow> OrderRows { get; set; }
    }
}