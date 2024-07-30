using MeetingManagement.Data;
using MeetingManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManagement.Pages
{
    public class StatusModel : PageModel
    {
        private readonly MeetingContext _context;

        public StatusModel(MeetingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Status Status { get; set; }
        
        [BindProperty]
        public string NewStatusDescription { get; set; }

        public MeetingItems MeetingItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            MeetingItem = await _context.MeetingItems
                .Include(mi => mi.Statuses)
                .FirstOrDefaultAsync(mi => mi.MeetingItemID == id);

            if (MeetingItem == null)
            {
                return NotFound();
            }

            Status = MeetingItem.Statuses.OrderByDescending(s => s.MeetingItemStatusID).FirstOrDefault();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var meetingItem = await _context.MeetingItems
                .Include(mi => mi.Statuses)
                .FirstOrDefaultAsync(mi => mi.MeetingItemID == Status.MeetingItemID);

            if (meetingItem == null)
            {
                return NotFound();
            }

            var now = DateTime.Now;

            var newStatus = new Status
            {
                MeetingItemID = Status.MeetingItemID,
                StatusDescription = NewStatusDescription,
                ResponsiblePerson = Status.ResponsiblePerson,
                ActionRequired = GetInitials(Status.ResponsiblePerson),
                StatusDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0)
            };

            meetingItem.Statuses.Add(newStatus);
            await _context.SaveChangesAsync();

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