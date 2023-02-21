using Blog.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class PostMapping : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Titulo)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Corpo)
               .IsRequired()
               .HasColumnType("varchar(5000)");

            builder.Property(p => p.Imagem)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.ToTable("Posts");
        }
    }
}
