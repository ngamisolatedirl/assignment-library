using LibraryData.Models;

namespace LibraryData.Service.IService
{
    public interface IMemberService
    {
        Task<List<Member>> GetAllMembers();
        Task<Member> GetMemberById(int id);
        Task AddMember(Member member);
        Task EditMember(Member member);
        Task DeleteMember(int id);
        
    }
}
