using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebsitePerfomance.Data.Interfaces;
using WebsitePerfomance.Data.Models;

namespace WebsitePerfomance.Data.Repositories
{
    public class SiteRepository : ISiteRepository
    {
        public void Add(Site Site)
        {
            using(var context = new WebsitePerfomanceDbContext())
            {
                context.Sites.Add(Site);
                context.SaveChanges();
            }
        }

        public void AddMeasurementToSite(Measurement measurement, int siteId)
        {
            using(var context = new WebsitePerfomanceDbContext())
            {
                var site = context.Sites.FirstOrDefault(s => s.Id == siteId);
                site.Measurements.Add(measurement);

                context.SaveChanges();
            }
        }

        public IList<Site>GetAll()
        {
            using(var context = new WebsitePerfomanceDbContext())
            {
                var sites = context.Sites.Include(s => s.Measurements).ToList();

                return sites;
            }
        }

        public Site GetSiteByUrl(string url)
        {
            using(var context = new WebsitePerfomanceDbContext())
            {
                var site = context.Sites.Include(s => s.Measurements).FirstOrDefault(s=>s.Url==url);

                return site;
            }
        }

        public Site GetSiteById(int id)
        {
            using (var context = new WebsitePerfomanceDbContext())
            {
                var site = context.Sites.Include(s => s.Measurements).FirstOrDefault(s => s.Id == id);

                return site;
            }
        }

        public void SaveMinMaxValues(Site site)
        {
            using (var context = new WebsitePerfomanceDbContext())
            {
                var currentSite = context.Sites.Include(s => s.Measurements).FirstOrDefault(s => s.Id == site.Id);

                currentSite.MinResponseTime = site.MinResponseTime;
                currentSite.MaxResponseTime = site.MaxResponseTime;

                context.SaveChanges();
            }
        }
    }
}
