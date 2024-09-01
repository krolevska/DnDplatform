using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public CharacterCreatorManager characterCreator;

    private List<Character> characters = new List<Character>();

    private void Start()
    {
        if (characterCreator != null)
        {
            characterCreator.OnCharacterCreated += AddCharacter;
        }
        else
        {
            Debug.LogError("CharacterCreatorManager not assigned to GameManager!");
        }
    }

    private void AddCharacter(Character newCharacter)
    {
        characters.Add(newCharacter);
        Debug.Log($"New character added: {newCharacter.Name}");
    }

    // Add other game management methods here, such as:
    // - Starting a game session
    // - Managing multiple characters in a party
    // - Handling game state (exploration, combat, etc.)
    // - Saving and loading game progress
}