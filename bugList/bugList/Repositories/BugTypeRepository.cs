using bugList.Models;
using bugList.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
namespace bugList.Repositories
{
    public class BugTypeRepository : IBugTypeRepository
    {
        private readonly IConfiguration _config;
        public BugTypeRepository(IConfiguration config)
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

        public List<BugType> GetAllBugsType()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select Id,BugType from BugType";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<BugType> bugType = new List<BugType>();
                        while (reader.Read())
                        {
                            BugType bugTypeList = new BugType()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                bugType = DbUtils.GetString(reader, "BugType"),
                            };
                            bugType.Add(bugTypeList);
                        }
                        return bugType;
                    }
                }
            }
        }

        public void createBugType(BugType Type)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Insert Into BugType(BugType) OUTPUT INSERTED.ID VALUES (@bugType)";
                    DbUtils.AddParameter(cmd, "@bugType", Type.bugType);
                    int id = (int)cmd.ExecuteScalar();
                    Type.Id = id;
                }
            }
        }
    }
}
