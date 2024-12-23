using ClientApplicationCore.Infrastructure;
using ClientApplicationCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ClientApplicationCore.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IConfiguration _configuration;
        private string endPoint;

        public CategoryController(IHttpClientService httpClientService,IConfiguration configuration)
        {
            _httpClientService = httpClientService;
            _configuration = configuration;
            endPoint = _configuration["EndPoint:CivicaApi"];
        }
        public IActionResult Index()

        {
            ServiceResponse<IEnumerable<CategoryViewModel>> response = new ServiceResponse<IEnumerable<CategoryViewModel>>();
            //string endPoint = _configuration["EndPoint:CivicaApi"];
            response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<CategoryViewModel>>>
                ($"{endPoint}Category/GetAllCategories", HttpMethod.Get, HttpContext.Request);
            if(response.Success)
            {
                return View(response.Data);
            }
            return View(new List<CategoryViewModel>());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //string endPoint = _configuration["EndPoint:CivicaApi"];
                string apiUrl = $"{endPoint}Category/Create";
                var response = _httpClientService.PostHttpResponseMessage<AddCategoryViewModel>(apiUrl, viewModel, HttpContext.Request);
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<UpdateCategoryViewModel>>(data);

                    if (serviceResponse != null && serviceResponse.Success && serviceResponse.Data != null)
                    {
                        return View(serviceResponse.Data);
                    }
                    else
                    {
                        TempData["SuccessMessage"] = serviceResponse.Message;
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    string errorData = response.Content.ReadAsStringAsync().Result;
                    var errorResponse = JsonConvert.DeserializeObject<ServiceResponse<UpdateCategoryViewModel>>(errorData);

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
            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            //string endPoint = _configuration["EndPoint:CivicaApi"];
            var apiUrl = $"{endPoint}Category/GetCategoryById/" + id;
            var response = _httpClientService.GetHttpResponseMessage<UpdateCategoryViewModel>(apiUrl, HttpContext.Request);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<UpdateCategoryViewModel>>(data);

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
                var errorResponse = JsonConvert.DeserializeObject<ServiceResponse<UpdateCategoryViewModel>>(errorData);

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
            var apiUrl = $"{endPoint}Category/GetCategoryById/" + id;
            var response = _httpClientService.GetHttpResponseMessage<UpdateCategoryViewModel>(apiUrl, HttpContext.Request);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<UpdateCategoryViewModel>>(data);

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
                var errorResponse = JsonConvert.DeserializeObject<ServiceResponse<UpdateCategoryViewModel>>(errorData);

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
        public IActionResult Edit(UpdateCategoryViewModel updateCategory)
        {
            if (ModelState.IsValid)
            {
                //string endPoint = _configuration["EndPoint:CivicaApi"];
                var apiUrl = $"{endPoint}Category/ModifyCategory";
                HttpResponseMessage response = _httpClientService.PutHttpResponseMessage(apiUrl, updateCategory, HttpContext.Request);
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
            return View(updateCategory);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            //string endPoint = _configuration["EndPoint:CivicaApi"];
            var apiUrl = $"{endPoint}Category/GetCategoryById/" + id;
            var response = _httpClientService.GetHttpResponseMessage<CategoryViewModel>(apiUrl, HttpContext.Request);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert
                    .DeserializeObject<ServiceResponse<CategoryViewModel>>(data);
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
                    .DeserializeObject<ServiceResponse<CategoryViewModel>>(errorData);
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
        public IActionResult DeleteConfirmed(int categoryId)
        {
            //string endPoint = _configuration["EndPoint:CivicaApi"];
            var apiUrl = $"{endPoint}Category/Remove/" + categoryId;
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
    }
}
