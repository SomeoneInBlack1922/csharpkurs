using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

public class Library : DbContext{
    public DbSet<Category> categorys {get; set;}
    public DbSet<Type> types{get; set;}
    public DbSet<Title> titles{get; set;}
    public DbSet<Description> descriptions{get; set;}
    public DbSet<Tag> tags{get; set;}
    public DbSet<AssignedTag> assignedTags{get;set;}
    //REMOVED=====>
    public DbSet<CategoryR> categorysR {get; set;}
    public DbSet<TypeR> typesR{get; set;}
    public DbSet<TitleR> titlesR{get; set;}
    public DbSet<DescriptionR> descriptionsR{get; set;}
    public DbSet<TagR> tagsR{get; set;}
    public DbSet<AssignedTagR> assignedTagsR{get;set;}
    // public Library () : base(){}
    protected override void OnConfiguring(DbContextOptionsBuilder options){
        options.UseSqlite("Data Source=db/~base.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        // modelBuilder.Entity<Category>()
        // .Property(t=>t.removed)
        // .HasColumnName("removed");

        modelBuilder.Entity<Category>()
        .Property(t=>t.removedTypeAm)
        .HasColumnName("removedTypeAm");

        // modelBuilder.Entity<Category>()
        // .Property(t=>t.removed)
        // .HasColumnName("removed")
        // .HasColumnType("INTEGER")
        // .HasDefaultValue(0);

        modelBuilder.Entity<Category>()
        .Property(t=>t.typeAm)
        .HasDefaultValue(0L);

        modelBuilder.Entity<Category>()
        .HasIndex(t=>t.order)
        .IsUnique();

        modelBuilder.Entity<Type>()
        .HasMany(t => t.taglist)
        .WithOne(t=>t.typeNav);
        // .UsingEntity<TypeTagJoin>();

        // modelBuilder.Entity<Type>()
        // .Property(t=>t.removed)
        // .HasColumnName("removed")
        // .HasColumnType("INTEGER")
        // .HasDefaultValue(0);

        modelBuilder.Entity<Type>()
        .HasIndex(t=>t.order)
        .IsUnique();

        modelBuilder.Entity<Type>()
        .Property(t=>t.titleAm)
        .HasDefaultValue(0L);

        modelBuilder.Entity<Type>()
        .HasOne(t=>t.categoryNav)
        .WithMany(t=>t.typelist)
        .HasForeignKey(t=>t.categoryName)
        .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Title>()
        .HasOne(t=>t.type)
        .WithMany(t=>t.titlelist)
        .OnDelete(DeleteBehavior.SetNull);

        // modelBuilder.Entity<Title>()
        // .Property(t=>t.removed)
        // .HasColumnName("removed")
        // .HasColumnType("INTEGER")
        // .HasDefaultValue(0);

        modelBuilder.Entity<Title>()
        .HasIndex(c=>c.name)
        .IsUnique();

        modelBuilder.Entity<Title>()
        .HasOne(t=>t.description)
        .WithOne(t=>t.title)
        .HasForeignKey<Description>(t=>t.titleId);

        modelBuilder.Entity<Title>()
        .Property(t=>t.image)
        .HasComputedColumnSql("[id] || \"-t.webp\"", stored: true);

        modelBuilder.Entity<Description>()
        .HasOne(t=>t.title)
        .WithOne(t=>t.description)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Description>()
        .HasMany(t=>t.assignedlist)
        .WithOne(t=>t.description);

        modelBuilder.Entity<Description>()
        .Property(t=>t.photoAm)
        .HasDefaultValue(0);

        modelBuilder.Entity<Tag>()
        .HasMany(t=>t.assignedList)
        .WithOne(t=>t.tagNav)
        .OnDelete(DeleteBehavior.Cascade);

        // modelBuilder.Entity<Tag>()
        // .HasMany(t=>t.typelist)
        // .WithMany(t=>t.taglist);

        modelBuilder.Entity<AssignedTag>()
        .HasOne(t=>t.tagNav)
        .WithMany(t=>t.assignedList)
        .HasForeignKey(t=>t.tagName)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AssignedTag>()
        .HasOne(t=>t.description)
        .WithMany(t=>t.assignedlist)
        .OnDelete(DeleteBehavior.Cascade);

        //REMOVED TABLES ======================>

        modelBuilder.Entity<CategoryR>()
        .Property(t=>t.removedTypeAm)
        .HasColumnName("removedTypeAm");

        modelBuilder.Entity<CategoryR>()
        .Property(t=>t.typeAm)
        .HasDefaultValue(0L);

        modelBuilder.Entity<CategoryR>()
        .HasIndex(t=>t.order)
        .IsUnique();

        modelBuilder.Entity<TypeR>()
        .HasMany(t => t.taglist)
        .WithOne(t=>t.typeNav);
        // .UsingEntity<TypeTagJoin>();

        modelBuilder.Entity<TypeR>()
        .HasIndex(t=>t.order)
        .IsUnique();

        modelBuilder.Entity<TypeR>()
        .Property(t=>t.titleAm)
        .HasDefaultValue(0L);

        modelBuilder.Entity<TypeR>()
        .HasOne(t=>t.categoryNav)
        .WithMany(t=>t.typelist)
        .HasForeignKey(t=>t.categoryName)
        .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<TitleR>()
        .HasOne(t=>t.type)
        .WithMany(t=>t.titlelist)
        .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<TitleR>()
        .HasIndex(c=>c.name)
        .IsUnique();

        modelBuilder.Entity<TitleR>()
        .HasOne(t=>t.description)
        .WithOne(t=>t.title)
        .HasForeignKey<DescriptionR>(t=>t.titleId);

        modelBuilder.Entity<TitleR>()
        .Property(t=>t.image)
        .HasComputedColumnSql("[id] || \"-t.webp\"", stored: true);

        modelBuilder.Entity<DescriptionR>()
        .HasOne(t=>t.title)
        .WithOne(t=>t.description)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DescriptionR>()
        .HasMany(t=>t.assignedlist)
        .WithOne(t=>t.description);

        modelBuilder.Entity<DescriptionR>()
        .Property(t=>t.photoAm)
        .HasDefaultValue(0);

        
        modelBuilder.Entity<TagR>()
        .HasMany(t=>t.assignedList)
        .WithOne(t=>t.tagNav)
        .OnDelete(DeleteBehavior.Cascade);

        // modelBuilder.Entity<Tag>()
        // .HasMany(t=>t.typelist)
        // .WithMany(t=>t.taglist);

        modelBuilder.Entity<AssignedTagR>()
        .HasOne(t=>t.tagNav)
        .WithMany(t=>t.assignedList)
        .HasForeignKey(t=>t.tagName)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AssignedTagR>()
        .HasOne(t=>t.description)
        .WithMany(t=>t.assignedlist)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
