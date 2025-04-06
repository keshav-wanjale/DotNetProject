using CasCap.ViewModels;
using Microsoft.Extensions.Logging;
using System.Web.Mvc;
namespace CasCap.Controllers
{
    public class AboutController : Controller
    {
        readonly ILogger<AboutController> _logger;
        readonly IDITestService _diTestSvc;

        public AboutController(ILogger<AboutController> logger, IDITestService diTestSvc)
        {
            _logger = logger;
            _diTestSvc = diTestSvc;
        }

        public ActionResult Index()
        {
            var vm = new IndexViewModel
            {
                SomeIntValues = _diTestSvc.GetIntValues(),
                SomeStringValues = _diTestSvc.GetStringValues()
            };
            return View(vm);
        }
    }
}