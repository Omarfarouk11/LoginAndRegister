using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestAPIJwtAgain.Helpers;
using TestAPIJwtAgain.Model;
using TestAPIJwtAgain.SeedingData;

namespace TestAPIJwtAgain.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public AuthService(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,IOptions<JWT> jwt)
        {

            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;

        }
        public async Task<AuthModel> Registerasync(RegisterModel model)
        {
            var user =await  _userManager.FindByEmailAsync(model.email);
            if (user != null) 
            {
                return new AuthModel { message = "Email Is Already Registered" };
            
            }
            var username=await _userManager.FindByNameAsync(model.username);
            if (username != null)
            {
                return new AuthModel { message = "Username Is Already Registered" };

            }
            var appuser = new ApplicationUser
            {
                FirstName = model.firstname,
                LastName = model.lastname,
                Email = model.email,
                UserName = model.username,
            };
            var result =await _userManager.CreateAsync(appuser,model.password);
            if(!result.Succeeded)
            {
                string err = string.Empty;
                foreach (var e in result.Errors)
                {
                    err += $"{e.Description} , ";

                }
                return new AuthModel { message = err };

            }
            await _userManager.AddToRoleAsync(appuser, Roles.User);
          
                var Token = GenerateToken(appuser,await _userManager.GetRolesAsync(appuser));
                return new AuthModel {token=Token,message="Your are Registered Successfully",isAuthnticated=true,email=appuser.Email,username=appuser.UserName,roles=new List<string> { Roles.User } };
                
            

        }
        public async Task<AuthModel> LoginAsync(LoginModel model)
        {
            var Authmodel=new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                Authmodel.message = "Email Or Password Is InCorrect";
                return Authmodel;

            }
            var Token = GenerateToken(user, await _userManager.GetRolesAsync(user));
            var userroles = await _userManager.GetRolesAsync(user);


            Authmodel.isAuthnticated = true;
            Authmodel.token = Token;
            Authmodel.email = user.Email;
            Authmodel.username = user.UserName;
            Authmodel.roles= userroles.ToList();

            return Authmodel;
        }

        // That For Generate Token
        public  string GenerateToken(ApplicationUser user,IList<string> roles)
        {
   
            var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                                        issuer:_jwt.Issuer,
                                        audience: _jwt.audience,
                                        claims,
                                        expires: DateTime.Now.AddDays(_jwt.Durationinday),
                                        signingCredentials: creds
                                    );

            return new JwtSecurityTokenHandler().WriteToken(token);





        }

        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var role = await _roleManager.RoleExistsAsync(model.RoleName);
            if (user == null || !role)
            {
                return "Invalid UserId Or Rolename";
            }
      
            if(await _userManager.IsInRoleAsync(user, model.RoleName))
            {
                return "User Already in This Role";
            }

            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            return result.Succeeded ? string.Empty : "Something is wrong";
           


        }
    }
}
