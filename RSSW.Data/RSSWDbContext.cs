using RSSW.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace RSSW.Data
{
    public class RSSWDbContext : DbContext {
        public virtual DbSet<Article> Articles { get; set; }

        public RSSWDbContext(DbContextOptions options) : base(options) { }
    }
}
