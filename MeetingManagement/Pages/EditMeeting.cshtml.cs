using MeetingManagement.Data;
using MeetingManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagement.Pages
{
    public class EditMeetingModel : PageModel
    {
        private readonly MeetingContext _context;

        public EditMeetingModel(MeetingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Meeting Meeting { get; set; }
        public Status Status { get; set; }
        public SelectList Statuses { get; set; }
        public IList<Meeting> Meetings { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Meeting = await _context.Meetings
                .Include(m => m.MeetingItems)
                .ThenInclude(mi => mi.Statuses)
                .Include(mt => mt.MeetingType)
                .FirstOrDefaultAsync(m => m.MeetingID == id && m.MeetingType.MeetingTypeID == m.MeetingTypeID);

            Meetings = await _context.Meetings
                .Include(m => m.MeetingItems)
                .ThenInclude(mi => mi.Statuses)
                .ToListAsync();


            var meetingItem = await _context.MeetingItems
                .Include(mi => mi.Statuses)
                .FirstOrDefaultAsync(mi => mi.MeetingItemID == id);

            if (meetingItem == null)
            {
                return RedirectToPage("/PreviousMeeting");
            }

            Status = meetingItem.Statuses.LastOrDefault() ?? new Status { MeetingItemID = (int)id };


            var statusList = await _context.Statuses.ToListAsync();
            Statuses = new SelectList(statusList, "MeetingItemStatusID", "StatusDescription", Status.StatusDescription);

            if (Meeting == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Meeting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingExists(Meeting.MeetingID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/PreviousMeeting");
        }

        private bool MeetingExists(int id)
        {
            return _context.Meetings.Any(e => e.MeetingID == id);
        }
    }
}
