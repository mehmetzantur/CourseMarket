using CourseMarket.Services.Catalog.Dtos;
using CourseMarket.Services.Catalog.Models;
using CourseMarket.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseMarket.Services.Catalog.Services
{
    internal interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
