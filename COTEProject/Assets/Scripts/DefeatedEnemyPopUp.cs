using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DefeatedEnemyPopUp : MonoBehaviour
{
    public GameObject popupPanel;
    public TMP_Text popupText;
    public Button closeButton;

    public GameObject earthPortal;
    public GameObject waterPortal;
    public GameObject airPortal;
    public GameObject firePortal;

    public GameObject earthTotem;
    public GameObject waterTotem;
    public GameObject airTotem;
    public GameObject fireTotem;
    public GameObject finalBoss;

    void Start()
    {
        int lastDefeated = PlayerPrefs.GetInt("LastDefeatedElement", -1);
        int highestCompleted = PlayerPrefs.GetInt("HighestCompletedLevel", -1);
        
        switch (lastDefeated)
        {
            case 0: if (earthPortal != null) earthPortal.SetActive(false); earthTotem.SetActive(true); break;
            case 1: if (waterPortal != null) waterPortal.SetActive(false); earthTotem.SetActive(true); waterTotem.SetActive(true); break;
            case 2: if (airPortal != null) airPortal.SetActive(false); earthTotem.SetActive(true); waterTotem.SetActive(true); airTotem.SetActive(true); break;
            case 3: if (firePortal != null) firePortal.SetActive(false); earthTotem.SetActive(true); waterTotem.SetActive(true); airTotem.SetActive(true); fireTotem.SetActive(true); finalBoss.SetActive(true); break;
        }

        switch (lastDefeated + 1)
        {
            case 1: if (waterPortal != null) waterPortal.SetActive(true); break;
            case 2: if (airPortal != null) airPortal.SetActive(true); break;
            case 3: if (firePortal != null) firePortal.SetActive(true); break;
        }
        
        if (lastDefeated != -1)
        {
            string sceneName = GetSceneNameFromIndex(lastDefeated);
            string nextLevel = GetNextLevelName(lastDefeated);

            popupText.text = $"You have successfully defeated the enemy" +
                             $"\nand collected the {sceneName} totem." +
                             $"\nYou can now move on to the {nextLevel} Level.";
            popupPanel.SetActive(true);
            PlayerController.isInPopUp = true;
            closeButton.onClick.AddListener(ClosePopup);
            PlayerPrefs.DeleteKey("LastDefeatedElement");
        }
        else
        {
            popupPanel.SetActive(false);
            PlayerController.isInPopUp = false;
        }
    }

    private string GetSceneNameFromIndex(int index)
    {
        switch (index)
        {
            case 0: return "Earth";
            case 1: return "Water";
            case 2: return "Air";
            case 3: return "Fire";
            default: return "Unknown";
        }
    }

    private string GetNextLevelName(int index)
    {
        switch (index + 1)
        {
            case 1: return "Water";
            case 2: return "Air";
            case 3: return "Fire";
            default: return "Final Battle";
        }
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
        PlayerController.isInPopUp = false;
    }
}
