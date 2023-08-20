using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SchoolProject.Core.Domain;
using SchoolProject.Core.Domain.Helpers;
using System.ComponentModel.Design;

namespace SchoolProject_Submit.Controllers
{
    public class StudentPortal : ControllerBase
    {
        private readonly IStudent _student;
        private ILogger<StudentPortal> _logger;
        public StudentPortal()
        {

        }

        /*[HttpGet("get-all-students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var allStdnt = await _GetStudents.GetStudents();
            return Ok(allStdnt);

        }*/

        //working.
        [HttpPost("register-new-studnt")]
        public async Task<IActionResult> RegisterNewStudent([FromBody] AddStudentDto pupil)
        {
            var stdnt = await _student.Register(pupil.Id ,pupil.SurName, pupil.FirstName, pupil.Age, pupil.Sex, pupil.Country, pupil.ClassArmId);
            return Ok(stdnt);
        }

        [HttpPost("register-with-validation")]
        public async Task<IActionResult> newOOP([FromBody] AddStudentDto std)
        {
            try
            {
                var stdnt = await _student.Register(std.Id, std.FirstName, std.SurName, std.Age, std.Sex, std.Country, std.ClassArmId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "new error occured");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response() { Message = "error occured" });
            }
        }

        //working.
        /*[HttpGet("get-student-with-full_Details/{id}")]
        public async Task<IActionResult> GetOneStudent(string id)
        {
            var pupil = await _GetStudent.GetS(id);
            return Ok(pupil);
            //return StatusCode(StatusCodes.Status200OK, new Succ() { ErrorMessage = ""})
        }*/


        /*[HttpPatch("update-studentRecord/{id}")]
        public async Task<IActionResult> UpdateStudent(string id, [FromBody] UpdateStudent student)
        {
            var student1 = await _update.UpdateS(id, student.FirstName, student.SurName, student.Age, student.Country);
            return Ok(student1);
        }*/

        //working.
        /*[HttpDelete("delete-student/{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var pupil = await _delete.DeleteS(id);
            return Ok("deleted successfully!");
        }*/

    }
}
