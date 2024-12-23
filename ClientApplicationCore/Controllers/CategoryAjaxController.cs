﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientApplicationCore.Controllers
{
    [Authorize]
    public class CategoryAjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(0);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpGet]   
        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}