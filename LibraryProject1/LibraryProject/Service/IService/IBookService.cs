using LibraryData.Models;

namespace LibraryData.Service.IService
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task AddBook(Book book);
        Task EditBook(Book book);
        Task DeleteBook(int id);
        bool Unique(string ten);
    }
}
