﻿using HomeWorkPronia.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeWorkPronia.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Card> Cards { get; set; } = null!;
        public DbSet<Slider> Sliders { get; set; } = null!;
    }
}
