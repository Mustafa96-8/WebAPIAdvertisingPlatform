using System.Text;

namespace WebAPIAdvertisingPlatform.Extensions;

public static class FormFileExtensions
{
    public static IEnumerable<string> ReadAllStrings(this IFormFile file)
    {
        var result = new List<string>();
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while(reader.Peek() >= 0)
                result.Add(reader.ReadLine()??"");
        }
        return result;
    }
}
