using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MVC_Project.Models;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace MVC_Project.Controllers
{
    public class ProductController : Controller
    {
        Uri BaseAddress = new Uri("https://localhost:7289/api");
        private readonly HttpClient _client;
        public TempDataDictionary TempDataa { get; set; }

        public ProductController()
        {
            _client= new HttpClient();
            _client.BaseAddress = BaseAddress;
        }
        public IActionResult Index()
        {
            List<Product> productlist=new List<Product>();
            ViewBag.Msg = "Product_Details";
            
            HttpResponseMessage respo = _client.GetAsync(_client.BaseAddress + "/Product/GetAllc/GetAll").Result;
            if (respo.IsSuccessStatusCode)
            {
                String data = respo.Content.ReadAsStringAsync().Result;
                productlist= JsonConvert.DeserializeObject<List<Product>>(data);


            }
            return View(productlist);
        }
        public IActionResult DetailsVM()
        {
            List<ProductViewModel> productlist=new List<ProductViewModel>();
            

            HttpResponseMessage respo = _client.GetAsync(_client.BaseAddress + "/Product/GetAllc/GetAll").Result;
            if (respo.IsSuccessStatusCode)
            {
                String data = respo.Content.ReadAsStringAsync().Result;
                productlist = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);


            }
            return View(productlist);
        }
        public IActionResult DetailsVMM()
        {
            List<ProductCopy> productlist = new List<ProductCopy>();
           


            HttpResponseMessage respo = _client.GetAsync(_client.BaseAddress + "/Product/GetAllc/GetAll").Result;
            if (respo.IsSuccessStatusCode)
            {
                String data = respo.Content.ReadAsStringAsync().Result;
                productlist = JsonConvert.DeserializeObject<List<ProductCopy>>(data);


            }

            TempData["ListOfProducts"] = productlist;
            return View();
        }
        
      
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            
            String data=JsonConvert.SerializeObject(model);
            StringContent content=new StringContent(data,Encoding.UTF8,"application/json");
            HttpResponseMessage respose = _client.PostAsync(_client.BaseAddress+ "/Product/CreateProduct", content).Result;
            if(respose.IsSuccessStatusCode)
            {
                TempData["msg"] = "Created Successfully";
                ModelState.Clear();
               // return RedirectToAction("Index");
            }
            else
            {
                TempData["msg"] = "OOPS.... Something Went Wrong!";
            }
            return View();
        }
        public IActionResult Edit(int id)
        {

            Product product = new Product();

            HttpResponseMessage respo = _client.GetAsync(_client.BaseAddress + "/Product/GetByIdProduct/GetId?id="+id).Result;
            if (respo.IsSuccessStatusCode)
            {
                String data = respo.Content.ReadAsStringAsync().Result;
                 product = JsonConvert.DeserializeObject<Product>(data);


            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            String data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage respose = _client.PutAsync(_client.BaseAddress + "/Product/UpdateProduct", content).Result;
            if (respose.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View("Create",model);
        }
       
        public IActionResult Delete()
        {


            
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
           
              
            HttpResponseMessage respose = _client.DeleteAsync(_client.BaseAddress + "/Product/deleteproduct/id?id=" + id).Result;
            if (respose.IsSuccessStatusCode)
            {
                TempData["msgr"] = "Deleted Successfully";
                ModelState.Clear();
                // return RedirectToAction("Index");
            }
            else
            {
                TempData["msgr"] = "OOPS.... Something Went Wrong!";
            }
            return View();
        }
    }
}
