using Course.Domain.Entity;
using Course.Infrastructure.Interfaces;
using DnsClient.Internal;
using LearningManagement.Common.ResponseInterceptor;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.CourseDelete
{
    public class CourseDeleteCommandHandler : IRequestHandler<CourseDeleteCommand, ValidatableResponse<string>>
    {
        private readonly ICourseRepository _courseRepo;
        private readonly ILogger<CourseDeleteCommandHandler> _logger;

        public CourseDeleteCommandHandler(ICourseRepository courseRepository, ILogger<CourseDeleteCommandHandler> logger)
        {
            _courseRepo = courseRepository;
            _logger = logger;
        }
        public async Task<ValidatableResponse<string>> Handle(CourseDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool val = await _courseRepo.DeleteCourse(request.Id);
                if (val)
                {
                    _logger.LogInformation("CourseDeleteCommandHandler course deleted for {id}", request.Id);
                    return new ValidatableResponse<string>("deleted successfully", null, "deleted successfully", StatusCodes.Status200OK);

                }
                _logger.LogWarning("CourseDeleteCommandHandler course delete failed {id}", request.Id);
                return new ValidatableResponse<string>("Failed to delete", null, "Failed to delete try again", StatusCodes.Status400BadRequest);
            }
            catch(Exception ex)
            {
                _logger.LogError("CourseDeleteCommandHandler exception {message}", ex.Message);
                return new ValidatableResponse<string>(ex.Message, ex.Message, StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}
