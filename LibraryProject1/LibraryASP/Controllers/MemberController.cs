using LibraryData.Models;
using LibraryData.Service.Entity;
using LibraryData.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace LibraryASP.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public async Task<IActionResult> Index()
        {
            var members = await _memberService.GetAllMembers();
            return View(members);
        }
        public async Task<IActionResult> Details(int id)
        {
            var member = await _memberService.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.FulltName))
                {
                    // Nếu tên để trống, thêm lỗi vào ModelState và hiển thị lại form
                    ModelState.AddModelError("Name", "Category name is required.");
                    return View(model);
                }
               
                var member = new Member
                {
                    FulltName = model.FulltName,
                    Email = model.Email,
                    Phone = model.Phone,
                };
                await _memberService.AddMember(member);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var member = await _memberService.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Member model)
        {
            if (id != model.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.FulltName))
                {
                    // Nếu tên để trống, thêm lỗi vào ModelState và hiển thị lại form
                    ModelState.AddModelError("Name", "Category name is required.");
                    return View(model);
                }
               

                await _memberService.EditMember(model);
                return RedirectToAction("Index");


            }
            return View(model);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var member = await _memberService.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _memberService.DeleteMember(id);
            return RedirectToAction("Index");
        }
    }
}
