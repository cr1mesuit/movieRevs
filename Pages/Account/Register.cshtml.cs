using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieRevs.Data;
using MovieRevs.Models;
using Microsoft.AspNetCore.Identity;

namespace MovieRevs.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly AppDbContext _db;

    public RegisterModel(AppDbContext db) => _db = db;

    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }
    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Введите имя и пароль!";
            return Page();
        }
        if (_db.Users.Any(u => u.Username == Username))
        {
            ErrorMessage = "Пользователь с таким именем уже существует.";
            return Page();
        }

        var hasher = new PasswordHasher<User>();
        var user = new User { Username = Username };
        user.PasswordHash = hasher.HashPassword(user, Password);

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return RedirectToPage("/Account/Login");
    }
}
