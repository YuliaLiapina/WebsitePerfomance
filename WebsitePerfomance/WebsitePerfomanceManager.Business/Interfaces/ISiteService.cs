using System.Collections.Generic;
using System.Web.Helpers;
using WebsitePerfomanceManager.Business.Models;

namespace WebsitePerfomanceManager.Business.Interfaces
{
    public interface ISiteService
    {
        void Add(SiteModel siteModel);
        IList<SiteModel> GetAll();
        string GetSitemap(string url);
        List<TestingPageModel> MeasureSpeedResponse(string sitemap);
        SiteModel GetSiteByUrl(string url);
        SiteModel GetSiteById(int id);
        List<TestingPageModel> GetMinMax(List<TestingPageModel> pages);
        List<TestingPageModel> GetListSpeeds(List<TestingPageModel> measures, List<TestingPageModel> pagesSite);
        void UpdateSite(List<TestingPageModel> pagesSite, int id);
    }
}
