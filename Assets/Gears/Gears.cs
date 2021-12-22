using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gears : MonoBehaviour
{
    public static Gears gears;

    public LayerMask groundLayer;

    public LayerMask playerLayer;

    public LayerMask SelectableLayer;

    public EventManager eventManagerMain;

    public Manager managerMain;

    public Camera mainCam;

    [Header("Prefabs")]
    public GameObject slotPrefab;

    public GameObject itemUIPrefab;
    public GameObject itemEquipmentOnUIPrefab;
    public GameObject useableItemsOnUiPrefab;
    public GameObject itemWorldPrefab;
    public GameObject uiButtonObject;

    public GameObject emptySprite;

    public GameObject buttonPrefab;
    
    public GameObject hpBarPrefab;

    void Awake()
    {
        if (gears == null)
        {
            DontDestroyOnLoad(gameObject);
            gears = this;
        }
        else if (gears != null)
        {
            Destroy(gameObject);
        }
        
        eventManagerMain = new EventManager();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
    public void LoadMenu()
    {
        
    }

    public void LoadGame()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
