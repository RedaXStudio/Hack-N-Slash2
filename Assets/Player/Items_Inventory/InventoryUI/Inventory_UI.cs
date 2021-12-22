using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public Player player;
    public Inventory inventory;

    public Transform slotContainer;
    
    public static int slotSize = 50;
    
    public float spaceBetweenSlot;
    public float slotStartX;
    public float slotStartY;
    
    public int slotNbrX;
    public int slotNbrY;

    public ItemEquipment_Slot helmetSlot;
    public ItemEquipment_Slot chestSlot;
    public ItemEquipment_Slot glovesSlot;
    public ItemEquipment_Slot bootsSlot;
    public ItemEquipment_Slot beltSlot;
    public ItemEquipment_Slot leftRingSlot;
    public ItemEquipment_Slot rightRingSLot;
    public ItemEquipment_Slot amuletSlot;

    void Start()
    {
        inventory = player.myInventory;
        inventory.inventoryUi = this;

        for (int i = 0; i < slotNbrX; i++)
        {
            for (int j = 0; j < slotNbrY; j++)
            {
                GameObject slot = Instantiate(Gears.gears.slotPrefab, slotContainer);
                
                slot.GetComponent<RectTransform>().sizeDelta = new Vector2(slotSize, slotSize);
                slot.GetComponent<BoxCollider2D>().size = new Vector2(slotSize, slotSize);
                
                slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(slotStartX + slotSize * i + spaceBetweenSlot * i, 
                    slotStartY - slotSize * j - spaceBetweenSlot* j);

                slot.GetComponent<SlotScript>().posX = i;
                slot.GetComponent<SlotScript>().posY = j;
            }
        }
    }
    
    void Update()
    {
        
    }
}
