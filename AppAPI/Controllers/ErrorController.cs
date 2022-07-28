using AppAPI.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    public class ErrorController : BaseRouteController
    {
        [Route("error/{code}")]
        // this will tell the swagger to neglect this api end point
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error(int code)
        {
            // getting the error code from program.cs middleware added and pass api response object
            return new ObjectResult(new ApiResponse(code));
        }
    }
}