using UnityEngine;

namespace Assets.Player.Items_Inventory
{
    public class Item : ScriptableObject
    {
        public Item(int xSlotTaken, int ySlotTaken, bool stackable, int ammount, int relativeChanceToLoot, Sprite sprite)
        {
            this.xSlotTaken = xSlotTaken;
            this.ySlotTaken = ySlotTaken;

            this.stackable = stackable;
            this.ammount = ammount;

            this.relativeChanceToLoot = relativeChanceToLoot;
            
            this.sprite = sprite;
        }

        public string itemName;
        
        public int xSlotTaken;
        public int ySlotTaken;

        [TextArea(10, 3)] public string description;

        public bool stackable;
        public int ammount;

        public Sprite sprite;
        public Mesh itemMesh;
        public Material itemMaterial;

        public int relativeChanceToLoot;
    
        public enum ItemRarity
        {
            normal, magic, rare, unique
        }

        public void SetItem(int xSlotTaken, int ySlotTaken, bool stackable, int ammount, Sprite sprite)
        {
            this.xSlotTaken = xSlotTaken;
            this.ySlotTaken = ySlotTaken;

            this.stackable = stackable;
            this.ammount = ammount;
            
            this.sprite = sprite;
        }

        public Item ShallowCopy()
        {
            return (Item) MemberwiseClone();
        }
    }
}
