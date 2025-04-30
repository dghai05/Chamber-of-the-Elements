using UnityEngine;

public class AirAbilityTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && AbilityLockManager.Instance != null)
            {
                AbilityLockManager.Instance.UnlockDoubleJump();
                player.UnlockDoubleJumpAbility(); // Use method instead
                Debug.Log("Double Jump ability unlocked!");
            }
        }
    }
}