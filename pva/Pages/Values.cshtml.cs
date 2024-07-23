using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pva.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pva.Pages
{
    public class ValuesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ValuesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Value> Values { get; set; }
        public IList<Station> Stations { get; set; }
        public IList<StationDailyAverage> StationDailyAverages { get; set; }
        public DateTime? FilterDate { get; set; }
        public int? FilterStationId { get; set; }

        public async Task OnGetAsync(DateTime? filterDate, int? filterStationId)
        {
            // Pøiøazení filtrù
            FilterDate = filterDate;
            FilterStationId = filterStationId;

            // Naèítání všech hodnot z databáze pro tabulku
            var query = _context.Values
                .Include(v => v.Station)
                .AsQueryable();

            // Aplikace filtrù
            if (FilterDate.HasValue)
            {
                query = query.Where(value => value.Timestamp.Date == FilterDate.Value.Date);
            }

            if (FilterStationId.HasValue)
            {
                query = query.Where(value => value.StationId == FilterStationId.Value);
            }

            // Naètení výsledkù pro tabulku
            Values = await query
                .OrderByDescending(v => v.Timestamp)
                .ToListAsync();

            // Naèítání seznamu stanic
            Stations = await _context.Stations.ToListAsync();

            // Naèítání dat pro graf pro aktuální den
            DateTime targetDate = FilterDate ?? DateTime.Now.Date;

            var validValuesForGraph = await _context.Values
                .Include(v => v.Station)
                .Where(value => value.Timestamp.Date == targetDate)
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
    }

    public class StationDailyAverage
    {
        public string StationName { get; set; }
        public DateTime Date { get; set; }
        public double AverageLevel { get; set; }
    }
}
