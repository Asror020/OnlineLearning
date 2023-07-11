using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Models;
using OnlineLearning.Services.CourseServices;
using OnlineLearning.Services.StudentServices;

namespace OnlineLearning.Controllers
{
    public class CoursesController : BaseController
    {
        private readonly ICourseService courseService;
        public CoursesController(
            IWebHostEnvironment hostEnvironment,
            ILogger<CoursesController> logger,
            ICourseService courseService)
            : base(hostEnvironment, logger)
        {
            this.courseService = courseService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var course = await this.courseService.GetByIdAsync(id);
            return course == null ? NotFound() : Ok(course);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            var createdCourse = await this.courseService.CreateAsync(course);

            return Ok(createdCourse);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Course course)
        {
            var updatedCourse = await this.courseService.UpdateAsync(id, course);

            return Ok(updatedCourse);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await this.courseService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
