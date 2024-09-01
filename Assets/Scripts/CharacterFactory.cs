using System;
using System.Collections.Generic;
using System.Linq;

public static class CharacterFactory
{
    public static Character CreateCharacter(string characterClass, string name, int level = 1)
    {
        switch (characterClass.ToLower())
        {
            case "fighter":
                return new Fighter(name, level);
            case "wizard":
                return new Wizard(name, level);
            case "sorcerer":
                return new Sorcerer(name, level);
            case "barbarian":
                return new Barbarian(name, level);
            default:
                throw new ArgumentException($"Unknown character class: {characterClass}");
        }
    }
}