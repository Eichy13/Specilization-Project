using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RusherAI : BaseAI
{
    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null)
        {
            GetTarget();
        }

        if (actionTimer > currentMobility)
        {
            GetTarget(); //Checks for nearest target
            actionTimer = 0;
        }

        if (dashTimer > dashCooldown)
        {
            Dash();
            dashTimer = 0;
            dashCooldown = Random.Range(5, 10);
        }

        Pathfind();
        Debug.Log(currentTarget);

        if (Vector3.Distance(currentTarget.transform.position, transform.position) <= navMeshAgent.stoppingDistance && currentMode == 0)
        {
            currentMode = 1; //In attacking distance
            Debug.Log("In attacking mode");
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
        dashTimer += Time.deltaTime; 
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

    public override void GetTarget() //Targets only the base defences
    {
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
            MonoBehaviour scriptComponent = obj.GetComponent<BaseDefenceAI>() as MonoBehaviour;

            if (scriptComponent != null)
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    currentTarget = obj;
                }
            }
        }
    }

    public override void Dash()
    {
        //Grab my dash code from previous project
    }

    public override void Attack()
    {
        if (pilotPlayStyle == 0) //Melee Attack
        {

        }
        else //Ranged attackk
        {
            
        }
    }

    void Pathfind()
    {
        navMeshAgent.destination = currentTarget.transform.position;
    }

}
