using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookContext _context;

        public BookController(BookContext context)
        {
            _context = context;
        }

        // GET: api/v1/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDetailDto>>> GetBooks()
        {
            var books = from b in _context.Books
                        select new BookDetailDto()
                        {
                            Id = b.Id,
                            Title = b.Name,
                            Year = b.YearOfPublishing,
                            AuthorName = b.Author.Name + ' ' + (b.Author.Patronymic == "" ?  "" : b.Author.Patronymic + ' ') + b.Author.Surname
                        };

            return await books.ToListAsync();

            //return await _context.Books.Include(a => a.Author).Select(b =>
            //    new BookDetailDto()
            //    {
            //        Id = b.Id,
            //        Title = b.Name,
            //        Year = b.YearOfPublishing,
            //        AuthorName = b.Author.Name
            //    }).ToListAsync();
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailDto>> GetBook(int id)
        {
            var book = await _context.Books.Include(b => b.Author).Select(b =>
                new BookDetailDto()
                {
                    Id = b.Id,
                    Title = b.Name,
                    Year = b.YearOfPublishing,
                    AuthorName = b.Author.Name + ' ' + (b.Author.Patronymic == "" ? "" : b.Author.Patronymic + ' ') + b.Author.Surname
                }).SingleOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Book/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Book
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
