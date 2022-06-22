using bugList.Models;
using bugList.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace bugList.Repositories
{
    public class BugRepository : IBugRepository
    {
        private readonly IConfiguration _config;
        public BugRepository(IConfiguration config)
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
        public List<Bug> GetAllBugs()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select b.projectId,b.id,b.Description,b.Sovled,b.line from Bug b left join BugType BT on b.BugTypeId =BT.id";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Bug> bug = new List<Bug>();
                        while (reader.Read())
                        {
                            Bug bugList = new Bug()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                projectId = DbUtils.GetInt(reader, "projectId"),
                                Description = DbUtils.GetString(reader, "description"),
                                Line = DbUtils.GetString(reader, "line"),
                                Solved= DbUtils.GetString(reader, "Sovled"),

                            };
                            bug.Add(bugList);
                        }
                        return bug;
                    }
                }

            }

        }

        public Bug GetBugsByProjectId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select b.id,b.projectId,b.Description,b.Sovled,b.line from Bug b left join BugType BT on b.BugTypeId =BT.id
where b.projectId=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) 
                        {
                            Bug bugDetails = new Bug
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                projectId = DbUtils.GetInt(reader, "projectId"),
                                Description = DbUtils.GetString(reader, "description"),
                                Line = DbUtils.GetString(reader, "line"),
                                Solved = DbUtils.GetString(reader, "Sovled"),
                            };
                            return bugDetails;
                        }
                        else
                        {
                            return null;
                        }
                    }

                }
            }
        }
    }
}
