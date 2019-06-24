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
           ProductListViewModel result = (ProductListViewModel)controller.List(2).Model;

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
            ProductListViewModel result = (ProductListViewModel)controller.List(2).Model;

            //assert 
            PaginInfo paginInfo = result.PaginInfo;
            Assert.AreEqual(paginInfo.CurrentPage, 2);
            Assert.AreEqual(paginInfo.ItemsPerPage, 3);
            Assert.AreEqual(paginInfo.TotalItems, 5);
            Assert.AreEqual(paginInfo.TotalPages, 2);
        }
    }
}
