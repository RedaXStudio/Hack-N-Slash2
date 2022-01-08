using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Player.Items_Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasMain : MonoBehaviour
{
    public static CanvasMain canvasMain;
    
    [Header("Items ToolTip")]
    
    public GameObject itemsTooltip;
    public TextMeshProUGUI itemTooltip_ItemName;
    public TextMeshProUGUI itemTooltip_ItemLvl;
    public Image itemTooltip_ItemSprite;

    public TextMeshProUGUI itemTooltip_ItemDescription;

    public TextMeshProUGUI[] itemTooltip_Modifiers;

    [Header("Others Settings")] 
    public GameObject skillTree;

    public GameObject inventoryUi;

    public GameObject itemWorldUi_Parent;

    public GameObject hpBarUi_Parent;

    public GameObject targetHpBar;

    public GameObject xpBarScaler;
    
    public GameObject hpBarMainUiScaler;

    public GameObject escapePanel;
    
    public TextMeshProUGUI hpText;

   void Awake()
    {
        if (canvasMain == null)
        {
           // DontDestroyOnLoad(gameObject);
            canvasMain = this;
        }
        else if (canvasMain != null)
        {
            Destroy(gameObject);
        }
        
        if (Gears.gears)
        {
            Gears.gears.managerMain.canvasMain = this;
        }
    }
    
    void Start()
    {
        Gears.gears.managerMain.canvasMain = this;
    }
    
    void Update()
    {
        if (!IsMouseOverUiTooltip())
        {
            HideItemsTooltip();
        }
    }

    #region ItemTooltip

    public void HideItemsTooltip()
    {
        if (canvasMain.itemsTooltip.activeSelf)
        {
            itemTooltip_ItemDescription.text = "";
            canvasMain.itemsTooltip.SetActive(false);
        }
    }

    public void SetItemTooltip(Item item)
    {
        canvasMain.itemTooltip_ItemName.text = item.itemName;
        canvasMain.itemTooltip_ItemSprite.sprite = item.sprite;
        
        //remove mods text
        foreach (var modTooltip in  Gears.gears.managerMain.canvasMain.itemTooltip_Modifiers)
        {
            modTooltip.text = "";
        }

        if (item.GetType().IsSubclassOf(typeof(Item_Equipment)) || item.GetType() == typeof(Item_Equipment))
        {
            canvasMain.itemsTooltip.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 400);
                
            canvasMain.itemTooltip_ItemLvl.text = "Item Level : " + ((Item_Equipment) item).itemLvl;
            
            //Debug.Log(((Item_Equipment) item).itemModifiers[0].modifierDescription + " " + item);

            for (int i = 0; i < ((Item_Equipment) item).itemModifiers.Count; i++)
            {
                if (((Item_Equipment) item).itemModifiers[i] != null)
                {
                    //Debug.Log(((Item_Equipment) item).itemModifiers[i].GetType());
                    canvasMain.itemTooltip_Modifiers[i].text = ((Item_Equipment) item).itemModifiers[i].modifierDescription;
                }
            }
        }else if (item.GetType().IsSubclassOf(typeof(Item_Useable)) || item.GetType() == typeof(Item_Useable))
        {
            canvasMain.itemsTooltip.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
            canvasMain.itemTooltip_ItemLvl.text = "";
            canvasMain.itemTooltip_ItemDescription.text = item.description;
        }
    }

    public void UpdateItemTooltipPos(GameObject go, Item item)
    {
        float offSet = 0;
        
        if (Input.mousePosition.x > go.GetComponent<RectTransform>().position.x)
        {
            if (go.GetComponent<ItemUI>())
            {
                offSet = item.xSlotTaken * Inventory_UI.slotSize / 2 + 180; //half the tooltip x pixel size
            }
            else
            {
                offSet = go.GetComponent<RectTransform>().sizeDelta.x / 2 + 150;
            }
        }
        else
        {
            if (go.GetComponent<ItemUI>())
            {
                offSet = -item.xSlotTaken * Inventory_UI.slotSize / 2 - 180;
            }
            else
            {
                offSet = -go.GetComponent<RectTransform>().sizeDelta.x / 2 - 150;
            }
        }
        //TODO : calculate Tooltip Size
            
        //Debug.Log(offSet);
        //150 200 = half size of tooltip
        Vector3 minPos = new Vector3(0 + 150,0 + 200,0);
            
        Vector3 maxPos = new Vector3(Gears.gears.mainCam.pixelWidth - 150, Gears.gears.mainCam.pixelHeight - 200);
            
            
        Vector3 normalPos = go.GetComponent<RectTransform>().position + new Vector3(offSet, 0, 0);
            
        Vector3 clampedInScreen = new Vector3(Mathf.Clamp(normalPos.x, minPos.x, maxPos.x), Mathf.Clamp(normalPos.y, minPos.y, maxPos.y));

        canvasMain.itemsTooltip.GetComponent<RectTransform>().position = clampedInScreen;
            
        if (!canvasMain.itemsTooltip.activeSelf)
        {
            SetItemTooltip(item);
            canvasMain.itemsTooltip.SetActive(true);
        }
    }

    #endregion

    #region MouseOver
    
    public bool IsMouseOverUiTooltip()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.GetComponent<ItemUI>() == null)
            {
                raycastResults.RemoveAt(i);
                i--;
            }
        }

        return raycastResults.Count > 0;
    }

    public bool IsMouseOverUiIgnore()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.GetComponent<IgnoreMouseOver>())
            {
                raycastResults.RemoveAt(i);
                i--;
            }
        }

        return raycastResults.Count > 0;
    }

    public bool MouseOverGameObject(GameObject go)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        /*for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.GetComponent<IgnoreMouseOver>())
            {
                raycastResults.RemoveAt(i);
                i--;
            }
        }*/

        return Array.Find(raycastResults.ToArray(), result => result.gameObject == go).gameObject;
    }

    public GameObject ObjectUnderCursor()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        
        if (raycastResults.Capacity == 0)
        {
            return null;
        }
        //Debug.Log(raycastResults[0].gameObject);
        return raycastResults[0].gameObject;
    }
    
    #endregion

    public void ClearUi()
    {
        //Debug.Log(itemWorldUi_Parent.transform.childCount);
        
        for (int i = 0; i < itemWorldUi_Parent.transform.childCount; i++)
        {
            Destroy(itemWorldUi_Parent.transform.GetChild(i).gameObject);
        }
        
        for (int i = 0; i < hpBarUi_Parent.transform.childCount; i++)
        {
            for (int j = 0; j < hpBarUi_Parent.transform.GetChild(i).transform.childCount; j++)
            {
                for (int k = 0; k < hpBarUi_Parent.transform.GetChild(j).transform.childCount; k++)
                {
                    Destroy(hpBarUi_Parent.transform.GetChild(k).gameObject);
                }
                
                Destroy(hpBarUi_Parent.transform.GetChild(j).gameObject);
            }
            
            Destroy(hpBarUi_Parent.transform.GetChild(i).gameObject);
        }
    }
}
