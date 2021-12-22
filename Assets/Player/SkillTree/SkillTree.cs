using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [Header("UI things")]
    
    public GameObject skillTree;

    public GameObject tipsContainer; //Set active true and change position on screen when a point is hightligthed
    
    public TextMeshProUGUI passivesPointsLeftDisplay;
    
    public TextMeshProUGUI passivesPointsDescriptionTextDisplay;
    public TextMeshProUGUI passivPointCostText;
    
    public float tipsPosOnScreenWidth;
    public float tipsPosOnScreenHeight;

    public GameObject lineGameObject;
    
    public GameObject lineContainer;

    public TextMeshProUGUI buttonSwitchModeText;

    [Header("Skill Tree")]
    
    public Player player;

    public int passivPointsLeft;
    
    public int nbrOfActivePoints;

    public List<LinesPoints> allLines = new List<LinesPoints>();

    public delegate void EachPointEffect();

     public EachPointEffect _eachPointEffect;

     private bool _removePointMode;

    //stats from passiv tree
    public StatsHolder statsHolder = new StatsHolder();
    
    public static int xpForLvl1 = 100;
    public int xpToNextLvl;
    public int xp;
    public float lateXp;
    public int lvl;

    void Start()
    {
        UpdatePassiveTextPoint();
    }
    
    void Update()
    {
       
    }

    public void UpdatePassiveTextPoint()
    {
        passivesPointsLeftDisplay.text = "Passive Point left : " + passivPointsLeft;
    }

    public void UpdateSkillTreeValues(int nbrOfActivePoints, bool useAllPoints)
    {
        //set all Stats to 0
        statsHolder.ResetStats();

        if (useAllPoints)
        {
            if (_eachPointEffect != null)
            {
                _eachPointEffect();
            }
        }
        else
        {
            for (int i = 0; i < nbrOfActivePoints; i++)
            {
                var test = _eachPointEffect.GetInvocationList()[i];

                test.DynamicInvoke();
            }
        }
    }

    public void Switch_Remove_Spend_Points()
    {
        if (_removePointMode)
        {
            Gears.gears.eventManagerMain.SkillTree_SpendPointMode_Trigger(player.id);
            buttonSwitchModeText.text = "Remove Points";
        }
        else
        {
            Gears.gears.eventManagerMain.SkillTree_RemovePointMode_Trigger(player.id);
            buttonSwitchModeText.text = "Spend Points";
        }

        _removePointMode = !_removePointMode;
    }
    
    #region LvlFunc
    public void GetXp(int xpReceived)
    { 
        //Debug.Log("Get xp");
        xp += xpReceived;

        if (xp >= xpToNextLvl)
        {
            int newXp = xp - xpToNextLvl;
            
            LvlUp();
            
            GetXp(newXp);
        }
    }

    public void LvlUp()
    {
        //TODO
        lvl++;
        player.unitLvl++;

        xp = 0;
        lateXp = 0;
        
        UpdateXpBar();
        
        passivPointsLeft++;
        UpdatePassiveTextPoint();
        
        SetXpToNextLvl();
    }

    public void SetXpToNextLvl()
    {
        float f;
        f = xpForLvl1 * Mathf.Pow(1.1f , lvl);

        xpToNextLvl = (int) f;
    }

    public void UpdateXpBar()
    {
        CanvasMain.canvasMain.xpBarScaler.transform.localScale = new Vector3(
            lateXp / xpToNextLvl,  CanvasMain.canvasMain.xpBarScaler.transform.transform.localScale.y, 
            CanvasMain.canvasMain.xpBarScaler.transform.transform.localScale.z);
    }
    
    #endregion
}