using Format.utils;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Text.Json;

namespace Format.spell;

public class SpellList : List<SpellClass>
{
    public SpellList() : base() {
        try
        {
            string jsonString = File.ReadAllText(Settings.EnvPathOption("storage", "spell.json"));
            MyConsole.WriteDebugLine($"json: {jsonString}");
            List<SpellClass> temp = JsonSerializer.Deserialize<List<SpellClass>>(jsonString) ?? new List<SpellClass>(0);
            MyConsole.WriteDebugLine($"temp:{temp?.ToString()}");
            foreach (SpellClass spell in temp!)
            {
                base.Add(spell);
            }
        }
        catch (Exception e) {
            MyConsole.WriteLine($"errore: {e.Message}\n{e.StackTrace}", ConsoleColor.Red);
        }
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
            MyConsole.WriteDebugLine(jsonString);
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
