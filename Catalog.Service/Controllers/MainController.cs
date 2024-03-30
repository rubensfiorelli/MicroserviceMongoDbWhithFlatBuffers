using Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private List<string> _getErrors = [];
        protected IReadOnlyList<string> Errors => _getErrors;

        protected ActionResult CustomResponse(object? result = null)
        {
            if (ValidOperation())
                return Ok(result);

            return BadRequest();
        }

        protected bool ResponseHasErros(Result result)
        {
            if (result == null || !result.Success) return false;

            return true;

        }

        protected bool ValidOperation() => Errors.Count == 0;
        protected void AddErrorToStack(string error) => _getErrors.Add(error);
        protected void ClearErrors() => _getErrors.Clear();
    }
}
