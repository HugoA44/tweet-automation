using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using CommandLine;
using System.Text;
using System.Net.Http;

namespace SocialAutomation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<Options>(args)
                .WithParsedAsync(async options =>
                {
                    var compagnyName = options.CompanyName;
                    var companySummary = options.CompanySummary;
                    var numberOfMaronniers = options.NumberOfMaronniers;

                    var dayEventGenerator = new DayEventGenerator();
                    var tweetGenerator = new TweetGenerator();
                    var imageGenerator = new ImageGenerator();
                    var asanaService = new AsanaService();

                    List<EventModel> eventDetailList = new List<EventModel>();

                    // Générer les tweets pour les maronniers
                    for (int i = 0; i < numberOfMaronniers; i++)
                    {
                        var dayEvent = dayEventGenerator.GetDayEvent();
                        var tweet = await tweetGenerator.GenerateTweet(compagnyName, companySummary, dayEvent.Subject);
                        var picture = await imageGenerator.GetPhotosFromTheme(AddSpaceBeforeUppercase(dayEvent.KeyWord));

                        var detail = new EventModel
                        {
                            Date = dayEvent.Date,
                            Event = dayEvent.Subject,
                            Tweet = tweet.Replace("\n", ""),
                            Image = picture
                        };

                        asanaService.PostEventDetailAsync(detail);

                        eventDetailList.Add(detail);
                    }


                    Console.WriteLine(JsonConvert.SerializeObject(eventDetailList));

                });
        }

        static string AddSpaceBeforeUppercase(string input)
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                // Ajoute un espace avant la lettre majuscule
                if (i > 0 && char.IsUpper(input[i]))
                {
                    output.Append(' ');
                }

                output.Append(input[i]);
            }

            return output.ToString();
        }

    }
}




