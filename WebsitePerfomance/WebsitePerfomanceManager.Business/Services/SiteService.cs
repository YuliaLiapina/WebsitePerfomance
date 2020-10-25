using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using WebsitePerfomance.Data.Interfaces;
using WebsitePerfomance.Data.Models;
using WebsitePerfomanceManager.Business.Interfaces;
using WebsitePerfomanceManager.Business.Models;

namespace WebsitePerfomanceManager.Business.Services
{
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IMapper _mapper;

        public SiteService(ISiteRepository siteRepository, IMapper mapper)
        {
            _siteRepository = siteRepository;
            _mapper = mapper;
        }
        public void Add(SiteModel siteModel, MeasurementModel measurement)
        {
            var site = _mapper.Map<Site>(siteModel);

            var checkSite = _siteRepository.GetSiteByUrl(siteModel.Url);

            if (checkSite == null)
            {
                _siteRepository.Add(site);
            }

            else
            {
                var result = _mapper.Map<Measurement>(measurement);
                _siteRepository.AddMeasurementToSite(result, checkSite.Id);
            }
        }

        public IList<SiteModel> GetAll()
        {
            var sites = _siteRepository.GetAll();

            var siteModels = _mapper.Map<IList<SiteModel>>(sites);

            return siteModels;
        }

        public string GetSitemap(string url)
        {
            url = url.Replace("https://", string.Empty);

            var siteMap = "https://" + url.Substring(0, url.IndexOf('/')) + "/sitemap.xml";

            return siteMap;
        }

        public void AddMeasurementToSite(MeasurementModel measurementModel, int siteId)
        {
            var measurement = _mapper.Map<Measurement>(measurementModel);

            _siteRepository.AddMeasurementToSite(measurement, siteId);
        }

        public MeasurementModel MeasureSpeedResponse(string sitemap)
        {
            var measure = new MeasurementModel();
            Stopwatch timer = new Stopwatch();

            timer.Start();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sitemap);
            request.UserAgent = "[any words that is more than 5 characters]";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            timer.Stop();

            measure.ResponseTime = timer.Elapsed.TotalMilliseconds;
            measure.Date = DateTime.Now.Date;

            return measure;
        }

        public SiteModel GetSiteByUrl(string url)
        {
            var site = _siteRepository.GetSiteByUrl(url);

            var siteModel = _mapper.Map<SiteModel>(site);

            return siteModel;
        }

        public Chart GetChartStatistics(SiteModel site)
        {
            var listDates = new List<string>();
            var listSpeeds = new List<string>();

            var measurments = site.Measurements.OrderBy(x => x.ResponseTime);

            foreach (var item in measurments)
            {
                listDates.Add(item.Date.ToString());
                listSpeeds.Add(item.ResponseTime.ToString());
            }

            var myChart = new Chart(width: 600, height: 300, theme: ChartTheme.Vanilla)
                   .AddTitle("Statistic")
                   .AddSeries(
                       name: "Statistic",
                       chartType: "Column",
                       xValue: listDates,
                   yValues: listSpeeds);

            myChart.SetXAxis("Date of testing");
            myChart.SetYAxis("Response time (ms)");

            return myChart;
        }

        public SiteModel GetMinMaxValues(SiteModel siteModel)
        {
            var result = _siteRepository.GetSiteByUrl(siteModel.Url);

            var resultModel = _mapper.Map<SiteModel>(result);

            resultModel.MinResponseTime = resultModel.Measurements[0].ResponseTime;
            resultModel.MaxResponseTime = resultModel.Measurements[0].ResponseTime;

            for (int i = 0; i < resultModel.Measurements.Count; i++)
            {
                if (resultModel.Measurements[i].ResponseTime < resultModel.MinResponseTime)
                {
                    resultModel.MinResponseTime = resultModel.Measurements[i].ResponseTime;
                }
            }

            for (int i = 0; i < resultModel.Measurements.Count; i++)
            {
                if (resultModel.Measurements[i].ResponseTime > resultModel.MaxResponseTime)
                {
                    resultModel.MaxResponseTime = resultModel.Measurements[i].ResponseTime;
                }
            }

            var site = _mapper.Map<Site>(resultModel);

            _siteRepository.SaveMinMaxValues(site);

            return resultModel;
        }

        public SiteModel GetSiteById(int id)
        {
            var site = _siteRepository.GetSiteById(id);

            var siteModel = _mapper.Map<SiteModel>(site);

            return siteModel;
        }
    }
}
