
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
        public IActionResult IndexMember()
        {
            var session = HttpContext.Session.GetString("MemberID");
            if (string.IsNullOrWhiteSpace(session))
            {
                TempData["Message"] = "Please Login!";
                return RedirectToAction("Login");
            }
            ViewBag.SessionID = session;
            return View();
        }
        public IActionResult IndexAdmin()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateMembers()
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
        public async Task<IActionResult> CreateMembers([Bind("MemberID,Phone,MemberName,Gender,Email,JoinDate,Password")] Member members)
        {
            if (ModelState.IsValid)
            {
                //將實體加入DbSet
                _context.Member.Add(members);
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
        //登入
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string mid, string Psd)
        {
            if (string.IsNullOrEmpty(mid) && string.IsNullOrEmpty(Psd))
            {
                TempData["Message"] = "Please enter account and password!";
                return RedirectToAction("Login", "DBGymSystem");
            }

            var users = await (from p in _context.Member
                               where p.MemberID == Convert.ToInt32(mid) && p.Password == Psd
                               select p).ToListAsync();

            if (users.Count() != 0)
            {              
                HttpContext.Session.SetString("MemberID", mid);
                HttpContext.Session.SetString("Role", users[0].Role);
                TempData["Message"] = "Logged in!";

                if(users[0].Role == "Admin")
                {
                    return RedirectToAction("IndexAdmin", "DBGymSystem");
                }
                else
                {
                    return RedirectToAction("IndexMember", "DBGymSystem");
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
            var coaches = await _context.Coache.ToListAsync();
            return View(coaches);
        }
        //課程列表
        public async Task<IActionResult> CoursesList()
        {
            var courses = await _context.Course
                .Include(c=>c.Coach)
                .ToListAsync();
            return View(courses);
        }
        //會員列表
        public async Task<IActionResult> MembersList()
        {
            var members = await _context.Member.ToListAsync();
            return View(members);
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
        public async Task<IActionResult> CreateCoaches([Bind("CoachID,CoachName,Gender,Phone,Speciality")] Coach coaches)
        {
            if (ModelState.IsValid)
            {
                _context.Coache.Add(coaches);
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
            var coachlist = await _context.Coache.ToListAsync();
            ViewBag.CoachID = new SelectList(coachlist, "CoachID", "CoachName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourses([Bind("CourseID,CourseName,Description,MaxCapacity,CourseDate,CoachID")] Course courses)
        {
            if (ModelState.IsValid)
            {
                _context.Course.Add(courses);
                await _context.SaveChangesAsync();
                TempData["CreateSuccessMessage"] = "新增成功!";
                return RedirectToAction(nameof(Index));
            }
            var coachlist = await _context.Coache.ToListAsync();
            ViewBag.CoachID = new SelectList(coachlist, "CoachID", "CoachName");
            return View(courses);
        }

        //Reserve 課程
        public async Task<IActionResult> Reserve()
        {
          
            var sessionId = HttpContext.Session.GetString("MemberID");
            if (string.IsNullOrEmpty(sessionId))
            {
                TempData["Message"] = "Please Login!";
                return RedirectToAction("Login");
            }
            var courses = await _context.Course.ToListAsync();
            int mId = int.Parse(sessionId);
            ViewBag.MemberID = mId;

            return View(courses);
        }
        //執行選課
        [HttpPost]
        public async Task<IActionResult> ReserveCourse(int courseId)
        {
            var sessionId = HttpContext.Session.GetString("MemberID");
            if (string.IsNullOrEmpty(sessionId))
            {
                TempData["Message"] = "Please Login!";
                return RedirectToAction("Login");
            }
            int memberId = int.Parse(sessionId);

            var course = await _context.Course
            .Include(c => c.Reserve)
            .ThenInclude(r => r.Member)
            .FirstOrDefaultAsync(c => c.CourseID == courseId);


            bool alreadyReserved = course.Reserve.Any(e => e.MemberID == memberId && e.CourseID == courseId);
            int currentCapacity = course.Reserve.Count();



            if (alreadyReserved)
            {
                TempData["Enrollmsg"] = $"{course.CourseName} 已經選過了！";
            }
            else if (currentCapacity >= course.MaxCapacity)
            {
                TempData["Enrollmsg"] = $"{course.CourseName} 人數已滿，選課失敗！";
            }
            else
            {
                var reserve = new Reserve
                {
                    MemberID = memberId,
                    CourseID = courseId
                };
                _context.Reserve.Add(reserve);
                await _context.SaveChangesAsync();
                TempData["Enrollmsg"] = $"{course.CourseName}選課成功!";
            }

            return RedirectToAction("Reserve");
            //return RedirectToAction("Details","Student",new {id = studentId});
        }
        //退選課程
        public async Task<IActionResult> DropCourse(int MemberID, int CourseID)
        {
            var Reserve = await _context.Reserve.FirstOrDefaultAsync(e => e.MemberID == MemberID && e.CourseID == CourseID);
            var course = await _context.Course.FindAsync(CourseID);
            if (Reserve != null)
            {
                _context.Reserve.Remove(Reserve);
                await _context.SaveChangesAsync();
                TempData["DropSuccessmsg"] = $"{course.CourseName} 退選成功!";
            }
            return RedirectToAction("SelectedCourse", new { id = MemberID });
        }


        //已選課程
        [HttpGet]
        public async Task<IActionResult> SelectedCourse(int? id)
        {
            if (id == null || _context.Member == null)
            {
                var msgObject = new
                {
                    statuscode = StatusCodes.Status400BadRequest,
                    error = "無效的請求，必須提供Id編號!"
                };
                return new BadRequestObjectResult(msgObject);
            }


            var member = await _context.Member
                .Include(s => s.Reserve)
                .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(m => m.MemberID == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }


        //Edit Courses
        [HttpGet]
        public async Task<IActionResult> Edit_Courses(int? id)
        {
            var coachList = await _context.Coache.ToListAsync();
            ViewBag.CoachID = new SelectList(coachList, "CoachID", "CoachName");

            if (id == null || _context.Course == null)
            {
                return NotFound();
            }
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Courses(int id, [Bind("CourseID,CourseName,Description,MaxCapacity,CourseDate,CoachID")] Course courses)
        {
            var coachList = await _context.Coache.ToListAsync();
            ViewBag.CoachID = new SelectList(coachList, "CoachID", "CoachName");

            if (id != courses.CourseID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Course.Update(courses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(courses.CourseID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexAdmin));
            }
            return View(courses);
        }
        private bool CourseExists(int id)
        {
            return _context.Course.Any(m => m.CourseID == id);
        }

        //Edit Coaches
        [HttpGet]
        public async Task<IActionResult> Edit_Coaches(int? id)
        {
            if (id == null || _context.Coache == null)
            {
                return NotFound();
            }
            var coache = await _context.Coache.FindAsync(id);
            if (coache == null)
            {
                return NotFound();
            }
            return View(coache);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Coaches(int id, [Bind("CoachID,CoachName,Phone,Speciality,Gender")] Coach coaches)
        {
            if (id != coaches.CoachID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Coache.Update(coaches);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachExists(coaches.CoachID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexAdmin));
            }
            return View(coaches);
        }
        private bool CoachExists(int id)
        {
            return _context.Coache.Any(m => m.CoachID == id);
        }


        //Edit Members
        [HttpGet]
        public async Task<IActionResult> Edit_Members(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }
            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Members(int id, [Bind("MemberID,Phone,MemberName,Gender,Email,JoinDate,Role,Password")] Member members)
        {
            if (id != members.MemberID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Member.Update(members);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(members.MemberID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexAdmin));
            }
            return View(members);
        }
        private bool MemberExists(int id)
        {
            return _context.Member.Any(m => m.MemberID == id);
        }
    }
}
