
namespace TodoAPP.Infrastructure.Configurations
{
    
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .HasMaxLength(1000);

            builder.Property(t => t.Priority)
                .HasConversion<int>();

            builder.Property(t => t.CreatedAt)
                .IsRequired();

            // Indexes for performance
            builder.HasIndex(t => t.IsCompleted);
            builder.HasIndex(t => t.CreatedAt);
            builder.HasIndex(t => t.Priority);
        }
    }
}
