using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersInfo.Data
{
    public interface IDataHelper<Table> 
    {
        Task<List<Table>> GetAllAsync();
        Task<List<Table>> SearchAsync(string Item);
        Task<Table> FindAsync(int Id);


        Task AddAsync(Table table);
        Task EditAsync(Table table);
        Task RemoveAsync(int id);

    }
}
