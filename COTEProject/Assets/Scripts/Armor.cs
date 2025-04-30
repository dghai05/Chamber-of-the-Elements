using DefaultNamespace;
using UnityEngine;

public class Armor:Item
{
    private int defenseBoost;
    
    public Armor(string itemName, string description, int price, int itemCount, bool isStackable, int maxStackSize, Sprite icon, int defenseBoost)
        : base(itemName, description, price, itemCount, isStackable, maxStackSize, icon)
    {
        ItemName = itemName;
        Description = description;
        ItemCount = 1;
        IsStackable = false;
        MaxStackSize = 1;
        Icon = icon;
        DefenseBoost = defenseBoost;
    }

    public static Armor IronArmor(int count)
    {
        return new Armor (
            itemName: "Iron Armor",
            description: "Armor",
            price: 300,
            itemCount: count,
            isStackable: false,
            maxStackSize: 1,
            icon: null,
            defenseBoost: 2
        );
    }
    public int DefenseBoost
    {
        get { return defenseBoost; }
        set { defenseBoost = value; }
    }
    
    public override void Use(Player player)
    {
        if (player.IsArmorEquipped)
        {
            Debug.Log("Armor already equipped.");
            return;
        }

        player.IsArmorEquipped = true;
        player.Defence += DefenseBoost;
        player.CharacterController.armorVisual?.SetActive(true);
    }

    public void Remove(Player player)
    {
        if (!player.IsArmorEquipped)
            return;

        player.Defence -= DefenseBoost;
        player.IsArmorEquipped = false;
        player.CharacterController.armorVisual?.SetActive(false);
    }
}