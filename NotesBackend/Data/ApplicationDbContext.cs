using Microsoft.EntityFrameworkCore;
using NotesBackend.Models;

namespace NotesBackend.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Note> Notes => Set<Note>();
}
