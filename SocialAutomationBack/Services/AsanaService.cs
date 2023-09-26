using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class AsanaService {
    private readonly HttpClient _client;
    private readonly string _url = "https://n8n-production-9a9b.up.railway.app/webhook/da4026c7-0a3b-4e68-9a96-5a22d2ec66d2";

    public AsanaService()
    {
        _client = new HttpClient();
    }

    public async Task PostEventDetailAsync(EventModel eventDetail)
    {
        var jsonContent = JsonConvert.SerializeObject(eventDetail);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync(_url, content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to post the event detail for date {eventDetail.Date}. Response: {responseContent}");
        }
    }
}
