using System.Collections;
using System.Collections.Generic;
using Assets.Player.Items_Inventory;
using UnityEngine;
using UnityEngine.UI;

public class UseItemsState : State
{
    public PlayerActionManager playerAction;

    public UseableItems_OnUI stackOfItemFrom;
    private Vector2 posFrom;

    private bool noStackFrom;
    
    private GameObject itemUI;

    private Vector2 mousePos;

    public UseItemsState(PlayerActionManager playerAction, GameObject itemUi, UseableItems_OnUI stackOfItemFrom)  : base(playerAction)
    {
        this.playerAction = playerAction;
        itemUI = itemUi;

        this.stackOfItemFrom = stackOfItemFrom;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Use Item State");
        if (stackOfItemFrom == itemUI.GetComponent<UseableItems_OnUI>())
        {
            noStackFrom = true;
            posFrom = itemUI.transform.position;
        }
        
        itemUI.GetComponent<UseableItems_OnUI>().dragged = true;
    }

    public override void Tick()
    {
        mousePos = Vector2.Lerp(new Vector2(Input.mousePosition.x, Input.mousePosition.y), 
            new Vector2(itemUI.transform.position.x, itemUI.transform.position.y), 0.15f);
        
        itemUI.GetComponent<RectTransform>().position = mousePos;

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject item = null;
            
            float distance = Mathf.Infinity;

            foreach (var itemUi in itemUI.GetComponent<UseableItems_OnUI>().itemUiOncollWith)
            {
                float dist = Vector2.Distance(itemUI.GetComponent<RectTransform>().position, itemUi.GetComponent<RectTransform>().position);
                
                if (dist < distance)
                {
                    distance = dist;

                    item = itemUi;
                }
            }
            
            ((Item_Useable) itemUI.GetComponent<UseableItems_OnUI>().Item).UseItem();
            ((Item_Useable) itemUI.GetComponent<UseableItems_OnUI>().Item).UseItem(item);
            
            noStackFrom = false;
            playerAction.SetState(null);
        }
        
        if (Input.GetButtonUp("Fire2"))
        {
            if (!noStackFrom)
            {
                stackOfItemFrom.Item.ammount++;
                stackOfItemFrom.UpdateAmmount();
            }
            playerAction.SetState(null);
        }
    }

    public override void OnStateExit()
    { 
        //Debug.Log("QUiT USE ITEM STATE");

        if (!noStackFrom)
        {
            stateMachine.DestroyObject(itemUI);
        }
        else
        {
            itemUI.GetComponent<ItemUI>().DebugedSetStored();
            itemUI.GetComponent<RectTransform>().position = posFrom;
        }
    }
}
