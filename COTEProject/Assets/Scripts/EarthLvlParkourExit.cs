using UnityEngine;

public class EarthLvlParkourExit : MonoBehaviour
{
    [SerializeField] private Transform stage3StartPoint; // puzzle start point
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Checkpoint.lastCheckpointPosition = stage3StartPoint.position;
        }
    }
}
