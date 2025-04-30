using System;
using DefaultNamespace;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro; 
public class ShopManager : Shop
{
    public List<Item> startingShopItems;
    public GameObject shopPanel;
    public TMP_Text coinText;
    public TMP_Text popUpText; 
    public GameObject popUpPanel;

    //Creating set shop inventory at start of game
    void Awake()
    {
        SetInventory(startingShopItems);
    }
    
    void Start()
    {
        CloseShop();
        shopPanel.SetActive(false);
        popUpPanel.SetActive(false);
        UpdateCoinUI();
    }
    public void OpenShop(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.isInPopUp = true; 
            shopPanel.SetActive(true);
            UpdateCoinUI();
        }
    }
    

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        PlayerController.isInPopUp = false;
    }
    
    //Basic purchase system
    public void BuyItem(Item item)
    {
        if (item is Armor)
        {
            if (Player.User.HasArmor)
            {
                ShowPopUp("You can only purchase this item once!");
                return;
            }
            
            Player.User.HasArmor = true; 
        }


        if (!CheckBalance(Player.User, item))
        {
            ShowPopUp("Insufficient balance. Collect more coins.");
            return;
        }

        Player.User.RemoveCoins(item.Price);
        Player.User.Inventory.AddItem(item);
        RemoveItemFromShop(item);
        UpdateCoinUI();
        Player.User.UpdateUI();
        Player.User.playerUI.CheckArmorVisibility();
        ShowPopUp($"{item.ItemName} added to inventory!");
        
    }

    
    //Speed potion purchase
    public void BuySpeedPotion()
    {
        BuyItem(Consumable.SpeedPotion(1));
    }
    
    //Shield potion purchase
    public void BuyShieldPotion()
    {
        BuyItem(Consumable.ShieldPotion(1));
    }
    
    //Health potion purchase
    public void BuyHealthPotion()
    {
        BuyItem(Consumable.HealthPotion(1));
    }
    
    //Armor purchase
    public void BuyIronArmor()
    {
        BuyItem(Armor.IronArmor(1));
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = Player.User.CoinBalance.ToString();
        }
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
        popUpPanel.SetActive(false);
    }
}
