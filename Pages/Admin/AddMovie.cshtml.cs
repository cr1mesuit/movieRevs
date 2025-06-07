using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieRevs.Data;
using MovieRevs.Models;

namespace MovieRevs.Pages.Admin
{
    [Authorize]
    public class AddMovieModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddMovieModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; } = new Movie();

        public string? Message { get; set; }

        public IActionResult OnGet()
        {
            if (!User.HasClaim("IsAdmin", "true"))
                return Forbid();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!User.HasClaim("IsAdmin", "true"))
                return Forbid();

            if (!ModelState.IsValid)
                return Page();

            _context.Movies.Add(Movie);
            _context.SaveChanges();

            Message = "Фильм добавлен!";
            Movie = new Movie(); // очистить форму
            return Page();
        }
    }
}
