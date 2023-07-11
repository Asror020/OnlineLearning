using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Models;
using OnlineLearning.Services.ProfessorServices;
using OnlineLearning.Services.StudentServices;

namespace OnlineLearning.Controllers
{
    public class InstructorsController : BaseController
    {
        private readonly IInstructorService instructorService;
        public InstructorsController(
            IWebHostEnvironment hostEnvironment,
            ILogger<InstructorsController> logger,
            IInstructorService instructorService)
            : base(hostEnvironment, logger)
        {
            this.instructorService = instructorService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Instructor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var instructor = await this.instructorService.GetByIdAsync(id);
            return instructor == null ? NotFound() : Ok(instructor);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Instructor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Instructor instructor)
        {
            var createdInstructor = await this.instructorService.CreateAsync(instructor);

            return Ok(createdInstructor);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Instructor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Instructor instroctor)
        {
            var updatedInstructor = await this.instructorService.UpdateAsync(id, instroctor);

            return Ok(updatedInstructor);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Instructor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await this.instructorService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
