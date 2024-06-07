using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private float timer;
    private float enemySpawnTimer;
    //Focus on the player resource right now, ai dosent need it yet
    public TeamManager team1;
    public TeamManager team2;

    [SerializeField] private TMP_Text costText;
    [SerializeField] protected Image costBarSprite;
    protected float costBarTarget;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        enemySpawnTimer = 0; //Temporary timer for enemy spawn for the alpha
    }

    // Update is called once per frame
    void Update()
    {
        costBarTarget = timer / 2;
        costBarSprite.fillAmount = Mathf.MoveTowards(costBarSprite.fillAmount, costBarTarget, 10 * Time.deltaTime);
        timer += Time.deltaTime;
        enemySpawnTimer += Time.deltaTime;
        if (timer >= 2)
        {
            costBarSprite.fillAmount = 0;
            timer = 0;
            team1.AddCost();
            //team2.AddCost();
        }
    }
}
