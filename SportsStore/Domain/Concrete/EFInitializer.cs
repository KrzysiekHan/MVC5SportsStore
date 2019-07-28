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
            //initialize product list
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

            #endregion

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


            context.Products.AddRange(products);
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
