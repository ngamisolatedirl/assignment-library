using LibraryData.Models;
using LibraryData.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace LibraryData.Service.Entity
{
    public class MemberService : IMemberService
    {
        private readonly LibraryDataContext _context;
        public MemberService(LibraryDataContext context)
        {
            _context = context;
        }

       
        public async Task AddMember(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMember(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditMember(Member member)
        {

            _context.Entry(member).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Member>> GetAllMembers()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member> GetMemberById(int id)
        {
            return await _context.Members.FindAsync(id);
        }

        
    }
}
