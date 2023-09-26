using System.Configuration;
using PexelsDotNetSDK.Api;

class ImageGenerator {


    private PexelsClient pexelsClient = new(ConfigurationManager.AppSettings["PexelsApiKey"]);

    public async Task<string> GetPhotosFromTheme(string dayEvent) {
        var result = await pexelsClient.SearchPhotosAsync(dayEvent);
        if (result.photos.Count > 0 ) {
            return result.photos[0].source.original;
        } else {
            return "";
        }
    }


}