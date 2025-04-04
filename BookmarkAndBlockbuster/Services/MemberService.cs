using BookmarkAndBlockbuster.Models;
using BookmarkAndBlockbuster.Data;
using BookmarkAndBlockbuster.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookmarkAndBlockbuster.Services
{
    public class MemberService : IMemberService
    {
        private readonly ApplicationDbContext _context;

        public MemberService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetMembers()
        {
            List<Member> members = await _context.Members.ToListAsync();
            return members;
        }

        public async Task<Member?> FindMember(int id)
        {
            Member? Member = await _context.Members.FirstOrDefaultAsync(m => m.MemberId == id);

            if(Member == null)
            {
                return null;
            }
            return Member;
        }

        public async Task AddMember(Member member)
        {
            _context.Members.Add(member);

            await _context.SaveChangesAsync();
        }

        public async Task<string> UpdateMember(int id, Member member)
        {
            if(id != member.MemberId)
            {
                return "Bad Request";
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
                {
                    return "Not Found";
                }
                else
                {
                    throw;
                }
            }

            return "No Content";
        }

        public async Task<string> DeleteMember(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return "Not Found";
            }

            _context.Members.Remove(member);

            await _context.SaveChangesAsync();

            return "No Content";
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }
    }
}



