using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class UserService : BaseService<User, LoginDTO, RegisterDTO, ForgotPasswordDTO>, IUserService
    {

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;   
        public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository)
        {
            _mapper = mapper;   
            _userRepository = userRepository;
        }

        public Task ForgotPassWordAsync(ForgotPasswordDTO forgotPasswordDTO)
        {
            throw new NotImplementedException();
        }

        public Task RegisterAsync(RegisterDTO registerDTO)
        {
            throw new NotImplementedException();
        }
        public async Task<LoginDTO?> FindAccountAsync(LoginDTO loginDTO)
        {
            // Tìm thông tin tài khoản trong db
            var findAccount = await _userRepository.FindAccountAsync(loginDTO.UserName, loginDTO.Password); 

            return MapEntityToDTO(findAccount);
            // Nếu không tìm thấy thì trả về thông báo cho FE
            //if (findAccount == null) 
            //{
            //    return null;
            //}
            //// Tìm thấy
            //else
            //{
            //    // Băm mật khẩu người dùng gửi lên
            //    string hashedPasssword = string.Empty;  
            //    // So sánh với mật khẩu trong db

            //    // Trả về kết quả so sánh
            //}
        }

        //    static string CreateAccessToken(
        //        JwtOptions jwtOptions,
        //        string username,
        //        TimeSpan expiration,
        //        string[] permissions)
        //    {
        //        var keyBytes = Encoding.UTF8.GetBytes(jwtOptions.SigningKey);
        //        var symmetricKey = new SymmetricSecurityKey(keyBytes);

        //        var signingCredentials = new SigningCredentials(
        //            symmetricKey,
        //             👇 one of the most popular.
        //            SecurityAlgorithms.HmacSha256);

        //        var claims = new List<Claim>()
        //{
        //    new Claim("sub", username),
        //    new Claim("name", username),
        //    new Claim("aud", jwtOptions.Audience)
        //};

        //        var roleClaims = permissions.Select(x => new Claim("role", x));
        //        claims.AddRange(roleClaims);

        //        var token = new JwtSecurityToken(
        //            issuer: jwtOptions.Issuer,
        //            audience: jwtOptions.Audience,
        //            claims: claims,
        //            expires: DateTime.Now.Add(expiration),
        //            signingCredentials: signingCredentials);

        //        var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
        //        return rawToken;
        //    }


        /// <summary>
        /// Hàm chuyển đổi RegisterDTO sang User
        /// </summary>
        /// <param name="createDTO">Thông tin đăng ký tài khoản</param>
        /// <returns>Thông tin tài khoản mật khẩu</returns>
        /// Created by: nkdang 21/12/2023
        public override User MapCreateDTOToEntity(RegisterDTO createDTO)
        {
            var user = _mapper.Map<User>(createDTO);    
            return user;    
        }

        /// <summary>
        /// Hàm chuyển đổi LoginDTO sang User
        /// </summary>
        /// <param name="dto">Thông tin đăng nhập</param>
        /// <returns>Thông tin tài khoản mật khẩu</returns>
        public override User MapDTOToEntity(LoginDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            return user;    
        }

        /// <summary>
        /// Hàm chuyển đổi User sang LoginDTO
        /// </summary>
        /// <param name="entity">Thông tin User</param>
        /// <returns>Thông tin đăng nhập Login</returns>
        /// Created by: nkdang 21/12/2023
        public override LoginDTO MapEntityToDTO(User entity)
        {
            var loginDTO = _mapper.Map<LoginDTO>(entity);   
            return loginDTO;    
        }

        /// <summary>
        /// Hàm chuyển đổi từ Thông tin quên mật khẩu sang tài khoản mật khẩu User
        /// </summary>
        /// <param name="updateDTO">Thông tin quên mật khẩu</param>
        /// <param name="entity">Thông tin tài khoản mật khẩu</param>
        /// <returns></returns>
        /// Created by: nkdang 21/12/2023
        public override User MapUpdateDTOToEntity(ForgotPasswordDTO updateDTO, User entity)
        {
            var user = _mapper.Map(updateDTO,  entity);
            return user;    
        }

        
    }
}
