using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

class DayEventGenerator
{
    public DayEventGenerator()
    {
    }

    public Event GetDayEvent()
    {
        // Lisez le contenu JSON depuis le fichier
        string jsonFilePath = "/Users/hugoa/dev/mydigitalschool/M2/natif/SocialAutomation/SocialAutomationBack/Assets/events.json";
        string jsonContent = File.ReadAllText(jsonFilePath);

        // Désérialisez le JSON en une liste d'objets Event
        List<Event> events = JsonConvert.DeserializeObject<List<Event>>(jsonContent);

        // Générez un index aléatoire
        Random random = new Random();
        int randomIndex = random.Next(0, events.Count);

        // Récupérez l'événement aléatoire
        Event randomEvent = events[randomIndex];

        return randomEvent;
    }
}

public class Event
{
    public string? Date { get; set; }
    public string? Subject { get; set; }
    public string? KeyWord { get; set; }
}
