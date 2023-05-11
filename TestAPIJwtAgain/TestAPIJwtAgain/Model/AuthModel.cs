using TestAPIJwtAgain.SeedingData;

namespace TestAPIJwtAgain.Model
{
    public class AuthModel
    {
        public string message { get; set; }
        public bool isAuthnticated { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public List<string> roles { get; set; } 
        public string token { get; set; }
        public DateTime Expireson { get; set; }


    }
}
