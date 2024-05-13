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

    public GameObject currentTarget; //Who to target

    //Info for the actual stats of the ai
    protected float currentHealth;
    protected float currentMaxHealth;
    protected int currentMeleeDamage;
    protected int currentRangedDamage;
    protected int currentMobility;
    protected int currentMode; //Mode 0 = chasing/looking for unit, Mode 1 = Found a target and attacks it
    protected int currentStrafe; //Mode 0 = Strafe left, Mode 1 = Strafe right
    protected bool dashActive; //If the dash is currently off cooldown
    
    //protected enum 


    [SerializeField] protected Image healthBarSprite;
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
        if (pilotPlayStyle == 0 && meleeTimer >= meleeCooldown) //Melee Attack
        {
            Debug.Log("Melee Attack");
            
            meleeTimer = 0;
            meleeCooldown = Random.Range(1, 3);

            BaseAI currentTargetAI = currentTarget.GetComponent<BaseAI>();
            if (currentTargetAI.dashActive == true)
            {
                StartCoroutine(currentTargetAI.Dash(1)); //Enemy will dodge the backwards if they have dash
            }
            else
            {
                //Damage dealt to enemy
                currentTargetAI.DamageTaken(currentMeleeDamage);
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
                        currentTargetAI.DamageTaken(currentRangedDamage);
                        //Damage
                    }
                }

                BaseDefenceAI currentTargetBase = currentTarget.GetComponent<BaseDefenceAI>();
                if (currentTargetBase != null) //Shooting at base
                {
                    currentTargetBase.DamageTaken(currentRangedDamage);
                }
                //Call the event here (But first do it without the event)
            }
        }
    }

    public void DamageTaken(int damagae)
    {
        currentHealth -= damagae / 10   ;
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
}
