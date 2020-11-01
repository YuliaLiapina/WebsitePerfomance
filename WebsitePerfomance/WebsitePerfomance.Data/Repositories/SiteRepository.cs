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
            using (var context = new WebsitePerfomanceDbContext())
            {
                context.Sites.Add(Site);
                context.SaveChanges();
            }
        }

        public IList<Site> GetAllSites()
        {
            using (var context = new WebsitePerfomanceDbContext())
            {
                var sites = context.Sites.Include(s => s.Pages.Select(p => p.Speeds)).ToList();

                return sites;
            }
        }

        public Site GetSiteByUrl(string url)
        {
            using (var context = new WebsitePerfomanceDbContext())
            {
                var currentSite = context.Sites.Include(s => s.Pages.Select(p => p.Speeds)).FirstOrDefault(x => x.Url == url);

                return currentSite;
            }
        }

        public Site GetSiteById(int id)
        {
            using (var context = new WebsitePerfomanceDbContext())
            {
                var site = context.Sites.Include(s => s.Pages.Select(p => p.Speeds)).FirstOrDefault(s => s.Id == id);

                return site;
            }
        }

        public void UpdateSite(List<TestingPage> pagesSite, int id)
        {
            using (var context = new WebsitePerfomanceDbContext())
            {
                var currentSite = context.Sites.Include(s => s.Pages.Select(p => p.Speeds)).FirstOrDefault(x => x.Id == id);

                for (int i=0;i<pagesSite.Count;i++)
                {
                    currentSite.Pages[i] = pagesSite[i];
                    pagesSite[i].Site = currentSite;
                }

                context.SaveChanges();
            }
        }
    }
}
