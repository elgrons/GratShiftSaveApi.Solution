using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GratShiftSaveApi.Models;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GratShiftSaveApiController.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UserController : ControllerBase
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public UserController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLogin model)
    {
      var user = await _userManager.FindByNameAsync(model.Email);
      if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
      {
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

        foreach (var userRole in userRoles)
        {
          authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = GetToken(authClaims);

        return Ok(new
        {
          token = new JwtSecurityTokenHandler().WriteToken(token),
          expiration = token.ValidTo
        });
      }
      return Unauthorized();
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] Register model)
    {
      var userExists = await _userManager.FindByNameAsync(model.Email);
      if (userExists != null)
        return StatusCode(StatusCodes.Status500InternalServerError, new UserResponse { Status = "Error", Message = "User already exists!" });

      IdentityUser user = new()
      {
        Email = model.Email,
        SecurityStamp = Guid.NewGuid().ToString(),
      };
      var result = await _userManager.CreateAsync(user, model.Password);
      if (!result.Succeeded)
        return StatusCode(StatusCodes.Status500InternalServerError, new UserResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

      return Ok(new UserResponse { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] Register model)
    {
      var userExists = await _userManager.FindByNameAsync(model.Email);
      if (userExists != null)
        return StatusCode(StatusCodes.Status500InternalServerError, new UserResponse { Status = "Error", Message = "Account already exists with that email address." });

      IdentityUser user = new()
      {
        Email = model.Email,
        SecurityStamp = Guid.NewGuid().ToString(),
      };
      var result = await _userManager.CreateAsync(user, model.Password);
      if (!result.Succeeded)
        return StatusCode(StatusCodes.Status500InternalServerError, new UserResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

      if (!await _roleManager.RoleExistsAsync(UserRole.Admin))
        await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
      if (!await _roleManager.RoleExistsAsync(UserRole.User))
        await _roleManager.CreateAsync(new IdentityRole(UserRole.User));

      if (await _roleManager.RoleExistsAsync(UserRole.Admin))
      {
        await _userManager.AddToRoleAsync(user, UserRole.Admin);
      }
      if (await _roleManager.RoleExistsAsync(UserRole.Admin))
      {
        await _userManager.AddToRoleAsync(user, UserRole.User);
      }
      return Ok(new UserResponse { Status = "Success", Message = "User created successfully!" });
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
      var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

      var token = new JwtSecurityToken(
          issuer: _configuration["JWT:ValidIssuer"],
          audience: _configuration["JWT:ValidAudience"],
          expires: DateTime.Now.AddHours(3),
          claims: authClaims,
          signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
          );

      return token;
    }
  }
}