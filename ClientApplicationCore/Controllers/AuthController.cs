using ClientApplicationCore.Infrastructure;
using ClientApplicationCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientApplicationCore.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IHttpClientService _httpClientService;

        private readonly IConfiguration _configuration;

        private string endPoint;

        public AuthController(IHttpClientService httpClientService, IConfiguration configuration)
        {
            _httpClientService = httpClientService;
            _configuration = configuration;
            endPoint = _configuration["EndPoint:CivicaApi"];
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RegisterUser(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var apiUrl = $"{endPoint}Auth/Register";
                var response = _httpClientService.PostHttpResponseMessage(apiUrl, registerViewModel, HttpContext.Request);

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(data);

                    if (serviceResponse != null && serviceResponse.Success && serviceResponse.Data != null)
                    {
                        return View(serviceResponse.Data);
                    }
                    else
                    {
                        TempData["SuccessMessage"] = serviceResponse.Message;
                        return RedirectToAction("RegisterSuccess");
                    }
                }
                else
                {
                    string errorData = response.Content.ReadAsStringAsync().Result;
                    var errorResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(errorData);

                    if (errorResponse != null)
                    {
                        TempData["ErrorMessage"] = errorResponse.Message;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Something went wrong please try after some time.";
                    }
                    //return RedirectToAction("Index");
                }





            }
            return View(registerViewModel);
        }


        public IActionResult RegisterSuccess()
        {
            return View();
        }


        [HttpGet]
        public IActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginUser(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string apiUrl = $"{endPoint}Auth/Login";
                var response = _httpClientService.PostHttpResponseMessage(apiUrl, viewModel, HttpContext.Request);
                if (response.IsSuccessStatusCode)
                {
                    string successResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successResponse);

                    string token = serviceResponse.Data;
                    //Response.Cookies.Append("jwtToken", token, new CookieOptions
                    //{
                    //    HttpOnly = true,
                    //    Secure = true,
                    //    SameSite = SameSiteMode.Strict
                    //});
                    Response.Cookies.Append("jwtToken", token, new CookieOptions
                    {
                        HttpOnly = false,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires= DateTime.UtcNow.AddHours(1)
                    });


                    return RedirectToAction("Index", "Category");
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
                        TempData["ErrorMessage"] = "Something went wrong, please try after some time.";
                    }
                }
            }
            return View(viewModel);


        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwtToken");

            return RedirectToAction("Index", "Home");
        }
    }
}
