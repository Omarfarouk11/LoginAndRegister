using System.IdentityModel.Tokens.Jwt;
using TestAPIJwtAgain.Model;

namespace TestAPIJwtAgain.Services
{
    public interface IAuthService
    {
        Task<AuthModel> Registerasync(RegisterModel model);
        public string GenerateToken(ApplicationUser user, IList<string> roles);

        Task<AuthModel> LoginAsync(LoginModel model);
        // This Method For Assign Role To User 
        Task<string> AddRoleAsync(AddRoleModel model);
    }
}
