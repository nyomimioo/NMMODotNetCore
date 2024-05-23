using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NMMODotNetCore.RestApi.Models;
using NMMODotNetCore.Shared;
using System.Data;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NMMODotNetCore.RestApi.Controllers
{
   // private BlogModel model = new();
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        private static readonly  DapperService _dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);

        [HttpGet]//Read
        public IActionResult GetBlogs()
        {
            string query = "select * from Table_Blog";
            var lst2 = _dapperService.Query<BlogModel>(query);
            return Ok(lst2);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            //string query = "select * from Table_Blog where blogid = @BlogId";
            //using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            //var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();

            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogModel model)
        {
            string query = @"INSERT INTO [dbo].[Table_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle,
          @BlogAuthor, 
           @BlogContent)";
            int result = _dapperService.Execute(query, model);
            string message = result > 0 ? "Creation is success" : "Creation is fail";
            return Ok(message);
        }

        [HttpPut("{id}")]
         public IActionResult UpdateBlogs(int id,BlogModel model)

        {
            var item = new FindById(id);
            if (item is null)
            {
                return NotFound("Data is not found");
            }
            string query = @"UPDATE [dbo].[Table_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId=@BlogId";
            int result = _dapperService.Execute(query, model);
            string message = result > 0 ? "Update is successful" : "Update is failed";
            return Ok(message);
        }


        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel model)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }

            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(model.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }

            if (!string.IsNullOrEmpty(model.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }

            if (!string.IsNullOrEmpty(model.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return NotFound("No data to update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);//remove ","
            model.BlogId = id;

            string query = $@"UPDATE [dbo].[Table_Blog]
   SET {conditions}
 WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, model);
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);

        }

        [HttpDelete("{ id}")]
        public IActionResult DeleteBlogs(int id)
        {

            var item = new FindById(id);
            if (item is null)
            {
                return NotFound("Data is not found");
            }
            string query = @"DELETE from [dbo].[Table_Blog] Where BlogId=@BlogId";
            int result = _dapperService.Execute(query);
            string message = result > 0 ? "Delete is successful" : "Delete is failed";
            return Ok(message);
        }
        
       
        private BlogModel? FindById(int id)
        {
            string query = "select * from Table_Blog where BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
            return item;

        }


    }
}
