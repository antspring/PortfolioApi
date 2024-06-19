using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Portfolio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController(ImagesService imagesService) : ControllerBase
    {
        [HttpPost("upload-avatar")]
        public async Task<IActionResult> UploadUserImage(IFormFile file)
        {
            var filePath = await imagesService.UploadUserImage(file, User.Identity.Name);
            return Ok(new { Path = filePath });
        }

        [AllowAnonymous]
        [HttpGet("/get/file/app/images/{username}/{filename}")]
        public IActionResult GetUserImage(string username, string filename)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", username, filename);
            if (System.IO.File.Exists(filePath))
            {
                return PhysicalFile(filePath, "image/png");
            }

            return NotFound();
        }
    }
}