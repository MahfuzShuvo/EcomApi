using EcomApi.Common.ModelVM;
using EcomApi.Common.OperationDTO;
using EcomApi.Model;
using EcomApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: api/Category/GetAllCategory
        [HttpGet("GetAllCategory")]
        public async Task<ResponseMessage> GetAllCategory()
        {
            return await _categoryService.GetAllCategory();
        }
        // POST: api/Category/save
        [HttpPost("save")]
        public async Task<ResponseMessage> SaveCategory(CategoryVM categoryVM)
        {
            return await _categoryService.SaveCategory(categoryVM);
        }
        // DELETE: api/Category/delete/{id}
        [HttpPost("delete")]
        public async Task<ResponseMessage> DeleteCategory(Category category)
        {
            return await _categoryService.DeleteCategory(category);
        }
    }
}
