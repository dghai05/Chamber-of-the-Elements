using UnityEngine;

public class EarthLvlStoneInteraction : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private KeyCode interactKey = KeyCode.F;
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private EarthLvlPuzzle puzzleManager; 
    [SerializeField] private GameObject interactPrompt;

    public enum CardinalDirection { North, East, South, West }
    public CardinalDirection stoneDirection;
    public Transform symbolFace;
    
    private GameObject player;
    private Collider stoneCollider;


    private void Start()
    {
        stoneCollider = GetComponent<Collider>();
        if (stoneCollider == null)
        {
            stoneCollider = gameObject.AddComponent<BoxCollider>();
            Debug.LogWarning("Added BoxCollider to " + gameObject.name);
        }
        // Ensure collider is NOT a trigger
        stoneCollider.isTrigger = false;
        
        interactPrompt.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        if (puzzleManager == null)
            Debug.LogError("Puzzle manager not assigned to stone!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey) && IsPlayerInRange())
        {
            transform.Rotate(0, rotationSpeed, 0);
            puzzleManager.CheckPuzzleSolution(); // Notify the puzzle manager
        }
        
        if (IsPlayerInRange())
        {
            interactPrompt.SetActive(true);
        }

        else
        {
            interactPrompt.SetActive(false);
        }
    }
    
    private bool IsPlayerInRange()
    {
        return Vector3.Distance(player.transform.position, transform.position) <= interactionRange;
    }
}