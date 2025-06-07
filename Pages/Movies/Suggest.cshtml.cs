using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieRevs.Data;
using MovieRevs.Models;

namespace MovieRevs.Pages.Movies
{
    public class SuggestModel : PageModel
    {
        private readonly AppDbContext _context;

        public SuggestModel(AppDbContext context) => _context = context;

        [BindProperty]
        public MovieSuggestion Suggestion { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Suggestion.SuggestedBy = User.Identity?.Name ?? "Аноним";
            _context.MovieSuggestion.Add(Suggestion);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
