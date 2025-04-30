using DefaultNamespace;
using UnityEngine;


public class EarthAbilityTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && AbilityLockManager.Instance != null)
            {
                AbilityLockManager.Instance.UnlockShield();
                player.UnlockShieldAbility();
          
                // Force UI update
                player.playerUI?.UpdateShieldStatus(
                    PlayerUI.ShieldState.Ready,
                    true
                );
            }
        }
    }
}