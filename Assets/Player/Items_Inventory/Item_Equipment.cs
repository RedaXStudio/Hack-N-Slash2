using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Player.Items_Inventory;
using UnityEngine;
using Random = UnityEngine.Random;

//[CreateAssetMenu(fileName = "New Item", menuName = "Equipments")]
public class Item_Equipment : Item
{
   [Header("Equipments Settings")]
   
   public int itemLvl;

   public ItemRarity itemRarity;

   public int chanceToDropMagic;
   public int chanceToDropRare;

   public int implicitArmorValue;

   public int implicitEnergyShieldValue;

   public static int chanceToGetNoModifier = 50;
   
   [NonSerialized] public int minimalNbrOfModifiers;

   //public Item_Modifier[] itemModifiers = new Item_Modifier[0];
   public List<Item_Modifier> itemModifiers = new List<Item_Modifier>();
   
   public Item_Equipment(int xSlotTaken, int ySlotTaken, bool stackable, int ammount, int relativeChanceToLoot, Sprite sprite, ItemRarity rarity, int itemLvl, int implicitArmor) : 
      base(xSlotTaken, ySlotTaken, stackable, ammount, relativeChanceToLoot, sprite)
   {
      itemRarity = rarity;
      this.itemLvl = itemLvl;
      implicitArmorValue = implicitArmor;
      
      UpdateRarity();
   }

   public void SetItem(int xSlotTaken, int ySlotTaken, bool stackable, int ammount, Sprite sprite, ItemRarity rarity, int itemLvl, int implicitArmor)
   {
      this.xSlotTaken = xSlotTaken;
      this.ySlotTaken = ySlotTaken;
      this.stackable = stackable;
      this.ammount = ammount;
      this.sprite = sprite;
      
      itemRarity = rarity;
      this.itemLvl = itemLvl;
      implicitArmorValue = implicitArmor;
      
      UpdateRarity();
   }

   public void SetRarity(ItemRarity itemRarity)
   { 
      this.itemRarity = itemRarity;
      UpdateRarity();
      //Debug.Log("minimalNbrOfMod : " + minimalNbrOfModifiers + " rarity : " + itemRarity);
   }
   
   public void UpdateRarity()
   {
      switch (itemRarity)
      {
         case ItemRarity.normal :
            itemModifiers = new List<Item_Modifier>(0);
            //Debug.Log("Rarity Set To : normal");
            break;
         case ItemRarity.magic :
            minimalNbrOfModifiers = 2;
            itemModifiers = new List<Item_Modifier>(3);
            //Debug.Log("Rarity Set To : magic");
            break;
         case ItemRarity.rare :
            minimalNbrOfModifiers = 3;
            itemModifiers = new List<Item_Modifier>(6);
            //Debug.Log("Rarity Set To : Rare");
            break;
         /*case EquipmentItemRarity.unique : 
            prefixModifier = new Item_Modifier[6];
            break;*/
      }
   }

   public void UpgradeToRareItem()
   {
      if (itemRarity == ItemRarity.normal)
      {
         itemRarity = ItemRarity.rare;
         UpdateRarity();
         RollNewModifier(itemModifiers, out itemModifiers, minimalNbrOfModifiers);
      }
      else if (itemRarity == ItemRarity.magic)
      {
         itemRarity = ItemRarity.rare;
         UpdateRarity();
         
         List<Item_Modifier> newMods = new List<Item_Modifier>(6 - itemModifiers.Count);
         RollNewModifier(newMods, out newMods, 1);
         itemModifiers.AddRange(newMods);
      }
   }

   public void UpgradeToMagicItem()
   {
      if (itemRarity == ItemRarity.normal)
      {
         itemRarity = ItemRarity.magic;
         UpdateRarity();
         RollNewModifier(itemModifiers, out itemModifiers, minimalNbrOfModifiers);
      }
   }

   public void RollRandomRarity()
   { 
      int rarityRange = Random.Range(0, 100);

      if (rarityRange <= chanceToDropRare)
      {
         UpgradeToRareItem();
      }else if (rarityRange <= chanceToDropMagic)
      {
         UpgradeToMagicItem();
      }
      else
      {
         UpdateRarity();
         Debug.Log(name + " : No rarity");
      }
   }

   public virtual void RollNewModifier(List<Item_Modifier> modifiers, out List<Item_Modifier> outMods, int minimalNbrOfMod)
   {
      Debug.Log("Roll new mods : " + this);
      
      UpdateRarity();
      
      /*for (int i = 0; i < modifiers.Count; i++)
      {
         modifiers[i] = null;
      }*/
      
      modifiers.Clear();

      List<Item_Modifier> allModifierRollable = new List<Item_Modifier>();
      
      AddRollableMods(allModifierRollable);

      for (int i = 0; i < modifiers.Capacity - 1; i++)
      { 
         Item_Modifier currentMod = null;
         
         int relativTotal = 0;
          
         int indexMod = 0;
          
         foreach (var modifier in allModifierRollable) {
            relativTotal += modifier.relativeChanceToRollThisMod; 
         }

         int range = Random.Range(0, relativTotal);
          
         int countChance = 0;
          
         //Debug.Log("roll : " + range + " relative total : " + relativTotal);
         
         for (int j = 0; j < allModifierRollable.Count; j++) 
         {
            countChance += allModifierRollable[j].relativeChanceToRollThisMod;
            
            if (j < allModifierRollable.Count - 1) {
               //Debug.Log(countChance + " < " + range + " < " + (countChance + allModifierRollable[j + 1].relativeChanceToRollThisMod));
               if (countChance + allModifierRollable[j + 1].relativeChanceToRollThisMod >= range && countChance <= range) {
                  currentMod = allModifierRollable[j];
                  indexMod = j;
                  j = allModifierRollable.Count;
               } 
            }
            else {
               //Debug.Log("Last Modifier on the list");
               currentMod = allModifierRollable[j];
               indexMod = j;
            }
         }
         
         bool noMod = false;

         if (minimalNbrOfMod > 0) { 
            minimalNbrOfMod--; 
         }else { 
            if (chanceToGetNoModifier > Random.Range(0f, 100f)) { 
               noMod = true;
               //Debug.Log("NoMod : " + noMod);
            } 
         }
         
         //Apply Mod
         if (allModifierRollable.Count > 0 && !noMod) {
            if (Array.Find(itemModifiers.ToArray(), modifier => modifier?.GetType() == currentMod?.GetType()) == null) {
               if (currentMod.GetType().IsSubclassOf(typeof(Item_Modifer_RNG))) {
                  modifiers.Add(((Item_Modifer_RNG) currentMod).RollValue(itemLvl));
               }
               else if (currentMod.GetType().IsSubclassOf(typeof(Item_Modifier))) {
                  modifiers.Add(currentMod);
               }
               
               //Debug.Log("Modifier : " + i + " = " + currentMod.GetType());
               allModifierRollable.RemoveAt(indexMod);
            }
            else
            {
               //if the item already have this mod -1 in loop(find another mod)
               //Debug.Log("This Item already have : " + currentMod.GetType() + " modifier");
               i--;
            }
         } 
      }
      
      outMods = modifiers;
   }

   public virtual void AddRollableMods(List<Item_Modifier> allMods)
   {
      allMods.AddRange(Items_Data.modfiersForAllItems);
   }
}
