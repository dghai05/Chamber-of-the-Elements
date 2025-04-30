using UnityEngine;

public class DashDeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                // Only kill the player if they're not dashing and not in a safe zone
                if (!player.IsDashing && !player.isInsideSafeZone)
                {
                    Debug.Log("☠️ Player entered DEATH ZONE without dashing or being in a safe zone");
                    Player.User.Die(); // Replace with your actual death logic
                }
                else
                {
                    Debug.Log("🌀 Player dashed through death zone or is protected by safe zone");
                }
            }
        }
    }
}