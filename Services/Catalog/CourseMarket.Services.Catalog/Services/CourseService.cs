using AutoMapper;
using CourseMarket.Services.Catalog.Dtos;
using CourseMarket.Services.Catalog.Models;
using CourseMarket.Services.Catalog.Settings;
using CourseMarket.Shared.Dtos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseMarket.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                    course.Category = await _categoryCollection.Find(c => c.Id == course.CategoryId).FirstAsync();
            }
            else
            {
                courses = new List<Course>();
            }

            var coursesDto = _mapper.Map<List<CourseDto>>(courses);
            return Response<List<CourseDto>>.Success(coursesDto, 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (course is null)
                return Response<CourseDto>.Fail("Course not found!", 404);

            course.Category = await _categoryCollection.Find(c => c.Id == course.CategoryId).FirstAsync();
            var courseDto = _mapper.Map<CourseDto>(course);
            return Response<CourseDto>.Success(courseDto, 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(c => c.UserId == userId).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                    course.Category = await _categoryCollection.Find(c => c.Id == course.CategoryId).FirstAsync();
            }
            else
            {
                courses = new List<Course>();
            }

            var coursesDto = _mapper.Map<List<CourseDto>>(courses);
            return Response<List<CourseDto>>.Success(coursesDto, 200);

        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var createCourse = _mapper.Map<Course>(courseCreateDto);
            createCourse.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(createCourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(createCourse),200);
        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(c => c.Id == updateCourse.Id,updateCourse);
            if (result is null)
                return Response<NoContent>.Fail("Course not found!", 404);
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(c => c.Id == id);
            if (result.DeletedCount != 0)
                return Response<NoContent>.Fail("Course not found!", 404);
            return Response<NoContent>.Success(204);
        }
    }
}
