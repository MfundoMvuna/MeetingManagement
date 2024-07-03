using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingManagement.Model
{
    public class MeetingType
    {
        [Key]
        [Required]
        public int MeetingTypeID { get; set; }

        public string? MeetingTypeName { get; set; }
    }
}
