using System.ComponentModel.DataAnnotations;

namespace EFCConsoleApp.Entities;

public class Book
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime PublishDate { get; set; }
    public decimal Price { get; set; }
    public string Publisher { get; set; }
    
    
    //Relationships
    //one to one
    public PriceOffer PriceOffer { get; set; }
    // a book can have many reviews (one to many)
    public ICollection<Review> Reviews { get; set; }
    //many to many
    public ICollection<Category> Categories { get; set; }
    
    //relationship attributes
    public ICollection<BookAuthor> AuthorsLink { get; set; }

} 