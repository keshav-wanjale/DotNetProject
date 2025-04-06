using Microsoft.Extensions.Logging;
using System.Web.Http;
namespace CasCap.Controllers
{
    [RoutePrefix("api")]
    public class ValuesController : ApiController
    {
        readonly ILogger<ValuesController> _logger;
        readonly IDITestService _diTestSvc;

        public ValuesController(ILogger<ValuesController> logger, IDITestService diTestSvc)
        {
            _logger = logger;
            _diTestSvc = diTestSvc;
        }

        [HttpGet]
        public IHttpActionResult TestDI()
        {
            _logger.LogTrace("TestDI REST endpoint fired...");
            var ints = _diTestSvc.GetIntValues();
            return Ok(ints);
        }
    }
}