
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using MVC_DBGym.Data;
using MVC_DBGym.Models;

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

        public IActionResult Index()
        {
            return View();
        }

       
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.GenderList = new List<SelectListItem>
            {
                new SelectListItem {Text = "男", Value= "男" },
                new SelectListItem {Text = "女", Value= "女" }
            };
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Phone,MemberName,Gender,Email,JoinDate,Password")] DBMembers members)
        {
            if (ModelState.IsValid)
            {
                //將實體加入DbSet
                _context.Members.Add(members);
                //將資料異動儲存到資料庫
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "註冊成功！";
                //導向至Index
                return RedirectToAction("Index","DBGymsystem");
               
            }
            ViewBag.GenderList = new List<SelectListItem>
            {
                new SelectListItem {Text = "男", Value= "男" },
                new SelectListItem {Text = "女", Value= "女" }
            };
            return View(members);
        }
       
        [HttpGet]
        public IActionResult Login()
        {
            //New Add session
            if (HttpContext.Session.GetString("Name") == null)
            {
                TempData["Message"] = "Please Login!";
                //return RedirectToAction("Login", "DBGymSystem");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Name, string Psd)
        {
            if (Name == null && Psd == null)
            {
                TempData["Message"] = "Please enter account and password!";
                return RedirectToAction("Login", "DBGymSystem");
            }

            var users = await _context.Members
            .FirstOrDefaultAsync(p => p.MemberName == Name && p.Password == Psd);
                              
            if (users != null)
            {              
                HttpContext.Session.SetString("MemberName", users.MemberName);
                HttpContext.Session.SetString("Role", users.Role);
                TempData["Meessage"] = "Logged in!";

                if(users.Role == "Admin")
                {
                    return RedirectToAction("Index", "DBGymSystem");
                }
                else
                {
                    return RedirectToAction("Index", "DBGymSystem");
                }
               
            }
            else
            {
                TempData["Message"] = "Login failed!";
                return RedirectToAction("Login", "DBGymSystem");
            }
        }
        //教練列表
        public async Task<IActionResult> CoachesList()
        {
            var coaches = await _context.Coaches.ToListAsync();
            return View(coaches);
        }
        //課程列表
        public async Task<IActionResult> CoursesList()
        {
            var courses = await _context.Courses
                .Include(c=>c.Coach)
                .ToListAsync();
            return View(courses);
        }
        //Create教練
        [HttpGet]
        public IActionResult CreateCoaches()
        {
            ViewBag.GenderList = new List<SelectListItem>
            {
                new SelectListItem {Text = "男", Value= "男" },
                new SelectListItem {Text = "女", Value= "女" }
            };
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCoaches([Bind("CoachID,CoachName,Gender,Phone,Speciality")] DBCoaches coaches)
        {
            if (ModelState.IsValid)
            {
                _context.Coaches.Add(coaches);
                await _context.SaveChangesAsync();
                TempData["CreateSuccessMessage"] = "新增成功!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.GenderList = new List<SelectListItem>
                {
                new SelectListItem {Text = "男", Value= "男" },
                new SelectListItem {Text = "女", Value= "女" }
            };
            return View(coaches);
        }
        //Create課程
        [HttpGet]
        public async Task<IActionResult> CreateCourses()
        {
            var coachlist = await _context.Coaches.ToListAsync();
            ViewBag.CoachID = new SelectList(coachlist, "CoachID", "CoachName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourses([Bind("CourseID,CourseName,Description,MaxCapacity,CourseDate,CoachID")] DBCourses courses)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(courses);
                await _context.SaveChangesAsync();
                TempData["CreateSuccessMessage"] = "新增成功!";
                return RedirectToAction(nameof(Index));
            }
            var coachlist = await _context.Coaches.ToListAsync();
            ViewBag.CoachID = new SelectList(coachlist, "CoachID", "CoachName");
            return View(courses);
        }
    }
}
