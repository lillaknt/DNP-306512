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
 
 

 //we need to overwrite a method and stating the connection string of the sqlite
 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 {
  //remember to copy relative path
  optionsBuilder.UseSqlite(@"Data Source = C:\\Users\\lilla\\RiderProjects\\DnpExam306512\\EFCConsoleApp\\BookDatabase.db");
  optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
 }
}