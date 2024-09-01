public class Fighter : Character
{
    public string FightingStyle { get; protected set; }

    public Fighter(string name, int level = 1) : base(name, level)
    {
        Class = "Fighter";
        HitDie = 10;
        Strength += 2; // Fighters typically have high Strength
    }

    protected override void InitializeCharacter()
    {
        string[] styles = { "Archery", "Defense", "Dueling", "Great Weapon Fighting" };
        FightingStyle = styles[UnityEngine.Random.Range(0, styles.Length)];
        base.InitializeCharacter();
    }

    public override string GetCharacterSheet()
    {
        return base.GetCharacterSheet() + $"\nFighting Style: {FightingStyle}";
    }
}
