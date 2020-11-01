using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebsitePerfomance.Models;
using WebsitePerfomanceManager.Business.Interfaces;
using WebsitePerfomanceManager.Business.Models;

namespace WebsitePerfomance.Controllers
{
    public class WebsitePerfomanceController : Controller
    {
        private readonly ISiteService _siteService;
        private readonly IMapper _mapper;

        public WebsitePerfomanceController(ISiteService siteService, IMapper mapper)
        {
            _siteService = siteService;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Measure(SitePostModel site)
        {
            if (site.Url != null)
            {
                var siteModel = _mapper.Map<SiteModel>(site);
                var measures = new List<TestingPageModel>();

                var checkSite = _siteService.GetSiteByUrl(site.Url); 

                if (checkSite == null)
                {
                    siteModel.Sitemap = _siteService.GetSitemap(site.Url);
                    measures = _siteService.MeasureSpeedResponse(siteModel.Sitemap);

                    if (measures == null)
                    {
                        return View("SiteMapNotFound");
                    }

                    siteModel.Pages = measures;
                    siteModel.Pages = _siteService.GetMinMax(siteModel.Pages);
                    _siteService.Add(siteModel);
                }

                else
                {
                    measures = _siteService.MeasureSpeedResponse(checkSite.Sitemap);
                    checkSite.Pages = _siteService.GetListSpeeds(measures, checkSite.Pages);
                    checkSite.Pages = _siteService.GetMinMax(checkSite.Pages);
                    _siteService.UpdateSite(checkSite.Pages, checkSite.Id);
                }

                var getSite1 = _siteService.GetSiteByUrl(site.Url);
                var result = _mapper.Map<SiteViewModel>(getSite1);

                result.Pages = result.Pages.OrderBy(m => m.CurrentResponseTime).ToList();

                result.Sites = GetSelectList();

                return View(result);
            }

            return View("Index");
        }

        public ActionResult TablePartial()
        {
            return PartialView();
        }

        public ActionResult ChartPartial()
        {
            return PartialView();
        }

        public ActionResult ChartArrayBasic(int id)
        {
            var siteModel = _siteService.GetSiteById(id);
            var chart = _siteService.GetChartStatistics(siteModel);

            var chartStatisticsModel = new ChartStatisticsViewModel();
            chartStatisticsModel.Chart = chart;
            chartStatisticsModel.Chart.Write();

            return null;
        }

        public ActionResult Details(int SelectedId)
        {
            var site = _siteService.GetSiteById(SelectedId);
            var siteViewModel = _mapper.Map<SiteViewModel>(site);

            return View(siteViewModel);
        }

        private IEnumerable<SelectListItem> GetSelectList()
        {
            var sites = _siteService.GetAll();
            var sitesViewModel = _mapper.Map<IList<SiteViewModel>>(sites);

            var selectList = from item in sitesViewModel
                             select new SelectListItem { Text = item.Url, Value = item.Id.ToString() };

            return selectList;
        }

        public ActionResult SiteMapNotFound()
        {
            return View();
        }

        public ActionResult HistoryPartial()
        {
            return PartialView();
        }
    }
}