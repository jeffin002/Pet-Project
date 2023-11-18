using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
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
        public async Task<(Guid,string)> CreateAppUser(AppUser user)
        {
            Guid emailGuid = Guid.NewGuid();
            string emailTemplate = null;

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
                        user.Salt,
                        Emailguid=emailGuid,
                        Emailtemplate=emailTemplate

                    };

                    await connection.ExecuteAsync
                        (
                         "dbo.CreateAppuser", parameters, commandType: System.Data.CommandType.StoredProcedure
                        );
                    emailGuid = parameters.Emailguid;
                    emailTemplate = parameters.Emailtemplate;
                    
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return (emailGuid,emailTemplate);
        }

        public async Task<(Guid, string)> CreateAppUser_V2(AppUser user)
        {
            Guid emailGuid = Guid.NewGuid();
            string emailtemplate = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    var p = new DynamicParameters();
                    p.Add("@FirstName", user.FirstName);
                    p.Add("@LastName", user.LastName);
                    p.Add("@Email", user.Email);
                    p.Add("@UserName", user.UserName);
                    p.Add("@PasswordHash", user.PasswordHash);
                    p.Add("@Salt", user.Salt);
                    p.Add("@Emailguid", null, dbType: DbType.Guid, direction: ParameterDirection.Output,size:null);
                    p.Add("@Emailtemplate", null, dbType: DbType.String, direction: ParameterDirection.Output, 500);

                    await connection.ExecuteAsync("dbo.CreateAppuser", p, commandType: CommandType.StoredProcedure);

                    emailGuid = p.Get<Guid>("@Emailguid");
                    emailtemplate = p.Get<string>("@Emailtemplate");
                   

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return (emailGuid, emailtemplate);
        }
    }
}
