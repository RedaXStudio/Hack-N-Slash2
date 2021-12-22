using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Player.Items_Inventory;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DragItemsUI_State : State
{
    public PlayerActionManager playerAction;
    //public Item Item;

    private GameObject itemUI;

    //private bool evenNbrOnX;

    private Vector2 mousePos;
    
    public DragItemsUI_State(PlayerActionManager playerAction, GameObject itemUi)  : base(playerAction)
    {
        this.playerAction = playerAction;
        itemUI = itemUi;
    }

    public override void OnStateEnter()
    { 
        itemUI.GetComponent<ItemUI>().dragged = true;
        //Debug.Log("DragItem UI : " + itemUI.name);
    }

    public override void Tick()
    {
        mousePos = Vector2.Lerp(new Vector2(Input.mousePosition.x, Input.mousePosition.y), 
            new Vector2(itemUI.transform.position.x, itemUI.transform.position.y), 0.15f);
        
        itemUI.GetComponent<RectTransform>().position = mousePos;
        
        //change the color of the slots under the item
        
        if (Input.GetButtonDown("Fire1"))
        {
            //Drop an item on the ground
            if (itemUI.GetComponent<ItemUI>().slotsOnCollisionWith.Count == 0 && !itemUI.GetComponent<ItemUI>().itemSlotOncollisionWith)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
            
                if (Physics.Raycast(ray, out hit, 1000, Gears.gears.groundLayer))
                {
                    
                }

                Vector3 dir = hit.point - playerAction.player.gameObject.transform.position;

                float distance = Vector3.Distance(playerAction.player.gameObject.transform.position, hit.point);

                distance = Mathf.Clamp(distance, 0.5f, 5);

                GameObject itemWorld = Item_World.CreateItemWorld(itemUI.GetComponent<ItemUI>().Item,new Vector3(playerAction.player.transform.position.x, 
                                                                                  playerAction.player.transform.position.y, 
                                                                                  playerAction.player.transform.position.z) + dir.normalized * distance);
                
                
                playerAction.DestroyObject(itemUI);
                
                Debug.Log("Drop Item");
                playerAction.SetState(null);
            }
            else
            {
                //Equip Item
                if (itemUI.GetComponent<ItemUI>().itemSlotOncollisionWith)
                {
                    ItemEquipment_Slot itemEquipmentSlot = itemUI.GetComponent<ItemUI>().itemSlotOncollisionWith.GetComponent<ItemEquipment_Slot>();
                    
                    if (itemEquipmentSlot.itemType == itemUI.GetComponent<ItemUI>().Item.GetType())
                    {
                        itemUI.GetComponent<RectTransform>().position = itemEquipmentSlot.gameObject.GetComponent<RectTransform>().position;

                        itemUI.transform.SetParent(itemEquipmentSlot.gameObject.transform);
                        
                        itemUI.GetComponent<Image>().color = new Color(itemUI.GetComponent<Image>().color.r,
                            itemUI.GetComponent<Image>().color.g, itemUI.GetComponent<Image>().color.b, 1);
                        
                        itemUI.GetComponent<ItemUI>().DebugedSetStored();
                        
                        itemUI.GetComponent<ItemUI>().itemSlotOncollisionWith.GetComponent<ItemEquipment_Slot>().EquipItem(itemUI);
                        
                        //Debug.Log("Equip Item : " + itemUI.name);
                    }
                }
                else
                {
                    //stack Item
                    if (itemUI.GetComponent<ItemUI>().Item.stackable)
                    {
                        //add distance check
                        GameObject sameItem = Array.Find(itemUI.GetComponent<ItemUI>().itemUiOncollWith.ToArray(), o
                            => o.GetComponent<ItemUI>().Item.GetType() == itemUI.GetComponent<ItemUI>().Item.GetType());

                        if (sameItem)
                        {
                            sameItem.GetComponent<ItemUI>().Item.ammount++;
                            sameItem.GetComponent<ItemUI>().UpdateAmmount();
                            
                            stateMachine.DestroyObject(itemUI);
                            playerAction.SetState(null);
                        }
                        else
                        {
                            if (CanBePlaced())
                            {
                                StoreItem();
                            }
                        }
                    }
                    else
                    {
                        //Store Item
                        if (CanBePlaced())
                        {
                           StoreItem();
                        }
                    }
                }
            }
        }
    }

    public override void OnStateExit()
    { 
        //Debug.Log("Exit dragItem UI State");
    }

    public bool CanBePlaced()
    {
        if (Array.Find(itemUI.GetComponent<ItemUI>().slotsOnCollisionWith.ToArray(), slot => slot.GetComponent<SlotScript>().filled) != null)
        {
            Debug.Log("Not All Slot are empty");
            
            return false;
        }

        if (itemUI.GetComponent<ItemUI>().slotsOnCollisionWith.Count < itemUI.GetComponent<ItemUI>().Item.xSlotTaken *
            itemUI.GetComponent<ItemUI>().Item.ySlotTaken)
        {
            Debug.Log("Not enough SLots " + itemUI.GetComponent<ItemUI>().slotsOnCollisionWith.Count + " " + itemUI.GetComponent<ItemUI>().Item.xSlotTaken *
                      itemUI.GetComponent<ItemUI>().Item.ySlotTaken);
            
            return false;
        }

        return true;
    }

    public void StoreItem()
    {
        SlotScript[] slotsUsed = new SlotScript[itemUI.GetComponent<ItemUI>().Item.xSlotTaken *
                                                                    itemUI.GetComponent<ItemUI>().Item.ySlotTaken];

        float nbrX = 0;
        float nbrY = 0;

        //Find wich slots will be used
        for (int i = 0; i < slotsUsed.Length; i++)
        {
            float distance = Mathf.Infinity;

            for (int j = 0; j < itemUI.GetComponent<ItemUI>().slotsOnCollisionWith.Count; j++)
            {
                bool rectangle = true;

                foreach (var slot2 in slotsUsed) 
                { 
                    if (slot2 && Mathf.Abs(slot2.posX - itemUI.GetComponent<ItemUI>().slotsOnCollisionWith[j].GetComponent<SlotScript>().posX)
                      > itemUI.GetComponent<ItemUI>().Item.xSlotTaken - 1 || 
                      slot2 && Mathf.Abs(slot2.posY - itemUI.GetComponent<ItemUI>().slotsOnCollisionWith[j].GetComponent<SlotScript>().posY) > 
                      itemUI.GetComponent<ItemUI>().Item.ySlotTaken - 1)
                    {
                        rectangle = false;
                    }
                }

                //condition to use a slot: not already use && closest && not filled
                if (Array.Find(slotsUsed,
                        slot => slot == itemUI.GetComponent<ItemUI>().slotsOnCollisionWith[j].GetComponent<SlotScript>()) == null && 
                    Vector2.Distance(itemUI.GetComponent<RectTransform>().position, 
                        itemUI.GetComponent<ItemUI>().slotsOnCollisionWith[j].GetComponent<RectTransform>().position) < distance 
                    && !itemUI.GetComponent<ItemUI>().slotsOnCollisionWith[j].GetComponent<SlotScript>().filled && rectangle)
                {
                    slotsUsed[i] = itemUI.GetComponent<ItemUI>().slotsOnCollisionWith[j].GetComponent<SlotScript>();

                    distance = Vector2.Distance(itemUI.GetComponent<RectTransform>().position, 
                        itemUI.GetComponent<ItemUI>().slotsOnCollisionWith[j].GetComponent<RectTransform>().position);
                }
            }

            nbrX += slotsUsed[i].gameObject.GetComponent<RectTransform>().position.x;
            nbrY += slotsUsed[i].gameObject.GetComponent<RectTransform>().position.y;

            //Debug.Log(slotsUsed[i]);

            slotsUsed[i].gameObject.GetComponent<Image>().color = new Color(0, 0, 1);
        }

        for (int i = 0; i < slotsUsed.Length; i++)
        {
            slotsUsed[i].GetComponent<SlotScript>().filled = true;
        }

        Vector2 posBetweenSlots = new Vector2(nbrX / slotsUsed.Length, nbrY / slotsUsed.Length);

        //Store Item
        itemUI.GetComponent<RectTransform>().position = posBetweenSlots;

        itemUI.transform.SetParent(playerAction.player.inventoryUi.gameObject.transform);

        itemUI.GetComponent<Image>().color = new Color(itemUI.GetComponent<Image>().color.r,
            itemUI.GetComponent<Image>().color.g, itemUI.GetComponent<Image>().color.b, 1);

        itemUI.GetComponent<ItemUI>().DebugedSetStored();

        //Debug.Log("Store Item");

        playerAction.SetState(null);
    }
}
