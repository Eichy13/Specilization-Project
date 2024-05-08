using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDefenceAI : MonoBehaviour
{
    public int health;
    public int damage;
    public int baseType;
    public int teamNumber;
    private float actionTimer;

    public GameObject currentTarget;
   
    void Start()
    {
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

    void Update()
    {
        if (currentTarget == null)
        {
            GetTarget();
        }

        if (actionTimer > 3) //When next attack
        {
            if (currentTarget != null)
            {
                Attack();
            }
            actionTimer = 0;
        }
    }

    public void GetTarget() //Targets only the base defences
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

        float nearestDistance = 5; //Try see this radius

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

    public void Attack() //Shoot the target
    {

    }
}
