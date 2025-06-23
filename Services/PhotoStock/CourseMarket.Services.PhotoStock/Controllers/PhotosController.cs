﻿using CourseMarket.Services.PhotoStock.Dtos;
using CourseMarket.Shared.ControllerBases;
using CourseMarket.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CourseMarket.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Save(IFormFile photo, CancellationToken cancellationToken) 
        {
            if(photo is not null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);
                
                var returnPath = "photos/" + photo.FileName;
                PhotoDto photoDto = new() { Url = returnPath };
                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }

            return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty!", 400));
        }

        public IActionResult Delete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
                return CreateActionResultInstance(Response<NoContent>.Fail("Photo not found!", 404));

            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}
