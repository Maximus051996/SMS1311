using StockManagementSystemBackend.DTO;
using System.Data;

namespace StockManagementSystemBackend.Interface
{
    public interface IUser 
    {
        public Task<UserDTO> ValidateUser(string userName, IDbConnection dbconnection);
        public Task<int> InsertUpdateUser(UserDTO userDTO, IDbConnection dbconnection);      
        public Task<IEnumerable<UserDTO>> GetUsers(int TenantId, IDbConnection dbconnection);
        public Task<int> EnableDisableUser(long userId, bool IsActive, IDbConnection dbconnection);
        public Task<UserDTO> GetUserById(long userId, IDbConnection dbconnection);

        public Task<IEnumerable<UserDTO>> GetUserDetailsByTenant(IDbConnection dbconnection);

    }
}
