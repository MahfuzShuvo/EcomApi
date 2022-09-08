using EcomApi.Common.Enums;
using EcomApi.Common.ModelVM;
using EcomApi.Common.OperationDTO;
using EcomApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private AppDataContext _dbContext;

        public CategoryService(IServiceScopeFactory serviceScopeFactory, AppDataContext appDataContext)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _dbContext = appDataContext;
        }
        public async Task<ResponseMessage> GetAllCategory()
        {
            ResponseMessage responseMessage = new();
            
            try
            {
                int totalCategory = await _dbContext.Category.CountAsync();

                List<Category> category = await _dbContext.Category
                    .OrderBy(o => o.ParentId)
                    .ToListAsync();
                responseMessage.ResponseObject = new
                {
                    rows = totalCategory,
                    result = category,
                };
                responseMessage.ResponseCode = (int)AppEnums.ResponseCode.Success;
            }
            catch (Exception)
            {
                responseMessage.ResponseCode = (int)System.Net.HttpStatusCode.BadRequest;
            }
            return responseMessage;
        }
        public async Task<ResponseMessage> SaveCategory(CategoryVM categoryVM)
        {
            ResponseMessage responseMessage = new();
           
            try
            {
                Category category = new()
                {
                    CategoryId = categoryVM.CategoryId,
                    Name = categoryVM.Name,
                    ParentId = categoryVM.ParentId,
                    Status = categoryVM.Status
                };

                if(category != null)
                {
                    if(category.Name != null && category.Name != "")
                    {
                        if (await IsExistCategory(category))
                        {
                            responseMessage.ResponseObject = category;
                            responseMessage.Message = "Already exist this category";
                            responseMessage.ResponseCode = (int)AppEnums.ResponseCode.Warning;
                            return responseMessage;
                        }
                        else if (category.CategoryId > 0)
                        {
                            _dbContext.Category.Update(category);
                            await _dbContext.SaveChangesAsync();

                            responseMessage.ResponseObject = category;
                            responseMessage.Message = "Category updated successfully";
                            responseMessage.ResponseCode = (int)AppEnums.ResponseCode.Success;
                        }
                        else
                        {
                            Category parentExist = _dbContext.Category.Where(x => x.CategoryId == category.ParentId).FirstOrDefault();
                            parentExist.HasChild = true;
                            _dbContext.Category.Update(parentExist);
                            _dbContext.Category.Add(category);
                            await _dbContext.SaveChangesAsync();

                            responseMessage.ResponseObject = category;
                            responseMessage.Message = "Category added successfully";
                            responseMessage.ResponseCode = (int)AppEnums.ResponseCode.Success;
                        }
                    } 
                    else
                    {
                        responseMessage.ResponseObject = null;
                        responseMessage.Message = "Please enter category name";
                        responseMessage.ResponseCode = (int)AppEnums.ResponseCode.Warning;
                    }
                }
            }
            catch (Exception)
            {
                responseMessage.ResponseObject = null;
                responseMessage.Message = "Failed to save category";
                responseMessage.ResponseCode = (int)System.Net.HttpStatusCode.BadRequest;
            }
            return responseMessage;
        }
        public async Task<ResponseMessage> DeleteCategory(int id)
        {
            ResponseMessage responseMessage = new();
            
            Category category = await _dbContext.Category.FindAsync(id);

            if (category != null)
            {
                _dbContext.Category.Remove(category);
                await _dbContext.SaveChangesAsync();

                responseMessage.Message = "Category deleted successfully";
                responseMessage.ResponseCode = (int)AppEnums.ResponseCode.Success;
            } 
            else
            {
                responseMessage.ResponseObject = null;
                responseMessage.Message = "Category not found";
                responseMessage.ResponseCode = (int)AppEnums.ResponseCode.Failed;
            }

            return responseMessage;
        }

        public async Task<bool> IsExistCategory(Category category)
        {
            return await _dbContext.Category.AnyAsync(x => x.Name == category.Name);
        }
    }

    public interface ICategoryService
    {
        Task<ResponseMessage> GetAllCategory();
        Task<ResponseMessage> SaveCategory(CategoryVM categoryVM);
        Task<ResponseMessage> DeleteCategory(int id);
    }
}
