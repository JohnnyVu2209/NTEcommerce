using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NTEcommerce.SharedDataModel;
using NTEcommerce.SharedDataModel.User;
using NTEcommerce.WebAPI.Constant;
using NTEcommerce.WebAPI.Model.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static NTEcommerce.WebAPI.Constant.MessageCode;

namespace NTEcommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;
        public AuthenticationController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim( JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new ResponseJWT { Token = new JwtSecurityTokenHandler().WriteToken(token), Expiration = token.ValidTo, Username = user.UserName});
            }
            return Unauthorized(ErrorCode.USERNAME_OR_PASSWORD_NOT_CORRECT);

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var userExists = await _userManager.FindByNameAsync(registerModel.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { StatusCode = 500, Message = ErrorCode.USER_ALREADY_EXISTS });

            User user = new()
            {
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerModel.UserName
            };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { StatusCode = 500, Message = ErrorCode.USER_REGISTER_FAILED});

            return Ok(SuccessCode.USER_REGISTER_SUCCESSFULLY);
        }
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { StatusCode = 500, Message = ErrorCode.USER_ALREADY_EXISTS });

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { StatusCode = 500, Message =ErrorCode.USER_REGISTER_FAILED });

            if (!await _roleManager.RoleExistsAsync(RoleConstant.Admin))
                await _roleManager.CreateAsync(new Role { Name = RoleConstant.Admin});
            if (!await _roleManager.RoleExistsAsync(RoleConstant.Customer))
                await _roleManager.CreateAsync(new Role { Name = RoleConstant.Customer });

            if (await _roleManager.RoleExistsAsync(RoleConstant.Admin))
            {
                await _userManager.AddToRoleAsync(user, RoleConstant.Admin);
            }
            if (await _roleManager.RoleExistsAsync(RoleConstant.Admin))
            {
                await _userManager.AddToRoleAsync(user, RoleConstant.Customer);
            }
            return Ok(SuccessCode.USER_REGISTER_SUCCESSFULLY);
        }
        [HttpGet("GetUserInfo/{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
                return NotFound(ErrorCode.USER_NOT_FOUND);

            var userDto = _mapper.Map<UserDetailModel>(user);

            return Ok(userDto);
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(6),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
