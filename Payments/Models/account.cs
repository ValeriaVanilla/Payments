using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Payments.Models
{
    public class account
    {
        /* Empty Constructor */
        public account() { }

        /* Variable Declaration */
        [Key]
        public int Account { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ProfilePicture { get; set; }
        
        public string PhoneNumber { get; set; }

        /* ToString Method */

        override
        public string ToString()
        {
            return "FirstName: " + FirstName + "LastName: " + LastName;
        }

    }
}