using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPoint : PassivePoint
{
    void Start()
    {
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
                Color colorReached = new Color(0.5f, 0.5f, 0);
                point.GetComponent<Image>().color = colorReached;
                linePoint.color = colorReached;
            }
        }
    }
    
    new void Update()
    {
        
    }
}
