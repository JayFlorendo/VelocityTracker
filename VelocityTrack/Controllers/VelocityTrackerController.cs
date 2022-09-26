using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using VelocityTrack.Models;

namespace VelocityTrack.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class VelocityTrackerController : Controller {

        private ProjectDetailsModel _projectDetails = new ProjectDetailsModel();
       
        public string con = @"Server=localhost; Database=master; Trusted_Connection=True;";

        [HttpPost("Save")]
        public bool Velocity_Tracker_Save(
            string project, string taskTitle, string taskDescription, int totalEstimate, string[] employee,
            int[] estimatedHours, int[] actualHours) {


            if (String.IsNullOrEmpty(project) || employee.Length == 0) {
                return false;
            }

            using SqlConnection connect = new(con);

            connect.Open();

            for (int i = 0; i < employee.Length; i++) {
                
                using SqlCommand cmd = new SqlCommand() {
                    Connection = connect,
                    CommandText = "Velocity.spVelocityTracker_Save",
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Projects", project);
                cmd.Parameters.AddWithValue("@TaskTitle", taskTitle);
                cmd.Parameters.AddWithValue("@TaskDescription", taskDescription);
                cmd.Parameters.AddWithValue("@TotalEstimate", totalEstimate);

                cmd.Parameters.AddWithValue("@Employee", employee[i]);
                cmd.Parameters.AddWithValue("@EstimatedHours", estimatedHours[i]);
                cmd.Parameters.AddWithValue("@ActualHours", actualHours[i]);

                cmd.ExecuteNonQuery();
            }

            connect.Close();

            return true;
        }


        [HttpPost("GetDetails")]
        public VelocityTrackerModel Velocity_Tracker_Get(string project) {

            VelocityTrackerModel result = new VelocityTrackerModel();


            if (String.IsNullOrEmpty(project)) {

                return result;
            }

            using SqlConnection connect = new(con);

            connect.Open();

            string SQL = "SELECT * FROM Velocity.VelocityTracker WHERE Projects = @Project";

            using SqlCommand cmd = new(SQL, connect);

            cmd.Parameters.AddWithValue("@Project", project);

            using(SqlDataReader rdr = cmd.ExecuteReader() ) {

                if (rdr.HasRows == true) {

                    List<EmployeeDetailsModel> employeeDetails = new List<EmployeeDetailsModel>();
                    ProjectDetailsModel projectItem = new ProjectDetailsModel();

                    string? projectTitle = "";
                    string? taskTitle = "";
                    string? taskDescription = "";
                    int totalEstimate = 0;

                    while (rdr.Read()) {
                        projectItem = new() {
                            Projects = Convert.ToString(rdr["Projects"]),
                            TaskTitle = Convert.ToString(rdr["TaskTitle"]),
                            TaskDescription = Convert.ToString(rdr["taskDescription"]),
                            TotalEstimate = Convert.ToInt32(rdr["TotalEstimate"])
                        };

                        EmployeeDetailsModel employeeDetailsItem = new EmployeeDetailsModel(){
                            Employee = Convert.ToString(rdr["Employee"]),
                            EstimatedHours = Convert.ToInt32(rdr["EstimatedHours"]),
                            ActualHours = Convert.ToInt32(rdr["ActualHours"])
                        };

                        employeeDetails.Add(employeeDetailsItem);
                    }

                    result.ProjectDetails = projectItem;
                    result.EmployeeDetails = employeeDetails;
                }
            }

            return result;

            
        }
    }
}
