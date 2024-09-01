using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class CharacterCreatorManager : MonoBehaviour
{
    [Header("Character Creation")]
    public InputField characterNameInput;
    public Dropdown classDropdown;
    public Slider characterLevelSlider;
    public InputField[] abilityInputFields; // Array of 6 input fields for abilities
    public Button createCharacterButton;

    [Header("Character Sheet")]
    public Text characterSheetText;

    [Header("Ability Score Increase")]
    public Dropdown abilityIncreaseDropdown;
    public Button increaseAbilityButton;

    [Header("Skills")]
    public Transform skillListContainer;
    public Toggle skillTogglePrefab;

    [Header("Navigation")]
    public GameObject characterCreationPanel;
    public GameObject characterSheetPanel;

    private Character currentCharacter;
    private Dictionary<string, Toggle> skillToggles = new Dictionary<string, Toggle>();

    public event Action<Character> OnCharacterCreated;

    private void Start()
    {
        SetupUI();
    }

    private void SetupUI()
    {
        createCharacterButton.onClick.AddListener(CreateCharacter);
        increaseAbilityButton.onClick.AddListener(IncreaseSelectedAbility);

        classDropdown.ClearOptions();
        classDropdown.AddOptions(new List<string> { "Fighter", "Wizard", "Sorcerer", "Barbarian" }); // Add more as you implement them

        abilityIncreaseDropdown.ClearOptions();
        abilityIncreaseDropdown.AddOptions(new List<string> { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" });

        SetupSkillToggles();

        characterCreationPanel.SetActive(true);
        characterSheetPanel.SetActive(false);
    }

    private void SetupSkillToggles()
    {
        foreach (var skillName in new string[] { "Acrobatics", "Animal Handling", "Arcana", "Athletics", "Deception", "History", "Insight", "Intimidation", "Investigation", "Medicine", "Nature", "Perception", "Performance", "Persuasion", "Religion", "Sleight of Hand", "Stealth", "Survival" })
        {
            Toggle toggle = Instantiate(skillTogglePrefab, skillListContainer);
            toggle.GetComponentInChildren<Text>().text = skillName;
            skillToggles[skillName] = toggle;
        }
    }

    public void CreateCharacter()
    {
        string name = characterNameInput.text;
        string characterClass = classDropdown.options[classDropdown.value].text;
        int level = (int)characterLevelSlider.value;

        try
        {
            currentCharacter = CharacterFactory.CreateCharacter(characterClass, name, level);

            // Set initial ability scores
            currentCharacter.Strength = int.Parse(abilityInputFields[0].text);
            currentCharacter.Dexterity = int.Parse(abilityInputFields[1].text);
            currentCharacter.Constitution = int.Parse(abilityInputFields[2].text);
            currentCharacter.Intelligence = int.Parse(abilityInputFields[3].text);
            currentCharacter.Wisdom = int.Parse(abilityInputFields[4].text);
            currentCharacter.Charisma = int.Parse(abilityInputFields[5].text);

            // Set skill proficiencies
            foreach (var kvp in skillToggles)
            {
                currentCharacter.SetSkillProficiency(kvp.Key, kvp.Value.isOn);
            }

            currentCharacter.UpdateSkillBonuses();

            OnCharacterCreated?.Invoke(currentCharacter);
            UpdateCharacterSheet();
            ShowCharacterSheet();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error creating character: {e.Message}");
        }
    }

    private void ShowCharacterSheet()
    {
        characterCreationPanel.SetActive(false);
        characterSheetPanel.SetActive(true);
        UpdateAbilityIncreaseUI();
    }

    private void UpdateCharacterSheet()
    {
        if (currentCharacter != null)
        {
            characterSheetText.text = currentCharacter.GetCharacterSheet();
        }
    }

    public void IncreaseSelectedAbility()
    {
        if (currentCharacter != null && currentCharacter.AbilityScoreIncreases > 0)
        {
            try
            {
                string selectedAbility = abilityIncreaseDropdown.options[abilityIncreaseDropdown.value].text;
                // Implement the ability increase logic here
                // For example: currentCharacter.IncreaseAbility(selectedAbility);
                UpdateCharacterSheet();
                UpdateAbilityIncreaseUI();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error increasing ability: {e.Message}");
            }
        }
    }

    private void UpdateAbilityIncreaseUI()
    {
        if (currentCharacter != null)
        {
            increaseAbilityButton.interactable = currentCharacter.AbilityScoreIncreases > 0;
            abilityIncreaseDropdown.interactable = currentCharacter.AbilityScoreIncreases > 0;
        }
    }

    public void ReturnToCharacterCreation()
    {
        characterSheetPanel.SetActive(false);
        characterCreationPanel.SetActive(true);
        ResetCharacterCreationFields();
    }

    private void ResetCharacterCreationFields()
    {
        characterNameInput.text = "";
        classDropdown.value = 0;
        characterLevelSlider.value = 1;
        foreach (var field in abilityInputFields)
        {
            field.text = "10";
        }
        foreach (var toggle in skillToggles.Values)
        {
            toggle.isOn = false;
        }
    }
}