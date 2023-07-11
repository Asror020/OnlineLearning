using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Models;
using OnlineLearning.Services.AssignmentServices;
using OnlineLearning.Services.CourseServices;

namespace OnlineLearning.Controllers
{
    public class AssignmentsController : BaseController
    {
        private readonly IAssignmentService assignmentService;
        public AssignmentsController(
            IWebHostEnvironment hostEnvironment,
            ILogger<AssignmentsController> logger,
            IAssignmentService assignmentService)
            : base(hostEnvironment, logger)
        {
            this.assignmentService = assignmentService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Assignment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var assignment = await this.assignmentService.GetByIdAsync(id);
            return assignment == null ? NotFound() : Ok(assignment);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Assignment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Assignment assignment)
        {
            var createdAssignment = await this.assignmentService.CreateAsync(assignment);

            return Ok(createdAssignment);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Assignment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Assignment assignment)
        {
            var updatedAssignment = await this.assignmentService.UpdateAsync(id, assignment);

            return Ok(updatedAssignment);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Assignment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await this.assignmentService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
