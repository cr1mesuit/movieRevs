using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieRevs.Data;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Movie> Movies { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public void OnGet()
    {
        var query = _context.Movies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(Search))
        {
            query = query.Where(m => m.Title.ToLower().Contains(Search.ToLower()));
        }

        Movies = query.ToList();
    }
}
