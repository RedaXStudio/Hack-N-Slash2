using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseableItems_OnUI : ItemUI
{
    //[Header("UseableItemUI Settings")]

    public override void Update()
    {
        base.Update();
        
        if (Gears.gears.managerMain.canvasMain.MouseOverGameObject(gameObject) && !dragged && Input.GetButtonDown("Fire2"))
        {
           UseItemStateTrigger();
        }
    }

    public void UseItemStateTrigger()
    {
        Debug.Log("Use item state Trigger");
        
        Gears.gears.managerMain.canvasMain.HideItemsTooltip();

        if (Item.stackable && Item.ammount > 1)
        {
            Item.ammount--;
            
            UpdateAmmount();
            
            GameObject itemUnstacked = Instantiate(Gears.gears.useableItemsOnUiPrefab, Gears.gears.managerMain.canvasMain.transform);

            Assets.Player.Items_Inventory.Item itemCopy = Item.ShallowCopy();

            itemCopy.ammount = 1;
            
            itemUnstacked.GetComponent<ItemUI>().SetItem(itemCopy);
            
            Gears.gears.managerMain.playerActionManager.SetState(new UseItemsState(Gears.gears.managerMain.playerActionManager, itemUnstacked, 
                this));
        }
        else
        {
            foreach (var slot in slotsOnCollisionWith)
            {
                slot.GetComponent<SlotScript>().filled = false;
                slot.GetComponent<Image>().color = new Color(1,1,1);
            }
            
            Gears.gears.managerMain.playerActionManager.SetState(new UseItemsState(Gears.gears.managerMain.playerActionManager, gameObject, this));
        }
    }
}
