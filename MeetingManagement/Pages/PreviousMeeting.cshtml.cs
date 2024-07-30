using MeetingManagement.Data;
using MeetingManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagement.Pages
{
    public class PreviousMeetingModel : PageModel
    {
        private readonly MeetingContext _context;

        public PreviousMeetingModel(MeetingContext context)
        {
            _context = context;
        }

        public IList<Meeting> Meetings { get; set; }

        public async Task OnGetAsync()
        {
            Meetings = await _context.Meetings
                .Include(m => m.MeetingType)
                .Include(m => m.MeetingItems)
                .ThenInclude(mi => mi.Statuses)
                .ToListAsync();
        }
    }
}
