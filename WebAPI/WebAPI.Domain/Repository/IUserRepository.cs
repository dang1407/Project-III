using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
     public interface IUserRepository : IBaseRepository<User>  
    {
        Task<User?> FindAccountAsync(string username, string password);    
    }
}
