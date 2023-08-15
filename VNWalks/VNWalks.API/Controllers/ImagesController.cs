using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNWalks.API.Models.Domain;
using VNWalks.API.Models.DTOs.Image;
using VNWalks.API.Repositories;

namespace VNWalks.API.Controllers
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

        /// <summary>
        /// Upload File
        /// </summary>
        /// <param name="imageUploadRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);

            if (ModelState.IsValid)
            {
                //Convert DTO -> Domain Models
                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileName = imageUploadRequestDto.FileName,
                    FileDescription = imageUploadRequestDto.FileDescription,
                    FileExtention = Path.GetExtension(imageUploadRequestDto.File.FileName)
                };


                //use repository to Upload
                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Validate file Upload
        /// </summary>
        /// <param name="imageUploadRequestDto"></param>
        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {

            //Cac duoi cho phep
            var allowedExtention = new string[] { ".jpg", ".jped", ".png" };

            if (allowedExtention.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)) == false)
            {
                ModelState.AddModelError("file", "Unsupported file extention");
            }

            // kiem tra kich thuoc theo byte
            if (imageUploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, Please upload a smaller size file");
            }
        }
    }




}
