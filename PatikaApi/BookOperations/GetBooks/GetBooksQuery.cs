using PatikaApi.Common;
using PatikaApi.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatikaApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext )
        {
            _dbContext = dbContext;
        }
        public List<BookViewModel> Handler()
        {
            var booklist=_dbContext.Books.OrderBy(x=>x.Id).ToList<Book>();
            List<BookViewModel> vm = new List<BookViewModel>();
            foreach (var book in booklist)
            {
                vm.Add(new BookViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MMM/yyyy"),
                    PageCount = book.PageCount
                });
            }
            return vm;
        }
    }
    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }



    }
}
