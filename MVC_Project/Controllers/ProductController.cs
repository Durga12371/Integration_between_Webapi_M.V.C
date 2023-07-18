
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Authorization;
using MVC_Project.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using NuGet.Protocol;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace MVC_Project.Controllers
{
    public class ProductController : Controller
    {
        Uri BaseAddress = new Uri("https://localhost:7289/api");
        private readonly HttpClient _client;
        public string resposeData { get; set; }
        public TempDataDictionary TempDataa { get; set; }
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _client= new HttpClient();
            _client.BaseAddress = BaseAddress;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Login(Users user)
        {
            TempData.Clear();
            response reew=new response();
            String data = JsonConvert.SerializeObject(user);
            //HttpResponseMessage accesstoken = _client.GetAsync(_client.BaseAddress + "/Login").Result;
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage respose = _client.PostAsync(_client.BaseAddress + "/Login", content).Result;
            if (respose.IsSuccessStatusCode)
            {
                resposeData= respose.Content.ReadAsStringAsync().Result;
                _configuration["response"] = resposeData;
               // res = JsonConvert.DeserializeObject<response>(resposeData);
                 
                TempData["msge"] = "Login Successfully";
                ModelState.Clear();
                return RedirectToAction("Index");


            }
            else
            {
                ModelState.Clear();
                TempData["msge"] = "Username or Password is incorrect";
            }
            return View();

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken.ToString());
            
           
        }
        public IActionResult Logout()
        {
            
            TempData.Clear();
            TempData["logout"] = "Logged out Successfully";
            return RedirectToAction("Login");
        }
        public IActionResult AfterLogin()
        {
            return View();
        }
        public IActionResult Index()
        {
            List<Product> productlist=new List<Product>();
            ViewBag.Msg = "Product_Details";
            //HttpResponseMessage accesstoken = _client.GetAsync(_client.BaseAddress + "/Login").Result;
            JObject json = JObject.Parse(_configuration["response"]);
            string token = json["token"].ToString();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            

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

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", " eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
                "eyJleHAiOjE2ODg0NzA5MzcsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyODkvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzI4OS8ifQ.k6dD2Yex98hSmml9InuPZN_4sx6bbw3wbh1egumNDso");
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

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", " eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
                "eyJleHAiOjE2ODg0NzA5MzcsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyODkvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzI4OS8ifQ.k6dD2Yex98hSmml9InuPZN_4sx6bbw3wbh1egumNDso");

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
            TempData.Clear();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            TempData.Clear();
            String data=JsonConvert.SerializeObject(model);
            StringContent content=new StringContent(data,Encoding.UTF8,"application/json");
            JObject json = JObject.Parse(_configuration["response"]);
            string token = json["token"].ToString();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
            TempData.Clear();
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

            TempData.Clear();
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
