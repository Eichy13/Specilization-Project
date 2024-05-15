using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaseAI : MonoBehaviour
{
    public int mechActiveType; //Mech selectd info (Pass through the selection screen (Ltr)
    public int mechHealth;
    public int mechMeleeDamage;
    public int mechRangedDamage;
    public int mechRangedRange; //Set unique stopping distance for specific mech ranges (Only triggers if the mech is in ranged mode)
    public int mechMobility;

    public int pilotPassiveType; //Pilot selected info
    public int pilotHealth;
    public int pilotMeleeDamage;
    public int pilotRangedDamage;
    public int pilotMobility;
    public enum PilotAIType {Fighter, Rusher, Defender};
    public PilotAIType pilotAIType;

    public GameObject currentTarget; //Who to target

    //Info for the actual stats of the ai
    protected float currentHealth;
    protected float currentMaxHealth;
    protected float currentMeleeDamage;
    protected float currentRangedDamage;
    protected float currentMobility;
    protected int currentMode; //Mode 0 = chasing/looking for unit, Mode 1 = Found a target and attacks it
    protected int currentStrafe; //Mode 0 = Strafe left, Mode 1 = Strafe right
    protected bool dashActive; //If the dash is currently off cooldown
    protected float damageBuff; //Goes up to -3 to +3
    protected float defenceBuff; //Goes up to -3 to +3
    protected float speedBuff; //Goes up to -3 to +3

    [SerializeField] protected Image healthBarSprite;
    [SerializeField] protected Image[] damageBuffSprite;
    [SerializeField] protected Image[] defenceBuffSprite;
    [SerializeField] protected Image[] speedBuffSprite;
    protected int pilotPlayStyle; //Melee or Ranged AI
    protected NavMeshAgent navMeshAgent;
    protected float actionTimer; //Time till next attack
    protected int actionCooldown;
    protected float meleeTimer; //Time till next meleeattack
    protected int meleeCooldown;
    protected int dashCooldown;
    protected float dashTimer;
    protected int teamNumber;
    protected bool retreatTrigger;
    public UnityEvent events = new UnityEvent(); //Event triggers for abilities and getting hit
    protected float healthBarTarget;

    //All objects in specific ranges
    protected List<GameObject> allyObjectsInRange3 = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = mechHealth + pilotHealth;
        currentMaxHealth = currentHealth;
        currentMeleeDamage = mechMeleeDamage + pilotMeleeDamage;
        currentRangedDamage = mechRangedDamage + pilotRangedDamage;
        currentMobility = mechMobility + pilotMobility;
        retreatTrigger = false;
        currentMode = 0;
        currentStrafe = 0;
        actionCooldown = Random.Range(2 ,4);
        meleeCooldown = Random.Range(2 ,4);
        dashCooldown = Random.Range(3, 5); 
        dashActive = false;
        damageBuff = 0;
        defenceBuff = 0;
        speedBuff = 0;
        healthBarTarget = 1;

        if (currentMeleeDamage > currentRangedDamage)
        {
            pilotPlayStyle = 0;
        }
        else
        {
            pilotPlayStyle = 1;
        }

        actionTimer = 0;
        NavSetUp();
        UpdateIcons();
        if (gameObject.tag == "Team1")
        {
            teamNumber = 1;
        }
        else if (gameObject.tag == "Team2")
        {
            teamNumber = 2;
        }
        GetTarget();
        events.AddListener(Hit);
        UpdateHealthBar();
    }

    public virtual void NavSetUp()
    {

    }   

    public virtual void GetTarget()
    {
    }

    public IEnumerator Dash(int typeOfDash)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition;
        float t = 0f;
        switch (typeOfDash)
        {
            case (0): //Forward Dash
            {
                endPosition = transform.position += transform.forward * (currentMobility / 200);
                break;
            }
            case (1): //Backwards Dash
            {
                endPosition = transform.position -= transform.forward * (currentMobility / 200);
                break;
            }
            case (2): //Left Dash
            {
                endPosition = transform.position -= transform.right * (currentMobility / 200);
                break;
            }
            case (3): //Right Dash
            {
                endPosition = transform.position += transform.right * (currentMobility / 200);
                break;
            }
            default: 
            {
                endPosition = startPosition;
                break;
            }
        }
        while (t < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            t += Time.deltaTime * (currentMobility / 60);
            yield return null;
        }
        dashActive = false;
        yield return null;
    }

    public virtual void Attack() //Add an unique attack function to each one so that the damage multipliers can be done accordingly
    {
        //Damage mulitplier Fighter: 
        //Fighter x Fighter = 1.0
        //Fighter x Rusher = 0.75
        //Fighter x Defender = 1.5
        //Fighter x Base = 0.5

        //Damage mulitplier Rusher: 
        //Rusher x Base = 2.0

        //Damage mulitplier Defender: 
        //Defender x Fighter = 0.5
        //Defender x Rusher = 1.5

        if (pilotPlayStyle == 0 && meleeTimer >= meleeCooldown) //Melee Attack
        {
            Debug.Log("Melee Attack");
            
            meleeTimer = 0;
            meleeCooldown = Random.Range(1, 3);

            BaseAI currentTargetAI = currentTarget.GetComponent<BaseAI>();
            if (currentTargetAI != null) //Attacking mech
            {
                if (currentTargetAI.dashActive == true)
                {
                    StartCoroutine(currentTargetAI.Dash(1)); //Enemy will dodge the backwards if they have dash
                }
                else
                {   
                    currentTargetAI.DamageTaken(currentMeleeDamage * (1.00f + ((damageBuff * 10) / 100)), pilotAIType);
                }
            }

            BaseDefenceAI currentTargetBase = currentTarget.GetComponent<BaseDefenceAI>();
            if (currentTargetBase != null) //Attacking base
            {
                currentTargetBase.DamageTaken(currentRangedDamage  * (1.00f + ((damageBuff * 10) / 100)), pilotAIType);
            }
        }
        else if (pilotPlayStyle == 1) //Ranged attack
        {
            GameObject projectile = ObjectPool.SharedInstance.GetPooledObject(); 
            if (projectile != null) {
                projectile.transform.position = transform.position;
                projectile.transform.rotation = transform.rotation;
                projectile.SetActive(true);
                if (gameObject.tag == "Team1")
                {
                    projectile.tag = "Team1";
                }
                else if (gameObject.tag == "Team2")
                {
                    projectile.tag = "Team2";
                }

                LazerProjectile lazerProjectile = projectile.GetComponent<LazerProjectile>();
                StartCoroutine(lazerProjectile.Shoot(currentTarget.transform.position));    

                BaseAI currentTargetAI = currentTarget.GetComponent<BaseAI>();
                if (currentTargetAI != null) //Shooting at A
                {
                    if (currentTargetAI.dashActive == true)
                    {
                        StartCoroutine(currentTargetAI.Dash(Random.Range(2, 4))); //Enemy will dodge the projectile if they have dash
                    }
                    else
                    {
                        currentTargetAI.DamageTaken(currentRangedDamage  * (1.00f + ((damageBuff * 10) / 100)), pilotAIType);
                        //Damage
                    }
                }

                BaseDefenceAI currentTargetBase = currentTarget.GetComponent<BaseDefenceAI>();
                if (currentTargetBase != null) //Shooting at base
                {
                    currentTargetBase.DamageTaken(currentRangedDamage  * (1.00f + ((damageBuff * 10) / 100)), pilotAIType);
                }
                //Call the event here (But first do it without the event)
            }
        }
    }

    public void DamageTaken(float damage, PilotAIType attackingPilotType)
    {
        switch (attackingPilotType)
        {
            case (PilotAIType.Fighter):
            {
                if (pilotAIType == PilotAIType.Rusher)
                {
                    damage = damage * 0.75f;
                }
                else if (pilotAIType == PilotAIType.Defender)
                {
                    damage = damage * 1.5f;
                }
                break;
            }
            case (PilotAIType.Defender):
            {
                if (pilotAIType == PilotAIType.Fighter)
                {
                    damage = damage * 0.5f;
                }
                else if (pilotAIType == PilotAIType.Rusher)
                {
                    damage = damage * 2f;
                }
                break;
            }
            default: 
            {
                break;
            }
        }

        currentHealth -= (damage / 10); //Prep defence buff
        Debug.Log("New health: " + currentHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Destroy(gameObject); //Death
        }
    }

    public void Hit() //Event of being hit
    {
        if (dashActive)
        {
            StartCoroutine(Dash(Random.Range(1, 4)));
            return;
        }
        else
        {
            //currentHealth -= damage;
            if (currentHealth <= 0)
            {
                //Death Code (Ui Stuff do later)
            }
        }
    }

    public void UpdateHealthBar()
    {
        healthBarTarget = currentHealth / currentMaxHealth;
    }

    public void MechActive()
    {
        MechActiveCheckers();
        //Check which mech active to use
        switch (mechActiveType)
        {
            case (0): // +1 Damage buff around an aoe (Small range)
            {
                foreach (GameObject obj in allyObjectsInRange3)
                {
                    BaseAI temp = obj.GetComponent<BaseAI>();
                    temp.damageBuff += 1;
                    temp.UpdateIcons();
                }
                damageBuff += 1;
                UpdateIcons();
                break;
            }
            case (1): 
            {
                break;
            }
            default: 
            {
                break;
            }
        }   
    }

    public void MechActiveCheckers() //Check all the near objects 
    {
        //Clear all lists before checking again
        allyObjectsInRange3.Clear();

        GameObject[] objectsWithTag;
        if (teamNumber == 1)
        {
            objectsWithTag = GameObject.FindGameObjectsWithTag("Team1");
        }
        else
        {
            objectsWithTag = GameObject.FindGameObjectsWithTag("Team2");
        }

        float nearestDistance = 3; //Try see this radius

        foreach (GameObject obj in objectsWithTag)
        {
            MonoBehaviour scriptComponent = obj.GetComponent<BaseAI>() as MonoBehaviour;

            if (scriptComponent != null)
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);

                if (distance <= nearestDistance)
                {
                    allyObjectsInRange3.Add(obj);
                }
            }
        }
    }

    public void UpdateIcons()
    {
        //Turn off every icon
        foreach (Image icon in damageBuffSprite)
        {
            icon.enabled = false;
        }
        foreach (Image icon in defenceBuffSprite)
        {
            icon.enabled = false;
        }
        foreach (Image icon in speedBuffSprite)
        {
            icon.enabled = false;
        }

        //Dont forget to add it to consider negative buffs 
        if (damageBuff != 0)
        {
            damageBuffSprite[(int)damageBuff + 3].enabled = true;
        }
        if (defenceBuff != 0)
        {
            defenceBuffSprite[(int)defenceBuff + 3].enabled = true;
        }
        if (speedBuff != 0)
        {
            speedBuffSprite[(int)speedBuff + 3].enabled = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a sphere to visualize the range in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);
    }
}
