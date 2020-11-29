using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace TestWebApp.Models
{
    public class CustomerModel
    {
        public int customerId { get; set; }
        [DisplayName("First Name")]
        public string firstName { get; set; }
        [DisplayName("Last Name")]
        public string lastName { get; set; }
        [DisplayName("Age")]
        public string age { get; set; }
        [DisplayName("City")]
        public string city { get; set; }
        
    }
}
