using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace Test
{
    public class SampleData
    {
        public static void Initialize(BookContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                   new Role
                   {
                       
                       Name = "admin",

                   },
                   new Role
                   {
                       
                       Name = "user",
                   }
               );
                context.SaveChanges();
            }
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                   new User
                   {
                       Email = "admin@mail.ru",
                       Password = "12345",
                       RoleId = 1
                   }
                );
                context.SaveChanges();
            }
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book
                    {
                        AuthorId = 1,
                        Name = "К югу от границы, на запад от Солнца",
                        PublishingHouse = "Эксмо",
                        YearOfPublishing = 2016                        
                    },
                    new Book
                    {
                        AuthorId = 1,
                        Name = "Бесцветный Цкуру Тадзаки и годы его странствий",
                        PublishingHouse = "Эксмо",
                        YearOfPublishing = 2016
                    },
                    new Book
                    {
                        AuthorId = 2,
                        Name = "Учение дона Хуана",
                        PublishingHouse = "София",
                        YearOfPublishing = 2018
                    }
                );
                context.SaveChanges();
            }
            if (!context.Authors.Any()) {
                context.Authors.AddRange(
                    new Author
                    {
                        Surname = "Мураками",
                        Name = "Харуки",
                        Patronymic = ""
                    },
                    new Author
                    {
                        Surname = "Кастанеда",
                        Name = "Карлос",
                        Patronymic = ""
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
