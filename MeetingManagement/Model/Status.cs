using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingManagement.Model
{
    public class Status
    {
        [Key]
        public int MeetingItemStatusID { get; set; }
        public int MeetingItemID { get; set; }

        [Required(ErrorMessage ="Please enter a comment.")]
        public string StatusDescription { get; set; }

        [Required(ErrorMessage ="Please enter the person responsible.")]
        public string ResponsiblePerson { get; set; }

        [Required(ErrorMessage ="Please enter the Action Required.")]
        public string ActionRequired { get; set; }

        public DateTime? StatusDate { get; set; }

        public MeetingItems MeetingItem { get; set; }

    }
}
