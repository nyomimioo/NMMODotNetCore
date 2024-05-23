using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
using NMMODotNetCore.RestApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace NMMODotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()

        {
            string query = "select * from Table_Blog";
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection is ready");

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();
            Console.WriteLine("Connection is close");
            //   // List<BlogModel> lst = new List<BlogModel>();

            //   // foreach (DataRow dr in dt.Rows)
            //{
            //    /*{   BlogModel model = new BlogModel();
            //        model.BlogId = Convert.ToInt32(dr["BlogId"]);
            //        model.BlogTitle = Convert.ToString(dr["BlogTitle"]);
            //        model.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
            //        model.BlogContent = Convert.ToString(dr["BlogContent"]);
            //        lst.Add(model);
            //    }*/
            //    /* BlogModel model = new BlogModel()
            //     {
            //         BlogId = Convert.ToInt32(dr["BlogID"]),
            //         BlogTitle = Convert.ToString(dr["BlogTitle"]),
            //         BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            //         BlogContent = Convert.ToString(dr["BlogContent"])
            //          };
            // lst.Add(model);
            // }*/
            //Select mean looping
            List<BlogModel> lst = dt.AsEnumerable().Select(dr => new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogID"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            }).ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            string query = "select * from Table_Blog where BlogId=@BlogId";
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection is ready");

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();
            Console.WriteLine("Connection is close");
            if (dt.Rows.Count == 0)
            {
                return NotFound("Data is not found");
            }
            DataRow dr = dt.Rows[0];
            var item = new BlogModel()
            {
                BlogId = Convert.ToInt32(dr["BlogID"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };

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
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", medel.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", medel.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", medel.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Creation is successful" : "Creation is failed";
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

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {condition}
 WHERE BlogId = @BlogId";
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@BlogId", id);
            if (!string.IsNullOrEmpty(model.BlogTitle))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
            }
            if (!string.IsNullOrEmpty(model.BlogAuthor))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
            }
            if (!string.IsNullOrEmpty(model.BlogContent))
            {
                cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
            }
            model.BlogId = id;

            int result = cmd.ExecuteNonQuery();

            connection.Close();
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
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", model.BlogId);
            cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Update is successful" : "Update is failed";
            Console.WriteLine(message);
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Table_Blog]
      WHERE BlogId=@BlogId";
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Delting is successful" : "Deleting is failed";
            return Ok(message);


        }

    }
















}


//Patch Delete Put study night