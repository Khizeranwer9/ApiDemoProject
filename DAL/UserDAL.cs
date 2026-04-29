using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class UserDAL
    {
        public static async Task<List<UserModel>> GetAllUsersAsync()
        {
            List<UserModel> Users = new List<UserModel>();

            using (SqlConnection con = DBHelper.GetConnection())
            {

                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("GetAllUsers", con))
                {
                    
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            UserModel user = new UserModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                Phone = reader.GetInt32(3)
                            };
                            Users.Add(user);
                        }
                    }
                }
                return Users;
            }
        }

        public static async Task<bool> SaveUser(UserModel user)
        {
            int i;
            using (SqlConnection con = DBHelper.GetConnection())
            {

                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("SaveUser", con))
                {
                    cmd.CommandType =System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Phone", user.Phone);
                    i = await cmd.ExecuteNonQueryAsync();
                }
                
            }
            return i > 0;






        }

        public static async Task<UserModel> GetUserById(int id)
        {
            UserModel? user = null;
            using (SqlConnection con=DBHelper.GetConnection() )
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("GetUserById", con))
                {
                    int i;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new UserModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                Phone = reader.GetInt32(3)
                            };
                        }

                    }
                }
                return user;



            }
        }

        public static async Task<bool> DeleteUser(int id)
        {
            int i;
             using (SqlConnection con=DBHelper.GetConnection())
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("DeletUser", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                     i = await cmd.ExecuteNonQueryAsync();
                }
                
            }
            return i > 0;

        }
    }
}


    
