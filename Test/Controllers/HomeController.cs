using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        BookContext db;
        public HomeController(BookContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Users.ToList());
        }

        //отображение списка авторов
        public IActionResult AuthorList()
        {
            return View(db);
        }
        // отображение списка книг
        public IActionResult BookList()
        {
            return View(db);
        }


        #region Удаление Автора
        [HttpGet]
        [ActionName("DeleteAuthor")]
        public async Task<IActionResult> ConfirmDeleteAuthor(int? id)
        {
            if (id != null)
            {
                Author author = await db.Authors.FirstOrDefaultAsync(a => a.Id == id);
                if (author != null)
                    return View(author);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAuthor(int? id)
        {
            if (id != null)
            {
                Author author = await db.Authors.FirstOrDefaultAsync(a => a.Id == id);
                if (author != null)
                {
                    db.Authors.Remove(author);
                    await db.SaveChangesAsync();
                    return RedirectToAction("AuthorList");
                }
            }
            return NotFound();
        }
        #endregion

        #region Добавление автора
        [HttpGet]
        public IActionResult CreateAuthor()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAuthor(CreateAuthorModel model)
        {
            if (ModelState.IsValid)
            {
                Author author = db.Authors.FirstOrDefault(a => a.Name == model.Name);
                if (author == null)
                {
                    // добавляем пользователя в бд
                    author = new Author { Name = model.Name, Surname = model.Surname, Patronymic = model.Patronymic};
                    

                    db.Authors.Add(author);
                    await db.SaveChangesAsync();

                

                    return RedirectToAction("AuthorList", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректный ввод данных");
            }
            return View(model);
        }
        #endregion

        #region Изменение записи об авторе
        public async Task<IActionResult> EditAuthor(int? id)
        {
            if (id != null)
            {
                Author author = await db.Authors.FirstOrDefaultAsync(a => a.Id == id);
                if (author != null)
                    return View(author);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditAuthor(Author author)
        {
            db.Authors.Update(author);
            await db.SaveChangesAsync();
            return RedirectToAction("AuthorList");
        }
        #endregion


        #region Удаление книги
        [HttpGet]
        [ActionName("DeleteBook")]
        public async Task<IActionResult> ConfirmDeleteBook(int? id)
        {
            if (id != null)
            {
                Book book = await db.Books.FirstOrDefaultAsync(b => b.Id == id);
                if (book != null)
                    return View(book);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int? id)
        {
            if (id != null)
            {
                Book book = await db.Books.FirstOrDefaultAsync(b => b.Id == id);
                if (book != null)
                {
                    db.Books.Remove(book);
                    await db.SaveChangesAsync();
                    return RedirectToAction("BookList");
                }
            }
            return NotFound();
        }
        #endregion

        #region Добавление книги
        [HttpGet]
        public IActionResult CreateBook()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook(CreateBookModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = db.Books.FirstOrDefault(b => b.Name == model.Name);
                if (book == null)
                {
                    // добавляем пользователя в бд
                    book = new Book { 
                        Name = model.Name, 
                        AuthorId = model.AuthorId, 
                        YearOfPublishing = model.YearOfPublishing,
                        PublishingHouse = model.PublishingHouse,
                    };
                    db.Books.Add(book);
                    await db.SaveChangesAsync();



                    return RedirectToAction("BookList", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректный ввод данных");
            }
            return View(model);
        }
        #endregion

        #region Изменение записи о книге
        public async Task<IActionResult> EditBook(int? id)
        {
            if (id != null)
            {
                Book book = await db.Books.FirstOrDefaultAsync(b => b.Id == id);
                if (book != null)
                    return View(book);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditBook(Book book)
        {
            db.Books.Update(book);
            await db.SaveChangesAsync();
            return RedirectToAction("BookList");
        }
        #endregion

    }
}
