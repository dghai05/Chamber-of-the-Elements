using UnityEngine;

public class SafeLandingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.isInsideSafeZone = true;

                if (!player.IsDashing)
                {
                    Debug.Log("💀 Player entered Safe Zone without dashing");
                    //Player.User.Die(); // Replace with your actual death logic
                }
                else
                {
                    Debug.Log("✅ Player dashed into Safe Zone");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.isInsideSafeZone = false;

                if (!player.IsDashing)
                {
                    Debug.Log("💀 Player exited Safe Zone without dashing");
                    Player.User.Die(); // Replace with your actual death logic
                }
                else
                {
                    Debug.Log("✅ Player dashed out of Safe Zone safely");
                }
            }
        }
    }
}