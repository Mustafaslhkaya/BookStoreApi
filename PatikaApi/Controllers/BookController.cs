using Microsoft.AspNetCore.Mvc;
using PatikaApi.BookOperations.CreateBooks;
using PatikaApi.BookOperations.GetBooks;
using PatikaApi.BookOperations.UpdateBooks;
using PatikaApi.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using static PatikaApi.BookOperations.CreateBooks.CreateBookCommand;
using static PatikaApi.BookOperations.GetBooks.GetBookDetail;
using static PatikaApi.BookOperations.UpdateBooks.UpdateBookCommand;

namespace PatikaApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooksList()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handler();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdBooks(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetail getBookDetail = new GetBookDetail(_context);
                getBookDetail.BookId = id;
                result = getBookDetail.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(result);


        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel NewBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = NewBook;
                command.Handle();
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book=_context.Books.SingleOrDefault(x=>x.Id== id);
            if (book is null)
            {
                return BadRequest();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
              
        }
    }
}
