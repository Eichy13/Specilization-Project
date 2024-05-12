using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerProjectile : MonoBehaviour
{
    public int projectileDamage;
    public IEnumerator Shoot(Vector3 enemyPosition, int damage)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = enemyPosition;
        projectileDamage = damage;
        float t = 0f;

        while (t < 1.25f)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            t += Time.deltaTime * Vector3.Distance(startPosition, endPosition);
            yield return null;
        }
        gameObject.SetActive(false);
        yield return null;
    }

    private void OnCollisionEnter(Collision collision) //Check if the bullet has hit a target
    {
        string tagToCheck = "";
        if (gameObject.tag == "Team1")
        {
            tagToCheck = "Team2";
        }
        else if (gameObject.tag == "Team2")
        {
            tagToCheck = "Team1";
        }
        // Check if the collided object has the desired tag
        if (collision.gameObject.CompareTag(tagToCheck))
        {
            // Check if the collided object has the desired script attached to it
            BaseAI targetHit = collision.gameObject.GetComponent<BaseAI>();
            if (targetHit != null)
            {
                // The collided object has both the desired tag and script
                Debug.Log("Hit enemy");
                targetHit.DamageTaken(projectileDamage);
                gameObject.SetActive(false);
            }

            BaseDefenceAI baseHit = collision.gameObject.GetComponent<BaseDefenceAI>();
            if (targetHit != null)
            {
                // The collided object has both the desired tag and script
                Debug.Log("Hit base");
                //baseHit.DamageTaken(projectileDamage); Damage to base not added yet
                gameObject.SetActive(false);
            }
        }
    }
}
