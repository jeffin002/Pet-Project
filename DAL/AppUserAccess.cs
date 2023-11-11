using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AppUserAccess
    {
        private readonly string _connectionString;
        private ILogger<AppUserAccess> _logger;

        public AppUserAccess(IConfiguration config, ILogger<AppUserAccess> logger)
        {
            _connectionString = config.GetConnectionString("MyConnection");
            _logger = logger;
        }

        public async Task<AppUser> GetAppUserByUserName(string userName)
        {
            AppUser user = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    var parameters = new
                    {
                        UserName = userName
                    };

                    user = await connection.QuerySingleAsync<AppUser>
                        (
                         "GetUserByUserName", parameters, commandType: System.Data.CommandType.StoredProcedure
                        );
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return user;
        }
        public async Task CreateAppUser(AppUser user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    var parameters = new
                    {
                        user.FirstName,
                        user.LastName,
                        user.Email,
                        user.UserName,
                        user.PasswordHash,
                        user.Salt
                    };

                    await connection.ExecuteAsync
                        (
                         "dbo.CreateAppuser", parameters, commandType: System.Data.CommandType.StoredProcedure
                        );
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
