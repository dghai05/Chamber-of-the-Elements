using UnityEngine;

public class FireAbilityTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && AbilityLockManager.Instance != null)
            {
                AbilityLockManager.Instance.UnlockTripleDmg();
                player.UnlockTripleDmgAbility(); // Use method instead
                Debug.Log("Triple damage ability unlocked!");
            }
        }
    }
}