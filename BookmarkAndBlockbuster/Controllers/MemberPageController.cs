using Microsoft.AspNetCore.Mvc;
using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Interfaces;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BookmarkAndBlockbuster.Models.ViewModels;

namespace BookmarkAndBlockbuster.Controllers
{
   
    public class MemberPageController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IBooksLogService _booksLogService;
        private readonly IMoviesLogService _moviesLogService;
        public MemberPageController(IMemberService MemberService, IBooksLogService BooksLogService, IMoviesLogService MoviesLogService)
        {
            _memberService = MemberService;
            _booksLogService = BooksLogService;
            _moviesLogService = MoviesLogService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET -> MemberPage/List
        public async Task<IActionResult> List()
        {
            IEnumerable<Member> Members = await _memberService.GetMembers();

            return View(Members);
        }

        // GET -> MemberPage/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            Member Member = await _memberService.FindMember(id);
            IEnumerable<BooksLogDto> BooksLog  = await _booksLogService.GetBooksLogForMember(id);
            IEnumerable<MoviesLogDto> MoviesLog = await _moviesLogService.GetMoviesLogForMember(id);

            MemberDetails MemberInfo = new MemberDetails()
            {
                Member = Member,
                MemberBooks = BooksLog,
                MemberMovies = MoviesLog
            };

            return View(MemberInfo);
        }

        // GET -> MemberPage/New
        public IActionResult New()
        {
            return View();

        }

        // POST -> MemberPage/Add
        [HttpPost]
        public async Task<IActionResult> Add(Member Member)
        {
            await _memberService.AddMember(Member);

            return RedirectToAction("List", "MemberPage");
        }

        // GET -> MemberPage/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Member Member = await _memberService.FindMember(id);

            return View(Member);
        }

        // POST -> MemberPage/Update/{id}

        [HttpPost]
        public async Task<IActionResult> Update(int id, Member member)
        {
            await _memberService.UpdateMember(id, member);
            return RedirectToAction("Details", "MemberPage", new {id = id});
        }

        // Get -> MemberPage/ConfirmDelete/{id}
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            Member member = await _memberService.FindMember(id);
            return View(member);
        }

        // Post -> MemberPage/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _memberService.DeleteMember(id);
            return RedirectToAction("List", "MemberPage");
        }
    }
}
