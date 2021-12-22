using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassivePoint : MonoBehaviour
{
    public SkillTree skillTreeLinkedTo;
    
    public int passivPointCost;
    protected int passivPointSpentInThisPoint;
    
    private Button myButton;

    public bool reached;

    public bool reachable;

    public PassivePoint[] pointsLinkedWith;
    
    [TextArea(3, 10)] public string effectDescription;

    private bool _highLighted;

    public List<Item_Modifier> modifiers = new List<Item_Modifier>();

    public virtual void Awake()
    {
        skillTreeLinkedTo = transform.GetComponentInParent<SkillTree>();
        
        myButton = GetComponent<Button>();
        
        SetToSpendMode(skillTreeLinkedTo.player.id);
        
        Gears.gears.eventManagerMain.SkillTree_SpendPointMode += SetToSpendMode;

        Gears.gears.eventManagerMain.SkillTree_RemovePointMode += SetToRemoveMode;

        if (pointsLinkedWith.Length != 0)
        {
            foreach (var point in pointsLinkedWith)
            {
                if (Array.Find(skillTreeLinkedTo.allLines.ToArray(), l => l.passivePoint1 == this && l.passivePoint2 == point ||
                                                                          l.passivePoint1 == point && l.passivePoint2 == this))
                {
                    //Debug.Log("line already Drawed");
                }
                else
                {
                    GameObject lineHandler = Instantiate(skillTreeLinkedTo.lineGameObject, CanvasMain.canvasMain.transform);
                
                    LinesPoints line = lineHandler.GetComponent<LinesPoints>();
                    line.passivePoint1 = this;
                    line.passivePoint2 = point;
                    line.Points = new Vector2[2];
                
                    line.Points[0] = GetComponent<RectTransform>().localPosition;
                    line.Points[1] = new Vector2(point.GetComponent<RectTransform>().localPosition.x, point.GetComponent<RectTransform>().localPosition.y);
                    line.transform.SetParent(skillTreeLinkedTo.lineContainer.transform);
                
                    skillTreeLinkedTo.allLines.Add(line);
                }
            }
        }
    }
    
    public void Update()
    {
        if (_highLighted)
        {
            skillTreeLinkedTo.tipsContainer.GetComponent<RectTransform>().transform.position = 
                new Vector3(Screen.width * skillTreeLinkedTo.tipsPosOnScreenWidth,
                    Screen.height * skillTreeLinkedTo.tipsPosOnScreenHeight, skillTreeLinkedTo.tipsContainer.GetComponent<RectTransform>().transform.position.z);
        }
    }

    #region PointsConditions
    public void AskToGet()
    {
        //Debug.Log("Ask to get");
        if (CanGetPoint())
        {
            //Debug.Log("Get Passiv point");
            
            skillTreeLinkedTo._eachPointEffect += Effect;

            Effect(); 
            
            skillTreeLinkedTo.passivPointsLeft -= passivPointCost;
            passivPointSpentInThisPoint = passivPointCost;
            skillTreeLinkedTo.UpdatePassiveTextPoint();
            
            reached = true;
            
            GetComponent<Image>().color = Color.yellow;
            
            foreach (var point in pointsLinkedWith)
            {
                LinesPoints linePoint = Array.Find(skillTreeLinkedTo.allLines.ToArray(), l =>
                    l.passivePoint1 == this && l.passivePoint2 == point || l.passivePoint1 == point && l.passivePoint2 == this);
                
                if (point.reached)
                {
                    linePoint.color = Color.yellow;
                }
                else
                {
                    Color colorReachable = new Color(0.5f, 0.5f, 0);
                    point.GetComponent<Image>().color = colorReachable;
                    linePoint.color = colorReachable;
                }
            }
        }
    }
    
    public bool CanGetPoint()
    {
        if (reached)
        {
            //Debug.Log("you already have this point");
            return false;
        }

        CheckIfPointIsReachableWithPassing();

        if (!reachable)
        {
            Debug.Log("Point Not reachable");
            return false;
        }
        
        if (passivPointCost > skillTreeLinkedTo.passivPointsLeft)
        {
            Debug.Log("Not enough passive points");
            return false;
        }

        return true;
    }

    public void CheckIfPointIsReachableWithPassing()
    {
        if (Array.Find(pointsLinkedWith, point => point.reached))
        {
            reachable = true;
        }
        else
        {
            reachable = false;
        }
    }

    public void AskRemovePoint()
    {
        if (CanRemovePoint())
        {
            skillTreeLinkedTo._eachPointEffect -= Effect;
            
            skillTreeLinkedTo.UpdateSkillTreeValues(0, true);
            
            skillTreeLinkedTo.passivPointsLeft += passivPointSpentInThisPoint;
            skillTreeLinkedTo.UpdatePassiveTextPoint();
            
            reached = false;
            
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0);
            
            foreach (var point in pointsLinkedWith)
            {
                point.CheckIfPointIsReachableWithPassing();
                
                LinesPoints linePoint = Array.Find(skillTreeLinkedTo.allLines.ToArray(), l =>
                    l.passivePoint1 == this && l.passivePoint2 == point || l.passivePoint1 == point && l.passivePoint2 == this);
                
                if (point.reached)
                {
                    Color colorReachable = new Color(0.5f, 0.5f, 0);
                    linePoint.color = colorReachable;
                }
                else
                {
                    linePoint.color = Color.white;
                    
                    if (Array.Find(point.pointsLinkedWith, passivePoint => passivePoint.GetType() == typeof(StartPoint)) == null)
                    {
                        point.GetComponent<Image>().color = Color.white;
                    }
                }
            }
        }
    }

    public bool CanRemovePoint()
    {
        if (!reached)
        {
            Debug.Log("You don't have this point");
            return false;
        }

        /*foreach (var point2 in pointsLinkedWith)
        {
            if (point2.reached)
            {
                if (Array.Find(point2.pointsLinkedWith, point => point.reached && point != this) == null)
                {
                    return true;
                }
            }
        }*/

        foreach (var point2 in pointsLinkedWith)
        {
            if (point2.reached && point2.GetType() != typeof(StartPoint))
            {
                if (Array.FindAll(point2.pointsLinkedWith, point => point.reached).Length <= 1)
                {
                    Debug.Log("Points can't be removed while its the only link to another reached point");
                    return false;
                }
            }
        }

        return true;
    }

    public void SetToRemoveMode(int id)
    {
        if (id == skillTreeLinkedTo.player.id)
        {
            myButton?.onClick.RemoveAllListeners();
            myButton?.onClick.AddListener(AskRemovePoint);
        }
    }
    
    public void SetToSpendMode(int id)
    {
        if (id == skillTreeLinkedTo.player.id)
        {
            myButton?.onClick.RemoveAllListeners();
            myButton?.onClick.AddListener(AskToGet);
        }
    }
    #endregion
    
    public void OnHighlightEnter()
    {
        skillTreeLinkedTo.passivesPointsDescriptionTextDisplay.text = effectDescription;
        
        skillTreeLinkedTo.passivPointCostText.text = "Cost : " + passivPointCost + " Passiv Point";

        skillTreeLinkedTo.tipsContainer.SetActive(true);

        _highLighted = true;
    }

    public void OnHighlightExit()
    {
        skillTreeLinkedTo.tipsContainer.SetActive(false);
        
        _highLighted = false;
    }
    
    public virtual void Effect()
    {
        //Debug.Log("Base Effect");

        foreach (var mod in modifiers)
        {
            mod.ModiferEffect();
        }

        skillTreeLinkedTo.player.UpdateAll();
    }
}