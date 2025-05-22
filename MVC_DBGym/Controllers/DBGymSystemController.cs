using Microsoft.AspNetCore.Mvc;
using MVC_DBGym.Data;

namespace MVC_DBGym.Controllers
{
    public class DBGymSystemController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly CmsContext _context;
        public DBGymSystemController(CmsContext context)
        {
            _context = context;
        }
    }
}
