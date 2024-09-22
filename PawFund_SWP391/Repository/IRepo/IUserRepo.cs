using Bos.Model;

namespace Repository.IRepo
{
    public interface IUserRepo
    {
        Task<User> AddUser(User user);
        Task DeleteUser(int id);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUserByID(int id);
        Task<User> Login(string username, string password);
        Task UpdateUser(User user);
    }
}