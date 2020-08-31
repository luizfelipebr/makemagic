using makemagic.Models;
using Microsoft.EntityFrameworkCore;

namespace makemagic.Data
{
    public class MakeMagicContext : DbContext
    {
        public MakeMagicContext(DbContextOptions<MakeMagicContext> options):base (options)
        {
        }

        public DbSet<Character> Characters { get; set; }
    }
}