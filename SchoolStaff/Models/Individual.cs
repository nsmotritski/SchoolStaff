using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ContosoUniversity.Models
{
    public class Individual
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string ContactPhone { get; set; }
        public string Secret { get; set; }
        public EntityState State { get; set; }
    }
}