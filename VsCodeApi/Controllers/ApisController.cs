using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VsCodeApi.Models;
using VsCodeApi.Services;

namespace VsCodeApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApisController : ControllerBase
    {
        private readonly ApiService apiService;

        public ApisController(ApiService apiService)
        {
            this.apiService = apiService;
        }

        [HttpGet("all-apis")]
        public async Task<IActionResult> GetApis()
        {
            var res = await apiService.GetApis();
            return Ok(res);
        }
        
        [HttpGet("search-apis/{text}")]
        public async Task<IActionResult> GetApisByName(string text)
        {
            var res = await apiService.GetApisByName(text);
            return Ok(res);
        }

        [HttpGet("api/{id}")]
        public async Task<IActionResult> GetApi(string id)
        {
            var res = await apiService.GetApi(id);
            return Ok(res);
        }

        [HttpGet("user-api/{UserId}")]
        public async Task<IActionResult> GetUserApis(string UserId)
        {
            var res = await apiService.GetUserApis(UserId);
            return Ok(res);
        }

        [HttpGet("user-upvoted/{UserId}")]
        public async Task<IActionResult> GetUpvotedApis(string UserId)
        {
            var res = await apiService.GetUpvotedApis(UserId);
            return Ok(res);
        }

        [HttpGet("upvotes")]
        public async Task<IActionResult> GetUpvotes()
        {
            var res = await apiService.GetUpvotes();
            return Ok(res);
        }

        [HttpPost("add-api")]
        public async Task<IActionResult> AddApi([FromBody] ApiInfo NewApi)
        {
            var res = await apiService.AddApi(NewApi);
            return res ? Ok() : StatusCode(409, "Api already existed");
        }

        [HttpPost("add-upvote/{UserId}/{ApiId}")]
        public async Task<IActionResult> AddUpvote(string UserId, string ApiId)
        {
            var res = await apiService.AddUpvote(UserId, ApiId);
            return res ? Ok() : StatusCode(409, "User already upvoted the api");
        }

        [HttpPut("update-api")]
        public async Task<IActionResult> UpdateApi([FromBody] ApiInfo api, [FromHeader] string UserId)
        {
            await apiService.UpdateApi(api, UserId);
            return Ok();
        }

        [HttpDelete("remove-upvote/{UserId}/{ApiId}")]
        public async Task<IActionResult> RemoveUpvote(string UserId, string ApiId)
        {
            await apiService.RemoveUpvote(UserId, ApiId);
            return Ok();
        }

        [HttpDelete("delete-api/{ApiId}")]
        public async Task<IActionResult> DeleteApi(string ApiId, [FromHeader] string UserId)
        {
            await apiService.DeleteApi(ApiId, UserId);
            return Ok();
        }
    }
}
