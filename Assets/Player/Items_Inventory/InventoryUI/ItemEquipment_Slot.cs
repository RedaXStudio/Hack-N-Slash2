using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemEquipment_Slot : MonoBehaviour
{
    public enum ItemEquipmentSlotType {Helmet, Chest, Gloves, Boots, Belt,  Amulet, Ring1, Ring2}

    public ItemEquipmentSlotType myType;
    
    public Inventory_UI inventoryUi;
    
    public Type itemType;

    public GameObject itemUIHold;

    void Start()
    {
        switch (myType)
        {
            case ItemEquipmentSlotType.Helmet :
                itemType = typeof(Item_Helmet);
                break;
            case ItemEquipmentSlotType.Chest :
                itemType = typeof(Item_Chest);
                break;
            case ItemEquipmentSlotType.Gloves :
                itemType = typeof(Item_Gloves);
                break;
            case ItemEquipmentSlotType.Boots :
                itemType = typeof(Item_Boots);
                break;
            case ItemEquipmentSlotType.Belt :
                itemType = typeof(Item_Belt);
                break;
            case ItemEquipmentSlotType.Amulet :
                itemType = typeof(Item_Amulet);
                break;
            case ItemEquipmentSlotType.Ring1 :
                itemType = typeof(Item_Ring);
                break;
            case ItemEquipmentSlotType.Ring2 :
                itemType = typeof(Item_Ring);
                break;
        }
    }
    
    void Update()
    {
        
    }

    public void EquipItem(GameObject itemUi)
    {
        Gears.gears.managerMain.playerActionManager.SetState(null);
        //Equip Item
        switch (myType)
        {
            case ItemEquipmentSlotType.Helmet :
                if (inventoryUi.inventory.helmet) 
                {
                    itemUIHold.GetComponent<ItemUI>().DragItem();
                    
                    inventoryUi.inventory.RemoveItem(inventoryUi.inventory.helmet, myType);
                }

                inventoryUi.inventory.helmet = (Item_Helmet) itemUi.GetComponent<ItemUI>().Item;
                break;
            case ItemEquipmentSlotType.Chest :
                if (inventoryUi.inventory.chest)
                {
                    itemUIHold.GetComponent<ItemUI>().DragItem();
                    
                    inventoryUi.inventory.RemoveItem(inventoryUi.inventory.helmet, myType);
                }
                
                inventoryUi.inventory.chest = (Item_Chest) itemUi.GetComponent<ItemUI>().Item;
                break;
            case ItemEquipmentSlotType.Gloves :
                if (inventoryUi.inventory.gloves)
                {
                    itemUIHold.GetComponent<ItemUI>().DragItem();
                    
                    inventoryUi.inventory.RemoveItem(inventoryUi.inventory.gloves, myType);
                }
                
                inventoryUi.inventory.gloves = (Item_Gloves) itemUi.GetComponent<ItemUI>().Item;
                break;
            case ItemEquipmentSlotType.Boots :
                if (inventoryUi.inventory.boots)
                {
                    itemUIHold.GetComponent<ItemUI>().DragItem();
                    
                    inventoryUi.inventory.RemoveItem(inventoryUi.inventory.boots, myType);
                }
                
                inventoryUi.inventory.boots = (Item_Boots) itemUi.GetComponent<ItemUI>().Item;
                break;
            case ItemEquipmentSlotType.Belt :
                if (inventoryUi.inventory.belt)
                {
                    itemUIHold.GetComponent<ItemUI>().DragItem();
                    
                    inventoryUi.inventory.RemoveItem(inventoryUi.inventory.belt, myType);
                }
                
                inventoryUi.inventory.belt = (Item_Belt) itemUi.GetComponent<ItemUI>().Item;
                break;
            case ItemEquipmentSlotType.Amulet :
                if (inventoryUi.inventory.amulet)
                {
                    itemUIHold.GetComponent<ItemUI>().DragItem();
                    
                    inventoryUi.inventory.RemoveItem(inventoryUi.inventory.amulet, myType);
                }
                
                inventoryUi.inventory.amulet = (Item_Amulet) itemUi.GetComponent<ItemUI>().Item;
                break;
            case ItemEquipmentSlotType.Ring1 :
                if (inventoryUi.inventory.ring1)
                {
                    itemUIHold.GetComponent<ItemUI>().DragItem();
                    
                    inventoryUi.inventory.RemoveItem(inventoryUi.inventory.ring1, myType);
                }
                
                inventoryUi.inventory.ring1 = (Item_Ring) itemUi.GetComponent<ItemUI>().Item;
                break;
            case ItemEquipmentSlotType.Ring2 :
                if (inventoryUi.inventory.ring2)
                {
                    itemUIHold.GetComponent<ItemUI>().DragItem();
                    
                    inventoryUi.inventory.RemoveItem(inventoryUi.inventory.ring2, myType);
                }
                
                inventoryUi.inventory.ring2 = (Item_Ring) itemUi.GetComponent<ItemUI>().Item;
                break;
        }
        
        itemUIHold = itemUi;
        
        //aplly effect
        
        foreach (var modifier in ((Item_Equipment) itemUi.GetComponent<ItemUI>().Item).itemModifiers)
        {
            if (modifier != null)
            {
                modifier.statsHolder = inventoryUi.inventory.statsHolder;
                inventoryUi.inventory.eachModifierEffect += modifier.ModiferEffect;
            }
        }
        
        inventoryUi.inventory.UpdateEquipments();
        
        inventoryUi.inventory.playerLinkedTo.UpdateInventoryAndPassiveTree();
        
        //inventoryUi.player.UpdateResistances();
        
        //Debug.Log("Equip Item : " + itemUi.name);
    }

    public void RemoveItemFromThisSLot()
    {
        switch (myType)
        {
            case ItemEquipmentSlotType.Helmet :
                inventoryUi.inventory.RemoveItem(inventoryUi.inventory.helmet, myType);
                break;
            case ItemEquipmentSlotType.Chest :
                inventoryUi.inventory.RemoveItem(inventoryUi.inventory.chest, myType);
                break;
            case ItemEquipmentSlotType.Gloves :
                inventoryUi.inventory.RemoveItem(inventoryUi.inventory.gloves, myType);
                break;
            case ItemEquipmentSlotType.Boots :
                inventoryUi.inventory.RemoveItem(inventoryUi.inventory.boots, myType);
                break;
            case ItemEquipmentSlotType.Belt :
                inventoryUi.inventory.RemoveItem(inventoryUi.inventory.belt, myType);
                break;
            case ItemEquipmentSlotType.Amulet :
                inventoryUi.inventory.RemoveItem(inventoryUi.inventory.amulet, myType);
                break;
            case ItemEquipmentSlotType.Ring1 :
                inventoryUi.inventory.RemoveItem(inventoryUi.inventory.ring1, myType);
                break;
            case ItemEquipmentSlotType.Ring2 :
                inventoryUi.inventory.RemoveItem(inventoryUi.inventory.ring2, myType);
                break;
        }
        
        itemUIHold.GetComponent<ItemUI>().DragItem();
        
        itemUIHold = null;

        inventoryUi.inventory.UpdateEquipments();
        
        inventoryUi.inventory.playerLinkedTo.UpdateInventoryAndPassiveTree();

        Debug.Log("Remove Item from : " + gameObject.name);
    }
}
