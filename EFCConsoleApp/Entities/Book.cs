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
}