using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace ChefsNDishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId {get;set;}

        [Required]
        public string First_Name {get;set;}

        [Required]
        public int Last_Name {get;set;}

        [Required]
        [DataType(DataType.Date)]
        [BirthdayValidator]
        public DateTime Date_Birth {get;set;}

        public int age {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        //Navigation property 
        public List<Dish> CreatedDishes {get;set;}
    }
    public class BirthdayValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime birthday = Convert.ToDateTime(value);
            DateTime today = DateTime.Now;

            if(birthday>today)
            {
                return new ValidationResult("Not a valid birthday");
            }
            return ValidationResult.Success;

        }
    }

}