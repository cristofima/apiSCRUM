using Microsoft.EntityFrameworkCore;

namespace ToDoAppAPI.Models
{
    public partial class SCRUMContext : DbContext
    {
        public SCRUMContext(DbContextOptions<SCRUMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Todo> Todo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Todo>(entity =>
            {
                entity.ToTable("Todos");

                entity.HasIndex(e => e.Id);

                entity.Property(e => e.IsComplete);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
        }
    }
}