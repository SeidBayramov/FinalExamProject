using FinalExamProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalExamProject.DAL.Configuration
{
    public class FruitConfiguration : IEntityTypeConfiguration<Fruit>
    {
        public void Configure(EntityTypeBuilder<Fruit> builder)
        {
            builder.Property(x => x.SubTitle).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(30);
            builder.Property(x => x.ImageUrl).IsRequired();
        }
    }
}
