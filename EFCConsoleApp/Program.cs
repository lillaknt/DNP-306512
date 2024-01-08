// See https://aka.ms/new-console-template for more information

using EFCConsoleApp.DataAccess;
using EFCConsoleApp.Entities;

////////////////////////////////////////////////////////////////
// ******** To add new book ***********
Book book = new()
{
    Id = 1,
    Price = 222.50m, //for decimal we need m
    Publisher = "MKL Deer",
    Title = "Let's pass DNP with 12",
    PublishDate = DateTime.Now
};


/*
// this is just to add, doesn't return anything
await AddBookAsync(book);
async Task AddBookAsync(Book book)
{
   await using MyDbContext context = new();
    await context.Books.AddAsync(book);
    await context.SaveChangesAsync();
}

//this adds, but also returns the entity that was just added
Book resultingBook = await AddBookAsync(book);
Console.WriteLine($"Book has been added to database: Id: {resultingBook.Id}, Title: {resultingBook.Title}");

async Task<Book> AddBookAsync(Book book)
{
   await using MyDbContext context = new();
    EntityEntry<Book> entry = await context.Books.AddAsync(book);
    await context.SaveChangesAsync();
    return entry.Entity;
}
*/


////////////////////////////////////////////////////////////////
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
    await using MyDbContext context = new();
    context.Books.Update(bookToUpdate);
    await context.SaveChangesAsync();
}

////////////////////////////////////////////////////////////////
// ********* GetById
//Book? retrievedBookById = await GetBookByIdAsync(3);
//Console.WriteLine($"Book retrieved from database: Id: {retrievedBookById.Id}, Title: {retrievedBookById.Title}");

async Task<Book?> GetBookByIdAsync(int id)
{
    await using MyDbContext context = new();
    var foundBook = await context.Books.FindAsync(id);
    return foundBook;
}

////////////////////////////////////////////////////////////////
//******* Update book by retrieving the book by Id first and using that to update a property of the book:
/*
Book? retrievedBookById = await GetBookByIdAsync(3);
if (retrievedBookById == null)
{
    throw new Exception($"Book with id not found");
}
retrievedBookById.Price = 42.00m;

await UpdateBookAsync(retrievedBookById);
*/


////////////////////////////////////////////////////////////////
// ********** Delete Book
/*
async Task DeleteBookAsync(int id)
{
    await using MyDbContext context = new();
    Book? bookToDelete = await context.Books.FindAsync(id); //retrieve book that we want to delete
    if (bookToDelete == null)
    {
        throw new Exception($"Book with id {id} not found");
    }
    context.Books.Remove(bookToDelete); //remove it from the collections
    await context.SaveChangesAsync(); //save the changes
}

await DeleteBookAsync(1);

*/


////////////////////////////////////////////////////////////////
/// ********** Price Offer Stuff - one to  one
///  ********** Add Price offer directly
/*
async Task AddPriceOfferAsync(PriceOffer po)
{
    await using MyDbContext context = new();
    await context.PriceOffers.AddAsync(po);
    await context.SaveChangesAsync();

}

PriceOffer po = new()
{
    PromotionalPrice = 15m,
    PromotionalText = "Super Cheap",
    BookId = 1
};

await AddPriceOfferAsync(po);
*/

///  ********** Add Price offer through book
/*
async Task AddPriceOfferToBook(PriceOffer po, int bookId)
{
    await using MyDbContext context = new();
    Book foundBook = (await context.Books.FindAsync(bookId))!; //find book with existing id
    foundBook.PriceOffer = po; //update its reference
    context.Books.Update(foundBook); //update book, EFC find out that book has a price offer which should be stored
    await context.SaveChangesAsync();
}

PriceOffer po2 = new()
{
    PromotionalPrice = 37m,
    PromotionalText = "Not quite so super cheap"
};

await AddPriceOfferToBook(po2, 2);
*/


////////////////////////////////////////////////////////////////
/// ********** Load a book with price offer
/*
async Task<Book> GetBookWithPriceOfferAsync(int id)
{
  await  using MyDbContext context = new();
    Book? foundBook = await context.Books
        //include is like sql join
        .Include(book => book.PriceOffer)
        .FirstOrDefaultAsync(book => book.Id == id);
    return foundBook!;
}
Book bookWithPriceOffer = await GetBookWithPriceOfferAsync(1);
*/


////////////////////////////////////////////////////////////////
/// ********** Add Review to book using foreign key
/*
async Task AddReviewUsingFkAsync(Review review)
{
    await  using MyDbContext context = new();
    await context.Reviews.AddAsync(review);
    await context.SaveChangesAsync();
}

Review review = new()
{
    Rating = 5,
    VoterName = "Lilla",
    Comment = "Very good",
    BookId = 1
};

await AddReviewUsingFkAsync(review);
*/

/// ********** Add Review to book using through book
/*
async Task AddReviewToBookAsync(Review review, int bookId)
{
    await  using MyDbContext context = new();
    Book? foundBook = await context.Books
        //include is like sql join
        .Include(book => book.Reviews)
        .FirstOrDefaultAsync(book => book.Id == bookId);
    foundBook.Reviews.Add(review);
     context.Books.Update(foundBook);
    await context.SaveChangesAsync();
}

Review review = new()
{
    Rating = 4,
    VoterName = "Lilla",
    Comment = "Decent book, good",
};

await AddReviewToBookAsync(review,2);

*/

/// ********** remove review from list
/*
async Task RemoveReviewFromBookAsync(int bookId, int reviewID)
{
    await using MyDbContext context = new();
    Review? toBeRemoved = await context.Reviews.FindAsync(reviewID);
   context.Reviews.Remove(toBeRemoved);
   await context.SaveChangesAsync();
}

await RemoveReviewFromBookAsync(1,2);

*/
/// ********** Load books with Reviews and price offer
/*
async Task<Book> GetBookWithReviewAndPOAsync(int bookId)
{
    await  using MyDbContext context = new();
    Book? foundBook = await context.Books
        //include is like sql join
        .Include(book => book.PriceOffer)
        .Include(book => book.Reviews)
        .FirstOrDefaultAsync(book => book.Id == bookId);
    return foundBook!;
}
Book bookWithReviewAndPo = await GetBookWithReviewAndPOAsync(2);

*/


////////////////////////////////////////////////////////////////
/// ********** Many to many relationship interactions
/*
await AddCategoryToBookAsync("Disney", 1);

async Task AddCategoryToBookAsync(string cat, int bookId)
{
    await  using MyDbContext context = new();
    //find existing book with associated categories
    Book? existingBoox = await context.Books
        //include is like sql join
        .Include(book => book.Categories)
        .FirstOrDefaultAsync(book => book.Id == bookId);

    //find existing category with associated books
    Category? existingCategory = await context.Categories
      .Include(ca => ca.Books)
        .FirstOrDefaultAsync(ca => ca.Name.Equals(cat));

    //Adding the selected category to the existing selected book
    existingCategory.Books.Add(existingBoox);
    context.Categories.Update(existingCategory);
    await context.SaveChangesAsync();

}
*/
/*
async Task AddAuthorToBookAsync(int authorId, int bookId, int order)
{
    //new link between author and book
    BookAuthor bookAuthor = new()
    {
        AuthorId = authorId,
        BookId = bookId,
        Order = order
    };

    await  using MyDbContext context = new();

    //add this link
    await context.Set<BookAuthor>().AddAsync(bookAuthor);
    await context.SaveChangesAsync();
}

await AddAuthorToBookAsync(1,1,1);
await AddAuthorToBookAsync(2,1,2);
*/

/*
await  using MyDbContext context = new();
context.Books.Include(book => book.AuthorsLink)
    .ThenInclude(ba => ba.Author)
    .Include(book => book.Reviews)
    .First();

    */