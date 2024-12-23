using ClientApplicationCore.Infrastructure;
using ClientApplicationCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientApplicationCore.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IConfiguration _configuration;
        private string endPoint;

        public ProductController(IHttpClientService httpClientService, IConfiguration configuration)
        {
            _httpClientService = httpClientService;
            _configuration = configuration;
            endPoint = _configuration["EndPoint:CivicaApi"];
        }

        public IActionResult Index()
        {
            ServiceResponse<IEnumerable<ProductViewModel>> response = new ServiceResponse<IEnumerable<ProductViewModel>>();
            //string endPoint = _configuration["EndPoint:CivicaApi"];
            response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<ProductViewModel>>>
                ($"{endPoint}Product/GetAllProducts", HttpMethod.Get, HttpContext.Request);
            if (response.Success)
            {
                return View(response.Data);
            }
            return View(new List<ProductViewModel>());
        }

        [HttpGet]
        [HttpGet]
        public IActionResult Create()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Categories = GetCategories();
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string apiUrl = $"{endPoint}Product/AddProduct";
                var response = _httpClientService.PostHttpResponseMessage(apiUrl, viewModel, HttpContext.Request);
                if (response.IsSuccessStatusCode)
                {
                    string successResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successResponse);
                    TempData["SuccessMessage"] = serviceResponse.Message;
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(errorResponse);
                    if (errorResponse != null)
                    {
                        TempData["ErrorMessage"] = serviceResponse.Message;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Something went wrong. Please try after sometime.";
                    }
                }

                return RedirectToAction("Index");
            }


            viewModel.Categories = GetCategories();
            return View(viewModel);
        }


        [HttpGet]
        public IActionResult CreateMVC5()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Categories = GetCategories();
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult CreateMVC5(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string apiUrl = $"{endPoint}Product/AddProduct";
                var response = _httpClientService.PostHttpResponseMessage(apiUrl, viewModel, HttpContext.Request);
                if (response.IsSuccessStatusCode)
                {
                    string successResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successResponse);
                    TempData["SuccessMessage"] = serviceResponse.Message;
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(errorResponse);
                    if (errorResponse != null)
                    {
                        TempData["ErrorMessage"] = serviceResponse.Message;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Something went wrong. Please try after sometime.";
                    }
                }

                return RedirectToAction("Index");
            }


            viewModel.Categories = GetCategories();
            return View(viewModel);
        }

        public IActionResult Details(int id)
        {

            var apiUrl = $"{endPoint}Product/GetProductById/" + id;
            var response = _httpClientService.GetHttpResponseMessage<ProductViewModel>(apiUrl, HttpContext.Request);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<ProductViewModel>>(data);

                if (serviceResponse != null && serviceResponse.Success && serviceResponse.Data != null)
                {
                    return View(serviceResponse.Data);
                }
                else
                {
                    TempData["ErrorMessage"] = serviceResponse.Message;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                string errorData = response.Content.ReadAsStringAsync().Result;
                var errorResponse = JsonConvert.DeserializeObject<ServiceResponse<ProductViewModel>>(errorData);

                if (errorResponse != null)
                {
                    TempData["ErrorMessage"] = errorResponse.Message;
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong please try after some time.";
                }

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            
            //string endPoint = _configuration["EndPoint:CivicaApi"];
            var apiUrl = $"{endPoint}Product/GetProductById/" + id;
            var response = _httpClientService.GetHttpResponseMessage<UpdateProductViewModel>(apiUrl, HttpContext.Request);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<UpdateProductViewModel>>(data);

                if (serviceResponse != null && serviceResponse.Success && serviceResponse.Data != null)
                {
                    serviceResponse.Data.Categories = GetCategories();
                    return View(serviceResponse.Data);
                }
                else
                {
                    TempData["ErrorMessage"] = serviceResponse.Message;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                string errorData = response.Content.ReadAsStringAsync().Result;
                var errorResponse = JsonConvert.DeserializeObject<ServiceResponse<UpdateProductViewModel>>(errorData);

                if (errorResponse != null)
                {
                    TempData["ErrorMessage"] = errorResponse.Message;
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong please try after some time.";
                }

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(UpdateProductViewModel updateProduct)
        {
            if (ModelState.IsValid)
            {
                //string endPoint = _configuration["EndPoint:CivicaApi"];
                var apiUrl = $"{endPoint}Product/ModifyProduct";
                HttpResponseMessage response = _httpClientService.PutHttpResponseMessage(apiUrl, updateProduct, HttpContext.Request);
                if (response.IsSuccessStatusCode)
                {
                    string successResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successResponse);
                    TempData["SuccessMessage"] = serviceResponse.Message;
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(errorResponse);
                    if (errorResponse != null)
                    {
                        TempData["ErrorMessage"] = serviceResponse.Message;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Something went wrong. Please try after sometime.";
                    }
                }
            }
            updateProduct.Categories = GetCategories();
            return View(updateProduct);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            //string endPoint = _configuration["EndPoint:CivicaApi"];
            var apiUrl = $"{endPoint}Product/GetProductById/" + id;
            var response = _httpClientService.GetHttpResponseMessage<ProductViewModel>(apiUrl, HttpContext.Request);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert
                    .DeserializeObject<ServiceResponse<ProductViewModel>>(data);
                if (serviceResponse != null && serviceResponse.Success && serviceResponse.Data != null)
                {
                    return View(serviceResponse.Data);
                }
                else
                {
                    TempData["ErrorMessage"] = serviceResponse.Message;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                string errorData = response.Content.ReadAsStringAsync().Result;
                var errorResponse = JsonConvert
                    .DeserializeObject<ServiceResponse<ProductViewModel>>(errorData);
                if (errorResponse != null)
                {
                    TempData["ErrorMessage"] = errorResponse.Message;
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong. Please try after sometime.";
                }

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int productId)
        {
            //string endPoint = _configuration["EndPoint:CivicaApi"];
            var apiUrl = $"{endPoint}Product/Remove/" + productId;
            //var response = _httpClientService.GetHttpResponseMessage<string>(apiUrl, HttpContext.Request);

            var response = _httpClientService.ExecuteApiRequest<ServiceResponse<string>>
                ($"{apiUrl}", HttpMethod.Delete, HttpContext.Request);

            if (response.Success)
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
                return RedirectToAction("Index");
            }


        }

        private List<CategoryViewModel> GetCategories()
        {
            ServiceResponse<IEnumerable<CategoryViewModel>> response = new ServiceResponse<IEnumerable<CategoryViewModel>>();
            //string endPoint = _configuration["EndPoint:CivicaApi"];
            response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<CategoryViewModel>>>
                ($"{endPoint}Category/GetAllCategories", HttpMethod.Get, HttpContext.Request);
            if (response.Success)
            {
                return response.Data.ToList();
            }
            return new List<CategoryViewModel>();
        }
    }
}
