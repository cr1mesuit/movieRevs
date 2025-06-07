using System.ComponentModel.DataAnnotations;

public class Review
{
    public int Id { get; set; }

    public int MovieId { get; set; }
    public Movie? Movie { get; set; }

    public string UserName { get; set; } = string.Empty;

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    public string Content { get; set; } = string.Empty;
}
