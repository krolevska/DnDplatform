public class Skill
{
    public string Name { get; private set; }
    public string AssociatedAbility { get; private set; }
    public bool IsProficient { get; set; }
    public int Bonus { get; set; }

    public Skill(string name, string associatedAbility)
    {
        Name = name;
        AssociatedAbility = associatedAbility;
        IsProficient = false;
        Bonus = 0;
    }
}
