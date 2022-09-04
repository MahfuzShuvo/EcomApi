using EcomApi.Common.Enums;
using EcomApi.Common.ModelVM;
using EcomApi.Common.OperationDTO;
using EcomApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomApi.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDataContext _dbContext;
        public BrandService(AppDataContext appDataContext)
        {
            _dbContext = appDataContext;
        }
        public async Task<ResponseMessage> GetAllBrand()
        {
            ResponseMessage responseMessage = new();
            try
            {
                int totalBrand = await _dbContext.Brand.CountAsync();

                List<Brand> brand = await _dbContext.Brand
                    .OrderByDescending(o => o.BrandId)
                    .ToListAsync();
                responseMessage.ResponseObject = new
                {
                    rows = totalBrand,
                    result = brand,
                };
                responseMessage.ResponseCode = (int)AppEnums.ResponseCode.Success;
            }
            catch(Exception)
            {
                responseMessage.ResponseCode = (int)System.Net.HttpStatusCode.BadRequest;
            }
            return responseMessage;
        }
        public async Task<ResponseMessage> SaveBrand(BrandVM brandVM)
        {
            ResponseMessage responseMessage = new();
            try
            {
                Brand brand = new()
                {
                    BrandId = brandVM.BrandId,
                    Name = brandVM.Name,
                    Description = brandVM.Description,
                    Logo = brandVM.Logo,
                    Status = brandVM.Status
                };
                if(brand != null)
                {
                    if(brand.Name != "" && brand.Name != null)
                    {
                        if (await IsExistBrand(brand))
                        {
                            responseMessage.ResponseObject = brand;
                            responseMessage.Message = "Already exist this brand";
                            responseMessage.ResponseCode = (int)AppEnums.ResponseCode.Warning;
                            return responseMessage;
                        }
                        else if (brand.BrandId > 0)
                        {
                            _dbContext.Brand.Update(brand);
                            await _dbContext.SaveChangesAsync();

                            responseMessage.ResponseObject = brand;
                            responseMessage.Message = "Brand updated successfully";
                            responseMessage.ResponseCode = (int)AppEnums.ResponseCode.Success;
                        }
                        else
                        {
                            if(brand.Logo == null)
                            {
                                brand.Logo = "demoLogo.png";
                            }
                            _dbContext.Brand.Add(brand);
                            await _dbContext.SaveChangesAsync();

                            responseMessage.ResponseObject = brand;
                            responseMessage.Message = "Brand added successfully";
                            responseMessage.ResponseCode = (int)AppEnums.ResponseCode.Success;
                        }
                    }
                    else
                    {
                        responseMessage.ResponseObject = null;
                        responseMessage.Message = "Please enter brand name";
                        responseMessage.ResponseCode = (int)AppEnums.ResponseCode.Warning;
                    }
                }
            }
            catch(Exception)
            {
                responseMessage.ResponseObject = null;
                responseMessage.Message = "Failed to save brand";
                responseMessage.ResponseCode = (int)System.Net.HttpStatusCode.BadRequest;
            }
            return responseMessage;
        }
        public async Task<ResponseMessage> DeleteBrand(int id)
        {
            ResponseMessage responseMessage = new();

            Brand brand = await _dbContext.Brand.FindAsync(id);

            if (brand != null)
            {
                _dbContext.Brand.Remove(brand);
                await _dbContext.SaveChangesAsync();

                responseMessage.Message = "Brand deleted successfully";
                responseMessage.ResponseCode = (int)AppEnums.ResponseCode.Success;
            }
            else
            {
                responseMessage.ResponseObject = null;
                responseMessage.Message = "Brand not found";
                responseMessage.ResponseCode = (int)AppEnums.ResponseCode.Failed;
            }

            return responseMessage;
        }
        public async Task<bool> IsExistBrand(Brand brand)
        {
            return await _dbContext.Brand.AnyAsync(x => x.Name == brand.Name);
        }
    }

    public interface IBrandService
    {
        Task<ResponseMessage> GetAllBrand();
        Task<ResponseMessage> SaveBrand(BrandVM brandVM);
        Task<ResponseMessage> DeleteBrand(int id);
    }
}
