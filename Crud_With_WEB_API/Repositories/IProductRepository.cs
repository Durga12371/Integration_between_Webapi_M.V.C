using Crud_With_WEB_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Crud_With_WEB_API.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Product_Table>> getAllEmployees();
        public Task<List<Product_Table>> AddProduct(Product_Table prduct);
        public Task<List<Product_Table>> DeleteProduct(int id);
        public Task<List<Product_Table>> UpdateProduct(Product_Table prduct);
        public Task<Product_Table> GetProductById(int id);

    }
}
