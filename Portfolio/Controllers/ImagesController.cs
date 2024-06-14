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

        [HttpGet("get-avatar")]
        public IActionResult GetUserImage()
        {
            try
            {
                var path = imagesService.GetUserImage(User.Identity.Name);
                return PhysicalFile(path, "image/png");
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }
    }
}