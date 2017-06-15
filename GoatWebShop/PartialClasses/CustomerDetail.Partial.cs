using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoatWebShop.Models
{
    [MetadataType(typeof(CustomerDetailMetaData))]
    public partial class CustomerDetail
    {
    }

    public class CustomerDetailMetaData
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime Birthday { get; set; }

        [Required]
        public string Adress { get; set; }

        //...
    }
}