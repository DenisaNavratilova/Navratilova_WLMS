using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pva.Models;

namespace pva.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public int TotalValues { get; set; }
        public int TotalStations { get; set; }

        public IList<StationDailyAverage> StationDailyAverages { get; set; }

        public async Task OnGetAsync()
        {
            TotalValues = await _context.Values.CountAsync();
            TotalStations = await _context.Stations.CountAsync();

            DateTime currentDate = DateTime.Now.Date;

            var validValuesForGraph = await _context.Values
                .Include(v => v.Station)
                .Where(value => value.Timestamp.Date == currentDate)
                .ToListAsync();

            StationDailyAverages = validValuesForGraph
                .GroupBy(v => new { v.Station.Name, v.Timestamp.Date })
                .Select(g => new StationDailyAverage
                {
                    StationName = g.Key.Name,
                    Date = g.Key.Date,
                    AverageLevel = g.Average(v => v.Level)
                })
                .OrderBy(avg => avg.StationName).ThenBy(avg => avg.Date)
                .ToList();
        }

        public class StationDailyAverage
        {
            public string StationName { get; set; }
            public DateTime Date { get; set; }
            public double AverageLevel { get; set; }
        }
    }
}