using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager
{
    //Player events
    public event Action<int> PlayerHited;

    public void PlayerHitedTrigger(int id)
    {
        if (PlayerHited != null)
        {
            PlayerHited(id);
        }
    }
    
    public event Action<int> PlayerOnKill;

    public void PlayerOnKillTrigger(int id)
    {
        if (PlayerOnKill != null)
        {
            PlayerOnKill(id);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    

    //Skill Tree events
    public event Action<int> SkillTree_RemovePointMode;
    
    public void SkillTree_RemovePointMode_Trigger(int id)
    {
        if (SkillTree_RemovePointMode != null)
        {
            SkillTree_RemovePointMode(id);
        }
    }
    
    public event Action<int> SkillTree_SpendPointMode;
    
    public void SkillTree_SpendPointMode_Trigger(int id)
    {
        if (SkillTree_SpendPointMode != null)
        {
            SkillTree_SpendPointMode(id);
        }
    }
}
