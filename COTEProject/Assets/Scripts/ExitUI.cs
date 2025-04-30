using UnityEngine;
//Class tha manages the closing of a UI
public class ExitUI : MonoBehaviour
{
    public GameObject popupUI;

    public void CloseShop()
    {
        popupUI.SetActive(false);
    }
}
