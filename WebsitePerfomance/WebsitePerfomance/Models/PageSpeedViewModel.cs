
namespace WebsitePerfomance.Models
{
    public class PageSpeedViewModel
    {
        public int Id { get; set; }
        public double Speed { get; set; }
        public int IdTestingPageViewModel { get; set; }
        public TestingPageViewModel Page { get; set; }
    }
}