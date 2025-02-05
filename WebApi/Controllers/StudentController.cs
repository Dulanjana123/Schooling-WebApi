using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] int page = 1,
                                             [FromQuery] int pageSize = 10,
                                             [FromQuery] string filter = "",
                                             [FromQuery] string sortBy = "firstName",
                                             [FromQuery] string sortDirection = "asc")
        {
            var result = await _studentService.GetAll(page, pageSize, filter, sortBy, sortDirection);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterStudent([FromForm] CreateStudentDto studentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var studentExists = await _studentService.StudentExists(studentDto);

            if (studentExists)
            {
                return Conflict(new { message = "Student already exists!" });
            }


            await _studentService.Add(studentDto);
            return Ok(new { message = "Student registered successfully!" });
        }


        [HttpGet("{id:int}", Name = "GetStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStudent(int id, [FromForm] UpdateStudentDto studentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var studentExists = await _studentService.StudentExistsInUpdate(studentDto);

            if (studentExists)
            {
                return Conflict(new { message = "Another student already exists with the same Email, NIC, or Mobile!" });
            }

            studentDto.Id = id;
            await _studentService.Update(studentDto);

            return Ok(new { message = "Student updated successfully!" });
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentService.Delete(id);
            return Ok(new { message = "Student deleted successfully!" });
        }

        [HttpPut("{id:int}/Deactivate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeactivateStudent(int id)
        {
            var student = await _studentService.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            await _studentService.Update(new UpdateStudentDto
            {
                Id = id,
                Active = false
            });

            return Ok(new { message = "Student deactivated successfully!" });
        }

    }
}
