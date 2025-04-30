using UnityEngine;

public class TriggerMemoryGame : MonoBehaviour
{
    public MemoryGameManager memoryGameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Memory game triggered by player.");
            memoryGameManager.OpenMemoryGame();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            memoryGameManager.CloseMemoryGame();
        }
    }

}
