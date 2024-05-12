using Microsoft.EntityFrameworkCore;
using NMMODotNetCore.RestApi.Models;

namespace NMMODotNetCore.RestApi.Database
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }



        public DbSet<BlogModel> Blogs { get; set; }



    }
}
