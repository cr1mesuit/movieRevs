using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieRevs.Data;
using MovieRevs.Models;

namespace MovieRevs.Pages.Admin;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Movie NewMovie { get; set; } = new();

    public List<Movie> Movies { get; set; } = new();

    public IActionResult OnGet()
    {
        // Проверяем, админ ли пользователь
        if (!User.HasClaim("IsAdmin", "true"))
        {
            return Forbid(); // 403
        }

        Movies = _context.Movies.ToList();
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!User.HasClaim("IsAdmin", "true"))
        {
            return Forbid(); // 403
        }

        if (!ModelState.IsValid) return Page();

        _context.Movies.Add(NewMovie);
        _context.SaveChanges();

        return RedirectToPage(); // обновим список
    }
}
