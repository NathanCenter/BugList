using bugList.Models;
using bugList.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace bugList.Repositories
{
    public class ProjectListRepository : IProjectListRepository
    {
        private readonly IConfiguration _config;
        public ProjectListRepository(IConfiguration config)
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
        public List<ProjectList> GetAllProjects()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select Id,UserProfileId,ProgrammingLanguage,projectName from ProjectList";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<ProjectList> projects = new List<ProjectList>();
                        while (reader.Read())
                        {
                            ProjectList project = new ProjectList
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                ProgrammingLangueage = DbUtils.GetString(reader, "ProgrammingLanguage"),
                                ProjectName = DbUtils.GetString(reader, "projectName"),
                            };
                            projects.Add(project);
                        }
                        return projects;
                    }
                }


            }
        }
        public ProjectList GetProjectById(int id)
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select Id,UserProfileId,ProgrammingLanguage,projectName from ProjectList
                        where id=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ProjectList project = new ProjectList
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                ProgrammingLangueage = DbUtils.GetString(reader, "ProgrammingLanguage"),
                                ProjectName = DbUtils.GetString(reader, "projectName"),

                            };
                            return project;
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
