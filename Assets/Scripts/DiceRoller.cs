using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller
{
    public int RollDie(int sides)
    {
        return Random.Range(1, sides + 1);
    }
}