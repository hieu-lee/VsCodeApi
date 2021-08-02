using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VsCodeApi.Data;
using VsCodeApi.Models;

namespace VsCodeApi.Services
{
    public class ApiService : IApiService
    {
        private readonly AppDbContext dbContext;

        public ApiService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiInfo>> GetApis()
        {
            var res = await dbContext.Apis.ToListAsync();
            res.Sort();
            return res;
        }

        public async Task<ApiInfo[]> GetApisByName(string Search)
        {
            if (Search.Length > 2)
            {
                var res = await dbContext.Apis.Where(s => s.Name.Contains(Search, StringComparison.CurrentCultureIgnoreCase)).ToArrayAsync();
                return res;
            }
            return null;
        }

        public async Task<ApiInfo> GetApi(string id)
        {
            var res = await dbContext.Apis.Where(s => s.Id == id).FirstOrDefaultAsync();
            return res;
        }

        public async Task<ApiInfo[]> GetUserApis(string UserId)
        {
            var res = await dbContext.Apis.Where(s => s.UserId == UserId).ToArrayAsync();
            return res;
        }

        public async Task<string[]> GetUpvotedApis(string UserId)
        {
            var res = await dbContext.Upvotes.Where(s => s.UserId == UserId).Select(s => s.ApiId).ToArrayAsync();
            return res;
        }

        public async Task<Upvote[]> GetUpvotes()
        {
            var res = await dbContext.Upvotes.ToArrayAsync();
            return res;
        }

        public async Task<bool> AddApi(ApiInfo api)
        {
            var MyApi = await dbContext.Apis.Where(s => s.Id == api.Id).FirstOrDefaultAsync();
            if (MyApi is null)
            {
                dbContext.Apis.Add(api);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AddUpvote(string UserId, string ApiId)
        {
            var id = UserId + ApiId;
            var upvote = await dbContext.Upvotes.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (upvote is null)
            {
                var task = dbContext.Apis.Where(s => s.Id == ApiId).FirstOrDefaultAsync();
                Upvote NewUpvote = new()
                {
                    Id = id,
                    UserId = UserId,
                    ApiId = ApiId
                };
                var task1 = dbContext.Upvotes.AddAsync(NewUpvote);
                var MyApi = await task;
                MyApi.Upvote++;
                dbContext.Apis.Update(MyApi);
                await task1;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task RemoveUpvote(string UserId, string ApiId)
        {
            var id = UserId + ApiId;
            var upvote = await dbContext.Upvotes.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (upvote is not null)
            {
                dbContext.Upvotes.Remove(upvote);
                var MyApi = await dbContext.Apis.Where(s => s.Id == ApiId).FirstOrDefaultAsync();
                MyApi.Upvote--;
                dbContext.Apis.Update(MyApi);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateApi(ApiInfo api, string UserId)
        {
            var MyApi = await dbContext.Apis.Where(s => s.Id == api.Id).FirstOrDefaultAsync();
            if (MyApi is not null && MyApi.UserId == UserId)
            {
                ApiInfo.MemSet(MyApi, api);
                dbContext.Update(MyApi);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteApi(string ApiId, string UserId)
        {
            var MyApi = await dbContext.Apis.Where(s => s.Id == ApiId).FirstOrDefaultAsync();
            if (MyApi is not null && MyApi.UserId == UserId)
            {
                dbContext.Apis.Remove(MyApi);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
