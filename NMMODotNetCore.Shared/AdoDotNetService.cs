using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMMODotNetCore.Shared
{
    public  class AdoDotNetService
    { 
        private readonly string _connectionString;
    
        public AdoDotNetService(string connctionString)
        {
            _connectionString = connctionString;
        }
     // public List<T> Query<T>(string query, AdoDotNetParameter[]?parameters=null)
             public List<T> Query<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            Console.WriteLine("Connection is ready");
            SqlCommand cmd = new SqlCommand(query, connection);
            if(parameters is not null && parameters.Length>0)
            {
                //foreach (var item in parameters)//method1
                //{
                //    cmd.Parameters.AddWithValue(item.Name,item.Value);
                //}

                //  cmd.Parameters.AddRange(parameters.Select(item=> new SqlParameter(item.Name, item.Value)).ToArray()); / method2
                var parameterArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(parameterArray);//method3
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();
            //change data table to object
            string json = JsonConvert.SerializeObject(dt);//change C# to Json
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json);//change Json to C#
            return lst;

        }
        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
            Console.WriteLine("Connection is ready");
            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {

              cmd.Parameters.AddRange(parameters.Select(item=> new SqlParameter(item.Name, item.Value)).ToArray()); 
               
            }
    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
    DataTable dt = new DataTable();
    sqlDataAdapter.Fill(dt);
            connection.Close();
            //change data table to object
            string json = JsonConvert.SerializeObject(dt);//change C# to Json
    List<T> lst = JsonConvert.DeserializeObject<List<T>>(json);//change Json to C#
            return lst[0];

        }
        public int Execute(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            Console.WriteLine("Connection is ready");
            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                //foreach (var item in parameters)//method1
                //{
                //    cmd.Parameters.AddWithValue(item.Name,item.Value);
                //}

                //  cmd.Parameters.AddRange(parameters.Select(item=> new SqlParameter(item.Name, item.Value)).ToArray()); / method2
                var parameterArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(parameterArray);//method3
            }
            var result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;

        }

    }

   
   

}
public class AdoDotNetParameter
    {
        public AdoDotNetParameter() { 
        }
        public AdoDotNetParameter(string name, object value)//calling this
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }


