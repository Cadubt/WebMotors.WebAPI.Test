using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMotorsTest.Models;

namespace WebMotorsTest.Services.Interfaces
{
    public interface IAdService
    {
        Task<List<AdModel>> List();
        Task<bool> Create(AdModel ad);
        Task<bool> Update(AdModel ad);
        Task<AdModel> FindById(int id);
        Task<bool> Delete(int id);
        Task<List<MakeModel>> GetMake();
        Task<List<ModelModel>> GetModel(int makeId);
        Task<List<VersionModel>> GetVersion(int modelId);
        Task<List<VehicleModel>> GetVehicles(int page);


    }
}
