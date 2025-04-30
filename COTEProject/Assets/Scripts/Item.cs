using UnityEngine;

namespace DefaultNamespace
{
    public class Item
    {
        private string itemName;
        private string description;
        private int price;
        private int itemCount;
        private bool isStackable;
        private int maxStackSize;
        private Sprite icon;

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        public int ItemCount
        {
            get { return itemCount; }
            set { this.itemCount = value; }
        }

        public bool IsStackable
        {
            get { return isStackable; }
            set { isStackable = value; }
        }

        public int MaxStackSize
        {
            get { return maxStackSize; }
            set { maxStackSize = value; }
        }

        public Sprite Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public Item(string itemName, string description, int price, int itemCount, bool isStackable, int maxStackSize, Sprite icon)
        {
            ItemName = itemName;
            Description = description;
            Price = price;
            ItemCount = 1;
            IsStackable = isStackable;
            MaxStackSize = maxStackSize;
            Icon = icon;
        }

        public virtual void Use(Player player)
        {}

    }
}