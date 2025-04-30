using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCSystem : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button responseButton;
    [SerializeField] private TextMeshProUGUI responseButtonText;
    [SerializeField] private GameObject interactPrompt;

    [Header("Dialogue Settings")]
    [SerializeField] private string[] npcDialogueLines;
    [SerializeField] private string[] playerResponsePrompts;

    private bool playerInRange = false;
    private int currentLineIndex = 0;
    private Transform playerTransform;

    void Start()
    {
        dialogueCanvas.SetActive(false);
        responseButton.gameObject.SetActive(false);
        interactPrompt.SetActive(false);
        
        var playerObj = FindObjectOfType<PlayerController>(); 
        if (playerObj != null) playerTransform = playerObj.transform;
    }

    void Update()
    {

        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !PlayerController.dialogue)
        {
            StartDialogue();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        playerInRange = true;
        interactPrompt.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        playerInRange = false;
        interactPrompt.SetActive(false);
        if (PlayerController.dialogue) EndDialogue();
    }


    public void StartDialogue()
    {
        PlayerController.dialogue = true;
        interactPrompt.SetActive(false); // Hide prompt when dialogue begins
        dialogueCanvas.SetActive(true);
        currentLineIndex = 0;
        ShowCurrentDialogue();
    }

    void ShowCurrentDialogue()
    {
        // Show NPC line
        dialogueText.text = npcDialogueLines[currentLineIndex];
        
        // Set and show response button if there are more lines
        if (currentLineIndex < playerResponsePrompts.Length)
        {
            responseButtonText.text = playerResponsePrompts[currentLineIndex];
            responseButton.gameObject.SetActive(true);
        }
    }

    // Called by the response button's OnClick event
    public void OnResponseClicked()
    {
        currentLineIndex++;
        
        if (currentLineIndex < npcDialogueLines.Length)
        {
            ShowCurrentDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        PlayerController.dialogue = false;
        dialogueCanvas.SetActive(false);
        currentLineIndex = 0;
    }
    
    void UpdatePromptVisibility()
    {
        bool shouldShow = playerInRange && !PlayerController.dialogue;
        interactPrompt.gameObject.SetActive(shouldShow);
    }
    
}
