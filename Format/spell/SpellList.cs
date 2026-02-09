using Format.setting;
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

    public new bool Add(SpellClass spell)
    {
        base.Add(spell);
        return Save();
    }

    public new bool RemoveAt(int index)
    {
        base.RemoveAt(index);
        return Save();
    }

    public bool Save()
    {
        try
        {
            MyConsole.WriteLine("Salvataggio in corso...", ConsoleColor.Gray);
            string jsonString = JsonSerializer.Serialize<SpellList>(this, new JsonSerializerOptions { WriteIndented = true });
            MyConsole.WriteDebugLine(jsonString);
            File.WriteAllText(Settings.EnvPathOption("storage", "spell.json"), jsonString);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Errore durante il salvataggio: " + e.Message);
            return false;
        }

    }
}
