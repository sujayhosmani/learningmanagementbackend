using LearningManagement.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Infrastructure.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUserName(string name)
        {
            var principal = _httpContextAccessor.HttpContext.User;
            var userId = principal.FindFirst(c => c.Type == ClaimTypes.Name);
            return userId?.Value ?? name;
        }
    }
}
