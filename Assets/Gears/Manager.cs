using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager manager;
    
    public Area currentArea;
    
    [Header("References")]
    public CanvasMain canvasMain;
    
    public PlayerActionManager playerActionManager;

    public GameObject itemWorldContainer;
    
    public Camera mainCam;

    [Header("Base Areas")]
    public List<Area> areas = new List<Area>();

    [Header("Items Bases")] 
    
    public List<Item_Helmet> helmetBaseList;
    
    public List<Item_Chest> chestBaseList;
    
    public List<Item_Gloves> gloveBaseList;
    
    public List<Item_Boots> bootsBaseList;
    
    public List<Item_Belt> beltBaseList;
    
    public List<Item_Amulet> amuletBaseList;
    
    public List<Item_Ring> ringBaseList;

    void Awake()
    {
        /*if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != null)
        {
            Destroy(gameObject);
        }*/
        
        if (Gears.gears)
        {
            Gears.gears.managerMain = this;
        }

        if (CanvasMain.canvasMain)
        {
            canvasMain = CanvasMain.canvasMain;
        }
    }
    
    void Start()
    {
        Gears.gears.managerMain = this;
        
        currentArea.baseItemLootable.Add(new Reforge_Useable_Item(1,1,true,1, 10,null));
        currentArea.baseItemLootable.Add(new UpgradeToRareItem_UseableItem(1, 1, true, 1, 3, null));
        currentArea.baseItemLootable.Add(new UpgradeToMagicItem_UseableItem(1, 1, true, 1, 5, null));
        currentArea.SpawnMonsters();
    }
    
    void Update()
    {
        
    }

    public void SetAndLoadCurrentArea(Area area)
    {
        playerActionManager.player.myInventory.storeEquipmentsMods();

        CanvasMain.canvasMain.ClearUi();
        
        currentArea = area;
        SceneManager.LoadScene(area.areaSceneName);
    }

    public void InstantiateFunc(GameObject go, Vector3 pos, Quaternion rot, Transform transform)
    {
        Instantiate(go, pos, rot, transform);
    }
}
