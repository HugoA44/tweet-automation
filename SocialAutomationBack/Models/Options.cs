using CommandLine;

class Options
{
    [Option('n', "name", Required = true, HelpText = "Nom de l'entreprise")]
    public string CompanyName { get; set; }

    [Option('s', "summary", Required = true, HelpText = "Résumé de l'entreprise")]
    public string CompanySummary { get; set; }

    [Option('c', "count", Required = true, HelpText = "Nombre de maronniers à générer")]
    public int NumberOfMaronniers { get; set; }
}
