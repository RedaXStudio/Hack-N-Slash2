using System.Collections;
using System.Collections.Generic;
using Assets.Player.Items_Inventory;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[System.Serializable]
public class Area
{
    public int areaLvl;
    public string areaName;
    
    public Scene areaScene;
    public string areaSceneName;
    
    public Manager manager;

    public GameObject centerOfTheMap;
    public float maxZ;
    public float minZ;
    public float maxX;
    public float minX;

    public int baseNbrOfPack;
    public int monsterPackSize = 5;
    public float maxHeightOfPackArea;
    public float maxWidthOfPackArea;

    public List<Item> baseItemLootable = new List<Item>();
    public GameObject[] enemiesInThisArea;

    public void SpawnMonsters()
    {
        for (int i = 0; i < baseNbrOfPack; i++)
        {
            int rangeMonster = Random.Range(0, enemiesInThisArea.Length);
            
            float posX = Random.Range(minX, maxX);
            float posZ = Random.Range(minZ, maxZ);
            
            Ray ray = new Ray(new Vector3(centerOfTheMap.transform.position.x + posX, centerOfTheMap.transform.position.y, 
                centerOfTheMap.transform.position.z + posZ), Vector3.down);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 1000, Gears.gears.groundLayer))
            {
                for (int j = 0; j < monsterPackSize; j++)
                {
                    float posOnCircle = Random.Range(0, 2 * Mathf.PI);
                    
                    float widthOfPackArea = Random.Range(1, maxWidthOfPackArea);
                    float heightOfPackArea = Random.Range(1, maxHeightOfPackArea);
                    
                    float x = Mathf.Cos(posOnCircle) * widthOfPackArea;
                    float z = Mathf.Sin(posOnCircle) * heightOfPackArea;
                    
                    manager.InstantiateFunc(enemiesInThisArea[rangeMonster], hit.point + new Vector3(x, 0, z), 
                        Quaternion.Euler(0, Random.Range(-180, 180), 0), centerOfTheMap.transform);
                }
            }
        }
    }

    public void SpawnElements()
    {
        
    }

    public void LootTrashMobs(Vector3 itemWorldPos)
    {
        Item item = null;
        
        int relativTotal = 0;

        foreach (var itemLootable in baseItemLootable) {
            relativTotal += itemLootable.relativeChanceToLoot; 
        }
        
        //Debug.Log(relativTotal);
        
        int range = Random.Range(0, relativTotal);
          
        int countChance = 0;

        for (int j = 0; j < baseItemLootable.Count; j++) 
        {
            countChance += baseItemLootable[j].relativeChanceToLoot;
            
            if (j < baseItemLootable.Count - 1) {
                //Debug.Log(countChance + " < " + range + " < " + (countChance + baseItemLootable[j + 1].relativeChanceToLoot));
                if (countChance <= range && range <= countChance + baseItemLootable[j + 1].relativeChanceToLoot) {
                    item = baseItemLootable[j + 1].ShallowCopy();
                    j = baseItemLootable.Count;
                } 
            }
            else {
                //Debug.Log("Last Item on the list");
                item = baseItemLootable[j].ShallowCopy();
            }
        }

        if (item.GetType().IsSubclassOf(typeof(Item_Equipment)))
        {
            ((Item_Equipment) item).RollRandomRarity();
        }
        
        Item_World.CreateItemWorld(item, itemWorldPos);
    }
}
