using UnityEngine;
using UnityEngine.EventSystems;
//This class deals with which item is selected to show its panel
public class SelectedShopItem : MonoBehaviour, IPointerClickHandler
{
    public GameObject panel;
    public ShowSelectedShopItem manager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (manager != null && panel != null)
        {
            manager.ShowPanel(panel);
        }
    }
}
