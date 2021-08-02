using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VsCodeApi.Models;

namespace VsCodeApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ApiInfo> Apis { get; set; }
        public DbSet<Upvote> Upvotes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
