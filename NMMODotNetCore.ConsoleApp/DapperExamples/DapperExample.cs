using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using NMMODotNetCore.ConsoleApp.Dtos;
using NMMODotNetCore.ConsoleApp.Services;

namespace NMMODotNetCore.ConsoleApp.DapperExamples
{
    internal class DapperExample
    {

        public void Run()
        {
            Read();
            //  Edit(1);
            // Edit(21);
            //Create("Angle","KnonNyo","Content");
            // Update(2, "Title 2", "Author 2", "Content 2");
            Delete(5);
        }



        public void Read()
        {
            /*using (IDbConnection db1=new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString))
            {

                db1.Open();
            }*/ //can use for connection
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst = db.Query<BlogDto>("select * from Table_Blog").ToList();
            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-----------------------");


            }
        }
        private void Edit(int id)
        {
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogDto>("select * from Table_Blog where BlogId=@BlogId", new BlogDto { BlogId = id }).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("Data is not found");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("-----------------------");

        }
        private void Create(string Angle, string KhonNyo, string Content)
        {
            var item = new BlogDto
            {
                BlogTitle = Angle,
                BlogAuthor = KhonNyo,
                BlogContent = Content,


            };
            string query = @"INSERT INTO [dbo].[Table_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle,
          @BlogAuthor, 
           @BlogContent)";
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);
            string message = result > 0 ? "Creation is success" : "Creation is fail";
            Console.WriteLine(message);

        }
        private void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,

            };
            string query = @"UPDATE [dbo].[Table_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId=@BlogId";
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);
            string message = result > 0 ? "Update is successful" : "Update is failed";
            Console.WriteLine(message);

        }
        private void Delete(int id)
        {
            var item = new BlogDto
            {
                BlogId = id,
            };
            string query = @"DELETE from [dbo].[Table_Blog] Where BlogId=@BlogId";
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);
            string message = result > 0 ? "Delete is successful" : "Delete is failed";
            Console.WriteLine(message);

        }

    }
}





