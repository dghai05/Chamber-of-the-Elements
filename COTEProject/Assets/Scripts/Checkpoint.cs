using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector3 lastCheckpointPosition;
    public static bool isCheckpointActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lastCheckpointPosition = transform.position;
            Debug.Log("Checkpoint reached!");
            Debug.Log(lastCheckpointPosition);
        }
    }
}   