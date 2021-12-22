using System.Collections;
using System.Collections.Generic;
using Assets.Player.Items_Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    //public Player player;
    
    public RectTransform _rectTransform;
    private Image _imageBackGround;
    public Image _imageItem;

    public TextMeshProUGUI ammountText;
    
    public Item Item;

    public List<GameObject> slotsOnCollisionWith = new List<GameObject>();
    
    public List<GameObject> itemUiOncollWith = new List<GameObject>();

    public GameObject itemSlotOncollisionWith;

    [SerializeField]

    public bool dragged;

    public IEnumerator setStored;

    public virtual void Start()
    {
        setStored = SetStored();

        dragged = true;

        _rectTransform = GetComponent<RectTransform>();

        _imageBackGround = GetComponent<Image>();
        

        if (Item != null)
        { 
            //Debug.Log(new Vector2(Inventory_UI.slotSize * Item.xSlotTaken, Inventory_UI.slotSize * Item.ySlotTaken));
            
            _rectTransform.sizeDelta = new Vector2(Inventory_UI.slotSize * Item.xSlotTaken, Inventory_UI.slotSize * Item.ySlotTaken);

            transform.GetChild(0).GetComponent<RectTransform>().sizeDelta =
                new Vector2(Inventory_UI.slotSize * Item.xSlotTaken * 0.9f, Inventory_UI.slotSize * Item.ySlotTaken * 0.9f);

            GetComponent<BoxCollider2D>().size = new Vector2(Inventory_UI.slotSize * Item.xSlotTaken,
                Inventory_UI.slotSize * Item.ySlotTaken);
            
            _imageItem.sprite = Item.sprite;

            if (Item.stackable)
            {
                ammountText.gameObject.SetActive(true);
                UpdateAmmount();
                ammountText.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(
                    _rectTransform.sizeDelta.x / 2, -_rectTransform.sizeDelta.y / 2);
            }
        }
    }
    
    public virtual void Update()
    {
        if (Gears.gears.managerMain.canvasMain.MouseOverGameObject(gameObject) && 
            !dragged && Gears.gears.managerMain.playerActionManager.currentState == null && Input.GetButtonDown("Fire1"))
        {
            if (itemSlotOncollisionWith == null)
            {
                DragItem();
            }
            else
            {
                dragged = true;
                itemSlotOncollisionWith.GetComponent<ItemEquipment_Slot>().RemoveItemFromThisSLot();
            }
        }

        if (!dragged && Gears.gears.managerMain.canvasMain.MouseOverGameObject(gameObject))
        {
            /*if (!Gears.gears.managerMain.canvasMain.itemsTooltip.activeSelf)
            {
                Gears.gears.managerMain.canvasMain.SetItemTooltip(Item);
                Gears.gears.managerMain.canvasMain.itemsTooltip.SetActive(true);
            }*/
            
            CanvasMain.canvasMain.UpdateItemTooltipPos(gameObject, Item);
        }
    }

    public void SetItem(Item item)
    {
        Item = item;

        if (_rectTransform != null)
        {
            _rectTransform.sizeDelta = new Vector2(Inventory_UI.slotSize * Item.xSlotTaken, Inventory_UI.slotSize * Item.ySlotTaken);
        }

        if (_imageItem != null)
        {
            _imageItem.sprite = Item.sprite;
        }
        
        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta =
            new Vector2(Inventory_UI.slotSize * Item.xSlotTaken * 0.9f, Inventory_UI.slotSize * Item.ySlotTaken * 0.9f);

        GetComponent<BoxCollider2D>().size = new Vector2(Inventory_UI.slotSize * Item.xSlotTaken,
            Inventory_UI.slotSize * Item.ySlotTaken);
            
        _imageItem.sprite = Item.sprite;

        if (Item.stackable)
        {
            ammountText.gameObject.SetActive(true);
            ammountText.gameObject.GetComponent<RectTransform>().position = new Vector3(
                (Inventory_UI.slotSize * Item.xSlotTaken) / 2f, -(Inventory_UI.slotSize * Item.ySlotTaken) / 2f);
        }
    }

    #region collision

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("New Collision");
        
        if (col.gameObject.GetComponent<SlotScript>())
        {
            slotsOnCollisionWith.Add(col.gameObject);
        
            //Debug.Log(onCollisionWith.Count);
        }

        if (col.gameObject.GetComponent<ItemEquipment_Slot>())
        { 
            //Debug.Log("col ItemEquipment SLot");
            if (Item.GetType() == col.gameObject.GetComponent<ItemEquipment_Slot>().itemType)
            {
                itemSlotOncollisionWith = col.gameObject;
            }
        }
        
        if (col.gameObject.GetComponent<ItemUI>())
        {
            itemUiOncollWith.Add(col.gameObject);
        }
    }
    
    public virtual void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<SlotScript>())
        {
            slotsOnCollisionWith.Remove(col.gameObject);
        }
        
        if (col.gameObject.GetComponent<ItemEquipment_Slot>())
        {
            if (Item.GetType() == col.gameObject.GetComponent<ItemEquipment_Slot>().itemType)
            {
                itemSlotOncollisionWith = null;
            }
        }
        
        if (col.gameObject.GetComponent<ItemUI>())
        {
            itemUiOncollWith.Remove(col.gameObject);
        }
    }

    #endregion

    #region MouseOver
    

    #endregion

    #region drag/storeItem

    public void DragItem()
    {
        Debug.Log("Grab Item");
        
        CanvasMain.canvasMain.HideItemsTooltip();
        
        foreach (var slot in slotsOnCollisionWith)
        {
            slot.GetComponent<SlotScript>().filled = false;
            slot.GetComponent<Image>().color = new Color(1,1,1);
        }
        
        transform.SetParent(CanvasMain.canvasMain.transform);
        
        Gears.gears.managerMain.playerActionManager.SetState(new DragItemsUI_State(Gears.gears.managerMain.playerActionManager, gameObject));
        
        dragged = true;
    }

    public IEnumerator SetStored()
    {
        yield return new WaitForSeconds(0.1f);
        dragged = false;
        
        CanvasMain.canvasMain.SetItemTooltip(Item);
        CanvasMain.canvasMain.itemsTooltip.SetActive(true);
    }

    public void DebugedSetStored()
    {
        StopCoroutine(setStored);
        setStored = SetStored();
        StartCoroutine(setStored);
    }

    #endregion

    public void UpdateAmmount()
    {
        ammountText.text = Item.ammount.ToString();
    }
}
