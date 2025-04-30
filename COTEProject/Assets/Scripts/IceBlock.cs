using UnityEngine;

public class IceBlock : MonoBehaviour
{
    public float disappearDelay = 1f;
    public bool isCorrectPath = false;
    public float respawnTime = 30f;

    private bool hasTriggered = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        // Save original transform in case you want to reset position/rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true;

        if (!isCorrectPath)
        {
            Debug.Log($"{gameObject.name} stepped on — will disappear in {disappearDelay}s");
            Invoke(nameof(Disappear), disappearDelay);
        }
    }

    void Disappear()
    {
        Debug.Log($"{gameObject.name} disappearing!");
        gameObject.SetActive(false);

        // Schedule the respawn
        Invoke(nameof(Respawn), respawnTime);
    }

    void Respawn()
    {
        Debug.Log($"{gameObject.name} respawned!");
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        gameObject.SetActive(true);
        hasTriggered = false; // Allow it to trigger again
    }
}