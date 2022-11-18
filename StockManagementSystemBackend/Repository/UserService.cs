using Dapper;
using StockManagementSystemBackend.DTO;
using StockManagementSystemBackend.Interface;
using System.Data;

namespace StockManagementSystemBackend.Repository
{
    public class UserService : IUser
    {
        private readonly IEmail mailService;
        public UserService(IEmail mailService)
        {
            this.mailService = mailService;
        }
        public async Task<UserDTO> ValidateUser(string userName, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserName", userName, DbType.String);

                using (var connection = dbconnection)
                {
                    var userdetails = (await connection.QueryFirstOrDefaultAsync<UserDTO>(SQL.GetUserDetails, parameters));
                    return userdetails;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> EnableDisableUser(long userId, bool IsActive, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserId", userId, DbType.Int32);
                //parameters.Add("TenantId", TenantId, DbType.Int32);
                parameters.Add("IsActive", IsActive, DbType.Boolean);

                using (var connection = dbconnection)
                {
                    return await connection.ExecuteAsync(SQL.EnableDisableUser, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<UserDTO> GetUserById(long userId, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserId", userId, DbType.Int32);
                //parameters.Add("TenantId", TenantId, DbType.Int32);
                using (var connection = dbconnection)
                {
                    return await connection.QueryFirstOrDefaultAsync<UserDTO>(SQL.GetUserById, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<UserDTO>> GetUsers(int TenantId, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("TenantId", TenantId, DbType.Int32);
                using (var connection = dbconnection)
                {
                    return (await connection.QueryAsync<UserDTO>(SQL.GetUsers, parameters)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> InsertUpdateUser(UserDTO userDTO, IDbConnection dbconnection)
        {
            try
            {
                RoleService roleService = new RoleService();
                var parameters = new DynamicParameters();
                string Query = SQL.InsertUser;
                if (userDTO.UserId > 0)
                {
                    Query = SQL.UpdateUser;
                    parameters.Add("UserId", userDTO.UserId, DbType.Int16);
                }
                else
                {
                    await mailService.SendEmailAsync(userDTO.Email,userDTO.UserName,userDTO.UserPassword,"Admin");
                    parameters.Add("UserPassword", BCrypt.Net.BCrypt.HashPassword(userDTO.UserPassword), DbType.String);
                }
                parameters.Add("RoleId", userDTO.RoleId, DbType.Int16);
                parameters.Add("UserName", userDTO.UserName, DbType.String);
                parameters.Add("Email", userDTO.Email, DbType.String);
                parameters.Add("ContactNumber", userDTO.ContactNumber, DbType.String);
                parameters.Add("TenantId", userDTO.TenantId, DbType.Int16);
                parameters.Add("Address", userDTO.Address, DbType.String);
                parameters.Add("IsActive", true, DbType.Boolean);

                using (var connection = dbconnection)
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    return await connection.ExecuteAsync(Query, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public async Task<IEnumerable<UserDTO>> GetUserDetailsByTenant(IDbConnection dbconnection) 
        {
            try
            {
               
                using (var connection = dbconnection)
                {
                    return (await connection.QueryAsync<UserDTO>(SQL.GetUsersByTenant)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
