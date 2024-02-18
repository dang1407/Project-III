using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface IUserService : IBaseService<LoginDTO, RegisterDTO, ForgotPasswordDTO>
    {
        Task<LoginDTO?> FindAccountAsync(LoginDTO loginDTO);
        Task RegisterAsync(RegisterDTO registerDTO);    
        Task ForgotPassWordAsync(ForgotPasswordDTO forgotPasswordDTO);  
    }
}
