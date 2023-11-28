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
        public IEnumerable<T> LoadDatas<T>(string sql)
        {
            IDbConnection connection = new SqlConnection(_config.GetConnectionString("DeafultConnection"));
            return connection.Query<T>(sql);
        }
        public async Task<T> LoadDataSingle<T>(string sql)
        {
            IDbConnection connection = new SqlConnection(_config.GetConnectionString("DeafultConnection"));
            return await connection.QuerySingleAsync<T>(sql);
        } 
        
        public T LoadDataSingles<T>(string sql)
        {
            IDbConnection connection = new SqlConnection(_config.GetConnectionString("DeafultConnection"));
            return connection.QuerySingle<T>(sql);
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
        public bool ExecuteSqlWithParameter(string sql, List<SqlParameter> parameters)
        {
            SqlCommand sqlCommand = new SqlCommand(sql);
            foreach (SqlParameter Parameter in parameters)
            {
                sqlCommand.Parameters.Add(Parameter);
            }
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("DeafultConnection"));
            connection.Open();
            sqlCommand.Connection = connection;
            int rowsAffected = sqlCommand.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }
    }
}
