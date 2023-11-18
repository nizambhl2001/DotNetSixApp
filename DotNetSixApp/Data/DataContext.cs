using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;


namespace DotNetSixApp.Data
{
    public class DataContext
    {
        private readonly IConfiguration _config;

        public DataContext(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T>(string sql)
        {
            IDbConnection connection = new SqlConnection(_config.GetConnectionString("DeafultConnection"));
            return await connection.QueryAsync<T>(sql);
        }
        public async Task<T> LoadDataSingle<T>(string sql)
        {
            IDbConnection connection = new SqlConnection(_config.GetConnectionString("DeafultConnection"));
            return await connection.QuerySingleAsync<T>(sql);
        }

        public async Task<bool> ExecuteSql(string sql)
        {
            IDbConnection connection = new SqlConnection(_config.GetConnectionString("DeafultConnection"));
            return await connection.ExecuteAsync(sql) > 0;
        }
        public async Task<int> ExecuteSqlWithRowCount<T>(string sql)
        {
            IDbConnection connection = new SqlConnection(_config.GetConnectionString("DeafultConnection"));
            return await connection.ExecuteAsync(sql);
        }
    }
}
