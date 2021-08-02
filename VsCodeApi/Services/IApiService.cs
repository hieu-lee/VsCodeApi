using System.Collections.Generic;
using System.Threading.Tasks;
using VsCodeApi.Models;

namespace VsCodeApi.Services
{
    public interface IApiService
    {
        Task<bool> AddApi(ApiInfo api);
        Task<bool> AddUpvote(string UserId, string ApiId);
        Task DeleteApi(string ApiId, string UserId);
        Task<ApiInfo> GetApi(string id);
        Task<List<ApiInfo>> GetApis();
        Task<ApiInfo[]> GetApisByName(string Search);
        Task<string[]> GetUpvotedApis(string UserId);
        Task<Upvote[]> GetUpvotes();
        Task<ApiInfo[]> GetUserApis(string UserId);
        Task RemoveUpvote(string UserId, string ApiId);
        Task UpdateApi(ApiInfo api, string UserId);
    }
}