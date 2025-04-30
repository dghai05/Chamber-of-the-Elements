using UnityEngine;

public class EarthLvlPuzzleExit : MonoBehaviour
{
    [SerializeField] private Transform stage4StartPoint; // final start position

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Checkpoint.lastCheckpointPosition = stage4StartPoint.position;
        }
    }
}
