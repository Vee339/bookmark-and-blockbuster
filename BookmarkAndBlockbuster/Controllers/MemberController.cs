using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Interfaces;

namespace BookmarkAndBlockbuster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService MemberService)
        {
            _memberService = MemberService;
        }

        // GET - api/member/list
        [HttpGet(template: "List")]
        public async Task<ActionResult<IEnumerable<Member>>> ListMembers()
        {
            IEnumerable<Member> Members = await _memberService.GetMembers();

            return Ok(Members);
        }

        // GET - api/member/Find/{id}
        [HttpGet(template: "Find/{id}")]
        public async Task<ActionResult<Member>> FindMember(int id)
        {
            var result = await _memberService.FindMember(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST - api/member/add
        [HttpPost(template:"AddMember")]
        public async Task<IActionResult> AddMember(Member member)
        {
            await _memberService.AddMember(member);

            return NoContent();
        }

        // PUT - api/member/update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, Member member)
        {
            var result = await _memberService.UpdateMember(id, member);

            if (result == "Bad Request")
            {
                return BadRequest();
            }

            if (result == "Not Found")
            {
                return NotFound();
            }

            if (result == "No Content")
            {
                return NoContent();
            }

            return NoContent();
        }

        // Delete - api/member/delete/{id}
        [HttpDelete(template:"Delete/{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var result = await _memberService.DeleteMember(id);

            if(result == "Not Found")
            {
                return NotFound();
            }

            return NoContent();
        }
    }  
}
