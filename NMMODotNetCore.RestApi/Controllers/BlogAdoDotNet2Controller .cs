using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
using NMMODotNetCore.RestApi.Models;
using System.Data;
using System.Data.SqlClient;
using NMMODotNetCore.Shared;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace NMMODotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult GetBlogs()

        {
            string query = "select * from Table_Blog";
            var lst = _adoDotNetService.Query<BlogModel>(query);
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            string query = "select * from Table_Blog where BlogId=@BlogId";
            //AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            //parameters[0] = new AdoDotNetParameter("@BlogId", id);
            //var lst = _adoDotNetService.Query<BlogModel>(query, parameters);

            var item= _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));
                
            //SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            //connection.Open();
            //Console.WriteLine("Connection is ready");

            //SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.Parameters.AddWithValue("@BlogId", id);
            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sqlDataAdapter.Fill(dt);
            //connection.Close();
            //Console.WriteLine("Connection is close");

            if (item is null)
            {
                return NotFound("Data is not found");
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlog(int id, BlogModel medel)
        {
            string query = @"INSERT INTO [dbo].[Table_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle,
          @BlogAuthor, 
           @BlogContent)";
            //SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            //connection.Open();

            //SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.Parameters.AddWithValue("@BlogTitle", medel.BlogTitle);
            //cmd.Parameters.AddWithValue("@BlogAuthor", medel.BlogAuthor);
            //cmd.Parameters.AddWithValue("@BlogContent", medel.BlogContent);
            //int result = cmd.ExecuteNonQuery();
            //connection.Close();
            int result = _adoDotNetService.Execute(query,

               new AdoDotNetParameter("@BlogTitle", medel.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", medel.BlogAuthor),
                 new AdoDotNetParameter("@BlogContent", medel.BlogContent)

                );

            string message = result > 0 ? "Creation is success" : "Creation is fail";
            Console.WriteLine(message);
            //return Ok(message);
            return StatusCode(500, message);

        }
        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel model)
        {
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(model.BlogTitle))
            {
                condition += "[BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(model.BlogAuthor))
            {
                condition += "[BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(model.BlogContent))
            {
                condition += "[BlogContent] = @BlogContent, ";
            }
            condition = condition.Substring(0, condition.Length - 2);
            string query = $@"UPDATE [dbo].[Table_Blog]
   SET {condition}
 WHERE BlogId = @BlogId";

            List<AdoDotNetParameter> list = new List<AdoDotNetParameter>();
            list.Add(new AdoDotNetParameter("@BlogId", id));
            list.Add(new AdoDotNetParameter("@BlogTitle", model.BlogTitle));
            list.Add(new AdoDotNetParameter("@BlogAuthor", model.BlogAuthor));
            list.Add(new AdoDotNetParameter("@BlogContent", model.BlogContent));
           
            int result = _adoDotNetService.Execute(query, list.ToArray());
            string message = result > 0 ? "Update Success" : "Update Fail";
            return Ok(message);


        }
        [HttpPut("{ id}")]
        public IActionResult PutBlog(int id, BlogModel model)
        {
            string query = @"UPDATE [dbo].[Table_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId=@BlogId";
            //SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            //connection.Open();
            //SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.Parameters.AddWithValue("@BlogId", model.BlogId);
            //cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
            //cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
            //cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
            //int result = cmd.ExecuteNonQuery();
            //connection.Close();
            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogId",id),
                  new AdoDotNetParameter("@BlogTitle",model.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor",model.BlogAuthor),
                 new AdoDotNetParameter("@BlogContent", model.BlogContent)
                );
            string message = result > 0 ? "Update is successful" : "Update is failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Table_Blog]
      WHERE BlogId=@BlogId";
            //SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            //connection.Open();
            //SqlCommand cmd = new(query, connection);
            //cmd.Parameters.AddWithValue("@BlogId", id);
            //int result = cmd.ExecuteNonQuery();
            //connection.Close();
            int result = _adoDotNetService.Execute(query,new AdoDotNetParameter("@BlogId", id));
            string message = result > 0 ? "Delting is successful" : "Deleting is failed";
            return Ok(message);

        }

    }
}