using UnityEngine;
/**** this is probably all wrong ****
**** unable to get it to follow player ***** 
*/
public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    
    public float rotationSpeed = 2f;
    public float height = 2f;
    public float distance = 2f;

    public float verticalRotationLimit = 80f; 

    private Vector3 offset;
    private float currentX = 0f;
    private float currentY = 0f;

    void Start()
    {
        offset = new Vector3(0, height, -distance); 
    }

    void Update()
    {
        HandleCameraRotation();  
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            transform.position = desiredPosition;
            transform.LookAt(player);  
        }
    }

    void HandleCameraRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            currentX -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            currentX += rotationSpeed * Time.deltaTime;
        }

        currentY = Mathf.Clamp(currentY, -verticalRotationLimit, verticalRotationLimit);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        offset = rotation * new Vector3(0, 0, -distance); 
    }
}
