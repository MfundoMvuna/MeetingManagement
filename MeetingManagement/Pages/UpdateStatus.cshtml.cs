using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetingManagement.Data;
using MeetingManagement.Model;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManagement.Pages
{
    public class UpdateStatusModel : PageModel
    {
        private readonly MeetingContext _context;

        public UpdateStatusModel(MeetingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Status Status { get; set; }

        public SelectList Statuses { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var meetingItem = await _context.MeetingItems
                .Include(mi => mi.Statuses)
                .FirstOrDefaultAsync(mi => mi.MeetingItemID == id);

            if (meetingItem == null)
            {
                return NotFound();
            }

            Status = meetingItem.Statuses.FirstOrDefault() ?? new Status { MeetingItemID = id };

            var statusList = await _context.Statuses.ToListAsync();
            Statuses = new SelectList(statusList, "StatusID", "StatusDescription");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var statusList = await _context.Statuses.ToListAsync();
                Statuses = new SelectList(statusList, "StatusID", "StatusDescription");
                return Page();
            }

            _context.Attach(Status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(Status.MeetingItemStatusID))
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

        private bool StatusExists(int id)
        {
            return _context.Statuses.Any(e => e.MeetingItemStatusID == id);
        }
    }
}
