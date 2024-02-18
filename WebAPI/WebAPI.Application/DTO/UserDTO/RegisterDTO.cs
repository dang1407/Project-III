using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class RegisterDTO : BaseDTO
    {
        public string UserName = string.Empty;
        public string Password = string.Empty;
        public string RewritePassword = string.Empty;
    }
}
