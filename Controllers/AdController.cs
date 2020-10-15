using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMotorsTest.Helpers;
using WebMotorsTest.Models;
using WebMotorsTest.Services.Interfaces;

namespace WebMotorsTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdController : ControllerBase
    {
        private readonly IAdService _adService;

        public AdController(IAdService adService)
        {
            _adService = adService;
        }

        /// <summary>
        /// Responsible to get a list of all registered Ads
        /// </summary>
        /// <returns></returns>
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            try
            {
                return HttpResponse.Send(true, 200, await _adService.List());
            }
            catch (AppException ex)
            {
                return HttpResponse.Send(false, ex.Code, null, ex.Message);
            }
        }

        /// <summary>
        /// Responsible to create an Ad
        /// </summary>
        /// <param name="ad"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(AdModel ad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return HttpResponse.Send(true, 201, await _adService.Create(ad), null);
                }

                return HttpResponse.Send(true, 400, ModelState, null);
            }
            catch (AppException ex)
            {
                return HttpResponse.Send(false, ex.Code, null, ex.Message);
            }
        }

        /// <summary>
        /// Responsible to update an Ad
        /// </summary>
        /// <param name="ad"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update(AdModel ad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return HttpResponse.Send(true, 200, await _adService.Update(ad));
                }

                return HttpResponse.Send(true, 400, ModelState, null);
            }
            catch (AppException ex)
            {
                return HttpResponse.Send(false, ex.Code, null, ex.Message);
            }
        }

        /// <summary>
        /// Responsible to Delete a Ad informad by an Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return HttpResponse.Send(true, 200, await _adService.Delete(id));

            }
            catch (AppException ex)
            {
                return HttpResponse.Send(false, ex.Code, null, ex.Message);
            }
        }

        /// <summary>
        /// Responsible to Get in webmotors's external service, a List of Make
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMake")]
        public async Task<IActionResult> GetMake()
        {
            try
            {
                return HttpResponse.Send(true, 200, await _adService.GetMake());
            }
            catch (AppException ex)
            {
                return HttpResponse.Send(false, ex.Code, null, ex.Message);
            }
        }

        /// <summary>
        /// Responsible to Get in webmotors's external service, a List of Car Models, using a Make Id as a search parameter
        /// </summary>
        /// <param name="makeId"></param>
        /// <returns></returns>
        [HttpGet("GetModel")]
        public async Task<IActionResult> GetModel(int makeId)
        {
            try
            {
                return HttpResponse.Send(true, 200, await _adService.GetModel(makeId));
            }
            catch (AppException ex)
            {
                return HttpResponse.Send(false, ex.Code, null, ex.Message);
            }
        }

        /// <summary>
        /// Responsible to Get in webmotors's external service, a List of Car Versions, using a Model Id as a search parameter
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        [HttpGet("GetVersion")]
        public async Task<IActionResult> GetVersion(int modelId)
        {
            try
            {
                return HttpResponse.Send(true, 200, await _adService.GetVersion(modelId));
            }
            catch (AppException ex)
            {
                return HttpResponse.Send(false, ex.Code, null, ex.Message);
            }
        }

        /// <summary>
        /// Responsible to Get in webmotors's external service, a paginated List of Vehicles.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("GetVehicles")]
        public async Task<IActionResult> GetVehicles(int page)
        {
            try
            {
                return HttpResponse.Send(true, 200, await _adService.GetVehicles(page));
            }
            catch (AppException ex)
            {
                return HttpResponse.Send(false, ex.Code, null, ex.Message);
            }
        }

    }
}