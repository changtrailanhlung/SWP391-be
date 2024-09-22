using Bos;
using Bos.Model;
using Microsoft.EntityFrameworkCore;
using Repository.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepo 
    {
        //private readonly PetContextDB _context;

        //private static UserRepo _instance = null;

        //public UserRepo(PetContextDB ctx)
        //{
        //    _context = ctx;
        //}

        //public async Task<IEnumerable<User>> GetAll()
        //{
        //    return await _context.Users.ToListAsync();
        //}

        //public async Task<User> GetUserByID(int id)
        //{

        //    return await _context.Users.FirstOrDefaultAsync(x => x.AccountID == id);
        //}
        //public async Task<User> AddUser(User user)
        //{
        //    try
        //    {
        //        var newUser = await GetUserByID(user.AccountID);
        //        if (newUser != null)
        //        {
        //            throw new Exception("User arealdy exists");
        //        }
        //        else
        //        {
        //            _context.Users.Add(user);
        //            await _context.SaveChangesAsync();
        //            return user;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task UpdateUser(User user)
        //{
        //    try
        //    {
        //        var newUser = await GetUserByID(user.AccountID);
        //        if (newUser == null)
        //        {
        //            throw new Exception("User does not exist");
        //        }
        //        else
        //        {
        //            newUser.Name = user.Name;
        //            newUser.Email = user.Email;
        //            newUser.Password = user.Password;
        //            newUser.PhoneNumber = user.PhoneNumber;
        //            newUser.Token = user.Token;
        //            _context.Users.Update(newUser);
        //            await _context.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public async Task DeleteUser(int id)
        //{
        //    try
        //    {
        //        var user = await GetUserByID(id);
        //        if (user == null)
        //        {
        //            throw new Exception("Account does not exist!");
        //        }
        //        else
        //        {
        //            _context.Users.Remove(user);
        //            await _context.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<User> Login(string username, string password)
        //{
        //    try
        //    {
        //        var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == username && u.Password == password);
        //        if (user == null)
        //        {
        //            throw new Exception("Account does not exist!");
        //        }
        //        else
        //        {
        //            return user;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
