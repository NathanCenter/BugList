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
        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select Id, Name,Email,FirebaseUserId,userType from UserProfile 
               WHERE FirebaseUserId = @FirebaseuserId";
                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);
                    UserProfile userProfile = null;
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            UserType = DbUtils.GetString(reader, "userType"),
                            FirebaseId = DbUtils.GetString(reader, "FirebaseUserId")
                        };
                    }
                    reader.Close();
                    return userProfile;
                }

            }
        }

        public void Add(UserProfile userProfile, string localId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO UserProfile (Name,FirebaseUserId,UserType,Email)  OUTPUT INSERTED.Id VALUES (@Name,@FirebaseUserId,@UserType,@Email)";
                    DbUtils.AddParameter(cmd, "@FirebaseUserId", localId);
                    DbUtils.AddParameter(cmd, "@Name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@UserType", userProfile.UserType);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    userProfile.Id = (int)cmd.ExecuteScalar();
                }

            }
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
                    var users = new List<UserProfile>();
                    while (reader.Read())
                        users.Add(new UserProfile
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            Name = DbUtils.GetString(reader, "name"),
                            FirebaseId = DbUtils.GetString(reader, "FirebaseUserId"),
                            Email = DbUtils.GetString(reader, "Email"),
                            UserType = DbUtils.GetString(reader, "UserType")

                        });
                    reader.Close();
                    return users;
                }
            }
        }

        public UserProfile GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select Id,Name,UserType,Email from UserProfile Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    UserProfile userProfile = null;
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            // FirebaseId=reader.GetString(reader.GetOrdinal("FirbaseUserId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            //UserType = reader.GetString(reader.GetOrdinal("UserType")),
                        };
                    }
                    reader.Close();
                    return userProfile;
                }
            }
        }
    }
}
