using UnityEngine;

public class EarthLvlParkourRespawn : MonoBehaviour
{
    
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private string mudTag = "Mud";
    private Rigidbody playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>(); // Cache the Rigidbody
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(mudTag))
        {
            Debug.Log("Touched Mud! Respawning...");
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        // Reset position
        transform.position = respawnPoint.position;

        // Reset physics 
        if (playerRigidbody != null)
        {
            playerRigidbody.linearVelocity = Vector3.zero; // Stop all movement
            playerRigidbody.angularVelocity = Vector3.zero;
        }

        // Optional: Teleport away briefly to force collision refresh
        transform.position += Vector3.up * 0.1f; 
    }
}
