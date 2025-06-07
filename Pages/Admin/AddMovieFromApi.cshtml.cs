using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieRevs.Models;
using MovieRevs.Services; // путь к твоему MovieApiService
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
                ErrorMessage = "Введите название фильма";
                return Page();
            }

            MovieFromApi = await _movieApiService.GetMovieByTitleAsync(Title);

            if (MovieFromApi == null)
            {
                ErrorMessage = "Фильм не найден в API";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostConfirmAsync()
        {
            if (MovieFromApi == null)
            {
                ErrorMessage = "Нет данных для добавления фильма";
                return Page();
            }

            // Проверка, есть ли уже такой фильм
            var existing = await _context.Movies.FirstOrDefaultAsync(m => m.Title == MovieFromApi.Title);
            if (existing != null)
            {
                ErrorMessage = "Такой фильм уже есть в базе";
                return Page();
            }

            _context.Movies.Add(MovieFromApi);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Index"); 
        }
    }
}
