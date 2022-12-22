using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Infrastructure.Interfaces
{
    public interface IUserService
    {
        string GetUserName(string name);
    }
}
