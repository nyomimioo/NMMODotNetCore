using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;


namespace NMMODotNetCore.ConsoleApp.AdoDotNetExamples
{
    internal class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DELL\\SQLEXPRESS",
            InitialCatalog = "DotNetTrainingBath4",
            UserID = "sa",
            Password = "sa@123"
        };

        public void Read()
        {

            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection is ready");
            string query = "select * from Table_Blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();
            Console.WriteLine("Connection is close");

            foreach (DataRow dataRow in dt.Rows)
            {
                Console.WriteLine("Blog Id=>" + dataRow["BlogID"]);
                Console.WriteLine("Blog Title=>" + dataRow["BlogTitle"]);
                Console.WriteLine("Blog Author=>" + dataRow["BlogAuthor"]);
                Console.WriteLine("Blog Content=>" + dataRow["BlogContent"]);
                Console.WriteLine("------------------------------------");
            }

        }
        public void Edit(int id)//same read but it read only id 
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection is ready");
            string query = "select * from Table_Blog where BlogId=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();
            Console.WriteLine("Connection is close");
            if (dt.Rows.Count == 0)
            {

                Console.WriteLine("No data found");
                return;//only to run if()
            }
            DataRow dataRow = dt.Rows[0];



            Console.WriteLine("Blog Id=>" + dataRow["BlogID"]);
            Console.WriteLine("Blog Title=>" + dataRow["BlogTitle"]);
            Console.WriteLine("Blog Author=>" + dataRow["BlogAuthor"]);
            Console.WriteLine("Blog Content=>" + dataRow["BlogContent"]);
            Console.WriteLine("------------------------------------");



        }
        public void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"INSERT INTO [dbo].[Table_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle,
          @BlogAuthor, 
           @BlogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);


            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Creation is success" : "Creation is fail";
            Console.WriteLine(message);

        }
        public void Update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"UPDATE [dbo].[Table_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);


            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Update is successful" : "Update is failed";
            Console.WriteLine(message);


        }
        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"DELETE FROM [dbo].[Table_Blog]
      WHERE BlogId=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);


            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Delting is successful" : "Deleting is failed";
            Console.WriteLine(message);

        }
    }
}