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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Station> Station { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Stations != null)
            {
                Station = await _context.Stations.ToListAsync();
            }
        }
    }
}
