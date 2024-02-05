using LibraryData.Models;

namespace LibraryData.Service.IService
{
    public interface ICartService
    {
        Task<List<Cart>> GetAllCarts();
        Task<Cart> GetCartById(int id);
        Task AddCart(Cart cart);
        Task EditCart(Cart cart);
        Task DeleteCart(int id);
    }
}
