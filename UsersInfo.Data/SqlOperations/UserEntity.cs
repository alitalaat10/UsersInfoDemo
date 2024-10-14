using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersInfo.Core;

namespace UsersInfo.Data.SqlOperations
{
    public class UserEntity : IDataHelper<User>
    {
        private AppDBContext db;

        public UserEntity() 
        { 
            db = new AppDBContext(); 
        }
        public async Task AddAsync(User table)
        {
            await db.Users.AddAsync(table);
            await db.SaveChangesAsync();
        }

        public async Task EditAsync(User table)
        {   db = new AppDBContext();
            db.Users.Update(table); 
            await db.SaveChangesAsync();    
            
        }

        public async Task<User> FindAsync(int Id)
        {
            return await db.Users.FindAsync(Id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await db.Users.ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var item = await FindAsync(id);
            db.Users.Remove(item);
            await db.SaveChangesAsync();    
        }

        public async Task<List<User>> SearchAsync(string Item)
        {
            return await db.Users.Where(x => x.Id.ToString().Contains(Item)
            || x.Name.Contains(Item)
            || x.Email.Contains(Item)
            || x.PhoneNumber.Contains(Item)).ToListAsync();
        }

    }
}
