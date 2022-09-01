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
                    cmd.CommandText = @"Select b.projectId,b.id,b.Description,b.Solved,b.line,Bt.BugType from Bug b left join BugType BT on b.BugTypeId =BT.id";
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
                                Solved = DbUtils.GetString(reader, "Solved"),

                            };
                            BugType bugType = new BugType()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                bugType = DbUtils.GetString(reader, "bugType"),

                            };

                            bug.Add(bugList);
                        }
                        return bug;
                    }
                }

            }

        }



        public List<Bug> GetBugsByProjectId(int id)
        {

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select b.id,b.projectId,b.Description,b.Solved,b.line,BT.BugType from Bug b left join BugType BT on b.BugTypeId =BT.id
where b.projectId=@id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        List<Bug> bugDetails = new List<Bug>();
                        while (reader.Read())
                        {
                            Bug bugList = new Bug()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                projectId = DbUtils.GetInt(reader, "projectId"),
                                Description = DbUtils.GetString(reader, "description"),
                                Line = DbUtils.GetString(reader, "line"),
                                Solved = DbUtils.GetString(reader, "Solved"),
                                BugType = new BugType()
                                {
                                    Id = DbUtils.GetInt(reader, "id"),
                                    bugType = DbUtils.GetString(reader, "BugType"),
                                }

                            };

                            bugDetails.Add(bugList);
                        }
                        return bugDetails;
                    }
                }
            }
        }


        public void CreateBug(Bug bug)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Insert Into Bug (Description,Line,projectId,BugTypeId,Solved) OUTPUT INSERTED.ID VALUES
(@Description,@line,@projectId,@BugTypeId,@Solved)";

                    DbUtils.AddParameter(cmd, "@Description", bug.Description);
                    DbUtils.AddParameter(cmd, "@line", bug.Line);
                    DbUtils.AddParameter(cmd, "@projectId", bug.projectId);
                    DbUtils.AddParameter(cmd, "@BugTypeId", bug.bugTypeId);
                    DbUtils.AddParameter(cmd, "@Solved", bug.Solved);
                    int id = (int)cmd.ExecuteScalar();

                    bug.Id = id;

                }
            }
        }

        public Bug GetBugById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select Id,Description,Line,Solved,projectId,BugTypeId from Bug where id=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Bug bug = new Bug
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Description = DbUtils.GetString(reader, "Description"),
                                Line = DbUtils.GetString(reader, "Line"),
                                Solved = DbUtils.GetString(reader, "Solved"),
                                projectId = DbUtils.GetInt(reader, "projectId"),
                                BugType = new BugType()
                                {
                                    Id = DbUtils.GetInt(reader, "BugTypeId")

                                }

                            };
                            return bug;

                        }
                        else
                        {
                            return null;
                        };
                    }

                }
            }
        }
        public void EditBug(Bug bug)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Update Bug Set Description=@Description, Solved=@Solved,Line=@Line, projectId=@projectId,BugTypeId=@BugTypeId where id=@Id";
                    DbUtils.AddParameter(cmd, "@Description", bug.Description);
                    DbUtils.AddParameter(cmd, "@Line", bug.Line);
                    DbUtils.AddParameter(cmd, "@Solved", bug.Solved);
                    DbUtils.AddParameter(cmd, "@projectId", bug.projectId);
                    DbUtils.AddParameter(cmd, "@BugTypeId", bug.bugTypeId);
                    DbUtils.AddParameter(cmd, "@Id", bug.Id);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void DeleteBug(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" DELETE FROM Bug WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
