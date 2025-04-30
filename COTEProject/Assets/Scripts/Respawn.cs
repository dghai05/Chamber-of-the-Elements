using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Respawn");
            Checkpoint.isCheckpointActive = true;
        }
    }
}
