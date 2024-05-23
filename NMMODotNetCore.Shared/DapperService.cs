using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ObjectiveC;

namespace NMMODotNetCore.Shared
{
    public class DapperService
    {
        private readonly string _connectionString;//Connection can chagne so it put dynamic
        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<M> Query<M>(string query,object? param=null)//Query of DapperService
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            //if(param!=null)
            //{
            //    var lst = db.Query<M>(query, param).ToList();
            //}
            //else
            //{
            //    var lst = db.Query<M>(query, param).ToList();
            //}

            var lst=db.Query<M>(query).ToList();//Query of Dapper
            return lst;
        }
        public M QueryFirstOrDefault<M>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            //if(param!=null)
            //{
            //    var lst = db.Query<M>(query, param).ToList();
            //}
            //else
            //{
            //    var lst = db.Query<M>(query, param).ToList();
            //}

            var item = db.Query<M>(query).FirstOrDefault();//Query of Dapper
            return item;
        }
        public int Execute(string query,object?param=null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
           var result= db.Execute(query, param);
            return result;
        }

    }
}
