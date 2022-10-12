namespace PTW_API.Controllers
{
    using FluentResults;
    using Microsoft.AspNetCore.Mvc;
    using PTW.Domain.Services;

    public class ExtendedApiController : ControllerBase
    {
        protected IActionResult OkOrError<T>(Result<T> result)
        {
            IActionResult? errorResponse = GetErrorResponse<T>(result);

            if (errorResponse != null)
            {
                return errorResponse;
            }

            return Ok(result);
        }

        private IActionResult? GetErrorResponse<T>(Result<T> result)
        {
            if (result.IsFailed)
            {
                IActionResult errorResponse = new ObjectResult(result)
                {
                    DeclaredType = typeof(T),
                    StatusCode = ConvertToHttpStatusCode<T>(result)
                };

                return errorResponse;
            }

            return null;
        }

        private int? ConvertToHttpStatusCode<T>(Result<T> result)
        {
            switch(result.Reasons[0])
            {
                case ResultErrorCodes:
                    return 500;
            }

            return null;
        }
    }
}
