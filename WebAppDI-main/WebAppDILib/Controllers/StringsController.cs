using Microsoft.Extensions.Logging;
using System.Web.Http;
namespace CasCap.Controllers
{
    [RoutePrefix("api")]
    public class StringsController : ApiController
    {
        readonly ILogger<StringsController> _logger;
        readonly IDITestService _diTestSvc;

        public StringsController(ILogger<StringsController> logger, IDITestService diTestSvc)
        {
            _logger = logger;
            _diTestSvc = diTestSvc;
        }

        [HttpGet]
        public IHttpActionResult TestDI()
        {
            _logger.LogTrace("TestDI REST endpoint fired...");
            var strings = _diTestSvc.GetStringValues();
            return Ok(strings);
        }
    }
}