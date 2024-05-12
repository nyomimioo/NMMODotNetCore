using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMMODotNetCore.RestApi
{
    internal class ConnectionString

    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DELL\\SQLEXPRESS",
            InitialCatalog = "DotNetTrainingBath4",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true,


        };

    }
}


