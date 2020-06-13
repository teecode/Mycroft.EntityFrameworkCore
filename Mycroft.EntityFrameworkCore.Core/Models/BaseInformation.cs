using System;
using System.Collections.Generic;
using System.Text;

namespace Mycroft.EntityFrameworkCore.Core.Models
{
    public class BaseInformation :BaseEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName {
            get
            {
                return $"{LastName} {FirstName} {MiddleName}";
            }
        }

        public int State { get; set; }
        public int LGA { get; set; }
        public string Address { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public string SecondaryPhoneNumber { get; set; }
        public string Email { get; set; }
        public string DateofBirth { get; set; }
    }
}
