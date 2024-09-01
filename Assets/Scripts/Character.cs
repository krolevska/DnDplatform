using System;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string Name { get; set; }
    public string Class { get; protected set; }
    public int Level { get; protected set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }
    public int Wisdom { get; set; }
    public int Charisma { get; set; }
    public int HitPoints { get; protected set; }
    protected int HitDie { get; set; }
    public int AbilityScoreIncreases { get; protected set; }
    public Dictionary<string, Skill> Skills { get; private set; }
    public int ProficiencyBonus { get; private set; }

    public Character(string name, int level = 1)
    {
        Name = name;
        Level = level;
        Strength = Dexterity = Constitution = Intelligence = Wisdom = Charisma = 10;
        AbilityScoreIncreases = 0;
        InitializeCharacter();
    }

    protected virtual void InitializeCharacter()
    {
        for (int i = 1; i < Level; i++)
        {
            LevelUp();
        }
        CalculateHitPoints();
        InitializeSkills();
    }

    public virtual void LevelUp()
    {
        Level++;
        if (Level % 4 == 0)
        {
            AbilityScoreIncreases += 2;
        }
        UpdateProficiencyBonus();
    }

    protected virtual void CalculateHitPoints()
    {
        int constitutionModifier = (Constitution - 10) / 2;
        HitPoints = HitDie + constitutionModifier;
        for (int i = 1; i < Level; i++)
        {
            HitPoints += UnityEngine.Random.Range(1, HitDie + 1) + constitutionModifier;
        }
    }

    private void InitializeSkills()
    {
        Skills = new Dictionary<string, Skill>
        {
            {"Acrobatics", new Skill("Acrobatics", "Dexterity")},
            {"Animal Handling", new Skill("Animal Handling", "Wisdom")},
            {"Arcana", new Skill("Arcana", "Intelligence")},
            {"Athletics", new Skill("Athletics", "Strength")},
            {"Deception", new Skill("Deception", "Charisma")},
            {"History", new Skill("History", "Intelligence")},
            {"Insight", new Skill("Insight", "Wisdom")},
            {"Intimidation", new Skill("Intimidation", "Charisma")},
            {"Investigation", new Skill("Investigation", "Intelligence")},
            {"Medicine", new Skill("Medicine", "Wisdom")},
            {"Nature", new Skill("Nature", "Intelligence")},
            {"Perception", new Skill("Perception", "Wisdom")},
            {"Performance", new Skill("Performance", "Charisma")},
            {"Persuasion", new Skill("Persuasion", "Charisma")},
            {"Religion", new Skill("Religion", "Intelligence")},
            {"Sleight of Hand", new Skill("Sleight of Hand", "Dexterity")},
            {"Stealth", new Skill("Stealth", "Dexterity")},
            {"Survival", new Skill("Survival", "Wisdom")}
        };

        UpdateProficiencyBonus();
        UpdateSkillBonuses();
    }

    private void UpdateProficiencyBonus()
    {
        ProficiencyBonus = (Level - 1) / 4 + 2;
    }

    public void UpdateSkillBonuses()
    {
        foreach (var skill in Skills.Values)
        {
            int abilityModifier = GetAbilityModifier(skill.AssociatedAbility);
            skill.Bonus = abilityModifier + (skill.IsProficient ? ProficiencyBonus : 0);
        }
    }

    private int GetAbilityModifier(string ability)
    {
        int abilityScore = ability switch
        {
            "Strength" => Strength,
            "Dexterity" => Dexterity,
            "Constitution" => Constitution,
            "Intelligence" => Intelligence,
            "Wisdom" => Wisdom,
            "Charisma" => Charisma,
            _ => throw new ArgumentException("Invalid ability name")
        };

        return (abilityScore - 10) / 2;
    }

    public void SetSkillProficiency(string skillName, bool isProficient)
    {
        if (Skills.TryGetValue(skillName, out Skill skill))
        {
            skill.IsProficient = isProficient;
            UpdateSkillBonuses();
        }
        else
        {
            throw new ArgumentException("Invalid skill name");
        }
    }

    public virtual string GetCharacterSheet()
    {
        string sheet = $"Name: {Name}\nClass: {Class}\nLevel: {Level}\nHP: {HitPoints}\n" +
                       $"STR: {Strength} DEX: {Dexterity} CON: {Constitution}\n" +
                       $"INT: {Intelligence} WIS: {Wisdom} CHA: {Charisma}\n" +
                       $"Available Ability Score Increases: {AbilityScoreIncreases}\n\n" +
                       "Skills:";

        foreach (var skill in Skills.Values)
        {
            sheet += $"\n{skill.Name} ({skill.AssociatedAbility}): {(skill.IsProficient ? "*" : "")}+{skill.Bonus}";
        }

        return sheet;
    }

    public virtual void LongRest()
    {
        Console.WriteLine("All spell slots have been restored after a long rest.");
    }
}