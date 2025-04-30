using UnityEngine;
using UnityEngine.UI;
/* This class is used for switching 
 * between a male and female character
 * in the character customizer scene
 */ 
public class CharactorSelector : MonoBehaviour
{
    // Creating references to the prefabs
    public GameObject maleCharacter;
    public GameObject femaleCharacter;
    
    // Buttons used in the UI to select male/female
    public Button maleButton;
    public Button femaleButton;
    
    // Called immediately on frame when script is enabled
    // This is updating male/female when the buttons are clicked
    void Start()
    {
        SelectMale(); // Default
        maleButton.onClick.AddListener(SelectMale);
        femaleButton.onClick.AddListener(SelectFemale);
		 
    }

    // Method sets the male prefabe to active (visible), and female inactive
    public void SelectMale()
    {
        maleCharacter.SetActive(true);
        femaleCharacter.SetActive(false);
        GenderManager.SaveGender(0); // Storing customization
    }
    // Method sets the female prefabe to active (visible), and male inactive
    public void SelectFemale()
    {
        maleCharacter.SetActive(false);
        femaleCharacter.SetActive(true);
        GenderManager.SaveGender(1); // Storing customization
    }
}
