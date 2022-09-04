using EcomApi.Common.ModelVM;
using EcomApi.Common.OperationDTO;
using EcomApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        // GET: api/Brand/GetAllBrand
        [HttpGet("getAllBrand")]
        public async Task<ResponseMessage> GetAllBrand()
        {
            return await _brandService.GetAllBrand();
        }
        // POST: api/Brand/save
        [HttpPost("save")]
        public async Task<ResponseMessage> SaveBrand(BrandVM brandVM)
        {
            return await _brandService.SaveBrand(brandVM);
        }
        // POST: api/Brand/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<ResponseMessage> DeleteBrand(int id)
        {
            return await _brandService.DeleteBrand(id);
        }
    }
}
