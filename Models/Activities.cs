using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


 
namespace belt.Models
{
    public class Activity : BaseEntity
    {
        [Key]
        public int ActivitiesId { get; set; }  //or put in the[key]

        public string  Title { get; set; }

        public string Time { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public string Hours { get; set; }
        public string Description { get; set; }
        
        // [Required]
        // [CompareAttribute("Password", ErrorMessage = "Password and Password Confrimation Must match :]")]
        // public string PasswrodConfirmation {get; set;}

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int UserId { get; set; }

        public List<Invitation> Invitations {get; set;}

        public User User {get; set;}

        public Activity(){
            Invitations = new List<Invitation>();
        
        }
    }
}