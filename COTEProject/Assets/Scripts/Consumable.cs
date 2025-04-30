using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class Consumable : Item
{
    private float healthRestore;
    private float defenseUp;
    private float speedUp;

    public float HealthRestore

    {
        get { return healthRestore; }
        set { healthRestore = value; }
    }

    public float DefenseUp
    {
        get { return defenseUp; }
        set { defenseUp = value; }
    }

    public float SpeedUp
    {
        get { return speedUp; }
        set { speedUp = value; }
    }

    public static Consumable HealthPotion(int count)
    {
        return new Consumable(
            itemName: "Health Potion",
            description: "Restores player to full health.",
            price: 100,
            itemCount: count,
            isStackable: true,
            maxStackSize: 10,
            icon: null,
            healthRestore: 100,
            defenseUp: 0,
            speedUp: 0
        );
    }

    public static Consumable ShieldPotion(int count)
    {
        return new Consumable(
            itemName: "Shield Potion",
            description: "Temporarily increases defense.",
            price: 100,
            itemCount: count,
            isStackable: true,
            maxStackSize: 10,
            icon: null,
            healthRestore: 0,
            defenseUp: 1.5f,
            speedUp: 0
        );
    }

    public static Consumable SpeedPotion(int count)
    {
        return new Consumable(
            itemName: "Speed Potion",
            description: "Doubles speed for 30 seconds.",
            price: 100,
            itemCount: count,
            isStackable: true,
            maxStackSize: 10,
            icon: null,
            healthRestore: 0,
            defenseUp: 0f,
            speedUp: 1.2f
        );
    }

    public Consumable(string itemName, string description, int price, int itemCount, bool isStackable, int maxStackSize,
        Sprite icon, int healthRestore, float defenseUp, float speedUp)
        : base(itemName, description, price, itemCount, isStackable, maxStackSize, icon)
    {
        ItemName = itemName;
        Description = description;
        ItemCount = itemCount;
        IsStackable = true;
        MaxStackSize = maxStackSize;
        Icon = icon;
        HealthRestore = healthRestore;
        DefenseUp = defenseUp;
        SpeedUp = speedUp;
    }

    public override void Use(Player player)
    {
        // Restore health
        if (HealthRestore > 0)    
        {
            player.Health += HealthRestore;
            if (player.Health > player.MaxHealth)
            {
                player.Health = player.MaxHealth;
            }
        }
    // Apply temporary buffs
        if (DefenseUp > 0)
        {
            float originalDefense = player.Defence;
            player.Defence *= (int)DefenseUp; // Multiply by the defense factor

            // Start coroutine to revert defense after 30 seconds
            player.StartCoroutine(RevertDefense(player, originalDefense, 30f));
        }

        if (SpeedUp > 0)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null)
            {
                float originalMultiplier = pc.SpeedMultiplier;
                pc.SpeedMultiplier *= SpeedUp;
                player.StartCoroutine(RevertSpeedBuff(pc, originalMultiplier, 30f));
            }
        }

    }

        // Coroutine to revert defense after the buff duration
        IEnumerator RevertDefense(Player player, float originalDefense, float duration)
        {
            yield return new WaitForSeconds(duration);
            player.Defence = (int)originalDefense;
            Debug.Log("Defense buff expired.");
        }

        // Coroutine to revert max health after the buff duration
        IEnumerator RevertSpeedBuff(PlayerController pc, float originalMultiplier, float duration)
        {
            yield return new WaitForSeconds(duration);
            pc.SpeedMultiplier = originalMultiplier;
            Debug.Log("Speed buff expired.");
        }
}