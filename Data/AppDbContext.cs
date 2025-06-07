using Microsoft.EntityFrameworkCore;
using MovieRevs.Models;
using System.Collections.Generic;

namespace MovieRevs.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<MovieSuggestion> MovieSuggestion => Set<MovieSuggestion>();
}
