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
                    cmd.CommandText = @"Select b.projectId,b.id,b.Description,b.Sovled,b.line,Bt.BugType from Bug b left join BugType BT on b.BugTypeId =BT.id";
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
                                Solved = DbUtils.GetString(reader, "Sovled"),

                            };
                            BugType bugType = new BugType() 
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                bugType=DbUtils.GetString(reader, "bugType"),

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
                    cmd.CommandText = @"Select b.id,b.projectId,b.Description,b.Sovled,b.line,BT.BugType from Bug b left join BugType BT on b.BugTypeId =BT.id
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
                                Solved = DbUtils.GetString(reader, "Sovled"),
                            BugType= new BugType() 
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
                    cmd.CommandText = @"Insert Into Bug (Description,Line,projectId,BugTypeId,Sovled) OUTPUT INSERTED.ID VALUES
(@Description,@line,@projectId,@BugTypeId,@Sovled)";
                    
                    DbUtils.AddParameter(cmd,"@Description", bug.Description);
                    DbUtils.AddParameter(cmd, "@line", bug.Line);
                    DbUtils.AddParameter(cmd, "@projectId", bug.projectId);
                    DbUtils.AddParameter(cmd, "@BugTypeId", bug.BugType.Id);
                    DbUtils.AddParameter(cmd, "@Sovled", bug.Solved);
                    int id = (int)cmd.ExecuteScalar();

                    bug.Id = id;

                }
            }
        }
    }
}
  