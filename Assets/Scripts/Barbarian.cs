public class Barbarian : Character
{
    public int RageCount { get; protected set; }

    public Barbarian(string name, int level = 1) : base(name, level)
    {
        Class = "Barbarian";
        HitDie = 12;
        Strength += 2;
        Constitution += 1;
    }

    protected override void InitializeCharacter()
    {
        RageCount = 2; // Start with 2 rages per day
        base.InitializeCharacter();
    }

    public override void LevelUp()
    {
        base.LevelUp();
        if (Level == 3 || Level == 6 || Level == 12 || Level == 17)
        {
            RageCount++;
        }
    }

    public override string GetCharacterSheet()
    {
        return base.GetCharacterSheet() + $"\nRages per Day: {RageCount}";
    }
}
