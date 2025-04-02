using System.Collections.Concurrent;
using System.Text;
using WebAPIAdvertisingPlatform.Extensions;
using WebAPIAdvertisingPlatform.Models;

namespace WebAPIAdvertisingPlatform.Services;

public class RegionService : IRegionService
{
    private IDictionary<string, List<string>> advertisingPlatforms = new ConcurrentDictionary<string, List<string>>();
    private readonly ILogger<RegionService> _logger;

    public RegionService(ILogger<RegionService> logger)
    {
        _logger = logger;
    }
    public int LoadPlatforms(IFormFile file)
    {
        try
        {
            string[] readLines = [.. file.ReadAllStrings()];
            foreach(string s in readLines)
            {
                string[] splitStr = s.Split(":");
                string name = splitStr[0];
                string[] locations = splitStr[1].Split(",");
                foreach(string location in locations)
                {

                    if(advertisingPlatforms.TryGetValue(location, out var places))
                    {
                        places.Add(name);
                    }
                    else
                    {
                        advertisingPlatforms.Add(location, new List<string>() { name });
                    }
                }
            }
            return 0;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error loading platforms");
            return 1;
        }
    }
    public IEnumerable<string> FindPlatforms(string location)
    {
        List<string> result = new();
        StringBuilder stringBuilder = new("/");
        foreach(string item in location.Split("/"))
        {
            if(!string.IsNullOrWhiteSpace(item))
            {
                stringBuilder.Append(item);

                if(advertisingPlatforms.TryGetValue(stringBuilder.ToString(), out var res))
                {
                    result.AddRange(res);
                }
                stringBuilder.Append('/');
            }
        }
        return result.Distinct();
    }
}
