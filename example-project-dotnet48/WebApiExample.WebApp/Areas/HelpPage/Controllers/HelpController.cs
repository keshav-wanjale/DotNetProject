using System;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.WebApp.Areas.HelpPage.ModelDescriptions;
using WebApiExample.WebApp.Areas.HelpPage.Models;

namespace WebApiExample.WebApp.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    [Route("api/[controller]")]
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController()
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Migration note: ASP.NET Core does not have GlobalConfiguration;
            // Replace with your own documentation provider logic as needed.
            ViewBag.DocumentationProvider = null;
            return View();
        }

        [HttpGet("{apiId}")]
        public IActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = null; // TODO: Retrieve your API model
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        [HttpGet("model/{modelName}")]
        public IActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescription modelDescription = null; // TODO: Retrieve your model description
                if (modelDescription != null)
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
