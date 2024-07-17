using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BaseAI;

public class BaseDefenceAI : MonoBehaviour
{
    public float currentMaxHealth;
    public float health;
    public int damage;
    public int baseType;
    public int teamNumber;
    private float actionTimer;
    [SerializeField] protected Image healthBarSprite;
    protected float healthBarTarget;

    [SerializeField] protected Image healthBar2Sprite;
    protected float healthBar2Target;

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

        health = currentMaxHealth;
        healthBarTarget = 1;
        UpdateHealthBar();
    }

    void Update()
    {
        actionTimer += Time.deltaTime;
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, healthBarTarget, 3 * Time.deltaTime);
        healthBar2Sprite.fillAmount = Mathf.MoveTowards(healthBar2Sprite.fillAmount, healthBarTarget, 3 * Time.deltaTime);

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
        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject();
        if (projectile != null)
        {
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
                    currentTargetAI.DamageTurretTaken(damage);
                }
            }
        }
    }

    public void DamageTaken(float damage, BaseAI.PilotAIType attackingPilotType)
    {
        switch (attackingPilotType)
        {
            case (BaseAI.PilotAIType.Fighter):
            {
                damage = damage * 0.5f;
                break;
            }
            case (BaseAI.PilotAIType.Rusher):
            {
                damage = damage * 2f;
                break;
            }
            default: 
            {
                break;
            }
        }

        health -= damage / 10   ;
        Debug.Log("New health: " + health);

        UpdateHealthBar();

        if (health <= 0)
        {
            Destroy(gameObject); //Death
        }
    }

    public void UpdateHealthBar()
    {
        Debug.Log("Update HP");
        healthBarTarget = health / currentMaxHealth;
        healthBar2Target = health / currentMaxHealth;
    }
}
