using System.ComponentModel.DataAnnotations;
using System;
namespace ChefsNDishes.Models
{
    public class Dish
    {
        [Key]
        public int DishId {get;set;}

        [Required]
        public int ChefId {get;set;} // foregin key

        [Required]
        public string Name {get;set;}

        [Required]
        [Range(1,5)]
        public int Tastiness {get;set;}

        [Required]
        [Range(0, 1500)]
        public int Calories {get;set;}

        [Required]
        public string Description {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        // Navigation property 
        public Chef Creator {get;set;}

    }



}