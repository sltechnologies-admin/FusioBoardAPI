using API.Constants;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected string CorrelationId =>
            HttpContext?.Items[HeaderKeys.CorrelationId]?.ToString() ?? Guid.NewGuid().ToString();
    }


}
