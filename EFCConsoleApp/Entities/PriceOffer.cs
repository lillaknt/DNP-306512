namespace EFCConsoleApp.Entities;

public class PriceOffer
{
    public int Id { get; set; }
    public decimal PromotionalPrice { get; set; }
    public string PromotionalText { get; set; }

    //Relationships
    public int BookId { get; set; }
}