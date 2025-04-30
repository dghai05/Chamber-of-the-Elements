using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public float rotationSpeed = 20f;

    public int coinValue = 20;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0,rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.User.PickUpCoin(coinValue);
            Destroy(gameObject);
        }
    }
}
