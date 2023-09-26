public class EventModel
{
    public string Date { get; set; }
    public string Event { get; set; }
    public string Tweet { get; set; }
    public string Image { get; set; }

    public override string ToString()
    {
        return $"Date: {Date}, Event: {Event}, Tweet: {Tweet}, Image: {Image}";
    }
}