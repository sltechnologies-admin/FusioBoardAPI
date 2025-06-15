using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected string CorrelationId =>
            HttpContext?.Items["X-Correlation-ID"]?.ToString() ?? Guid.NewGuid().ToString();
    }


}
