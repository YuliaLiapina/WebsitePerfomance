using System.Collections.Generic;
using WebsitePerfomance.Data.Models;

namespace WebsitePerfomance.Data.Interfaces
{
    public interface ISiteRepository
    {
        void Add(Site Site);
        IList<Site> GetAllSites();
        Site GetSiteByUrl(string url);
        Site GetSiteById(int id);
        void UpdateSite(List<TestingPage> pagesSite, int id);
    }
}
