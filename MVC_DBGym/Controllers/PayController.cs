using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_DBGym.Data;
using MVC_DBGym.Models;
using MVC_DBGym.ViewModels;
using System.Threading.Tasks;

namespace MVC_DBGym.Controllers
{
    public class PayController : Controller
    {
        private readonly CmsContext _context;

        public PayController(CmsContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Payment()
        {
            var p = await _context.Payment
                .Include(m => m.PType)
                .Include(e => e.Member)
                .Include(m=> m.Course)
                .ToListAsync();
            return View(p);
        }
        public IActionResult CreatePayment(int memberId, int courseId)
        {
            var course = _context.Course.Find(courseId);
            if (course == null) return NotFound();

            var model = new PaymentViewModel
            {
                MemberID = memberId,
                CourseID = courseId,
                Amount = course.Price
            };
            ViewBag.PTypeList = _context.PType
            .Select(p => new SelectListItem { Text = p.PTypeName, Value = p.PTypeID.ToString() })
            .ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePayment(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // 偵錯
                //var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                //TempData["Errors"] = string.Join("; ", errors);

                ViewBag.PTypeList = _context.PType
                    .Select(p => new SelectListItem { Text = p.PTypeName, Value = p.PTypeID.ToString() })
                    .ToList();
                return View(model);
            }
            //交易寫入Payment
            var payment = new Payment
            {
                MemberID = model.MemberID,
                Amount = model.Amount,
                PTypeID = model.PTypeID,
                CourseID= model.CourseID,
                PayDate = DateTime.Now
            };

            _context.Payment.Add(payment);

            // 付款成功後更新 Reserve 狀態
            var reserve = await _context.Reserve.FirstOrDefaultAsync(r => r.MemberID == model.MemberID && r.CourseID == model.CourseID);
            if (reserve != null)
            {
                reserve.IsPaid = true;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("MyReservations", "Pay", new {memberid = model.MemberID});
        }

        public async Task<IActionResult> MyReservations(int memberId)
        {
            var member = await _context.Member
                .Include(m => m.Reserve)
                .ThenInclude(r => r.Course)
                .FirstOrDefaultAsync(m => m.MemberID == memberId);

            if (member == null)
                return NotFound();

            return View(member);
        }

    }
}
