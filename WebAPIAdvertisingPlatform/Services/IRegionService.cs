
namespace WebAPIAdvertisingPlatform.Services;

public interface IRegionService
{
    IEnumerable<string> FindPlatforms(string location);
    int LoadPlatforms(IFormFile file);
}