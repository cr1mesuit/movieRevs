using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieRevs.Data;
using MovieRevs.Models;
using System.Security.Claims;

namespace MovieRevs.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Movie? Movie { get; set; }

        [BindProperty]
        public Review NewReview { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Movie = await _context.Movies
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == Id);

            return Movie == null ? NotFound() : Page();
        }


        [BindProperty]
        public int Rating { get; set; }

        [BindProperty]
        public string Content { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            var movie = await _context.Movies.Include(m => m.Reviews).FirstOrDefaultAsync(m => m.Id == Id);
            if (movie == null) return NotFound();

            if (!User.Identity?.IsAuthenticated ?? true)
                return Forbid();

            var review = new Review
            {
                MovieId = Id,
                UserName = User.Identity?.Name ?? "Аноним",
                Rating = Rating,
                Content = Content
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = Id });
        }

        public async Task<IActionResult> OnPostDeleteReviewAsync(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
                return NotFound();

            if (!User.IsInRole("Admin"))
                return Forbid();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = Id });
        }

    }
}
