using Course.Application.Commands.CourseDelete;
using Course.Application.CourseAdd;
using Course.Application.Queries.CourseAll;
using Course.Application.Queries.CourseByDuration;
using Course.Application.Queries.CourseByTech;
using Course.Application.Queries.GetDuration;
using Course.Application.Queries.GetTechnology;
using Course.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagement.Courses.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/lms/courses")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpPost(Constants.Add)]
        public async Task<ActionResult> Course([FromBody] CourseAddCommand courseAddCommand)
        {
            var response = await _mediator.Send(courseAddCommand);
            return response.ResponseData;
        }

        [HttpGet(Constants.GetAll)]
        public async Task<ActionResult> Courses()
        {
            var response = await _mediator.Send(new CourseAllQuery());
            return response.ResponseData;
        }

        [HttpGet(Constants.Tech)]
        public async Task<ActionResult> Technologies()
        {
            var response = await _mediator.Send(new CourseTechnologiesQuery());
            return response.ResponseData;
        }

        [HttpGet(Constants.Duration)]
        public async Task<ActionResult> Duration()
        {
            var response = await _mediator.Send(new CourseDurationQuery());
            return response.ResponseData;
        }

        [HttpGet(Constants.GetByDuration)]
        public async Task<ActionResult> CourserByTechnologyAndDuration([FromRoute] int from, int to, string? technology)
        {
            CourseByDurationQuery techDuration = new() { Technology = technology, From = from, To = to };   
            var response = await _mediator.Send(techDuration);
            return response.ResponseData;
        }

        [HttpGet(Constants.GetByTech)]
        public async Task<ActionResult> CoursesByTechnology([FromRoute] string technology)
        {
            CourseByTechQuery tech = new() { Technology = technology };
            var response = await _mediator.Send(tech);
            return response.ResponseData;
        }

        [Authorize(Roles = "admin")]
        [HttpDelete(Constants.DeleteById)]
        public async Task<ActionResult> DeleteCourse([FromRoute] string id)
        {
            CourseDeleteCommand delete = new() { Id = id };
            var response = await _mediator.Send(delete);
            return response.ResponseData;
        }
    }
}
