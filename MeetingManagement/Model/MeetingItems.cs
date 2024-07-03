using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingManagement.Model
{
    public class MeetingItems
    {
        [Key]
        [Required]
        public int MeetingItemID { get; set; }

        [Required]
        public int MeetingID { get; set; }

        [Required]
        public string? Description { get; set;}

        [Required]
        public DateTime DueDate { get; set;}

        public string PersonResponsible { get; set; }

        [Required]
        public int MeetingItemStatusID { get; set;}
    }
}
