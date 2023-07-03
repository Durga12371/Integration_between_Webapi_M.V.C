using Crud_With_WEB_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud_With_WEB_API.Models;

namespace Crud_With_WEB_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductRepository _repository;

        public ProductController(IProductRepository repo)
        {
            _repository= repo;
        }
        
       [HttpPost]
        public async Task<ActionResult<List<Product_Table>>> CreateProduct(Product_Table prod)
        {
            return await _repository.AddProduct(prod);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Product_Table>>> GetAllc()
        {
            return await _repository.getAllEmployees();
        }

        [HttpGet("GetId")]
        public async Task<ActionResult<Product_Table>> GetByIdProduct(int id)
        {
            return await _repository.GetProductById(id);
        }

        [HttpPut]
        public async Task<ActionResult<List<Product_Table>>> UpdateProduct(Product_Table prod)
        {
            return await _repository.UpdateProduct(prod);
        }


        [HttpDelete("id")]
        public async Task<ActionResult<List<Product_Table>>> deleteproduct(int id)
        {
            return await _repository.DeleteProduct(id);
        }
    }
}
