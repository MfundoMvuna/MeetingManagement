using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetingManagement.Data;
using MeetingManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;

namespace MeetingManagement.Pages
{
    public class CreateModel : PageModel
    {
        private readonly MeetingContext _context;

        public CreateModel(MeetingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Meeting Meeting { get; set; } = new Meeting();

        public SelectList MeetingTypes { get; set; }
        public int MeetingTypeID { get; set; }
        public DateTime Date { get; set; }
        public List<MeetingItems> PrevMeetingItems { get; set; }
        public int LastMeetingID {  get; set; }
        public MeetingItems MeetingItems { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var meetingTypes = await _context.MeetingTypes.ToListAsync();
            if (meetingTypes != null && meetingTypes.Any())
            {
                MeetingTypes = new SelectList(meetingTypes, "MeetingTypeID", "MeetingTypeName");
            }

            var now = DateTime.Now;

            Meeting.Date = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);

            var lastMeeting = await _context.Meetings
                .OrderByDescending(m => m.MeetingID)
                .FirstOrDefaultAsync();

            if (lastMeeting != null)
            {
                LastMeetingID = lastMeeting.MeetingID;
            }

            return Page();
        }

        public async Task<IActionResult> OnGetGetMeetingItemsAsync(int meetingTypeID, int meetingID)
        {
            var lastMeeting = await _context.Meetings
                .Where(m => m.MeetingTypeID == meetingTypeID)
                .OrderByDescending(m => m.MeetingID)
                .FirstOrDefaultAsync();

            if (lastMeeting != null) 
            { 
                meetingID = lastMeeting.MeetingID;
            }

            PrevMeetingItems = await _context.MeetingItems
                    .Where(mi => mi.MeetingID == meetingID)
                    .ToListAsync();

            var meetingItems = await _context.MeetingItems
                .Where(mi => mi.Meeting.MeetingTypeID == meetingTypeID && mi.MeetingID == meetingID)
                .Select(mi => new
                {
                    mi.MeetingItemID,
                    mi.Description
                })
                .ToListAsync();

            return new JsonResult(meetingItems);
        }

        public async Task<IActionResult> OnPostAsync(List<int> selectedMeetingItems)
        {            

            var lastMeeting = await _context.Meetings.OrderByDescending(m => m.MeetingID).FirstOrDefaultAsync();
            var meetingType = await _context.MeetingTypes.FindAsync(Meeting.MeetingTypeID);

            int meetingNumber = 1;
            if (lastMeeting != null)
            {
                LastMeetingID = lastMeeting.MeetingID;
                var match = System.Text.RegularExpressions.Regex.Match(lastMeeting.MeetingNumber, @"\d+");
                if (match.Success)
                {
                    meetingNumber = int.Parse(match.Value) + 1;
                }
            }


            Meeting.MeetingNumber = meetingType.MeetingTypeName switch
            {
                "Finance" => $"F{meetingNumber}",
                "Project Team Leaders" => $"P.TL{meetingNumber}",
                _ => $"M{meetingNumber}"
            };

            //_context.Meetings.Add(Meeting);
            //await _context.SaveChangesAsync();

            var now = DateTime.Now;

            foreach (var itemId in selectedMeetingItems)
            {
                var meetingItem = await _context.MeetingItems.FindAsync(itemId);
                if (meetingItem != null)
                {
                    var latestMeetingItem = await _context.MeetingItems
                        .Include(mi => mi.Statuses)
                        .FirstOrDefaultAsync(mi => mi.MeetingItemID == itemId);

                    if (latestMeetingItem == null)
                    {
                        return NotFound();
                    }

                    var latestStatusDescription = latestMeetingItem.Statuses
                        .OrderByDescending(s => s.MeetingItemStatusID)
                        .FirstOrDefault()?.StatusDescription;
                    

                    var newMeetingItem = new MeetingItems
                    {
                        MeetingID = Meeting.MeetingID,
                        Description = meetingItem.Description,
                        PersonResponsible = meetingItem.PersonResponsible,
                        DueDate = meetingItem.DueDate,
                        Statuses = new List<Status>
                        {
                            new Status
                            {
                                MeetingItemID = meetingItem.MeetingItemID,
                                StatusDescription = latestStatusDescription,
                                ResponsiblePerson = meetingItem.PersonResponsible, 
                                ActionRequired = GetInitials(meetingItem.PersonResponsible),
                                StatusDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0)
                            }
                        }
                    };

                    //_context.MeetingItems.Add(newMeetingItem);
                }
            }

        //await _context.SaveChangesAsync();
            return RedirectToPage("/PreviousMeeting");
        }

        public static string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return string.Empty;
            }

            var names = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var initials = new StringBuilder();

            foreach (var name in names)
            {
                initials.Append(name[0]);
            }

            return initials.ToString().ToUpper();
        }
    }
}