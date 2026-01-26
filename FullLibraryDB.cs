using Microsoft.EntityFrameworkCore;

public class FullLibrary : DbContext{
    public DbSet<CategoryF> categorys {get; set;}
    public DbSet<TypeF> types{get; set;}
    public DbSet<TitleF> titles{get; set;}
    public DbSet<DescriptionF> descriptions{get; set;}
    public DbSet<TagF> tags{get; set;}
    public DbSet<AssignedTagF> assignedTags{get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder options){
        options.UseSqlite("Data Source=db/~base.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        // modelBuilder.Entity<CategoryF>().ToTable("categorys");

        // modelBuilder.Entity<CategoryF>()
        // .Property(t=>t.removed)
        // .HasColumnName("removed");

        modelBuilder.Entity<CategoryF>()
        .Property(t=>t.removedTypeAm)
        .HasColumnName("removedTypeAm");

        // modelBuilder.Entity<CategoryF>()
        // .Property(t=>t.removed)
        // .HasColumnName("removed")
        // .HasColumnType("INTEGER")
        // .HasDefaultValue(0);

        modelBuilder.Entity<CategoryF>()
        .Property(t=>t.typeAm)
        .HasDefaultValue(0L);

        modelBuilder.Entity<CategoryF>()
        .HasIndex(t=>t.order)
        .IsUnique();

        // modelBuilder.Entity<TypeF>().ToTable("types");

        modelBuilder.Entity<TypeF>()
        .ToTable("types");

        modelBuilder.Entity<TypeF>()
        .HasMany(t => t.taglist)
        .WithOne(t=>t.typeNav);
        // .UsingEntity<TypeTagJoin>();

        // modelBuilder.Entity<TypeF>()
        // .Property(t=>t.removed)
        // .HasColumnName("removed")
        // .HasColumnType("INTEGER")
        // .HasDefaultValue(0);

        modelBuilder.Entity<TypeF>()
        .HasIndex(t=>t.order)
        .IsUnique();

        modelBuilder.Entity<TypeF>()
        .Property(t=>t.titleAm)
        .HasDefaultValue(0L);

        modelBuilder.Entity<TypeF>()
        .HasOne(t=>t.categoryNav)
        .WithMany(t=>t.typelist)
        .HasForeignKey(t=>t.categoryName)
        .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<TitleF>().ToTable("Titles");

        modelBuilder.Entity<TitleF>()
        .HasOne(t=>t.type)
        .WithMany(t=>t.titlelist)
        .OnDelete(DeleteBehavior.SetNull);

        // modelBuilder.Entity<TitleF>()
        // .Property(t=>t.removed)
        // .HasColumnName("removed")
        // .HasColumnType("INTEGER")
        // .HasDefaultValue(0);

        modelBuilder.Entity<TitleF>()
        .HasIndex(c=>c.name)
        .IsUnique();

        modelBuilder.Entity<TitleF>()
        .HasOne(t=>t.description)
        .WithOne(t=>t.title)
        .HasForeignKey<DescriptionF>(t=>t.titleId);

        modelBuilder.Entity<TitleF>()
        .Property(t=>t.image)
        .HasComputedColumnSql("[id] || \"-t.webp\"", stored: true);

        modelBuilder.Entity<DescriptionF>()
        .HasOne(t=>t.title)
        .WithOne(t=>t.description)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DescriptionF>()
        .HasMany(t=>t.assignedlist)
        .WithOne(t=>t.description);

        modelBuilder.Entity<DescriptionF>()
        .Property(t=>t.photoAm)
        .HasDefaultValue(0);

        modelBuilder.Entity<TagF>()
        .HasMany(t=>t.assignedList)
        .WithOne(t=>t.tagNav)
        .OnDelete(DeleteBehavior.Cascade);

        // modelBuilder.Entity<Tag>()
        // .HasMany(t=>t.typelist)
        // .WithMany(t=>t.taglist);

        modelBuilder.Entity<AssignedTagF>()
        .HasOne(t=>t.tagNav)
        .WithMany(t=>t.assignedList)
        .HasForeignKey(t=>t.tagName)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AssignedTagF>()
        .HasOne(t=>t.description)
        .WithMany(t=>t.assignedlist)
        .OnDelete(DeleteBehavior.Cascade);
    }
}