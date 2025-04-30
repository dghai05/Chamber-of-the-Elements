using Unity.Cinemachine;
using UnityEngine;

/* This class is used to spawn the customized character
*/
public class CreatePlayer : MonoBehaviour
{
	// Creating needed refercnces
    public Transform spawnPoint;

	// Calling spawn character when game starts
    void Start()
    {
        GameObject player = CustomizationManager.instance.Player; 
        player.transform.position = spawnPoint.position;
        player.transform.rotation = Quaternion.identity;
    }
}
