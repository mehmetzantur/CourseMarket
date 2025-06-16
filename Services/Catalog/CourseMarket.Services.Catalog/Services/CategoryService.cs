using AutoMapper;
using CourseMarket.Services.Catalog.Dtos;
using CourseMarket.Services.Catalog.Models;
using CourseMarket.Services.Catalog.Settings;
using CourseMarket.Shared.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseMarket.Services.Catalog.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CatalogCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
            return Response<List<CategoryDto>>.Success(categoriesDto, 200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Response<CategoryDto>.Success(categoryDto, 201);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (category is null)
                return Response<CategoryDto>.Fail("Category not found!", 404);

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Response<CategoryDto>.Success(categoryDto, 200);
        }
    }
}
