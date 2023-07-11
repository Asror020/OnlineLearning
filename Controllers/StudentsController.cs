using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OnlineLearning.Models;
using OnlineLearning.Models.Exceptions;
using OnlineLearning.Services.StudentServices;

namespace OnlineLearning.Controllers
{
    public class StudentsController : BaseController
    {
        private readonly IStudentService studentService;
        public StudentsController(
            IWebHostEnvironment hostEnvironment,
            ILogger<StudentsController> logger,
            IStudentService studentService)
            : base(hostEnvironment, logger)
        {
            this.studentService = studentService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var student = await this.studentService.GetByIdAsync(id);
            return student == null ? NotFound() : Ok(student);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody]Student student)
        {
            var createdStudent = await this.studentService.CreateAsync(student);

            return Ok(createdStudent);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Student student)
        {
            var updatedStudent = await this.studentService.UpdateAsync(id, student);

            return Ok(updatedStudent);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await this.studentService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
