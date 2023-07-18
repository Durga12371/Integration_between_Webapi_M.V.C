using Crud_With_WEB_API.Models;
using Crud_With_WEB_API.Repositories;
using Dapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS_Unit_Testing_in_Webapi
{
    [TestClass]
    public class Business_Logic_Unit_Testing
    {
        private Mock<IDbConnection> _dbConnectionMock;
        private IProductRepository _productRepository;

        [TestInitialize]
        public void Setup()
        {
            _dbConnectionMock = new Mock<IDbConnection>();
            _productRepository = new ProductRepository(_dbConnectionMock.Object);
        }

        //[TestMethod]
        //public async Task GetAllEmployees_ReturnsListOfEmployees()
        //{
        //    // Arrange
        //    var expectedEmployees = new List<Product_Table> { };
        //    _dbConnectionMock
        //         .Setup(c => c.QueryAsync<Product_Table>("GetProductList", null, null, null, CommandType.StoredProcedure))
        //         .ReturnsAsync(expectedEmployees);


        //    // Act
        //    var result = await _productRepository.getAllEmployees();

        //    // Assert
        //    Assert.IsNotNull(result);
        //    // Perform assertions on the returned result
        //}

    }
}
