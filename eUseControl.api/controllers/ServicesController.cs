using Microsoft.AspNetCore.Mvc;

namespace eUseControl.api.controllers;

public class ServicesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}