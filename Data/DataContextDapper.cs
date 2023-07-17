using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DotnetAPI.Data
{
    class DataContextDapper
    {
        private readonly IConfiguration _config;
        public DataContextDapper(IConfiguration config)
        {
            _config = config;
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dBConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dBConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dBConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dBConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSQL(string sql)
        {
            IDbConnection dBConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dBConnection.Execute(sql) > 0;
        }

        public int ExecuteSQLWithRowCount(string sql)
        {
            IDbConnection dBConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dBConnection.Execute(sql);
        }
    }
}