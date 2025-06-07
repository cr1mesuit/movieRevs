using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieRevs.Data;
using MovieRevs.Models;

namespace MovieRevs.Pages.Admin
{
    public class EditMovieModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditMovieModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            Movie = movie;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var movie = _context.Movies.FirstOrDefault(m => m.Id == Movie.Id);
            if (movie == null)
                return NotFound();

            movie.Title = Movie.Title;
            movie.Description = Movie.Description;
            movie.ReleaseDate = Movie.ReleaseDate;

            _context.SaveChanges();
            return RedirectToPage("/Movies/Index"); 
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (!User.Identity?.IsAuthenticated ?? true || !User.HasClaim("IsAdmin", "true"))
                return Forbid();

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

    }
}
