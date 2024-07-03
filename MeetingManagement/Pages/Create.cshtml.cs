using MeetingManagement.Data;
using MeetingManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeetingManagement.Pages
{
    public class CreateModel : PageModel
    {
        public Meeting Meeting { get; set; } = new Meeting();
        private List<MeetingType> MeetingTypes { get; set; } = new List<MeetingType>();
        public async Task<IActionResult> OnGetAsync()
        {
            await MeetingContext.Meetings.AddAsync(Meeting);
            MeetingContext.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
