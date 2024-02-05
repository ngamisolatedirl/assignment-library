using LibraryData.Models;
using LibraryData.Service.Entity;
using LibraryData.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var members = await _memberService.GetAllMembers();
            return Ok(members);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetMember(int id)
        {
            var category = await _memberService.GetMemberById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> PostMember(Member model)
        {
            var member = new Member
            {
                FulltName = model.FulltName,
                Email = model.Email,
                Phone = model.Phone,
            };
            await _memberService.AddMember(member);
            return Ok(member);
        }
        [HttpPut]
        public async Task<IActionResult> EditMember( int id)
        {
            var member = await _memberService.GetMemberById(id);
            if(member == null)
            {
               return BadRequest();
            }
            await _memberService.EditMember(member);
            return Ok(member);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _memberService.GetMemberById(id);
            if( member == null)
            {
                return BadRequest();
            }
            await _memberService.DeleteMember(id);
            return Ok(member);
        }
    }
}
