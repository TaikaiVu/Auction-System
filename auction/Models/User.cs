using Microsoft.AspNetCore.Identity;

namespace assignment1web.Models
{
    public class User: IdentityUser
    {
        public string First_name { get; set; }
        public string Last_name { get; set; }
    }
}
