using System;
using System.Collections.Generic;

public static class SpellList
{
    public static Dictionary<string, Spell> Spells { get; private set; }

    static SpellList()
    {
        InitializeSpells();
    }

    private static void InitializeSpells()
    {
        Spells = new Dictionary<string, Spell>
        {
            {"Magic Missile", new Spell("Magic Missile", 1, "Evocation", "1 action", "120 feet", "V, S", "Instantaneous", "You create three glowing darts of magical force...")},
            {"Fireball", new Spell("Fireball", 3, "Evocation", "1 action", "150 feet", "V, S, M", "Instantaneous", "A bright streak flashes from your pointing finger to a point you choose...")},
            {"Mage Armor", new Spell("Mage Armor", 1, "Abjuration", "1 action", "Touch", "V, S, M", "8 hours", "You touch a willing creature who isn't wearing armor, and a protective magical force surrounds it until the spell ends...")},
            {"Shield", new Spell("Shield", 1, "Abjuration", "1 reaction", "Self", "V, S", "1 round", "An invisible barrier of magical force appears and protects you...")},
            {"Detect Magic", new Spell("Detect Magic", 1, "Divination", "1 action", "Self", "V, S", "Concentration, up to 10 minutes", "For the duration, you sense the presence of magic within 30 feet of you...")},
            // Add more spells here...
        };
    }

    public static Spell GetSpell(string spellName)
    {
        if (Spells.TryGetValue(spellName, out Spell spell))
        {
            return spell;
        }
        throw new ArgumentException($"Spell '{spellName}' not found.");
    }

    public static List<Spell> GetSpellsByLevel(int level)
    {
        List<Spell> spellsOfLevel = new List<Spell>();
        foreach (var spell in Spells.Values)
        {
            if (spell.Level == level)
            {
                spellsOfLevel.Add(spell);
            }
        }
        return spellsOfLevel;
    }
}
