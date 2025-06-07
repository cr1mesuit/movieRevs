public class MovieSuggestion
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? SuggestedBy { get; set; }
    public int SuggestedAt { get; set; }
    public bool ReviewedByAdmin { get; set; } = false;
}
