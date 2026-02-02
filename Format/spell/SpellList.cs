using Format.utils;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Text.Json;

namespace Format.spell;

public class SpellList : List<SpellClass>
{
    public SpellList() : base() {
        try {
            string jsonString = File.ReadAllText(utils.Settings.env("storage", "spell.json"));
            var temp = JsonSerializer.Deserialize<List<SpellClass>>(jsonString);
            foreach (var spell in temp)
            {
                base.Add(spell);
            }
        }
        catch (Exception) {}
    }

    public new void Add(SpellClass spell)
    {
        base.Add(spell);
        if (Save())
        {
            MyConsole.WriteLine("Incantesimo aggiunto con successo.", ConsoleColor.Green);
        }
    }

    public new void RemoveAt(int index)
    {
        base.RemoveAt(index);
        if (Save())
        {
            MyConsole.WriteLine("Incantesimo rimosso con successo.", ConsoleColor.Green);
        }
    }

    public bool Save()
    {
        try
        {
            MyConsole.WriteLine("Salvataggio in corso...", ConsoleColor.Gray);
            string jsonString = JsonSerializer.Serialize<SpellList>(this, new JsonSerializerOptions { WriteIndented = true });
            MyConsole.WriteLine(jsonString, ConsoleColor.DarkGray);
            File.WriteAllText(utils.Settings.env("storage", "spell.json"), jsonString);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Errore durante il salvataggio: " + e.Message);
            return false;
        }

    }
}
