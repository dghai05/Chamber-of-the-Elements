using UnityEngine;
using System.Collections.Generic;
//This class is used to show the description(panel) of the selected shop item
public class ShowSelectedShopItem : MonoBehaviour
{
    public List<GameObject> panels = new List<GameObject>(); 

    private GameObject currentPanel;

    public void ShowPanel(GameObject panelToShow)
    {
        if (currentPanel == panelToShow) return;

        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        panelToShow.SetActive(true);
        currentPanel = panelToShow;
    }
}
