using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeetingManagement.Data;
using MeetingManagement.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingManagement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MeetingContext _context;

        public IndexModel(MeetingContext context)
        {
            _context = context;
        }

        public IList<Meeting> Meetings { get; set; }
        public ICollection<MeetingItems> MeetingItems { get; set; }
        public async Task OnGetAsync()
        {

        }
    }
}