using System;
using System.Collections.Generic;
using System.Linq;

public class Sorcerer : Character
{
    public int SpellsKnown { get; protected set; }
    public int SorceryPoints { get; protected set; }
    public List<Spell> KnownSpells { get; protected set; }
    public int[] SpellSlots { get; protected set; }
    public int[] CurrentSpellSlots { get; protected set; }

    public Sorcerer(string name, int level = 1) : base(name, level)
    {
        Class = "Sorcerer";
        HitDie = 6;
        Charisma += 2;
        KnownSpells = new List<Spell>();
        InitializeCharacter();
    }

    protected override void InitializeCharacter()
    {
        base.InitializeCharacter();
        CalculateSpellsKnown();
        CalculateSorceryPoints();
        CalculateSpellSlots();
        ResetSpellSlots();
    }

    private void CalculateSpellsKnown()
    {
        if (Level < 11)
        {
            SpellsKnown = Math.Min(11, Level + 1);
        }
        else
        {
            SpellsKnown = (Level % 2 == 0) ? Level : Math.Min(15, Level + 1);
        }
    }

    private void CalculateSorceryPoints()
    {
        SorceryPoints = Level >= 2 ? Level : 0;
    }

    private void CalculateSpellSlots()
    {
        SpellSlots = new int[10]; // 0-9 spell levels
        SpellSlots[0] = Level <= 3 ? 4 : (Level <= 9 ? 5 : 6); // Cantrips
        SpellSlots[1] = Math.Min(4, Level >= 1 ? Level + 1 : 0);
        SpellSlots[2] = Level >= 3 ? Math.Min(3, Level - 2) : 0;
        SpellSlots[3] = Level >= 5 ? Math.Min(3, Level - 4) : 0;
        SpellSlots[4] = Level >= 7 ? Math.Min(3, Level - 6) : 0;
        SpellSlots[5] = Level >= 9 ? Math.Min(Level > 17 ? 3 : 2, Level - 8) : 0;
        SpellSlots[6] = Level >= 11 ? (Level > 18 ? 2 : 1) : 0;
        SpellSlots[7] = Level >= 13 ? (Level > 19 ? 2 : 1) : 0;
        SpellSlots[8] = Level >= 15 ? 1 : 0;
        SpellSlots[9] = Level >= 17 ? 1 : 0;
    }

    private void ResetSpellSlots()
    {
        CurrentSpellSlots = (int[])SpellSlots.Clone();
    }

    public override void LevelUp()
    {
        base.LevelUp();
        CalculateSpellsKnown();
        CalculateSorceryPoints();
        CalculateSpellSlots();
        ResetSpellSlots();
    }

    public void LearnSpell(string spellName)
    {
        if (KnownSpells.Count < SpellsKnown)
        {
            Spell spell = SpellList.GetSpell(spellName);
            KnownSpells.Add(spell);
        }
        else
        {
            throw new InvalidOperationException("Cannot learn more spells. Maximum spells known reached.");
        }
    }

    public bool CastSpell(string spellName)
    {
        Spell spell = KnownSpells.FirstOrDefault(s => s.Name.Equals(spellName, StringComparison.OrdinalIgnoreCase));
        if (spell == null)
        {
            throw new ArgumentException($"Spell '{spellName}' is not known by this sorcerer.");
        }

        int spellLevel = spell.Level;
        if (CurrentSpellSlots[spellLevel] > 0)
        {
            CurrentSpellSlots[spellLevel]--;
            Console.WriteLine($"Casting {spellName}. {CurrentSpellSlots[spellLevel]} level {spellLevel} slots remaining.");
            return true;
        }
        else
        {
            Console.WriteLine($"Cannot cast {spellName}. No level {spellLevel} spell slots remaining.");
            return false;
        }
    }

    public override void LongRest()
    {
        ResetSpellSlots();
        Console.WriteLine("All spell slots have been restored after a long rest.");
    }

    public override string GetCharacterSheet()
    {
        string baseSheet = base.GetCharacterSheet();
        string spellList = string.Join(", ", KnownSpells.Select(s => s.Name));
        string spellSlotInfo = string.Join(", ", CurrentSpellSlots.Select((slots, level) => $"Level {level}: {slots}/{SpellSlots[level]}"));
        string sorcererInfo = $"\nSpells Known: {SpellsKnown}" +
                              $"\nSorcery Points: {SorceryPoints}" +
                              $"\nKnown Spells: {spellList}" +
                              $"\nCurrent Spell Slots: {spellSlotInfo}";
        return baseSheet + sorcererInfo;
    }
}