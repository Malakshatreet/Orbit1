using Microsoft.EntityFrameworkCore;

namespace souq02.Models
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Teammember> TeamMembers { get; set; }
        public DbSet<ContactUs> Contacts { get; set; }

    }
}
