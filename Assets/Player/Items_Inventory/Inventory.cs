using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    //create inventory units and player
    public Player playerLinkedTo;

    public Inventory_UI inventoryUi;

    //Items Equipment
    public delegate void EachModifierEffect();

    public EachModifierEffect eachModifierEffect;
    
    public Item_Helmet helmet;

    public Item_Chest chest;

    public Item_Gloves gloves;

    public Item_Boots boots;

    public Item_Belt belt;

    public Item_Amulet amulet;

    public Item_Ring ring1;

    public Item_Ring ring2;

    public StatsHolder statsHolder = new StatsHolder();
    
    public static List<Item_Modifier> helmetMods;
    public static List<Item_Modifier> chestMods;
    public static List<Item_Modifier> glovesMods;
    public static List<Item_Modifier> bootsMods;
    public static List<Item_Modifier> beltMods;
    public static List<Item_Modifier> leftRingMods;
    public static List<Item_Modifier> rightRingMods;
    public static List<Item_Modifier> amuletMods;

    public void storeEquipmentsMods()
    {
        if (helmet) {
            helmetMods = helmet.itemModifiers;
        }
        if (chest) {
            chestMods = chest.itemModifiers;
        }
        if (gloves) {
            glovesMods = gloves.itemModifiers;
        }
        if (boots) {
            bootsMods = boots.itemModifiers;
        }
        if (belt) {
            beltMods = belt.itemModifiers;
        }
        if (ring1) {
            leftRingMods = ring1.itemModifiers;
        }
        if (ring2) {
            rightRingMods = ring2.itemModifiers;
        }
        if (amulet) {
            amuletMods = amulet.itemModifiers;
        }
    }

    public void RestorEquipmentsMods()
    {
        if (helmet) {
            helmet.itemModifiers = helmetMods;

            ((Item_Equipment) inventoryUi.helmetSlot.itemUIHold.GetComponent<ItemUI>().Item).itemModifiers = helmetMods;
        }
        if (chest) {
            chest.itemModifiers = helmetMods;

            ((Item_Equipment) inventoryUi.chestSlot.itemUIHold.GetComponent<ItemUI>().Item).itemModifiers = chestMods;
        } 
        if (gloves) {
            gloves.itemModifiers = glovesMods;

            ((Item_Equipment) inventoryUi.glovesSlot.itemUIHold.GetComponent<ItemUI>().Item).itemModifiers = glovesMods;
        }
        if (boots) {
            boots.itemModifiers = bootsMods;

            ((Item_Equipment) inventoryUi.bootsSlot.itemUIHold.GetComponent<ItemUI>().Item).itemModifiers = bootsMods;
        }
        if (belt) {
            belt.itemModifiers = beltMods;

            ((Item_Equipment) inventoryUi.beltSlot.itemUIHold.GetComponent<ItemUI>().Item).itemModifiers = beltMods;
        }
        if (ring1) {
            ring1.itemModifiers = leftRingMods;

            ((Item_Equipment) inventoryUi.leftRingSlot.itemUIHold.GetComponent<ItemUI>().Item).itemModifiers = leftRingMods;
        }
        if (ring2) {
            ring2.itemModifiers = rightRingMods;

            ((Item_Equipment) inventoryUi.rightRingSLot.itemUIHold.GetComponent<ItemUI>().Item).itemModifiers = rightRingMods;
        }
        if (amulet) {
            amulet.itemModifiers = amuletMods;

            ((Item_Equipment) inventoryUi.amuletSlot.itemUIHold.GetComponent<ItemUI>().Item).itemModifiers = amuletMods;
        }
    }

    public void UpdateEquipments()
    {
        //Reset Stats
        statsHolder.ResetStats();
        
        //call modifier
        if (eachModifierEffect != null)
        {
            eachModifierEffect();
        }
    }

    public void EquipItem(Item_Equipment equipment)
    {
        //EquipItem
        if (equipment.GetType() == typeof(Item_Helmet))
        {
            helmet = (Item_Helmet) equipment; 
        }

        //apply Effect
        List<Item_Modifier> allModifier = new List<Item_Modifier>();
        
        allModifier.AddRange(helmet.itemModifiers);

        foreach (var modifier in allModifier)
        {
            if (modifier != null)
            {
                modifier.statsHolder = statsHolder;
                eachModifierEffect += modifier.ModiferEffect;
            }
        }
        
        UpdateEquipments();
        
        playerLinkedTo.UpdateInventoryAndPassiveTree();

        Debug.Log("Equip Item");
    }

    public void RemoveItem(Item_Equipment equipment, ItemEquipment_Slot.ItemEquipmentSlotType slotType)
    {
        foreach (var modifier in equipment.itemModifiers)
        {
            if (modifier != null)
            {
                modifier.statsHolder = null;
                //can be improved
                eachModifierEffect -= modifier.ModiferEffect;
            }
        }

        if (equipment.GetType() == typeof(Item_Helmet)) {
           helmet = null; 
        }
        if (equipment.GetType() == typeof(Item_Chest)) {
            chest = null; 
        }
        if (equipment.GetType() == typeof(Item_Gloves)) {
            gloves = null; 
        }
        if (equipment.GetType() == typeof(Item_Boots)) {
            boots = null; 
        }
        if (equipment.GetType() == typeof(Item_Ring)) {
            if (slotType == ItemEquipment_Slot.ItemEquipmentSlotType.Ring1) {
                ring1 = null;
            }
            else {
                ring2 = null;
            }
        }
        if (equipment.GetType() == typeof(Item_Amulet)) {
            amulet = null; 
        }
    }
}
