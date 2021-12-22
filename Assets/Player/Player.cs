using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Player.Items_Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Player : Units
{
    [Header("Player settings")]

    public SkillTree mySkillTree;

    public Inventory myInventory;
    public GameObject inventoryUi;

    public NavMeshAgent navMeshAgent;
    
    //public float rotSpeed = 5f;

    public bool moving = false;

    //public Rigidbody rigi;

    new void Start()
    {
        base.Start();

        ShowHpBar();
        
        mySkillTree.SetXpToNextLvl();

        mySkillTree = CanvasMain.canvasMain.skillTree.GetComponent<SkillTree>();

        mySkillTree.player = this;

        inventoryUi = CanvasMain.canvasMain.inventoryUi;

        if (inventoryUi.GetComponent<Inventory_UI>().inventory == null) {
            myInventory = new Inventory();
        }else {
            myInventory = inventoryUi.GetComponent<Inventory_UI>().inventory;
        }

        myInventory.playerLinkedTo = this;
        
        myInventory.RestorEquipmentsMods();

        navMeshAgent = GetComponent<NavMeshAgent>();

        UpdateAll();

        actionsState.InsertStateList(new Player_movments(this), movementStateIndex);
        
        actionsState.AddState(new BaseAttack(this), out attackStateIndex);
        
        actionsState.AddState(new Player_Targeting(this), out targetingStateIndex);
        
        //rigi = GetComponent<Rigidbody>();

        //Test
    }
    
    new void Update()
    {
        base.Update();
        
        //ex : Find if facing an enemy
        /*Vector3 dir = targetStest.transform.position - transform.position;
        Debug.Log(Vector3.Angle(transform.forward, dir));*/
        
        if (!Mathf.Approximately(mySkillTree.lateXp, mySkillTree.xp))
        {
            mySkillTree.lateXp = Mathf.Lerp(mySkillTree.lateXp, mySkillTree.xp, 0.15f);
                        
            mySkillTree.UpdateXpBar();
        }

        if (Input.GetButtonDown("P"))
        {
            if (mySkillTree.skillTree.activeSelf)
            {
                mySkillTree.skillTree.SetActive(false);
            }
            else
            {
                mySkillTree.skillTree.SetActive(true);
            }
        }

        if (Input.GetButtonDown("B"))
        {
            if (inventoryUi.activeSelf)
            {
                inventoryUi.SetActive(false);
            }
            else
            {
                inventoryUi.SetActive(true);
            }
            
            Gears.gears.managerMain.canvasMain.HideItemsTooltip();
        }

        if (Input.GetButtonDown("A"))
        {
            //TEST
            mySkillTree.UpdateSkillTreeValues(0, true);

            //Damages Test
            
            //Damage Taken As Test
            //DamageTakenAs_Modifiers.Add(new DamageTakenAs(this, "FIRE", "CHAOS", 50));
            
            Damage[] t = {new Damage(CombatSystemeData.DamageType.FIRE,CombatSystemeData.DamageForm.HIT, 5)};
            
            TakeDamage(t, this, nbrOfDamageConversion);
            
            //Stun Test
            /*SetState(new StunnedState(this, navMeshAgent, actionsState, 1, this, this), 
                actionsState, out actionsState);*/
            
            //Root Test
            /*actionsState.SetStateAtIndex(new CC_RootState(this, navMeshAgent, actionsState.stateMultis[movementModifierIndex], 1, 
                this, this), movementModifierIndex);*/

            //Items Test
            /*Item item = new Reforge_Useable_Item(1,1,true,1, 3,null);

            Item_World.CreateItemWorld(item, transform.position);


            Item_Helmet helemtTest = (Item_Helmet) Gears.gears.managerMain.helmetBaseList[0].ShallowCopy();
            
            helemtTest.UpgradeToRareItem();

            Item_World.CreateItemWorld(helemtTest, transform.position);
            
            
            Item_Chest chestTest = (Item_Chest) Gears.gears.managerMain.chestBaseList[0].ShallowCopy();
            
            chestTest.UpgradeToRareItem();

            Item_World.CreateItemWorld(chestTest, transform.position);
            
            
            Item_Gloves glovesTest = (Item_Gloves) Gears.gears.managerMain.gloveBaseList[0].ShallowCopy();
            
            glovesTest.UpgradeToRareItem();

            Item_World.CreateItemWorld(glovesTest, transform.position);
            
            
            Item_Boots bootsTEst = (Item_Boots) Gears.gears.managerMain.bootsBaseList[0].ShallowCopy();
            
            bootsTEst.UpgradeToRareItem();

            Item_World.CreateItemWorld(bootsTEst, transform.position);
            
            
            Item_Belt beltTest = (Item_Belt) Gears.gears.managerMain.beltBaseList[0].ShallowCopy();
            
            beltTest.UpgradeToRareItem();

            Item_World.CreateItemWorld(beltTest, transform.position);
            
            
            Item_Amulet amuletTest = (Item_Amulet) Gears.gears.managerMain.amuletBaseList[0].ShallowCopy();
            
            amuletTest.UpgradeToRareItem();

            Item_World.CreateItemWorld(amuletTest, transform.position);
            
            
            Item_Ring ringTest = (Item_Ring) Gears.gears.managerMain.ringBaseList[0].ShallowCopy();
            
            ringTest.UpgradeToRareItem();

            Item_World.CreateItemWorld(ringTest, transform.position);
            Item_World.CreateItemWorld(ringTest, transform.position);*/

            //Scenes Test
            Gears.gears.managerMain.SetAndLoadCurrentArea(Gears.gears.managerMain.areas[0]);
        }

        if (Input.GetButtonDown("E"))
        {
            //Test2
            /*Item_Helmet helemtTest = (Item_Helmet) Gears.gears.managerMain.helmetBaseList[0].ShallowCopy();
            
            helemtTest.UpgradeToRareItem();

            Item_World.CreateItemWorld(helemtTest, transform.position);*/
        }
    }

    #region TakeDamageFuncRegion
    public override Damage[] DamageTakenAs(Damage[] damagesTypes, Units attacker)
    {
        float[] reduceDamages = new float[damagesTypes.Length];

        List<Damage> newDamagesFromConversion = new List<Damage>();
        
        
        List<DamageTakenAs> allDamageTakenAs_Modifiers = statsHolder.damageTakenAs_Modifiers;

        for (int i = 0; i < damagesTypes.Length; i++)
        {
            foreach (var damageTakenAs in allDamageTakenAs_Modifiers)
            {
                damageTakenAs.Conversion(damagesTypes[i].ShallowCopy(), reduceDamages[i], out reduceDamages[i] , newDamagesFromConversion);
            }
        }

        for (int i = 0; i < damagesTypes.Length; i++)
        {
            //Debug.Log(damagesTypes[i].damages + damagesTypes[i].type + " Reduced by : " + reduceDamages[i]);
            
            //1 - (1 - 0.9) + (1 - 0.9) = 0.8 utility calc
            damagesTypes[i].damages -= reduceDamages[i];
        }
        
        return newDamagesFromConversion.ToArray();
    }

    public override void TakeFinalDamages(Damage[] damageTypes, Units attacker)
    {
        foreach (var damageType in damageTypes)
        {
            Debug.Log(gameObject.name + " Took : " + damageType.damages + " " + damageType.myType + " damages");
            
            currentLife -= damageType.damages;

            currentLife = Mathf.Clamp(currentLife, 0, maximumLife);
        }
        
        if (Array.Find(damageTypes, types => types.formOfDamages == CombatSystemeData.DamageForm.HIT) != null)
        {
            WhenHit();
            Gears.gears.eventManagerMain.PlayerHitedTrigger(id);
        } //make a delegate func at the here and after checkDeath to add a mode that change if OnHit() is called befor or after checkdeath
        
        CheckDeath(attacker);
    }

    #endregion

    #region Updates
    public void UpdateInventoryAndPassiveTree()
    {
        statsHolder.ResetStats(baseStats);

        statsHolder.AddStats(myInventory.statsHolder);
        statsHolder.AddStats(mySkillTree.statsHolder);
        
        statsHolder.UpdateResistance();
        UpdateFinalDamages();
        
        Debug.Log(statsHolder.addedPhysicalDamagesLow + " to " + statsHolder.addedPhysicalDamagesHigh + " phys dmg");
    }
    
    public override void UpdateAll()
    {
        //base.UpdateAll();
        //statsHolder.ResetStats(baseStats);
        UpdateInventoryAndPassiveTree();
        
        //statsHolder.UpdateResistance();
        UpdateMaximumLife();
        //UpdateFinalDamages();
        UpdateMovementSpeed();
    }

    public override void UpdateMovementSpeed()
    {
        base.UpdateMovementSpeed();
        Gears.gears.managerMain.playerActionManager.player.navMeshAgent.speed = finalMovementSpeed;
    }
    #endregion

    public override void UpdateHpBar()
    {
        base.UpdateHpBar();
        
        CanvasMain.canvasMain.hpBarMainUiScaler.transform.localScale = new Vector3(
            CanvasMain.canvasMain.hpBarMainUiScaler.transform.localScale.x, currentLife / maximumLife, 
            CanvasMain.canvasMain.hpBarMainUiScaler.transform.localScale.z);

        CanvasMain.canvasMain.hpText.text = StringOfAFloat(currentLife) + "/" + StringOfAFloat(maximumLife);
    }

    public String StringOfAFloat(float nbr)
    {
        //displaying hp like : 5,1/10
        List<Char> allChar = new List<char>();
        
        for (int i = 0; i < nbr.ToString().ToCharArray().Length; i++)
        {
            allChar.Add(nbr.ToString().ToCharArray()[i]);

            if (i - 1 > 0)
            {
                if (nbr.ToString().ToCharArray()[i - 1].ToString() == ",")
                {
                    i = nbr.ToString().ToCharArray().Length;
                }
            }
        }

        return new string(allChar.ToArray());
    }
}
