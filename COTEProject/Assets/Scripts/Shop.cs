using System;
using DefaultNamespace;
using UnityEngine;
using System.Collections.Generic;
public class Shop : MonoBehaviour
{
    protected Inventory shopInventory = new Inventory();

    public virtual void SetInventory(List<Item> items)
    {
        shopInventory.AddItem(Armor.IronArmor(1));
        shopInventory.AddItem(Consumable.HealthPotion(8));
        shopInventory.AddItem(Consumable.SpeedPotion(8));
        shopInventory.AddItem(Consumable.ShieldPotion(8));
    }

    public virtual bool CheckBalance(Player player, Item item)
    {
        return player.CoinBalance >= item.Price;
    }

    public virtual void RemoveItemFromShop(Item item)
    {
        if (item is Armor armor)
            shopInventory.Armors.Remove(armor);
        else if (item is Consumable consumable)
            shopInventory.Consumables.Remove(consumable);
    }

    public Inventory GetShopInventory()
    {
        return shopInventory;
    }
}
