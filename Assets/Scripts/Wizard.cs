using System;
using System.Collections.Generic;

public class Wizard : Character
{
    public int SpellsKnown { get; protected set; }
    public List<Spell> KnownSpells { get; protected set; }

    public Wizard(string name, int level = 1) : base(name, level)
    {
        Class = "Wizard";
        HitDie = 6;
        Intelligence += 2; // Wizards typically have high Intelligence
        KnownSpells = new List<Spell>();
        InitializeCharacter();
    }

    protected override void InitializeCharacter()
    {
        SpellsKnown = 6; // Start with 6 spells
        base.InitializeCharacter();
        CalculateSpellsKnown();
    }

    public override void LevelUp()
    {
        base.LevelUp();
        CalculateSpellsKnown();
    }

    private void CalculateSpellsKnown()
    {
        // This is a simplified version. Adjust according to D&D rules if needed.
        SpellsKnown = Math.Min(15, Level + 1);
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

    public override string GetCharacterSheet()
    {
        string baseSheet = base.GetCharacterSheet();
        string sorcererInfo = $"\nSpells Known: {SpellsKnown}" +
                              $"\nKnown Spells: {string.Join(", ", KnownSpells)}";
        return baseSheet + sorcererInfo;
    }
}