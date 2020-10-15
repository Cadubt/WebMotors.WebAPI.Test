using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMotorsTest.Context;
using WebMotorsTest.Models;
using WebMotorsTest.Repositories.Interfaces;

namespace WebMotorsTest.Repositories
{
    public class AdRepository : RepositoryBase, IAdRepository
    {
        public AdRepository(WebMotorsDataContext db) : base(db) { }

        public async Task<AdModel> Add(AdModel ad)
        {
            await db.Ad.AddAsync(ad);
            return ad;
        }

        public async Task<List<AdModel>> List()
        {
            return await db.Ad
                .AsNoTracking()
                .ToListAsync<AdModel>();
        }

        public async Task<AdModel> FindAsync(int id)
        {
            return await db.Ad
                .Where(q => q.Id == id)
                .DefaultIfEmpty()
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public void Update(AdModel ad)
        {
            db.Update(ad);
            db.Entry(ad).State = EntityState.Modified;
        }

        public async Task<AdModel> FindById(int id)
        {
            return await db.Ad
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
