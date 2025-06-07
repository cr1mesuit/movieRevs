using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieRevs.Models;
using MovieRevs.Services; // ���� � ������ MovieApiService
using MovieRevs.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieRevs.Pages.Admin
{
    public class AddMovieFromApiModel : PageModel
    {
        private readonly MovieApiService _movieApiService;
        private readonly AppDbContext _context;

        public AddMovieFromApiModel(MovieApiService movieApiService, AppDbContext context)
        {
            _movieApiService = movieApiService;
            _context = context;
        }

        [BindProperty]
        public string Title { get; set; } = string.Empty;

        [BindProperty]
        public Movie? MovieFromApi { get; set; }

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                ErrorMessage = "������� �������� ������";
                return Page();
            }

            MovieFromApi = await _movieApiService.GetMovieByTitleAsync(Title);

            if (MovieFromApi == null)
            {
                ErrorMessage = "����� �� ������ � API";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostConfirmAsync()
        {
            if (MovieFromApi == null)
            {
                ErrorMessage = "��� ������ ��� ���������� ������";
                return Page();
            }

            // ��������, ���� �� ��� ����� �����
            var existing = await _context.Movies.FirstOrDefaultAsync(m => m.Title == MovieFromApi.Title);
            if (existing != null)
            {
                ErrorMessage = "����� ����� ��� ���� � ����";
                return Page();
            }

            _context.Movies.Add(MovieFromApi);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Index"); 
        }
    }
}
