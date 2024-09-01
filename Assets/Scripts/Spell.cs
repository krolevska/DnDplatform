using System;
using System.Collections.Generic;

public class Spell
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public string School { get; private set; }
    public string CastingTime { get; private set; }
    public string Range { get; private set; }
    public string Components { get; private set; }
    public string Duration { get; private set; }
    public string Description { get; private set; }

    public Spell(string name, int level, string school, string castingTime, string range, string components, string duration, string description)
    {
        Name = name;
        Level = level;
        School = school;
        CastingTime = castingTime;
        Range = range;
        Components = components;
        Duration = duration;
        Description = description;
    }
}

