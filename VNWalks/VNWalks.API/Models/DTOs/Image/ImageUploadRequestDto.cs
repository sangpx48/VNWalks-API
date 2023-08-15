using System.ComponentModel.DataAnnotations;

namespace VNWalks.API.Models.DTOs.Image
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }


    }
}
