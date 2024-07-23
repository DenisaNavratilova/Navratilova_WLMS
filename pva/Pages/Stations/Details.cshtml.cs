using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pva.Models;

namespace pva.Pages.Stations
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Station Station { get; set; } = default!;
        public IList<StationHistoricalData> HistoricalData { get; set; } = new List<StationHistoricalData>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stations == null)
            {
                return NotFound();
            }

            Station = await _context.Stations.FirstOrDefaultAsync(m => m.StationId == id);
            if (Station == null)
            {
                return NotFound();
            }

            // Načítání všech hodnot pro danou stanici
            HistoricalData = await _context.Values
                .Where(v => v.StationId == id)
                .GroupBy(v => v.Timestamp.Date) // Skupiny podle data
                .Select(g => new StationHistoricalData
                {
                    Date = g.Key,
                    AverageLevel = g.Average(v => v.Level)
                })
                .OrderBy(d => d.Date)
                .ToListAsync();

            return Page();
        }
    }

    public class StationHistoricalData
    {
        public DateTime Date { get; set; }
        public double AverageLevel { get; set; }
    }
}
