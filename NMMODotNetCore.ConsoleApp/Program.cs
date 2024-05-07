using NMMODotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Text;
Console.WriteLine("Hello, World!");
//SqlConnection
//npm
//pub.dev
//nuget
//SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
//stringBuilder.DataSource = "Dell\\SQLEXPRESS";//server name
//stringBuilder.InitialCatalog = "DotNetTrainingBath4";//database name
//stringBuilder.UserID = "sa";//user name
//stringBuilder.Password = "sa@123";//server password
//SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
//connection.Open();
//Console.WriteLine("Connection is ready");
//string query = "select * from Table_Blog";
//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
//DataTable dt = new DataTable();
//sqlDataAdapter.Fill(dt);
//connection.Close();
//Console.WriteLine("Connection is close");
////dataset=>data tables
////data table=>data rows
////data row=>data columns
//foreach(DataRow dataRow in dt.Rows)
//{
//    Console.WriteLine("Blog Id=>" + dataRow["BlogID"]);
//    Console.WriteLine("Blog Title=>" + dataRow["BlogTitle"]);
//    Console.WriteLine("Blog Author=>" + dataRow["BlogAuthor"]);
//    Console.WriteLine("Blog Content=>" + dataRow["BlogContent"]);
//    Console.WriteLine("------------------------------------");
//}
//// end Ado.Net Read
///
AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
adoDotNetExample.Read();
/*adoDotNetExample.Create("title", "author", "content");*/
/*adoDotNetExample.Update(1002, "test title", "test author", "test content");*/
/*adoDotNetExample.Delete(1003);*/
/*adoDotNetExample.Edit(1003);
adoDotNetExample.Edit(1002);
*/



Console.ReadKey();

