using UnityEngine;

public class TorchPuzzle : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey = KeyCode.F;
    [SerializeField] private float interactionRange = 10f;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject disable;

    
    private GameObject player;
    private Collider torchCollider;


    private void Start()
    {
        fire.GetComponent<MeshRenderer>().enabled = false;
        torchCollider = GetComponent<Collider>();
        if (torchCollider == null)
        {
            torchCollider = gameObject.AddComponent<SphereCollider>();
            Debug.LogWarning("Added SphereCollider to " + gameObject.name);
        }
        // Ensure collider is NOT a trigger
        torchCollider.isTrigger = false;
        
        interactPrompt.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey) && IsPlayerInRange())
        {
            fire.GetComponent<MeshRenderer>().enabled = true;
            disable.SetActive(false);
        }

        if (IsPlayerInRange())
        {
            interactPrompt.SetActive(true);
        }

        else
        {
            interactPrompt.SetActive(false);
        }
    }
    
    private bool IsPlayerInRange()
    {
        return Vector3.Distance(player.transform.position, transform.position) <=  interactionRange;
    }
}

