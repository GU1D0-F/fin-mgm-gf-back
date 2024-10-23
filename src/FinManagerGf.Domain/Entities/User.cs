using Microsoft.AspNetCore.Identity;

namespace FinManagerGf.Domain.Entities
{
    public class User : IdentityUser
    {
        public User() : base()
        {
        }

        public string? FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Admin { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
