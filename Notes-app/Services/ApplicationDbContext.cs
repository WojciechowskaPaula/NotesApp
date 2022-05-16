using Microsoft.EntityFrameworkCore;
using Notes_app.Models;

namespace Notes_app.Services
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Notes> Notes { get; set; }
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}



