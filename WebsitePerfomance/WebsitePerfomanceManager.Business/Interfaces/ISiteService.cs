using System.Collections.Generic;
using System.Web.Helpers;
using WebsitePerfomanceManager.Business.Models;

namespace WebsitePerfomanceManager.Business.Interfaces
{
    public interface ISiteService
    {
        void Add(SiteModel siteModel, MeasurementModel measurement);
        IList<SiteModel> GetAll();
        string GetSitemap(string url);
        MeasurementModel MeasureSpeedResponse(string sitemap);
        void AddMeasurementToSite(MeasurementModel measurement, int siteId);
        SiteModel GetSiteByUrl(string url);
        Chart GetChartStatistics(SiteModel site);
        SiteModel GetMinMaxValues(SiteModel siteModel);
        SiteModel GetSiteById(int id);
    }
}
