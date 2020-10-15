using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebMotorsTest.Helpers;
using WebMotorsTest.Models;
using WebMotorsTest.Repositories.Interfaces;
using WebMotorsTest.Services.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace WebMotorsTest.Services
{
    public class AdService : IAdService
    {
        private IConfiguration _config { get; set; }
        private readonly IAdRepository _adRepo;
        public AdService(IAdRepository adRepo, IConfiguration config)
        {
            _adRepo = adRepo;
            _config = config;
        }

        public async Task<List<AdModel>> List()
        {
            List<AdModel> ad = new List<AdModel>();
            ad = await _adRepo.List();

            return ad;
        }


        public async Task<bool> Create(AdModel ad)
        {
            try
            {
                AdModel _ad = new AdModel()
                {
                    ano = ad.ano,
                    Marca = ad.Marca,
                    Modelo = ad.Modelo,
                    Observacao = ad.Observacao,
                    Quilometragem = ad.Quilometragem,
                    Versao = ad.Versao
                };
                await _adRepo.Add(_ad);

                int changed = await _adRepo.Commit();

                if (changed > 0)
                {
                    return true;
                }

                return false;
            }
            catch (AppException ex)
            {
                throw new AppException(ex.Message, ex.Code);
            }


        }

        public async Task<bool> Update(AdModel ad)
        {
            try
            {
                AdModel entity = await _adRepo.FindAsync(ad.Id);

                entity.ano = ad.ano;
                entity.Marca = ad.Marca;
                entity.Modelo = ad.Modelo;
                entity.Observacao = ad.Observacao;
                entity.Quilometragem = ad.Quilometragem;
                entity.Versao = ad.Versao;

                _adRepo.Update(entity);

                int changed = await _adRepo.Commit();

                if (changed > 0)
                    return true;

                return false;

            }
            catch (AppException ex)
            {
                throw new AppException(ex.Message, ex.Code);
            }
        }

        public async Task<AdModel> FindById(int id)
        {
            return await _adRepo.FindById(id);
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                AdModel entity = await _adRepo.FindAsync(id);
                _adRepo.SoftDelete<AdModel>(entity);

                return await _adRepo.Commit() > 0;
            }
            catch (AppException ex)
            {
                throw new AppException(ex.Message, ex.Code);
            }


        }

        public async Task<List<MakeModel>> GetMake()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.BaseAddress = new System.Uri(_config["ExternalServices:DesafioOnlineURL"]);


                HttpResponseMessage response = client.GetAsync("OnlineChallenge/Make").Result;

                List<MakeModel> lstMake = new List<MakeModel>();
                lstMake = JsonSerializer.Deserialize<List<MakeModel>>(response.Content.ReadAsStringAsync().Result);


                if (response.StatusCode == System.Net.HttpStatusCode.OK )
                    return lstMake;
                else
                    return null;
            }

        }

        public async Task<List<ModelModel>> GetModel(int makeId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.BaseAddress = new System.Uri(_config["ExternalServices:DesafioOnlineURL"]);

                HttpResponseMessage response = client.GetAsync("OnlineChallenge/Model?MakeID=" + makeId.ToString()).Result;

                List<ModelModel> lstModel = new List<ModelModel>();
                lstModel = JsonSerializer.Deserialize<List<ModelModel>>(response.Content.ReadAsStringAsync().Result);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return lstModel;
                else
                    return null;
            }
        }

        public async Task<List<VersionModel>> GetVersion(int modelId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.BaseAddress = new System.Uri(_config["ExternalServices:DesafioOnlineURL"]);

                HttpResponseMessage response = client.GetAsync("OnlineChallenge/Version?ModelID=" + modelId.ToString()).Result;

                List<VersionModel> lstVersion = new List<VersionModel>();
                lstVersion = JsonSerializer.Deserialize<List<VersionModel>>(response.Content.ReadAsStringAsync().Result);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return lstVersion;
                else
                    return null;
            }
        }

        public async Task<List<VehicleModel>> GetVehicles(int page)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                client.BaseAddress = new System.Uri(_config["ExternalServices:DesafioOnlineURL"]);

                HttpResponseMessage response = client.GetAsync("OnlineChallenge/Vehicles?Page=" + page.ToString()).Result;

                List<VehicleModel> lstVehicle = new List<VehicleModel>();
                lstVehicle = JsonSerializer.Deserialize<List<VehicleModel>>(response.Content.ReadAsStringAsync().Result);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return lstVehicle;
                else
                    return null;
            }
        }
    }
}
