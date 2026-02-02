using Format.utils;

namespace Format.spell;

public class SpellClass
{
    public string Name { get; set; }
    public int Level { get; set; }
    public string School { get; set; }
    public string CastingTime { get; set; }
    public string Range { get; set; }
    public string Components { get; set; }
    public string Duration { get; set; }
    public string Description { get; set; }
    public string HigherLevels { get; set; }
    public string[] Classes { get; set; }
    public string Sorgente { get; set; }
    static public SpellList spells { get; set; }

    /**
     * @param name il nome dell'incantesimo
     * @param level il livello dell'incantesimo
     * @param school la scuola dell'incantesimo
     * @param castingTime il tempo di lancio dell'incantesimo
     * @param range il raggio d'azione dell'incantesimo
     * @param components i componenti dell'incantesimo
     * @param duration la durata dell'incantesimo
     * @param description la descrizione dell'incantesimo
     * @param higherLevels l'effetto ai livelli superiori dell'incantesimo
     * @param classes le classi nella cui lista è presente questo incantesimo
     * @param sorgente link alla sorgente dell'incantesimo (opzionale)
     * @constructor Crea un nuovo incantesimo partendo dai parametri
     */
    public SpellClass(string name, int level, string school, string castingTime, string range, string components, string duration, string description, string higherLevels, string[] classes, string sorgente)
    {
        this.Name = name;
        this.Level = level;
        this.School = school;
        this.CastingTime = castingTime;
        this.Range = range;
        this.Components = components;
        this.Duration = duration;
        this.Description = description;
        this.HigherLevels = higherLevels;
        this.Classes = classes;
        this.Sorgente = sorgente;
    }

    public SpellClass()
    {
    }

    /**
     * @constructor Crea un nuovo incantesimo chiedendo all'utente di inserire i vari parametri
     */
    public SpellClass(string pippo)
    {
        this.Name = MyConsole.ReadString("inserisci il nome di questo incantesimo");
        Level = MyConsole.ReadInt("inserisci il livello dell'incantesimo (0 per trucchetto):");
        School = MyConsole.ReadString("inserisci la scuola dell'incantesimo:");
        CastingTime = MyConsole.ReadString("inserisci il tempo di lancio dell'incantesimo:");
        Range = MyConsole.ReadString("inserisci la gittata dell'incantesimo:");
        Components = MyConsole.ReadString("inserisci i componenti dell'incantesimo: (V, S, M [...])");
        Duration = MyConsole.ReadString("inserisci la durata dell'incantesimo:");
        Description = MyConsole.ReadString("inserisci il primo paragrafo della descrizione dell'incantesimo:");
        string line;
        while (!string.IsNullOrEmpty(line = MyConsole.ReadString("inserisci il prossimo paragrafo della descrizione dell'incantesimo (se sono finiti lascia vuoto)", ConsoleColor.Yellow)))
        {
            Description = $"{Description}\n{line}";
        }
        HigherLevels = MyConsole.ReadString("inserisci l'effetto ai livelli superiori dell'incantesimo (se non c'è, lascia vuoto):");
        List<string> classes = [MyConsole.ReadString("inserisci la prima classe di questo incantesimo:")];
        while (!string.IsNullOrEmpty(line = MyConsole.ReadString("inserisci la prossima classe di questo incantesimo (se sono finite lascia vuoto)", ConsoleColor.Yellow)))
        {
            classes.Add(line);
        }
        this.Classes = [.. classes];
        Sorgente = MyConsole.ReadString("inserisci il link sorgente dell'incantesimo (se non c'è, lascia vuoto):");
        Console.WriteLine("incantesimo creato:");
        Console.WriteLine(this.ToMarkdown());
    }

    public override string ToString()
    {
        string ret = $"""
            Nome:               {Name}
            Livello:            {Level}
            Scuola:             {School}
            Tempo di lancio:    {CastingTime}
            Raggio d'azione:    {Range}
            Componenti:         {Components}
            Durata:             {Duration}
            Descrizione:        {Description}
            <higherLevels>Classi:{Classes}
            <source>
            """;
        if (!string.IsNullOrEmpty(HigherLevels))
        {
            ret = ret.Replace("<higherLevels>", $"Effetto ai livelli superiori:\t{HigherLevels}\n");
        }
        else
        {
            ret = ret.Replace("<higherLevels>", "");
        }

        if (!string.IsNullOrEmpty(Sorgente))
        {
            ret = ret.Replace("<source>", $"Sorgente:\t{Sorgente}\n");
        }
        else
        {
            ret = ret.Replace("<source>", "");
        }

        return ret;
    }

    public string ToMarkdown()
    {
        string ret = $"# {Name}\n*";
        if (Level == 0)
        {
            ret = $"{ret}Trucchetto,";
        }
        else
        {
            ret = $"{ret}{Level}° livello,";
        }
        ret = $"{ret} {School}*";
        ret = $"{ret}\n\n";
        ret = $"{ret}- **Tempo di lancio:** {CastingTime}\n";
        ret = $"{ret}- **Raggio d'azione:** {Range}\n";
        ret = $"{ret}- **Componenti:** {Components}\n";
        ret = $"{ret}- **Durata:** {Duration}\n\n";
        ret = $"{ret}{Description}\n\n";
        if (!string.IsNullOrEmpty(HigherLevels))
        {
            ret = $"{ret}**Ai livelli superiori:** {HigherLevels}\n\n";
        }
        ret = $"{ret}**Classi:** {string.Join(", ", Classes)}\n\n";
        if (!string.IsNullOrEmpty(Sorgente))
        {
            // render link as clickable markdown but show the URL as link text
            ret = $"{ret}**Sorgente:** [{Sorgente}]({Sorgente})\n\n";
        }
        ret = $"{ret}---";
        return ret;
    }

    public static string ToCamelCase(string originalName)
    {
        originalName = originalName.Replace("'", "");
        if (string.IsNullOrWhiteSpace(originalName))
            return string.Empty;

        var parts = originalName
            .ToLower()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 0)
            return string.Empty;

        var result = "";

        foreach (var part in parts)
        {
            if (part.Length > 0)
            {
                result = $"{result}{(part != parts[0] ? "-" : "")}{part}";
            }
        }

        return result;
    }

    public string ToObsidianReference()
    {
        string ret = $"![[tutti/{ToCamelCase(Name).ToLower()}]]";
        return ret;
    }
}