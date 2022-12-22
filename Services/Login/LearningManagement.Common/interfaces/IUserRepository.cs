using LearningManagement.Domain.DTOs;
using LearningManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Common.interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken);

        Task<User> GetUserByEmailId(string emailId, CancellationToken cancellationToken);
        Task<UserDto> CreateUser(User user, CancellationToken cancellationToken);
        Task<bool> DeleteUser(int id, CancellationToken cancellationToken);
    }
}
