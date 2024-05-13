using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseDefenceAI : MonoBehaviour
{
    public float health;
    private int currentMaxHealth;
    public int damage;
    public int baseType;
    public int teamNumber;
    private float actionTimer;
    [SerializeField] protected Image healthBarSprite;
    protected float healthBarTarget;

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

        healthBarTarget = 1;
        UpdateHealthBar();
    }

    void Update()
    {
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, healthBarTarget, 3 * Time.deltaTime);

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

    public void DamageTaken(int damagae)
    {
        health -= damagae / 10   ;
        Debug.Log("New health: " + health);

        UpdateHealthBar();

        if (health <= 0)
        {
            Destroy(gameObject); //Death
        }
    }

    public void UpdateHealthBar()
    {
        healthBarTarget = health / currentMaxHealth;
    }
}
