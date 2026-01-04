
using Microsoft.EntityFrameworkCore;
using ClassProject.Models;
using System.Collections.Generic;

namespace ClassProject.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }

    public DbSet<Models.User> Users { get; set; }
}
