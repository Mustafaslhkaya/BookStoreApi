using Microsoft.AspNetCore.Mvc;
using PatikaApi.Common;
using PatikaApi.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using static PatikaApi.BookOperations.CreateBooks.CreateBookCommand;

namespace PatikaApi.BookOperations.UpdateBooks
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookModel Model { get; set; }
        public int BookId { get; set; }
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı.");
            }
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();


        }
        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            
            
        }
    }
}
