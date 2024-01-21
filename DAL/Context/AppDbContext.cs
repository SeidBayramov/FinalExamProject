using FinalExamProject.DAL.Configuration;
using FinalExamProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FinalExamProject.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(FruitConfiguration).Assembly);
            base.OnModelCreating(builder);  
        }

        public DbSet<Fruit> Fruits { get; set; }
    }
}
