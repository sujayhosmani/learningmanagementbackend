using AutoMapper;
using LearningManagement.Common.interfaces;
using LearningManagement.Domain.DTOs;
using LearningManagement.Domain.Entities;
using LearningManagement.Infrastructure.DbContexts;
using LearningManagement.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _dbContext;

        public UserRepository(IMapper mapper, IApplicationDbContext applicationDbContext)
        {
            _mapper = mapper;
            _dbContext = applicationDbContext;
        }

        public async Task<UserDto> CreateUser(User user, CancellationToken cancellationToken)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);

            await _dbContext.SaveDbAsync(cancellationToken, user.Name);
            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<bool> DeleteUser(int id, CancellationToken cancellationToken)
        {
            User user = await _dbContext.Users.Where(e => e.Id == id).FirstOrDefaultAsync(cancellationToken) ?? new();
            _dbContext.Users.Remove(user);
            return await _dbContext.SaveDbAsync(cancellationToken, user.Name) != 0;
        }

        public async Task<User> GetUserByEmailId(string emailId, CancellationToken cancellationToken)
        {
            User? user = await _dbContext.Users.Where(e => e.EmailId == emailId).FirstOrDefaultAsync(cancellationToken);
            return user;
        }

        public async Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken)
        {
            List<User> users = await _dbContext.Users.ToListAsync(cancellationToken);
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
