using UnityEngine;

public class WaterAbilityTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && AbilityLockManager.Instance != null)
            {
                AbilityLockManager.Instance.UnlockDash();
                player.UnlockDashAbility(); // Use method instead
                Debug.Log("Dash ability unlocked!");
            }
        }
    }
}