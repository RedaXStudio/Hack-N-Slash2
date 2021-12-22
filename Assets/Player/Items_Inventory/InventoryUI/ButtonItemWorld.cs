using System.Collections;
using System.Collections.Generic;
using Assets.Player.Items_Inventory;
using UnityEngine;
using UnityEngine.UI;

public class ButtonItemWorld : MonoBehaviour
{
    public Item_World itemWorld;

    public GameObject text;
    
    private Button _button;

    public bool _mouseOver;
    
    void Start()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(Gears.gears.managerMain.canvasMain.HideItemsTooltip);
    }
    
    void Update()
    {
        if (Gears.gears.managerMain.canvasMain.ObjectUnderCursor() == gameObject || Gears.gears.managerMain.canvasMain.ObjectUnderCursor() == text)
        {
            Gears.gears.managerMain.canvasMain.UpdateItemTooltipPos(gameObject, itemWorld.item);
        }
    }

    void OnMouseOver()
    {
        /*if (Gears.gears.managerMain.canvasMain.ObjectUnderCursor() == gameObject || Gears.gears.managerMain.canvasMain.ObjectUnderCursor() == text)
        {
            Debug.Log(Gears.gears.managerMain.canvasMain.ObjectUnderCursor());
            Gears.gears.managerMain.canvasMain.SetItemTooltip(itemWorld.item);
            _mouseOver = true;
        }*/
    }

    void OnMouseExit()
    {
        _mouseOver = false;
    }
}
