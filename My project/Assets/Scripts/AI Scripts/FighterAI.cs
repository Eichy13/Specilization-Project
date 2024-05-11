using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FighterAI : BaseAI
{
    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null)
        {
            currentMode = 0;
            GetTarget();
        }

        if (actionTimer > actionCooldown) //When next attack
        {
            GetTarget(); //Checks for nearest target
            if (currentMode == 1) //Mech is in attack mode 
            {
                Attack();
            }
            actionCooldown = Random.Range(2 ,4);
            currentStrafe = Random.Range(0, 2);
            actionTimer = 0;
        }

        Debug.Log(currentTarget);

        if (dashTimer > dashCooldown)
        {
            dashActive = true;
            if (currentMode == 0) //Dash off cooldown if its a mobility dash to catch up to its target
            {
                Debug.Log("Dashing");
                StartCoroutine(Dash(0));
            }
            dashTimer = 0;
            dashCooldown = Random.Range(3, 5); 
        }

        if (Vector3.Distance(currentTarget.transform.position, transform.position) <= navMeshAgent.stoppingDistance && currentMode == 0)
        {
            currentMode = 1; //In attacking distance
            Debug.Log("In attacking mode");
        }

        //When the enemy reaches too close, retreat away (Ranged)
        if (Vector3.Distance(currentTarget.transform.position, transform.position) < navMeshAgent.stoppingDistance - 0.5 && !retreatTrigger && pilotPlayStyle == 1)
        {
            Debug.Log("Target too close");
            retreatTrigger = true;
        }
        else
        {
            Pathfind();
        }

        if (retreatTrigger)
        {
            transform.position += -transform.forward * Time.deltaTime;
            if (Vector3.Distance(currentTarget.transform.position, transform.position) >= navMeshAgent.stoppingDistance)
            {
                retreatTrigger = false;
            }
        }

        if (currentMode == 1) //Attack mode special movement
        {
            if (currentStrafe == 0)
            {
                transform.position += transform.right * Time.deltaTime * 0.5f;
            }
            else
            {
                transform.position += -transform.right * Time.deltaTime * 0.5f;
            }
        }

    }

    private void FixedUpdate()
    {
        actionTimer += Time.deltaTime;
        if (!dashActive)
        {
            dashTimer += Time.deltaTime;
        }
    }

    public override void NavSetUp()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = currentMobility / 200;
        if (pilotPlayStyle == 0) // Melee Type
        {
            navMeshAgent.stoppingDistance = 1;
        }
        else // Ranged Type
        {
            navMeshAgent.stoppingDistance = mechRangedRange;
        }
    }

    public override void GetTarget()
    {
        if (currentTarget != null && currentTarget.GetComponent<BaseAI>()) //Once a fighter locks onto a mech, it dosent stop chasing it till it dies
        {
            return; 
        }

        GameObject[] objectsWithTag;
        if (teamNumber == 1)
        {
            objectsWithTag = GameObject.FindGameObjectsWithTag("Team2");
        }
        else
        {
            objectsWithTag = GameObject.FindGameObjectsWithTag("Team1");
        }

        float nearestDistance = Mathf.Infinity;

        foreach (GameObject obj in objectsWithTag)
        {
           
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                currentTarget = obj;
            }
        }
    }

    void Pathfind()
    {
        transform.LookAt(currentTarget.transform);
        navMeshAgent.destination = currentTarget.transform.position;
    }

}
