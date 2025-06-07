using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieRevs.Data;
using MovieRevs.Models;

namespace MovieRevs.Pages.Admin
{
    public class SuggestionsModel : PageModel
    {
        private readonly AppDbContext _context;

        public SuggestionsModel(AppDbContext context) => _context = context;

        public List<MovieSuggestion> Suggestions { get; set; } = new();

        public async Task OnGetAsync()
        {
            Suggestions = await _context.MovieSuggestion
                .OrderByDescending(s => s.SuggestedAt)
                .ToListAsync();
        }
    }
}
