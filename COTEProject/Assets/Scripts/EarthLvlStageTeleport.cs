using UnityEngine;

public class EarthLvlStageTeleport : MonoBehaviour
{
    [SerializeField] private Transform stage2StartPoint; // parkour stage start position

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched door!"); 
            Checkpoint.lastCheckpointPosition = stage2StartPoint.position;
        }
    }
}
