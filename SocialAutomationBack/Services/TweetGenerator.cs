using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Configuration;
using System.Drawing.Printing;

class TweetGenerator
{
    private readonly HttpClient _httpClient;
    private readonly string OpenAiApiKey = ConfigurationManager.AppSettings["OpenAiApiKey"];

    public TweetGenerator()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", OpenAiApiKey);
    }

    public async Task<string> GenerateTweet(string compagnyName, string companySummary, string dayEvent)
    {
        var prompt = "Tu es un expert du marketing." +
                      " Tu dois créer un post de 140 caractères maximum en Français qui va lier notre entreprise et des jours marquant de l'année." +
                      " Options:" +
                      $" Nom de l'entreprise: {compagnyName}" +
                      $" Résumé de l'entreprise: {companySummary}" +
                      $" Jour de fête: {dayEvent}";



        // Appel à l'API d'OpenAI pour générer un tweet
        var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/completions", new
        {
            prompt,
            model = "text-davinci-003",
            max_tokens = 100 // Ajustez selon vos besoins
        });


        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<OpenAiResponse>(content);
            return result.choices[0].text;
        }

        return "Échec de la génération du tweet.";
    }
}

