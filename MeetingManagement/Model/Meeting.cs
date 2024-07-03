using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingManagement.Model
{
    public class Meeting
    {
        [Key]
        [Required]
        public int MeetingID { get; set; }

        [Required]
        public int MeetingTypeID { get; set;}

        public string MeetingNumber { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public MeetingType MeetingType { get; set; }
    }
}
