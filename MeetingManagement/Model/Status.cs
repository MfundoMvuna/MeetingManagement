using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingManagement.Model
{
    public class Status
    {
        [Key]
        [Required]
        public int MeetingItemStatusID { get; set; }

        [Required]
        public int MeetingItemID { get; set; }

        [Required]
        public int MeetingID { get; set; }

        [Required]
        public string? StatusDescription { get; set;}

        [Required]
        public string? ResponsiblePerson { get; set;}

        [Required]
        public string? ActionRequired { get; set; }

        public MeetingItems MeetingItem { get; set; }
        public Meeting Meeting { get; set; }
    }
}
