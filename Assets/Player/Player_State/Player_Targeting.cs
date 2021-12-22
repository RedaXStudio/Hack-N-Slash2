using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Targeting : UnitTargeting
{
    public Player_Targeting(Units stateMachine) : base(stateMachine)
    {
        unit = stateMachine;
    }
    
    public override void OnStateEnter() { }

    public override void Tick()
    {
        if (Input.GetButtonDown("Fire2") && !Gears.gears.managerMain.canvasMain.IsMouseOverUiIgnore() && 
            Gears.gears.managerMain.playerActionManager.currentState == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 1000, Gears.gears.SelectableLayer))
            {
                target = hit.collider.gameObject;
                //Debug.Log("targeting");
                
                //Gears.gears.managerMain.canvasMain.targetHpBar.SetActive(true);
                CanvasMain.canvasMain.targetHpBar.SetActive(true);

                CanvasMain.canvasMain.targetHpBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = target.name;
            }
        }

        if (Input.GetButtonDown("Fire1") && Gears.gears.managerMain.playerActionManager.currentState == null)
        {
            target = null;
        }

        if (target)
        {
            CanvasMain.canvasMain.targetHpBar.transform.GetChild(0).transform.localScale = 
                new Vector3(target.GetComponent<Units>().currentLife / target.GetComponent<Units>().maximumLife, 
                    CanvasMain.canvasMain.targetHpBar.transform.GetChild(0).transform.localScale.y, 
                    CanvasMain.canvasMain.targetHpBar.transform.GetChild(0).transform.localScale.z);
        }
        else
        {
            //TODO : add onKill modifier to manage Ui
            //Gears.gears.managerMain.canvasMain.targetHpBar.SetActive(false);
            CanvasMain.canvasMain.targetHpBar.SetActive(false);
        }
    }
    
    public override void OnStateExit() { }
}
