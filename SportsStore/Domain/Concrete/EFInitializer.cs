using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
   public class EFInitializer: DropCreateDatabaseAlways<EFDbContext>
    {
        
        protected override void Seed(EFDbContext context)
        {
            #region OrderStatuses
            IList<OrderStatus> orderStatus = new List<OrderStatus>();
            orderStatus.Add(new OrderStatus { OrderStatusId = 1, Description = "Oczekujące na płatność" });
            orderStatus.Add(new OrderStatus { OrderStatusId = 2, Description = "Kolekcjonowanie zamówienia" });
            orderStatus.Add(new OrderStatus { OrderStatusId = 3, Description = "Pakowanie" });
            orderStatus.Add(new OrderStatus { OrderStatusId = 4, Description = "Oczekiwanie na kuriera" });
            orderStatus.Add(new OrderStatus { OrderStatusId = 5, Description = "Wysłane" });
            orderStatus.Add(new OrderStatus { OrderStatusId = 6, Description = "Anulowane" });
            context.OrderStatuses.AddRange(orderStatus);
            #endregion
            #region Products
            IList<Product> products = new List<Product>();
            products.Add(new Product
            {
                ProductID = 1,
                Category = "Szachy",
                Name = "Szachownica",
                Description = "Drewniana szachownica",
                Price = 49.99M,
                ImageData = this.ImageToArray("szachy.jpg"),
                ImageMimeType = "image/jpeg"
            });
            products.Add(new Product
            {
                ProductID = 2,
                Category = "Szachy",
                Name = "Zegar",
                Description = "Zegar do szachów",
                Price = 25.00M,
                ImageData = this.ImageToArray("czapka.jpg"),
                ImageMimeType = "image/jpeg"
            });
            products.Add(new Product
            {
                ProductID = 3,
                Category = "Piłka nożna",
                Name = "Piłka",
                Description = "Piłka skórzana rozmiar 5",
                Price = 34.40M,
                ImageData = this.ImageToArray("piłka.jpg"),
                ImageMimeType = "image/jpeg"
            });
            products.Add(new Product
            {
                ProductID = 3,
                Category = "Piłka nożna",
                Name = "Bramka",
                Description = "Bramka treningowa dla juniorów",
                Price = 165.00M,
                ImageData = this.ImageToArray("bramka.jpg"),
                ImageMimeType = "image/jpeg"
            });
            context.Products.AddRange(products);
            #endregion
            #region Users
            UserDetail userDetail = new UserDetail()
            {
                ID = 1,
                FirstName = "Jan",
                LastName = "Nowak",
                City = "Kraków",
                State = "Małopolska",
                Street = "11 Listopada",
                BuildingNr = "13",
                Country = "Polska",
                EmailAdress = "Jn@gmail.com",
                PhoneNumber = "+48 333 243 554",
                Zip = "35-359"
            };
            context.Users.Add(new User
            {
                UserId = 1,
                Username = "admin",
                Password = "a",
                UserDetail = userDetail,
                UserRole = "admin"
            });
            #endregion

            base.Seed(context);
        }

        public byte[] ImageToArray(string filename)
        {
            string filePath = @"c:\img\" + filename;
            byte [] arr = File.ReadAllBytes(filePath);
            return arr;
        }
    }
}
