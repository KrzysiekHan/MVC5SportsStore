using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Controllers;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.HtmlHelpers;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[]
                {
                    new Product{ProductID = 1,Name = "P1"},
                    new Product{ProductID = 2,Name = "P2"},
                    new Product{ProductID = 3,Name = "P3"},
                    new Product{ProductID = 4,Name = "P4"},
                    new Product{ProductID = 5,Name = "P5"}
                }
                );
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //act
            ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Model;

            //assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //arrange
            HtmlHelper myHelper = null;

            PaginInfo paginInfo = new PaginInfo
            {
                CurrentPage = 2,
                TotalItems = 30,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Strona" + i;

            //act
            MvcHtmlString result = myHelper.PageLinks(paginInfo, pageUrlDelegate);

            //assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Strona1"">1</a>" + @"<a class=""btn btn-default btn-primary selected"" href=""Strona2"">2</a>" + @"<a class=""btn btn-default"" href=""Strona3"">3</a>", result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product{ProductID = 1,Name = "P1"},
                new Product{ProductID = 2,Name = "P2"},
                new Product{ProductID = 3,Name = "P3"},
                new Product{ProductID = 4,Name = "P4"},
                new Product{ProductID = 5,Name = "P5"}
            }
            );

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //act
            ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Model;

            //assert 
            PaginInfo paginInfo = result.PaginInfo;
            Assert.AreEqual(paginInfo.CurrentPage, 2);
            Assert.AreEqual(paginInfo.ItemsPerPage, 3);
            Assert.AreEqual(paginInfo.TotalItems, 5);
            Assert.AreEqual(paginInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product{ProductID = 1,Name = "P1",Category = "Cat1"},
                new Product{ProductID = 2,Name = "P2",Category = "Cat2"},
                new Product{ProductID = 3,Name = "P3",Category = "Cat1"},
                new Product{ProductID = 4,Name = "P4",Category = "Cat2"},
                new Product{ProductID = 5,Name = "P5",Category = "Cat3"}
            }
            );

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //act
            Product[] result = ((ProductListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            //assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            //sprawdzenie czy kontroler usuwa duplikaty i segreguje kategorie według alfabetu
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m=>m.Products).Returns(new Product[] {
                new Product{ProductID = 1, Name = "P1", Category = "Jabłka"},
                new Product{ProductID = 2, Name = "P2", Category = "Jabłka"},
                new Product{ProductID = 3, Name = "P3", Category = "Śliwki"},
                new Product{ProductID = 4, Name = "P4", Category = "Pomarańcze"}
            });
            NavController target = new NavController(mock.Object);

            //act
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

            //assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Jabłka");
            Assert.AreEqual(results[1], "Pomarańcze");
            Assert.AreEqual(results[2], "Śliwki");

        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            //sprawdzamy czy metoda menu prawidłowo dodaje informacje na temat wybranej kategorii
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ProductID = 1, Name = "P1",Category = "Jabłka"},
                new Product {ProductID = 4, Name = "P2",Category = "Pomarańcze"},
            });

            NavController target = new NavController(mock.Object);
            string categoryToSelect = "Jabłka";

            //act
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            //assert
            Assert.AreEqual(categoryToSelect, result);
        }
    }
}
