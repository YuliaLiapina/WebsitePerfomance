using System.Data.Entity;
using WebsitePerfomance.Data.Models;

namespace WebsitePerfomance.Data
{
    public class WebsitePerfomanceDbContext:DbContext
    {
        public WebsitePerfomanceDbContext():base("DefaultConnection")
        {

        }

        public DbSet<Site> Sites { get; set; } 
        public DbSet<Measurement> Measurements { get; set; }
    }
}
