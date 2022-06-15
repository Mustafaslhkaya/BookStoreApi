using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace PatikaApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.AddRange(
                new Book
                {
                    //Id = 1,
                    Title = "Lean Startup",
                    GenreId = 1,
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12)

                },
                new Book
                    {
                        //Id = 2,
                        Title = "Akşam Güneşi",
                        GenreId = 2,
                        PageCount = 300,
                        PublishDate = new DateTime(2005, 09, 22)

                    },

                     new Book
                     {
                         //Id = 3,
                         Title = "Çalıkuşu",
                         GenreId = 2,
                         PageCount = 100,
                         PublishDate = new DateTime(2015, 11, 05)

                     }
  


                    );

                context.SaveChanges();
            
            }
            

            
        }
    }
}
