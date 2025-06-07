public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseDate { get; set; }
    public string? SuggestedByUser { get; set; }

    public List<Review> Reviews { get; set; } = new();
}
