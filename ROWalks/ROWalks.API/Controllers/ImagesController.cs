﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using ROWalks.API.Models.Domain;
using ROWalks.API.Models.DTO;
using ROWalks.API.Repositories;
using System.Runtime.CompilerServices;

namespace ROWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if(ModelState.IsValid)
            {
                //Convert DTO to domain model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };

                //User repository to upload image

                await imageRepository.Upload(imageDomainModel);
                return Ok(request);
            }
            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg",".jpeg",".png" };
            if(!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file","Unsuported file extension");
            }

            if(request.File.Length > 104857760)
            {
                ModelState.AddModelError("file","File size more than 10MB, please upload a smaller size file.");

            }
        }
    }
}
