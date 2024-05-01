using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAI : MonoBehaviour
{
    public int mechActiveType; //Mech selectd info (Pass through the selection screen (Ltr)
    public int mechHealth;
    public int mechMeleeDamage;
    public int mechRangedDamage;
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

    protected int pilotPlayStyle; //Melee or Ranged AI
    protected NavMeshAgent navMeshAgent;
    protected float actionTimer;
    protected int dashCooldown;
    protected float dashTimer;
    protected int teamNumber;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = mechHealth + pilotHealth;
        currentMeleeDamage = mechMeleeDamage + pilotMeleeDamage;
        currentRangedDamage = mechRangedDamage + pilotRangedDamage;
        currentMobility = mechMobility + pilotMobility;
        dashCooldown = Random.Range(5, 10); 

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
    }

    public virtual void NavSetUp()
    {

    }   

    public virtual void GetTarget()
    {
    }

    public virtual void Dash()
    {

    }
}
