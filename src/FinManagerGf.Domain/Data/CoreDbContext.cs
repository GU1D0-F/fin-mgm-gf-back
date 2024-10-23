using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FinManagerGf.Domain.Entities;

namespace FinManagerGf.Data
{
    public class CoreDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
    {
    }
}
