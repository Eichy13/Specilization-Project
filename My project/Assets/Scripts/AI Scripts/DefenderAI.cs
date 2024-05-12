using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DefenderAI : BaseAI
{
    public GameObject currentDefender;
    // Update is called once per frame
    void Update()
    {
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, healthBarTarget, 3 * Time.deltaTime);

        if (currentDefender == null)
        {
            GetBaseDefender();
        }

        if (currentTarget == null)
        {
            GetTarget();
        }

        if (actionTimer > actionCooldown)
        {
            GetTarget(); //Checks for nearest target
            if (currentMode == 1)
            {
                Attack();
            }
            actionCooldown = Random.Range(2, 4);
            actionTimer = 0;
        }

        if (dashTimer > dashCooldown)
        {
            dashActive = true;
            dashTimer = 0;
            dashCooldown = Random.Range(3, 5);
        }

        if (currentTarget != null)
        {
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

            //When to go into attacking mode
            if (Vector3.Distance(currentTarget.transform.position, transform.position) <= navMeshAgent.stoppingDistance && currentMode == 0)
            {
                currentMode = 1; //In attacking distance
                Debug.Log("In attacking mode");
            }

            if (Vector3.Distance(currentTarget.transform.position, transform.position) <= navMeshAgent.stoppingDistance && currentMode == 0 && pilotPlayStyle == 0) //Melee Attacking check
            {
                currentMode = 1;
                Attack();
            }
            else if (Vector3.Distance(currentTarget.transform.position, transform.position) >= navMeshAgent.stoppingDistance && currentMode == 0 && pilotPlayStyle == 0)
            {
                currentMode = 0;
            }
        }
        else //No targets
        {
            Pathfind(); //For if the protected turret change
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

    public void GetBaseDefender() //Targets only the base defences
    {
        GameObject[] objectsWithTag;
        if (teamNumber == 1)
        {
            objectsWithTag = GameObject.FindGameObjectsWithTag("Team1");
        }
        else
        {
            objectsWithTag = GameObject.FindGameObjectsWithTag("Team2");
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
                    currentDefender = obj;
                }
            }
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

        float nearestDistance = 7; //Try see this radius

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

    void Pathfind()
    {
        if (currentTarget == null)
        {
            navMeshAgent.destination = currentDefender.transform.position;
        }
        else
        {
            navMeshAgent.destination = currentTarget.transform.position;
        }
    }

}
