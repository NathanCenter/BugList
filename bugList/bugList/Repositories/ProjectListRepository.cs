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
        //create a project https://github.com/nashville-software-school/bangazon-inc/blob/main/book-2-mvc/chapters/ADD_AND_UPDATE_DATA_IN_MVC.md 
        //Insert Into ProjectList (Id,UserProfileId,ProgrammingLanguage,ProjectName)  OUTPUT INSERTED.ID VALUES (4,1,'c#','test')
        public void  CreateProject(ProjectList project)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Insert Into ProjectList ([Id],UserProfileId,ProgrammingLanguage,ProjectName) 
            OUTPUT INSERTED.ID VALUES (@Id,@UserProfileId,@ProgrammingLanguage,@ProjectName)";

                    cmd.Parameters.AddWithValue("@Id", project.Id);
                    cmd.Parameters.AddWithValue("@UserProfileId", project.UserProfileId);
                    cmd.Parameters.AddWithValue("@ProgrammingLanguage", project.ProgrammingLangueage);
                    cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);

                    int id = (int)cmd.ExecuteScalar();

                    project.Id = id;

                }



            }
           
        }
        public void Delete(int projectId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) 
                {
                    cmd.CommandText = @"
                            DELETE FROM 
                            ProjectList
                            WHERE Id = @id
                        ";
                    cmd.Parameters.AddWithValue("@id", projectId);

                    cmd.ExecuteNonQuery();
                }
            
            }
        }
        //edit project list
        //Update ProjectList Set ProgrammingLanguage='@ProgrammingLanguage', ProjectName='ProjectName'
        //Where Id = '@Id'
        public void Edit(ProjectList project)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Update ProjectList Set ProgrammingLanguage=@ProgrammingLanguage, ProjectName=@ProjectName
                Where Id = @Id";
                    cmd.Parameters.AddWithValue("@ProgrammingLanguage", project.ProgrammingLangueage);
                    cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                    cmd.Parameters.AddWithValue("@Id", project.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }



    }
}
