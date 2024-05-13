using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerProjectile : MonoBehaviour
{
    public IEnumerator Shoot(Vector3 enemyPosition)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = enemyPosition;
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
}
