using ExerciseMicroservices.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseMicroservices.Shared.ControllerBases
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(ResponseDto<T> responseDto)
        {
            return new ObjectResult(responseDto)
            {
                StatusCode = responseDto.StatusCode
            };
        }
    }
}
