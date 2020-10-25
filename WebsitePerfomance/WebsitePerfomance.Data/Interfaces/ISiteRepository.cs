using System.Collections.Generic;
using WebsitePerfomance.Data.Models;

namespace WebsitePerfomance.Data.Interfaces
{
    public interface ISiteRepository
    {
        void Add(Site Site);
        IList<Site> GetAll();
        Site GetSiteByUrl(string url);
        void AddMeasurementToSite(Measurement measurement, int siteId);
        Site GetSiteById(int id);
        void SaveMinMaxValues(Site site);
    }
}
