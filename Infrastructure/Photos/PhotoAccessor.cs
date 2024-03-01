using Application;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public class PhotoAccessor : IPhotoAccessor
{
  private readonly Cloudinary _cloudinay;
  public PhotoAccessor(IOptions<CloudinarySettings> config)
  {
    // Initailizes a new instance of the Account class 
    var account = new Account(
      config.Value.CloudName,
      config.Value.ApiKey,
      config.Value.ApiSecret
    );
    // Initializes a new instance of the Cloudinary class with Cloudinary account.
    _cloudinay = new Cloudinary(account);
  }
  public async Task<PhotoUploadResult> AddPhoto(IFormFile file)
  {
    if (file.Length > 0)
    {
      //   Returns a task that represents the asynchronous dispose operation.
      await using var stream = file.OpenReadStream();
      var uploadParams = new ImageUploadParams
      {
        File = new FileDescription(file.FileName, stream),
        Transformation = new Transformation().Height(500).Width(500).Crop("fill")
      };

      var uploadResult = await _cloudinay.UploadAsync(uploadParams);

      // If it fails
      if (uploadResult.Error != null)
      {
        throw new Exception(uploadResult.Error.Message);
      }

      // success, returns PublicId and Url
      return new PhotoUploadResult
      {
        PublicId = uploadResult.PublicId,
        Url = uploadResult.SecureUrl.ToString()
      };
    }

    // if no file found
    return null;
  }

  public async Task<string> DeletePhoto(string publicId)
  {
    var deleteParams = new DeletionParams(publicId);
    // Deletes file from Cloudinary asynchronously.
    var result = await _cloudinay.DestroyAsync(deleteParams);
    // (string DeletionResult.Result) 
    // Gets or sets result description.
    return result.Result == "ok" ? result.Result : null;
  }
}
