using Crud_With_WEB_API.Controllers;
using Crud_With_WEB_API.Models;
using Crud_With_WEB_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MS_Unit_Testing_in_Webapi
{
    [TestClass]
    public class UnitTest1
    {
        private ProductController _productController;
        private Mock<IProductRepository> _mockRepository;

        public UnitTest1()
        {
            
        }

        [TestInitialize]
        public void TestSetup()
        {
            _mockRepository = new Mock<IProductRepository>();
            _productController = new ProductController(_mockRepository.Object);
        }

        [TestMethod]
        [TestCategory("Create_Product")]
        public async Task CreateProduct_ReturnsActionResult()
        {
            // Arrange
            var product = new Product_Table();
           

            // Act
            var result = await _productController.CreateProduct(product);
            

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Product_Table>>));
          
            
        }

        [TestMethod]
        [TestCategory("Get_Products")]
        public async Task GetAllc_ReturnsActionResult()
        {
            // Act
            var result = await _productController.GetAllc();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Product_Table>>));
        }

        [TestMethod("GetProduct_By_ID")]
        public async Task GetByIdProduct_ReturnsActionResult()
        {
            // Arrange
            int productId = 1;

            // Act
            var result = await _productController.GetByIdProduct(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Product_Table>));
        }

        [TestMethod]
        [TestCategory("Update_Product_Table")]
        public async Task UpdateProduct_ReturnsActionResult()
        {
            // Arrange
            var product = new Product_Table();

            // Act
            var result = await _productController.UpdateProduct(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Product_Table>>));
        }

        [TestMethod]
        [TestCategory("Delete_Product")]
        public async Task deleteproduct_ReturnsActionResult()
        {
            // Arrange
            int productId = 1;
            System.Threading.Thread.Sleep(1000);
            // Act
            var result = await _productController.deleteproduct(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Product_Table>>));
        }
    } 
}