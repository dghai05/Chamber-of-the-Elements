using UnityEngine;

public class CloudMovement : MonoBehaviour
{
	//Making reference to the two points the cloude will be moving between
	public Transform pointA; 
	public Transform pointB;
	//Moement speed of the cloud
	public float speed = 1f;
	
	private Vector3 position;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = pointA.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
		
		if (Vector3.Distance(transform.position, position) < 0.01f)
        {
            position = (position == pointB.position) ? pointA.position : pointB.position;
        }
    }
}
