using UnityEngine;
using UnityEngine.UI;
//This class is used to trigger open the shop panel when the player steps on the platform
public class OpenShopTrigger : MonoBehaviour
{
    public ShopManager shopManager;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Shop Trigger entered by: {other.name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Shop now open.");
            shopManager.OpenShop(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Shop now closed.");
        shopManager.CloseShop();
    }
}