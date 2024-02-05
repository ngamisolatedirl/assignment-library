using LibraryData.Models;
using LibraryData.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace LibraryData.Service.Entity
{
    public class CartService : ICartService
    {
        private readonly LibraryDataContext _context;
        public CartService(LibraryDataContext context)
        {
            _context = context;
        }
        public async Task AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditCart(Cart cart)
        {
            _context.Entry(cart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cart>> GetAllCarts()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart> GetCartById(int id)
        {
            return await _context.Carts.FindAsync(id);
        }
    }
}
