using BookmarkAndBlockbuster.Models;

namespace BookmarkAndBlockbuster.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetMembers();

        Task<Member> FindMember(int id);

        Task AddMember(Member member);

        Task<string> UpdateMember(int id, Member member);

        Task<string> DeleteMember(int id);
    }
}
