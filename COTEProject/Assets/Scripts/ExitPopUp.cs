using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// Class used for managing Pop-Up UI 
public class ExitPopUp : MonoBehaviour
{
	// Created needed references
    public GameObject exitPopupPanel;
    public GameObject backgroundUI;
    public Button exitButton;
    public Button yesButton;
    public Button noButton;

	// ****** Issue loading next scene ****
    public string nextScene = "MainHub";

	// Dealing with UI buttons and UI visibility
    void Start()
    {
        exitPopupPanel.SetActive(false);
        exitButton.onClick.AddListener(ShowPopup);
        yesButton.onClick.AddListener(LoadNextScene);
        noButton.onClick.AddListener(HidePopup);
    }

	// Method for showing pop-up and hiding customization UI
    void ShowPopup()
    {
        exitPopupPanel.SetActive(true);
        backgroundUI.SetActive(false);
    }

	// Method for hiding pop-up and re-showing customization UI
    void HidePopup()
    {
        exitPopupPanel.SetActive(false);
        backgroundUI.SetActive(true);
    }

	// Method to load next scene when player completes customization
    void LoadNextScene()
    {
        CustomizationManager.instance.SaveCustomizationData(
            GenderManager.GetGender(),
            CustomizationManager.instance.skinColor,
            CustomizationManager.instance.hairColor,
            CustomizationManager.instance.clothingColor
        );
        CustomizationManager.instance.CreatePlayer();
        SceneManager.LoadScene(nextScene);
    }
}