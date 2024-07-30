using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingManagement.Model
{
    public class MeetingItems
    {
        [Key]
        public int MeetingItemID { get; set; }
        public int MeetingID { get; set; }

        [Required(ErrorMessage = "Please enter a comment.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "A date is required.")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Person Responsible is required.")]
        public string PersonResponsible { get; set; }

        [ForeignKey("MeetingID")]
        public Meeting Meeting { get; set; }
        public ICollection<Status> Statuses { get; set; }
    }
}
