using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Payments.Models
{
    public class payment
    {
        /* Empty Constructor */
        public payment() { }

        /* Variable Declaration */

        [Key]
        public int ReferenceNo { get; set; }
        public double Amount { get; set; }

        public string Description { get; set; }

        public string Beneficiary { get; set; }

        public DateTime Date { get; set; }

        /* Foreing Key */
        public int AccountReference { get; set; }


    }
}