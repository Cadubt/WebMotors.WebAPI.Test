using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMotorsTest.Models;

namespace WebMotorsTest.Repositories.Interfaces
{
    public interface IAdRepository
    {
        Task<List<AdModel>> List();
        Task<AdModel> Add(AdModel ad);
        Task<int> Commit();
        Task<AdModel> FindAsync(int id);

        void Update(AdModel ad);

        Task<AdModel> FindById(int id);

        Entity SoftDelete<Entity>(Entity entity) where Entity : class;
    }
}
