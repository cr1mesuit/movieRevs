using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using MovieRevs.Models;

namespace MovieRevs.Services;

public class MovieApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "16183f";

    public MovieApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Movie?> GetMovieByTitleAsync(string title)
    {
        var url = $"https://www.omdbapi.com/?apikey={_apiKey}&t={Uri.EscapeDataString(title)}";
        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonDocument.Parse(json).RootElement;

        if (data.GetProperty("Response").GetString() == "False")
            return null;

        int year = 0;
        var yearString = data.GetProperty("Year").GetString();
        if (yearString != null && int.TryParse(yearString[..4], out int parsedYear))
            year = parsedYear;

        return new Movie
        {
            Title = data.GetProperty("Title").GetString() ?? "",
            Description = data.GetProperty("Plot").GetString() ?? "",
            ReleaseDate = year
        };
    }

}
