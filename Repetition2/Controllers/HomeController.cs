using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Repetition2.Interfaces;
using Repetition2.Models;
using Repetition2.Resources;

namespace Repetition2.Controllers
{
    public class HomeController : Controller
    {
		private ITimeProvider timeProvider;
		private readonly IStringLocalizer localizer;

		public HomeController(ITimeProvider _timeProvider, IStringLocalizerFactory factory)
		{
			timeProvider = _timeProvider;
			localizer = factory.Create(typeof(SharedResources));
		}

		public IActionResult Index()
        {
			ViewBag.Time = timeProvider.Now.ToString();
			return View();
        }

		public IActionResult About()
		{
			ViewData["Message"] = CultureInfo.CurrentUICulture.ToString();

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = localizer["ContactUs"];

			return View();
		}


		public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
