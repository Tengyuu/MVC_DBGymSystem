using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_DBGym.Data;
using System.Threading.Tasks;

namespace MVC_DBGym.Controllers
{
    public class IndexController : Controller
    {
        private readonly CmsContext _context;

        public IndexController(CmsContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> 課程介紹()
        {
            var course = await _context.Course
                .Include(c=>c.Coach)
                .ToListAsync();
            return View(course);
        }
        public async Task<IActionResult> 教練介紹()
        {
            var coach = await _context.Coach.ToListAsync();
            return View(coach);
        }
    }
}
