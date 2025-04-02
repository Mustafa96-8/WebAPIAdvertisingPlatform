using System.Collections.Concurrent;
using System.Linq;

namespace WebAPIAdvertisingPlatform.Models;

public class AdvertisingPlatform
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public ConcurrentDictionary<int, string> Locations { get; private set; }

    private AdvertisingPlatform(string name, string locationsString)
    {
        this.Locations = new ConcurrentDictionary<int,string>(locationsString.Split(",").ToDictionary(u=>u.GetHashCode()));
        this.Name = name;
        Id = new Guid();
    }
    public static AdvertisingPlatform Create(string name, string locationsString)
    {
        return new AdvertisingPlatform(name, locationsString);
    }
}
                                                    