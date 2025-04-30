using DefaultNamespace;
using UnityEngine;

public class Weapon:Item
{
    private int attackPower;
    private int attackSpeed;
    private string weaponType;
    private string element;
    private bool isRanged;
    public int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }     
    }

    public int AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }

    public string WeaponType
    {
        get { return weaponType; }
        set { weaponType = value; }
    }

    public string Element
    {
        get { return element; }
        set { element = value; }
    }

    public bool IsRanged
    {
        get { return isRanged; }
        set { isRanged = value; }
    }

    public Weapon(string itemName, string description, int price, int itemCount, Sprite icon,
        int attackPower, int attackSpeed, string weaponType, string element, bool isRanged,
        bool isStackable, int maxStackSize)
        : base(itemName, description, price, itemCount, isStackable, maxStackSize, icon)
    {
        ItemName = itemName;
        Description = description;
        ItemCount = 0;
        IsStackable = false;
        Icon = icon;
        AttackPower = attackPower;
        AttackSpeed = attackSpeed;
        WeaponType = weaponType;
        Element = element;
        IsRanged = isRanged;
        MaxStackSize = 1;
    }
}
