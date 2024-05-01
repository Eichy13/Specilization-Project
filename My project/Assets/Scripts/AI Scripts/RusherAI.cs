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
            navMeshAgent.stoppingDistance = 5;
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
            MonoBehaviour scriptComponent = obj.GetComponent<BaseAI>() as MonoBehaviour;

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

    void Action()
    {

    }

    void Pathfind()
    {
        navMeshAgent.destination = currentTarget.transform.position;
    }

}
