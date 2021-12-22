using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : StateMachine
{
    public Player player;

    public GameObject itemsTooltip;

    public override void Awake()
    {
        base.Awake();
        
        if (Gears.gears)
        {
            Gears.gears.managerMain.playerActionManager = this;
        }
    }

    public override void Start()
    {
        base.Start();
        
        Gears.gears.managerMain.playerActionManager = this;
    }


    public override void Update()
    {
        base.Update();

        if (Input.GetButtonDown("Escape"))
        {
            if (CanvasMain.canvasMain.escapePanel.activeSelf)
            {
                CanvasMain.canvasMain.escapePanel.SetActive(false);
            }else
            {
                CanvasMain.canvasMain.escapePanel.SetActive(true);
            }
        }
    }
}
