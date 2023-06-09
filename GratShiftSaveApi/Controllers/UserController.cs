using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GratShiftSaveApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Cloud.Firestore;

namespace GratShiftSaveApiController.Controllers
{
  [ApiController]
  [Route("api/")]
  public class UserController : ControllerBase
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly FirestoreDb _db;
    public UserController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            FirestoreDb db)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _configuration = configuration;
      _db = db;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register-user")]
    public async Task<IActionResult> Register([FromBody] UserRegister model)
    {
      var userExists = await _userManager.FindByNameAsync(model.Username);
      if (userExists != null)
        return StatusCode(StatusCodes.Status500InternalServerError, new UserResponse { Status = "Error", Message = "User already exists!" });

      var userId = Guid.NewGuid().ToString();
      // var firebaseUid = IdentityUser.Uid;

      IdentityUser user = new()
      {
        Email = model.Email,
        SecurityStamp = Guid.NewGuid().ToString(),
        UserName = model.Username,
        Id = userId
        // Uid = FirebaseUid
      };
      var result = await _userManager.CreateAsync(user, model.Password);
      if (!result.Succeeded)
        return StatusCode(StatusCodes.Status500InternalServerError, new UserResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

      var firestoreUser = new
      {
        Id = user.Id,
        Email = user.Email,
        UserName = user.UserName
      };

      var userCollection = _db.Collection("Users");
      await userCollection.Document(user.Id).SetAsync(firestoreUser);

      return Ok(new UserResponse { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAdmin([FromBody] UserRegister model)
    {
      var userExists = await _userManager.FindByNameAsync(model.Username);
      if (userExists != null)
        return StatusCode(StatusCodes.Status500InternalServerError, new UserResponse { Status = "Error", Message = "Account already exists with that email address." });

      var userId = Guid.NewGuid().ToString();

      IdentityUser user = new()
      {
        Email = model.Email,
        SecurityStamp = Guid.NewGuid().ToString(),
        UserName = model.Username,
        Id = userId
      };
      var result = await _userManager.CreateAsync(user, model.Password);
      if (!result.Succeeded)
        return StatusCode(StatusCodes.Status500InternalServerError, new UserResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

      var firestoreUser = new
      {
        Id = user.Id,
        Email = user.Email,
        UserName = user.UserName
      };

      var userCollection = _db.Collection("Users");
      await userCollection.Document(user.Id).SetAsync(firestoreUser);

      if (!await _roleManager.RoleExistsAsync(UserRole.Admin))
        await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
      if (!await _roleManager.RoleExistsAsync(UserRole.User))
        await _roleManager.CreateAsync(new IdentityRole(UserRole.User));

      if (await _roleManager.RoleExistsAsync(UserRole.Admin))
      {
        await _userManager.AddToRoleAsync(user, UserRole.Admin);
      }
      if (await _roleManager.RoleExistsAsync(UserRole.User))
      {
        await _userManager.AddToRoleAsync(user, UserRole.User);
      }
      return Ok(new UserResponse { Status = "Success", Message = "User created successfully!" });
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims, string userId)
    {
      var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

      authClaims.Add(new Claim("userId", userId));

      var token = new JwtSecurityToken(
          issuer: _configuration["JWT:ValidIssuer"],
          audience: _configuration["JWT:ValidAudience"],
          expires: DateTime.Now.AddHours(12),
          claims: authClaims,
          signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
          );

      return token;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLogin model)
    {
      var user = await _userManager.FindByNameAsync(model.Username);
      if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
      {
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
        foreach (var userRole in userRoles)
        {
          authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = GetToken(authClaims, user.Id);

        return Ok(new
        {
          token = new JwtSecurityTokenHandler().WriteToken(token),
          expiration = token.ValidTo
        });
      }
      return Unauthorized();
    }
  }
}