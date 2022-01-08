using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Units : MultiStateMachine
{
    [Header("Units Settings")]
    
    public int id;

    public int unitLvl;
    
    public State_Multi_Modifier actionsState;
    
    [NonSerialized] public int attackStateIndex;
    [NonSerialized] public int targetingStateIndex;
    [NonSerialized] public int movementStateIndex;

    public string team;
    
    public TrueStatsHolder baseStats = new TrueStatsHolder();
    public StatsHolder statsHolder = new StatsHolder();

    [NonSerialized] public float finalMovementSpeed;
    [NonSerialized] public float finalAttackPhysicalLow;
    [NonSerialized] public float finalAttackPhysicalHigh;
    
    [NonSerialized] public float finalAttackFireLow;
    [NonSerialized] public float finalAttackFireHigh;
    
    [NonSerialized] public float finalAttackColdLow;
    [NonSerialized] public float finalAttackColdHigh;
    
    [NonSerialized] public float finalAttackLightningLow;
    [NonSerialized] public float finalAttackLightningHigh;
    
    [NonSerialized] public float finalAttackChaosLow;
    [NonSerialized] public float finalAttackChaosHigh;

    [NonSerialized] public float finalAttackSpeed;

    public float maximumLife;

    public float currentLife;
    
    public int nbrOfDamageConversion = 1;

    //ui
    protected GameObject hpBar;
    public float hpBarPos;
    
    //public event Action<List<Damage>> onGetDamage;

    public override void Start()
    {
        base.Start();

        SetState(new State_Multi_Modifier(this), actionsState, out actionsState);

        //onGetDamage += TestEvent;
    }

    public override void Update()
    {
        base.Update();
        actionsState?.Tick();

        currentLife += Time.deltaTime * statsHolder.lifeRegenPerSecond;

        currentLife = Mathf.Clamp(currentLife, 0, maximumLife);

        if (hpBar)
        {
            UpdateHpBar();
        }
    }

    /*public void TestEvent(List<Damage> u)
    {
        Debug.Log("TestEvent : " + u[0].damages);
        foreach (var damage in u)
        {
            damage.damages *= 1.1f;
        }
    }*/
    
    public void SetState(State_Multi_Modifier newState, State_Multi_Modifier definedState, out State_Multi_Modifier outState)
    {
        if (definedState != null)
            definedState.OnStateExit();

        outState = newState;

        outState.OnStateEnter();
    }
    
    #region Damages_Taken

    public virtual void TakeDamage(Damage[] damageTypes, Units attacker, int NbrOfConversion)
    {
        List<Damage> allDamageToTake = new List<Damage>();
        
        Damage[] damagesToConvert = damageTypes;
        
        DamageModifier(damagesToConvert, attacker);
        
        allDamageToTake.AddRange(damagesToConvert);
        
        for (int i = 0; i < NbrOfConversion; i++)
        {
            damagesToConvert = DamageTakenAs(damagesToConvert, attacker);

            DamageModifier(damagesToConvert, attacker);

            allDamageToTake.AddRange(damagesToConvert);
        }
        
        TakeFinalDamages(allDamageToTake.ToArray(), attacker);
    }

    public virtual Damage[] DamageTakenAs(Damage[] damagesTypes, Units attacker)
    {
        float[] reduceDamages = new float[damagesTypes.Length];

        List<Damage> newDamagesFromConversion = new List<Damage>();

        for (int i = 0; i < damagesTypes.Length; i++)
        {
            foreach (var damageTakenAs in statsHolder.damageTakenAs_Modifiers)
            {
                damageTakenAs.Conversion(damagesTypes[i].ShallowCopy(), reduceDamages[i], 
                    out reduceDamages[i] , newDamagesFromConversion);
            }
        }

        for (int i = 0; i < damagesTypes.Length; i++)
        {
            //1 - (1 - 0.9) + (1 - 0.9) = 0.8 utility calc
            damagesTypes[i].damages -= reduceDamages[i];
        }
        
        return newDamagesFromConversion.ToArray();
    }
    
    public virtual void DamageModifier(Damage[] damagesTypes, Units attacker)
    {
        for (int i = 0; i < damagesTypes.Length; i++) 
        {
            foreach (var damageModifier in statsHolder.damageTakenModifiers)
            {
                //Debug.Log("index : " + i + " = " + damagesTypes[i].myType + " " + damagesTypes[i].damages);
                
                damageModifier.Modifier(damagesTypes[i].ShallowCopy(), damagesTypes[i]);
            }
        }
    }

    public virtual void TakeFinalDamages(Damage[] damageTypes, Units attacker)
    {
        foreach (var damageType in damageTypes)
        {
            //Debug.Log(gameObject.name + " Took : " + damageType.damages + " " + damageType.type + " damages");
            
            currentLife -= damageType.damages;

            currentLife = Mathf.Clamp(currentLife, 0, maximumLife);
        }
        
        if (Array.Find(damageTypes, types => types.formOfDamages == CombatSystemeData.DamageForm.HIT) != null)
        {
            WhenHit();
        } //make a delegate func at the here and after checkDeath to add a mode that change if OnHit() is called befor or after checkdeath
        
        CheckDeath(attacker);
    }

    public virtual void CheckDeath(Units killer)
    {
        if (currentLife <= 0)
        {
            Debug.Log(gameObject.name + " DEAD");
            
            foreach (var deathsEffect in statsHolder.DeathsEffects)
            {
                deathsEffect.DeathRattle();
                deathsEffect.DeathRattle(killer);
            }

            if (killer.GetType() == typeof(Player))
            {
              
                Gears.gears.eventManagerMain.PlayerOnKillTrigger(((Player)killer).id);
            }
            else
            {
                killer.OnKill();
            }
            
            Destroy(gameObject);

           
        }
    }

    public Damage[] GetDamages(float damageEffectiveness)
    {
        List<Damage> damagesRoll = new List<Damage>();

        if (finalAttackPhysicalHigh > 0) {
            damagesRoll.Add(new Damage(CombatSystemeData.DamageType.PHYSICAL, CombatSystemeData.DamageForm.HIT, 
                Random.Range(finalAttackPhysicalLow, finalAttackPhysicalHigh) * damageEffectiveness));
        }
        if (finalAttackFireHigh > 0) {
            //Debug.Log("Fire Damage added to attack/Spell");
            damagesRoll.Add(new Damage(CombatSystemeData.DamageType.FIRE, CombatSystemeData.DamageForm.HIT, 
                Random.Range(finalAttackFireLow, finalAttackFireHigh) * damageEffectiveness));
        }
        if (finalAttackColdHigh > 0) {
            //Debug.Log("Cold Damage added to attack/Spell");
            damagesRoll.Add(new Damage(CombatSystemeData.DamageType.COLD, CombatSystemeData.DamageForm.HIT, 
                Random.Range(finalAttackColdLow, finalAttackColdHigh) * damageEffectiveness));
        }
        if (finalAttackLightningHigh > 0) {
            //Debug.Log("Lightning Damage added to attack/Spell");
            damagesRoll.Add(new Damage(CombatSystemeData.DamageType.LIGHTING, CombatSystemeData.DamageForm.HIT, 
                Random.Range(finalAttackLightningLow, finalAttackLightningHigh) * damageEffectiveness));
        }
        if (finalAttackChaosHigh > 0) {
            //Debug.Log("Chaos Damage added to attack/Spell");
            damagesRoll.Add(new Damage(CombatSystemeData.DamageType.CHAOS, CombatSystemeData.DamageForm.HIT, 
                Random.Range(finalAttackChaosLow, finalAttackChaosHigh) * damageEffectiveness));
        }

        /*if (onGetDamage != null)
        {
            onGetDamage(damagesRoll);
            Debug.Log(damagesRoll[0].damages);
        }*/

        return damagesRoll.ToArray();
    }
    
    #endregion

    #region UpdatesStats
    
    public virtual void UpdateAll()
    {
        statsHolder.ResetStats(baseStats);
        statsHolder.UpdateResistance();
        UpdateMaximumLife();
        UpdateFinalDamages();
        UpdateMovementSpeed();
    }
    
    public void UpdateMaximumLife()
    {
        //TODO
        float percentLife = currentLife / maximumLife;
        
        if (currentLife == 0)
        {
            percentLife = 1;
        }

        maximumLife = 0;

        maximumLife = statsHolder.baseLife * (1 + statsHolder.increasedMaximumLife / 100);
        
        //Debug.Log("Maximum Life : " + maximumLife);

        currentLife = maximumLife * percentLife;
    }

    public virtual void UpdateMovementSpeed()
    {
        finalMovementSpeed = 0;
        finalMovementSpeed = statsHolder.baseMovementSpeed * (1 + statsHolder.increaseMovementSpeed / 100);
    }

    protected void UpdateFinalDamages()
    {
        finalAttackPhysicalHigh = 0;
        finalAttackPhysicalLow = 0;

        finalAttackFireLow = 0;
        finalAttackFireHigh = 0;

        finalAttackColdLow = 0;
        finalAttackColdHigh = 0;

        finalAttackLightningLow = 0;
        finalAttackLightningHigh = 0;

        finalAttackChaosLow = 0;
        finalAttackChaosHigh = 0;
        
        finalAttackSpeed = 0;
        
        if (statsHolder.addedPhysicalDamagesHigh > 0)
        {
            finalAttackPhysicalLow += statsHolder.addedPhysicalDamagesLow;
            finalAttackPhysicalHigh += statsHolder.addedPhysicalDamagesHigh;
        }
        
        if (statsHolder.addedFireDamagesHigh > 0)
        {
            finalAttackFireLow += statsHolder.addedFireDamagesLow;
            finalAttackFireHigh += statsHolder.addedFireDamagesHigh;
        }
        
        if (statsHolder.addedColdDamageHigh > 0)
        {
            finalAttackColdLow += statsHolder.addedColdDamageLow;
            finalAttackColdHigh += statsHolder.addedColdDamageHigh;
        }
        
        if (statsHolder.addedLightningDamageHigh > 0)
        {
            finalAttackLightningLow += statsHolder.addedLightningDamageLow;
            finalAttackLightningHigh += statsHolder.addedLightningDamageHigh;
        }
        
        if (statsHolder.addedChaosDamageHigh > 0)
        {
            finalAttackChaosLow += statsHolder.addedChaosDamageLow;
            finalAttackChaosHigh += statsHolder.addedChaosDamageHigh;
        }

        finalAttackSpeed = statsHolder.baseAttackSpeed * (1 + statsHolder.increaseAttackSpeed / 100);
    }
    
    #endregion

    #region EventFunctions

    public virtual void OnKill()
    {
        //Debug.Log("BaseOnKill");

        foreach (var onKillModifier in statsHolder.onKillModifiers)
        {
            onKillModifier.Effect();
        }
    }

    public virtual void WhenHit()
    {
        //Debug.Log("Base On Hit");

        foreach (var whenHitModifier in statsHolder.whenHitModifiers)
        {
            whenHitModifier.Effect();
        }
    }


    #endregion

    #region UI

    public void ShowHpBar()
    {
        if (hpBar)
        {
            hpBar.SetActive(true);
        }
        else
        {
            hpBar = Instantiate(Gears.gears.hpBarPrefab, CanvasMain.canvasMain.hpBarUi_Parent.transform);
        }
    }

    public virtual void UpdateHpBar()
    {
        //Debug.Log("Update Hp Bar");
        //pos
        hpBar.GetComponent<RectTransform>().position = Gears.gears.mainCam.WorldToScreenPoint(transform.position + new Vector3(0, hpBarPos, 0));

        //hp
        hpBar.transform.GetChild(0).transform.localScale = new Vector3(currentLife / maximumLife, hpBar.transform.GetChild(0).transform.localScale.y, 
            hpBar.transform.GetChild(0).transform.localScale.z);
    }

    public void HideHpBar()
    {
        if (hpBar)
        {
            hpBar.SetActive(false);
        }
    }

    #endregion
}
