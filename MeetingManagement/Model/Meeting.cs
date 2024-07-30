using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingManagement.Model
{
    public class Meeting
    {
        [Key]
        public int MeetingID { get; set; }

        [Required(ErrorMessage = "Title cannot be blank. Please enter a Title for the meeting.")]
        public string Title { get; set; }
        public int MeetingTypeID { get; set; }
        public string MeetingNumber { get; set; }

        [Required(ErrorMessage ="A date is required.")]
        public DateTime Date { get; set; }

        public MeetingType MeetingType { get; set; }
        public ICollection<MeetingItems> MeetingItems { get; set; }

    }
}
