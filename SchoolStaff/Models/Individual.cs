using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ContosoUniversity.Models
{
    public class Individual
    {
        public int ID { get; set; }
        [Required, StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }
        [StringLength(50, ErrorMessage = "Middle Name cannot be longer than 50 characters.")]
        public string MiddleName { get; set; }
        [Required, StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [RegularExpression("[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}")]
        public string Email { get; set; }
        [RegularExpression("^\\d{3}-\\d{3}-\\d{4}$")]
        public string ContactPhone { get; set; }
        public string Secret { get; set; }
        public EntityState State { get; set; }
    }
}