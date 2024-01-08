using EFCConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;

// this class is our gateway to the database
// it configures the scema and defines the tables
// all settings are done here
namespace EFCConsoleApp.DataAccess;

public class MyDbContext : DbContext
{
    //define tables here:
    //DBSets looks like a collection/list, but represent the Book table
    public DbSet<Book> Books => Set<Book>();
    public DbSet<PriceOffer> PriceOffers => Set<PriceOffer>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Author> Author => Set<Author>();


    //we need to overwrite a method and stating the connection string of the sqlite
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //remember to copy relative path
        optionsBuilder.UseSqlite(
            @"Data Source = C:\\Users\\lilla\\RiderProjects\\DnpExam306512\\EFCConsoleApp\\BookDatabase.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //book can only have 1 price offer constraint
        modelBuilder.Entity<PriceOffer>()
            .HasIndex(offer => offer.BookId)
            .IsUnique();

        //first specifing which entity I want to mess with
        modelBuilder.Entity<Category>()
            .HasKey(category => category.Name); //setting the Name property as the key

        //composite key
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });
    }
}