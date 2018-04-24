using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Connection
{
    public class TwitterBackupDbContext : IdentityDbContext<User>
    {
        public TwitterBackupDbContext(DbContextOptions<TwitterBackupDbContext> options)
            : base(options)
        {
        }

    }
}
