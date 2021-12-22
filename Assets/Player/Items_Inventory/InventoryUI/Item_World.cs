using System.Collections;
using System.Collections.Generic;
using Assets.Player.Items_Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item_World : MonoBehaviour
{
    public Item item;

    private GameObject instanceButton;

    public Button button;

    public bool showItemOnUi;

    void Start()
    {
        ShowItemOnUi();
    }
    
    void Update()
    {
        if (showItemOnUi)
        {
            instanceButton.GetComponent<RectTransform>().position = Gears.gears.mainCam.WorldToScreenPoint(transform.position);
        }
    }

    public void SetItem(Item item)
    {
        this.item = item;
        GetComponent<MeshFilter>().mesh = this.item.itemMesh;
        
        if (this.item.itemMaterial)
        {
            GetComponent<MeshRenderer>().material = this.item.itemMaterial;
            //else base material
        }
    }

    public void ShowItemOnUi()
    {
        //Add condition(setting)
        if (!showItemOnUi)
        {
            if (!instanceButton)
            { 
                instanceButton = Instantiate(Gears.gears.buttonPrefab, CanvasMain.canvasMain.itemWorldUi_Parent.transform);

                instanceButton.GetComponent<ButtonItemWorld>().itemWorld = this;

                instanceButton.name += "from : " + gameObject.name;
                
                button = instanceButton.GetComponent<Button>();
        
                button.onClick.AddListener(PickUpItem);
            }
            
            showItemOnUi = true;

            UpdateAmmount();
        }
        else
        {
            instanceButton.SetActive(false);
        }
    }

    public void UpdateAmmount()
    {
        if (showItemOnUi)
        {
            if (item.stackable)
            {
                instanceButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.itemName + "(" + item.ammount + ")";
            }
            else
            {
                instanceButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.itemName;
            }
        }
    }

    public void PickUpItem()
    {
        //Debug.Log("pick up Item");
        GameObject itemUI1;
        
        if (item.GetType().IsSubclassOf(typeof(Item_Equipment)))
        {
            itemUI1 = Instantiate(Gears.gears.itemEquipmentOnUIPrefab, CanvasMain.canvasMain.transform);
        }
        else if(item.GetType().IsSubclassOf(typeof(Item_Useable)))
        {
            itemUI1 = Instantiate(Gears.gears.useableItemsOnUiPrefab, CanvasMain.canvasMain.transform);
        }
        else
        {
            itemUI1 = Instantiate(Gears.gears.itemUIPrefab, CanvasMain.canvasMain.transform);
        }
        
        GameObject itemUi = itemUI1;
        
        itemUi.name += "from : " + gameObject.name;
        
        itemUi.GetComponent<ItemUI>().SetItem(item.ShallowCopy());

        itemUi.GetComponent<Image>().color = new Color(itemUi.GetComponent<Image>().color.r,
            itemUi.GetComponent<Image>().color.g, itemUi.GetComponent<Image>().color.b, 0.8f);
        
        Gears.gears.managerMain.playerActionManager.SetState(new DragItemsUI_State(Gears.gears.managerMain.playerActionManager, itemUi));

        Destroy(instanceButton);

        Destroy(gameObject);
    }
    
    public static GameObject CreateItemWorld(Item item, Vector3 pos)
    {
        GameObject instanceItemWorld = Instantiate(Gears.gears.itemWorldPrefab, pos, 
            Gears.gears.itemWorldPrefab.transform.rotation, Gears.gears.managerMain.itemWorldContainer.transform);
        
        instanceItemWorld.GetComponent<Item_World>().SetItem(item);
        //instanceItemWorld.GetComponent<Item_World>().ShowItemOnUi();
        
        return instanceItemWorld;
    }
}
