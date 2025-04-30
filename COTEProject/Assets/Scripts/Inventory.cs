using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

namespace DefaultNamespace
{
    public class Inventory
    {
        
        private List<Weapon> weapons = new List<Weapon>();
        private List<Armor> armors = new List<Armor>();
        private List<Consumable> consumables = new List<Consumable>();
        
        public List<Weapon> Weapons { get { return weapons; } }
        public List<Armor> Armors { get { return armors; } }
        public List<Consumable> Consumables { get { return consumables; } }

        public Inventory()
        {
            Consumables.Add(Consumable.HealthPotion(1));
            Consumables.Add(Consumable.ShieldPotion(1));
            Consumables.Add(Consumable.SpeedPotion(1));
        }
        public void AddItem(Item item)
        {
            List<Item> targetList = item switch
            {
                Weapon => weapons.Cast<Item>().ToList(),
                Armor => armors.Cast<Item>().ToList(),
                Consumable => consumables.Cast<Item>().ToList(),
                _ => null
            };

            if (targetList == null)
            {
                Debug.LogWarning($"Unknown item type: {item.ItemName}");
                return;
            }

            // Try to find an existing item of the same name
            Item existing = targetList.Find(i => i.ItemName == item.ItemName);

            if (existing != null && item.IsStackable)
            {
                int newCount = existing.ItemCount + item.ItemCount;
                existing.ItemCount = Mathf.Min(existing.MaxStackSize, newCount);
                Debug.Log($"Stacked {item.ItemName}. New count: {existing.ItemCount}");
            }
            else
            {
                // Add a new item instance
                targetList.Add(item);
                Debug.Log($"Added new item: {item.ItemName} (x{item.ItemCount})");
            }
        }

        public void RemoveItem(Item item, int count = 1)
        {
            List<Item> targetList = item switch
            {
                Weapon => weapons.Cast<Item>().ToList(),
                Armor => armors.Cast<Item>().ToList(),
                Consumable => consumables.Cast<Item>().ToList(),
                _ => null
            };

            if (targetList == null)
            {
                Debug.LogWarning($"Unknown item type: {item.ItemName}");
                return;
            }

            Item existingItem = targetList.Find(i => i.ItemName == item.ItemName);

            if (existingItem == null)
            {
                Debug.LogWarning($"Item {item.ItemName} not found in inventory.");
                return;
            }

            if (existingItem.IsStackable)
            {
                if (existingItem.ItemCount > count)
                {
                    existingItem.ItemCount -= count;
                    Debug.Log($"Removed {count}x {item.ItemName}. Remaining: {existingItem.ItemCount}");
                }
                else
                {
                    existingItem.ItemCount = 0;
                    Debug.Log($"{item.ItemName} is now at 0.");
                }
            }
            else
            {
                // For non-stackable items, still remove them (since 1 = 1)
                targetList.Remove(existingItem);
                Debug.Log($"Removed {item.ItemName} (non-stackable)");
            }
        }

        public int GetConsumableCount(string itemName)
        {
            Consumable consumable = Consumables.Find(c => c.ItemName == itemName);
            if (consumable != null)
            {
                Debug.Log($"Consumable {itemName} was found in the list.");
                return consumable.ItemCount;
            }
            else
            {
                Debug.LogWarning($"Consumable {itemName} not found.");
                return 0;
            }
        }
        
    }
}