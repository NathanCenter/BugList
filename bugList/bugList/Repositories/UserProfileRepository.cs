using bugList.Models;
using bugList.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace bugList.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IConfiguration _config;
        public UserProfileRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        public void Add(UserProfile userProfile)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(UserProfile userProfile)
        {
            throw new System.NotImplementedException();
        }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection) 
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select Id,Name,UserType,FirebaseUserId,Email from UserProfile";
                    var reader = cmd.ExecuteReader();
                    var users=new List<UserProfile>();
                    while (reader.Read())
                        users.Add(new UserProfile 
                        {
                            Id = DbUtils.GetInt(reader,"id"),
                            Name = DbUtils.GetString(reader,"name"),
                            FirebaseId = DbUtils.GetString(reader, "FirebaseUserId"),
                            Email = DbUtils.GetString(reader, "Email"),
                            UserType=DbUtils.GetString(reader, "UserType")

                        });
                    reader.Close();
                    return users;

                    
                }
            }
        }

        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            throw new System.NotImplementedException();
        }

        public UserProfile GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select Id,Name,UserType,FirbaseUserId,Email from UserProfile Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    UserProfile userProfile = null;
                    var reader = cmd.ExecuteReader();
                    if (reader.Read()) 
                    {
                        userProfile = new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email=reader.GetString(reader.GetOrdinal("Email")),
                            FirebaseId=reader.GetString(reader.GetOrdinal("FirbaseUserId")),
                            Name=reader.GetString(reader.GetOrdinal("Name")),
                            UserType = reader.GetString(reader.GetOrdinal("UserType")),
                        };
                    }
                    reader.Close();
                    return userProfile;

                }

            }
        }
    }
}
