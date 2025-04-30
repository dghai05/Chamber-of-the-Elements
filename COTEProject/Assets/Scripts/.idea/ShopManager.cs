namespace DefaultNamespace;

public class ShopManager : Shop
{
    public Player player;
    public List<Item> startingShopItems;
    public Text coinText;               
    public Text popUpText;           
    public GameObject popUpPanel;   

    private void Start()
    {
        SetInventory(startingItems);
        UpdateCoinUI();
    }

    public void BuyItem(Item item)
    {
        if (!CheckBalance(player, item))
        {
            ShowPopUp("Insufficient balance. Collect more coins.");
            return;
        }

        player.RemoveCoins(item.price);
        player.Inventory.SortItemsByCat(item);
        RemoveItemFromShop(item);
        UpdateCoinUI();
        ShowPopUp($"{item.ItemName} added to inventory!");
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = "Coins: " + player.CoinBalance;
    }

    private void ShowPopUp(string message)
    {
        if (popUpPanel != null && popUpText != null)
        {
            popUpText.text = message;
            popUpPanel.SetActive(true);
            CancelInvoke(nameof(HidePopUp));
            Invoke(nameof(HidePopUp), 4f); 
        }
    }

    private void HidePopUp()
    {
        if (popUpPanel != null)
            popUpPanel.SetActive(false);
    }
}