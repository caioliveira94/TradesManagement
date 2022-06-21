using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TradesManagement.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponsePreserve(object result = null)
        {
            if (IsValidOperation())
            {
                //result = JsonSerializer.Serialize(result, new JsonSerializerOptions
                //{
                //    PropertyNameCaseInsensitive = true,
                //    ReferenceHandler = ReferenceHandler.Preserve
                //});
                result = JsonSerializer.Serialize(result);

                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AddError(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool IsValidOperation()
        {
            return !Errors.Any();
        }

        protected void AddError(string erro)
        {
            Errors.Add(erro);
        }

        protected void ClearErrors()
        {
            Errors.Clear();
        }

        protected bool IsValidId(long id)
        {
            return id > 0;
        }

        protected bool IsArgumentNull(object obj)
        {
            bool result = obj is null;
            if (result)
                AddError("Invalid Argument");
            return result;
        }
    }
}
