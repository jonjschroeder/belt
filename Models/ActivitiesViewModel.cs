using System;
using System.ComponentModel.DataAnnotations;
 
namespace belt.Models
{
    public class ActivitiesViewModel 
    {

        
        [Required]
        [MinLength(2)]
  
        public string Title { get; set; }

        [Required]
        [MinLength(2)]


        public string Time { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public string Hours { get; set; }
        public string Description { get; set; }


    }
}