using Microsoft.VisualStudio.TestTools.UnitTesting;
using VelocityTrack.Controllers;
using VelocityTrack.Models;

namespace VelocityTrackerTest {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void VelocityTracket_Save() {
            VelocityTrackerController controller = new VelocityTrackerController();

            string project = "VelocityProject";
            string taskTitle = "Save Web API";
            string taskDescription = "Save Velocity Tracking Details";
            int totalEstimate = 100;

            string[] employee = { "John Smith", "Sabrina Farr", "Jamir Alexei" };
            int[] estimatedHours = { 48, 22, 15 };
            int[] actualHours = { 32, 25, 8 };

            bool result = controller.Velocity_Tracker_Save(project, taskTitle, taskDescription,totalEstimate, employee, estimatedHours, actualHours);

            Assert.AreEqual(true, result);

        }

        [TestMethod]
        public void VelocityTracket_Get() {

            VelocityTrackerController controller = new VelocityTrackerController();

            VelocityTrackerModel result = new VelocityTrackerModel();

            string project = "VelocityProject";

            result = controller.Velocity_Tracker_Get(project);


            Assert.AreEqual(project, result.ProjectDetails.Projects);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.EmployeeDetails);

        }
    }
}