using Dapper;
using Crud_With_WEB_API.Models;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Crud_With_WEB_API.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Crud_With_WEB_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection dbConnection;
        private IDbConnection @object;

        public ProductRepository(IConfiguration configuration)
        {
            this.dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public ProductRepository(IDbConnection @object)
        {
            this.@object = @object;
        }

        public async Task<List<Product_Table>> getAllEmployees()
        {


            var employees = await dbConnection.QueryAsync<Product_Table>("GetProductList", commandType: CommandType.StoredProcedure);
            return (employees.ToList());
        }
        public async Task<Product_Table> GetProductById(int id)
        {
            var emp= await dbConnection.QueryFirstOrDefaultAsync<Product_Table>("Getid",new {ProductID=id }, commandType: CommandType.StoredProcedure);
            return emp;
        }
        public async Task<List<Product_Table>> AddProduct(Product_Table prduct)
        {

            var pr = await dbConnection.QueryAsync<Product_Table>("InsertProduct",
                new { Id = prduct.ProductID, ProductName = prduct.ProductName, Price = prduct.Price, Qty = prduct.Qty }, commandType: CommandType.StoredProcedure);
            return (await getAllEmployees());
        }
        



         public async Task<List<Product_Table>> DeleteProduct(int id)
        {
            var del= await dbConnection.QueryAsync<Product_Table>("Deleteproduct",
                new { Id = id }, commandType: CommandType.StoredProcedure);
            
            return (await getAllEmployees());

        }

        public  async Task<List<Product_Table>> UpdateProduct(Product_Table prduct)
        {
            var pr = await dbConnection.QueryAsync<Product_Table>("UpdateProduct",
 
            new { Id= prduct.ProductID, ProductName = prduct.ProductName, Price = prduct.Price, Qty = prduct.Qty }, commandType: CommandType.StoredProcedure);
            return (await getAllEmployees());
        }
    }
}
