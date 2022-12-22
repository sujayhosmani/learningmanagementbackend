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

namespace Course.Application.Queries.CourseByDuration
{
    public class CourseByDurationQueryHandler : IRequestHandler<CourseByDurationQuery, ValidatableResponse<List<Courses>>>
    {
        private readonly ICourseRepository _repo;
        private readonly ILogger<CourseByDurationQueryHandler> _logger;

        public CourseByDurationQueryHandler(ICourseRepository repo, ILogger<CourseByDurationQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        public async Task<ValidatableResponse<List<Courses>>> Handle(CourseByDurationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Courses> courses = await _repo.CourserByTechnologyAndDuration(request.Technology, request.From, request.To);
                _logger.LogInformation("CourseByDurationQueryHandler get duration requested for {tech} {from} {to}", request.Technology ?? "", request.From, request.To);
                return new ValidatableResponse<List<Courses>>("successful", null, courses, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError("CourseByDurationQueryHandler exception {message} ", ex.Message);
                return new ValidatableResponse<List<Courses>>(ex.Message, ex.Message, StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}
