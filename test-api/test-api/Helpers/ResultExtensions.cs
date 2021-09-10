using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace test_api.Helpers
{
    public static class ResultExtensions
    {
        public static async Task<IActionResult> ToActionResult<T>(this Task<T> resultTask)
        {
            var result = await resultTask;

            return result is null or Unit
                ? new OkResult()
                : new OkObjectResult(result);
        }
    }
}
