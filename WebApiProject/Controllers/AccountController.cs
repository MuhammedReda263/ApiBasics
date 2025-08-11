using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiProject.DTO;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly IConfiguration _config;
        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration config)
        {
             _userManager = userManager;
             _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser();
            if (ModelState.IsValid)
            {
                user.Email = registerDTO.Email;
                user.UserName = registerDTO.Name;
                var success = await _userManager.CreateAsync(user,registerDTO.Password);
                if (success.Succeeded)
                return Ok("Created");
                foreach(var error in success.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
           }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
              var user = await _userManager.FindByNameAsync(loginDTO.UserName);
                if (user != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                    if (found)
                    {
                        //Generate Token
                        List<Claim> userClaims = new List<Claim>();
                        userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        userClaims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var UserRoles = await _userManager.GetRolesAsync(user);
                        foreach (var Role in UserRoles)
                        {
                            userClaims.Add(new Claim(ClaimTypes.Role, Role));
                        }
                        var SignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWT:SecritKey"));
                        SigningCredentials signingCredentials = new SigningCredentials(SignInKey, SecurityAlgorithms.HmacSha256);

                        //Design Token
                        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                            audience: _config["JWT:AudienceIp"],
                            issuer: _config["JWT:IssuerIp"],
                            expires: DateTime.Now.AddHours(1),
                            claims: userClaims,
                            signingCredentials: signingCredentials

                           );

                        //Generate TokenResponse

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                            expiretion = DateTime.Now.AddHours(1)

                        });
           
                    }

                }
                ModelState.AddModelError("", "UserName or Password is Invalid");
            }
            return BadRequest(ModelState);
        }
    }
}
