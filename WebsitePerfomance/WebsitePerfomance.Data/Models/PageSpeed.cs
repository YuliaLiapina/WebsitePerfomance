
namespace WebsitePerfomance.Data.Models
{
    public class PageSpeed
    {
        public int Id { get; set; }
        public double Speed { get; set; }
        public int IdTestingPage { get; set; }
        public TestingPage Page { get; set; }
    }
}
