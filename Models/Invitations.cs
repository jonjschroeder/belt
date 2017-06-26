using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using belt.Models;

namespace belt.Models
{

    public class Invitation : BaseEntity
    {
        [Key]
        public int InvitationId { get; set; }  //or put in the[key]

        public int  UserId { get; set; }
        public User User { get; set; }

        public int ActivitiesId { get; set; }

        public Activity Activities { get; set; }
        
    }
}