using DAL;
using IServices;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class UserService : IUserService
    {
        public Task<bool> DeleteUser(int id)
        {
            return UserDAL.DeleteUser(id);
        }

        public Task<List<UserModel>> GetAllUsersAsync()
        {
            return UserDAL.GetAllUsersAsync();
        }

        public Task<UserModel> GetUserById(int id)
        {
            return UserDAL.GetUserById(id);
        }

        public Task<bool> SaveUser(UserModel user)
        {
            return UserDAL.SaveUser(user);
        }
    }
}
