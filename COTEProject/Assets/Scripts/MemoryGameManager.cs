using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

// 0 = Red, 1 = Blue, 2 = Green, 3 = Yellow : colour code for sequence
public class MemoryGameManager : MonoBehaviour
{
    public GameObject memoryGamePanel;
    public GameObject[] highlightOverlays; 
    public Button[] colorButtons;          
    public Button startButton;
    public Button exitButton;
    public GameObject bridge;
    public GameObject resultPopupPanel;
    public TMP_Text resultPopupText;


    private List<int> sequence = new List<int>();
    private int playerIndex = 0;
    private bool acceptingInput = false;

    void Start()
    {
        startButton.onClick.AddListener(PlaySequence);
        exitButton.onClick.AddListener(CloseMemoryGame);

        foreach (var btn in colorButtons)
        {
            btn.onClick.AddListener(() => OnColorButtonClicked(btn));
        }

        DisableColorButtons();
        resultPopupText.text = "";

        // Hardcoded sequence: red blue blue blue green yellow green red blue yellow
        sequence = new List<int> {
            0, 1, 1, 1, 2, 3, 2, 0, 1, 3
        };
    }

    void PlaySequence()
    {
        resultPopupText.text = "";
        playerIndex = 0;
        DisableColorButtons();
        StartCoroutine(PlaySequenceCoroutine());
    }

    IEnumerator PlaySequenceCoroutine()
    {
        foreach (GameObject overlay in highlightOverlays)
            overlay.SetActive(false);

        yield return new WaitForSeconds(1f);

        foreach (int index in sequence)
        {
            highlightOverlays[index].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            highlightOverlays[index].SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }

        acceptingInput = true;
        EnableColorButtons();
    }

    public void OnColorButtonClicked(Button btn)
    {
        if (!acceptingInput) return;

        int index = System.Array.IndexOf(colorButtons, btn);

        StartCoroutine(FlashOverlay(index));

        if (index == sequence[playerIndex])
        {
            playerIndex++;
            if (playerIndex >= sequence.Count)
            {
                bridge.SetActive(true);
                ShowPopup("Sequence complete! The bridge has now appeared");
                acceptingInput = false;
                DisableColorButtons();
                StartCoroutine(HideResultAfterDelay());
            }
        }
        else
        {
            ShowPopup("Wrong sequence! Press start to try again.");
            playerIndex = 0;
            acceptingInput = false;
            DisableColorButtons();
            StartCoroutine(HideResultAfterDelay());
        }
    }
    
    void ShowPopup(string message)
    {
        resultPopupText.text = message;
        resultPopupPanel.SetActive(true);
    }

    IEnumerator FlashOverlay(int index)
    {
        highlightOverlays[index].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        highlightOverlays[index].SetActive(false);
    }


    void EnableColorButtons()
    {
        foreach (var btn in colorButtons)
            btn.interactable = true;
    }

    void DisableColorButtons()
    {
        foreach (var btn in colorButtons)
            btn.interactable = false;
    }
    public void OpenMemoryGame()
    {
        memoryGamePanel.SetActive(true);           
        resultPopupText.text = "";                   
        playerIndex = 0;       
        bridge.SetActive(false);    
        DisableColorButtons();  
        PlayerController.isInPopUp = true;
    }

    public void CloseMemoryGame()
    {
        memoryGamePanel.SetActive(false);
        PlayerController.isInPopUp = false;
    }
    
    IEnumerator HideResultAfterDelay()
    {
        yield return new WaitForSeconds(7f);
        resultPopupPanel.SetActive(false);

        if (!bridge.activeSelf)
        {
            playerIndex = 0;
            acceptingInput = true;
            EnableColorButtons();
        }
    }
}