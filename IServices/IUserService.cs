using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IServices
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsersAsync();
        Task<bool> SaveUser(UserModel user);
        Task<UserModel> GetUserById(int id);

        Task<bool> DeleteUser(int id);

    }
}
