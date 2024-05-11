using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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
    protected int currentHealth; 
    protected int currentMeleeDamage;
    protected int currentRangedDamage;
    protected int currentMobility;
    protected int currentMode; //Mode 0 = chasing/looking for unit, Mode 1 = Found a target and attacks it
    protected int currentStrafe; //Mode 0 = Strafe left, Mode 1 = Strafe right
    protected bool dashActive; //If the dash is currently off cooldown


    protected int pilotPlayStyle; //Melee or Ranged AI
    protected NavMeshAgent navMeshAgent;
    protected float actionTimer; //Time till next attack
    protected int actionCooldown;
    protected int dashCooldown;
    protected float dashTimer;
    protected int teamNumber;
    protected bool retreatTrigger;
    public UnityEvent events = new UnityEvent(); //Event triggers for abilities and getting hit

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = mechHealth + pilotHealth;
        currentMeleeDamage = mechMeleeDamage + pilotMeleeDamage;
        currentRangedDamage = mechRangedDamage + pilotRangedDamage;
        currentMobility = mechMobility + pilotMobility;
        retreatTrigger = false;
        currentMode = 0;
        currentStrafe = 0;
        actionCooldown = Random.Range(2 ,4);
        dashCooldown = Random.Range(3, 5); 
        dashActive = false;

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

    public void Attack()
    {
        if (pilotPlayStyle == 0) //Melee Attack
        {

        }
        else //Ranged attack
        {
            GameObject projectile = ObjectPool.SharedInstance.GetPooledObject(); 
            if (projectile != null) {
                projectile.transform.position = transform.position;
                projectile.transform.rotation = transform.rotation;
                projectile.SetActive(true);

                LazerProjectile lazerProjectile = projectile.GetComponent<LazerProjectile>();
                StartCoroutine(lazerProjectile.Shoot(currentTarget.transform.position));    

                //BaseAI currentTargetAI = currentTarget.GetComponent<BaseAI>();
                //Call the event here
            }
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
}
