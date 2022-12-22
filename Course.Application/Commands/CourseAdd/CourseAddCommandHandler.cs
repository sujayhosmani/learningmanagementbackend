using AutoMapper;
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

namespace Course.Application.CourseAdd
{
    public class CourseAddCommandHandler : IRequestHandler<CourseAddCommand, ValidatableResponse<Courses>>
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IUserService _userService;
        private readonly ILogger<CourseAddCommandHandler> _logger;

        public CourseAddCommandHandler(ICourseRepository courseRepository, IUserService userService, ILogger<CourseAddCommandHandler> logger)
        {
            _courseRepo = courseRepository;
            _userService = userService;
            _logger = logger;
        }
        public async Task<ValidatableResponse<Courses>> Handle(CourseAddCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Courses coursesMap = new()
                {
                    Name = request.Name,
                    Technology = request.Technology,
                    Description = request.Description,
                    LaunchURL = request.LaunchURL,
                    Duration = request.Duration,
                    CreatedBy = _userService.GetUserName(),
                    CreatedTimestamp = DateTime.UtcNow,
                };
                Courses course = await _courseRepo.AddCourse(coursesMap);
                _logger.LogInformation("CourseAddCommandHandler course added: {Name}", request.Name);
                return new ValidatableResponse<Courses>("Addes Successfully", null, course, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError("CourseAddCommandHandler exception: {message}", ex.Message);
                return new ValidatableResponse<Courses>(ex.Message, ex.ToString(), StatusCodes.Status500InternalServerError);

            }

        }
    }
}
