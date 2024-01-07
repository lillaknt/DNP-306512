// See https://aka.ms/new-console-template for more information

using EFCConsoleApp.DataAccess;
using EFCConsoleApp.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

// ******** To add new book ***********
Book book = new()
{
    //Id = set by database
    Price = 2.50m, //for decimal we need m
    Publisher = "DD",
    Title = "Life beyound the universe",
    PublishDate = DateTime.Now
};

/*
 * // this is just to add, doesn't return anything
await InsertBookAsync(book);
async Task<Book> InsertBookAsync(Book book)
{
    using MyDbContext context = new();
    await context.Books.AddAsync(book);
    await context.SaveChangesAsync();
}

//this adds, but also returns the entity that was just added
Book resultingBook = await InsertBookAsync(book);
Console.WriteLine($"Book has been added to database: Id: {resultingBook.Id}, Title: {resultingBook.Title}");
*/

async Task<Book> InsertBookAsync(Book book)
{
    using MyDbContext context = new();
    EntityEntry<Book> entry = await context.Books.AddAsync(book);
    await context.SaveChangesAsync();
    return entry.Entity;
}

// ************** Update the entire book *********
Book bookToUpdate = new()
{
    Id = 6,
    Price = 122.50m, //for decimal we need m
    Publisher = "DD",
    Title = "Life beyond the universe",
    PublishDate = DateTime.Now
};

// await UpdateBookAsync(bookToUpdate);

async Task UpdateBookAsync(Book bookToUpdate)
{
    using MyDbContext context = new();
    context.Books.Update(bookToUpdate);
    await context.SaveChangesAsync();
    
    
}


// ********* GetById
//Book? retrievedBookById = await GetBookByIdAsync(3);
//Console.WriteLine($"Book retrieved from database: Id: {retrievedBookById.Id}, Title: {retrievedBookById.Title}");

async Task<Book?> GetBookByIdAsync(int id)
{
    using MyDbContext context = new();
    Book? foundBook = await context.Books.FindAsync(id);
    return foundBook;
} 

//******* Update book by retrieving the book by Id first and using that to update a property of the book:
Book? retrievedBookById = await GetBookByIdAsync(3);
retrievedBookById.Price = 42.00m;
//await UpdateBookAsync(retrievedBookById);


// ********** Delete Book
await DeleteBookAsync(3);

async Task DeleteBookAsync(int id)
{
    using MyDbContext context = new();
    Book? bookToDelete = await context.Books.FindAsync(id); //retrieve book that we want to delete
    context.Books.Remove(bookToDelete); //remove it from the collections
    await context.SaveChangesAsync(); //save the changes
}