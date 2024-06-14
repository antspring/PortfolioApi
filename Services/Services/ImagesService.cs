using DataAccess.Repositories.Implementations;
using Microsoft.AspNetCore.Http;

namespace Services.Services;

public class ImagesService(UserRepository userRepository)
{
    public async Task<string> UploadUserImage(IFormFile file, string username)
    {
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "images", username);
        Directory.CreateDirectory(directoryPath);
        var filePath = Path.Combine(directoryPath, file.FileName);
        await using var stream = File.Create(filePath);
        await file.CopyToAsync(stream);
        UpdateUserImage(username, filePath);
        return filePath;
    }

    public string GetUserImage(string username)
    {
        var user = userRepository.GetFirstOrDefault(user => user.Username == username);
        if (File.Exists(user.ImageUrl))
        {
            return user.ImageUrl;
        }

        throw new FileNotFoundException();
    }

    private void UpdateUserImage(string username, string filePath)
    {
        var user = userRepository.GetFirstOrDefault(user => user.Username == username);
        if (File.Exists(user.ImageUrl))
        {
            File.Delete(user.ImageUrl);
        }

        user.ImageUrl = filePath;
        userRepository.Update(user);
    }
}