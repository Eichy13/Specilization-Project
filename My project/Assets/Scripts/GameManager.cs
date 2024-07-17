using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private float timer;
    private float enemySpawnTimer;
    private float countdownTime = 10f;

    private bool gameStarted;
    private bool gameFrozen;
    //Focus on the player resource right now, ai dosent need it yet
    public TeamManager team1;
    public TeamManager team2;

    [SerializeField] private TMP_Text costText;
    [SerializeField] protected Image costBarSprite;
    [SerializeField] private TMP_Text team1HpText;
    [SerializeField] private TMP_Text team2HpText;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private TMP_Text winText;
    [SerializeField] private TMP_Text win2Text;

    [SerializeField] private GameObject bgObject;
    [SerializeField] private GameObject mapObject;
    [SerializeField] private GameObject notificationObject1;
    [SerializeField] private GameObject notificationObject2;
    [SerializeField] private GameObject abilityNotificationObject1;
    [SerializeField] private GameObject abilityNotificationObject2;
    [SerializeField] private GameObject deathNotificationObject;
    [SerializeField] private GameObject winNotificationObject;

    [SerializeField] private BaseDefenceAI team1Turret1;
    [SerializeField] private BaseDefenceAI team1Turret2;
    [SerializeField] private BaseDefenceAI team1Ship;

    [SerializeField] private BaseDefenceAI team2Turret1;
    [SerializeField] private BaseDefenceAI team2Turret2;
    [SerializeField] private BaseDefenceAI team2Ship;

    private float team1Turret1HP;
    private float team1Turret2HP;
    private float team1ShipHP;
    private float team1CurrentHP;
    private float team1MaxHp;

    private float team2Turret1HP;
    private float team2Turret2HP;
    private float team2ShipHP;
    private float team2CurrentHP;
    private float team2MaxHp;


    protected float costBarTarget;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Find an existing instance in the scene
                instance = FindObjectOfType<GameManager>();

                // If no instance is found, create a new one
                if (instance == null)
                {
                    GameObject singleton = new GameObject("GameManager");
                    instance = singleton.AddComponent<GameManager>();

                    // Optionally, you can set the GameManager to not be destroyed on scene load
                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Check if there is already an instance of GameManager
        if (instance == null)
        {
            // If not, set it to this instance
            instance = this;

            // Optionally, you can set the GameManager to not be destroyed on scene load
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        team1Turret1HP = team1Turret1.currentMaxHealth;
        team1Turret2HP = team1Turret2.currentMaxHealth;
        team1ShipHP = team1Ship.currentMaxHealth;
        team1MaxHp = team1Turret1HP + team1Turret2HP + team1ShipHP;

        team2Turret1HP = team2Turret1.currentMaxHealth;
        team2Turret2HP = team2Turret2.currentMaxHealth;
        team2ShipHP = team2Ship.currentMaxHealth;
        team2MaxHp = team2Turret1HP + team2Turret2HP + team2ShipHP;

        gameStarted = true;
        gameFrozen = false;

        timer = 0;
        enemySpawnTimer = 0; //Temporary timer for enemy spawn for the alpha
        StartCoroutine(StartCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthText();

        if (gameStarted && !gameFrozen)
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

    IEnumerator StartCountdown()
    {
        while (countdownTime > 0)
        {
            // Update the countdown text to show remaining time in seconds
            countdownText.text = countdownTime.ToString("F0"); // Displaying as an integer

            // Wait for a second
            yield return new WaitForSeconds(1f);

            // Decrease the countdown time
            countdownTime--;
        }

        // When the countdown is done, you can add additional actions here
        countdownText.text = "0";
        GameEnd();
    }

    public void BlackBGSpawn()
    {
        bgObject.SetActive(true);
    }

    public void BlackBGClose()
    {
        bgObject.SetActive(false);
    }

    public void MapSpawn()
    {
        mapObject.SetActive(true);
    }

    public void MapClose()
    {
        mapObject.SetActive(false);
    }

    public void SpawnMechFreeze(float freezeDuration)
    {
        StartCoroutine(SpawnNotification(freezeDuration));
    }

    private IEnumerator SpawnNotification(float freezeDuration)
    {
        notificationObject1.SetActive(true);
        //Freeze all active mechs, turrets and timer (maybe idk might be too hard too do)
        yield return new WaitForSeconds(freezeDuration);
        notificationObject1.SetActive(false);
    }

    public void AbilityFreeze(float freezeDuration)
    {
        StartCoroutine(SpawnAbilityNotification(freezeDuration));
    }

    private IEnumerator SpawnAbilityNotification(float freezeDuration)
    {
        abilityNotificationObject1.SetActive(true);
        //Freeze all active mechs, turrets and timer (maybe idk might be too hard too do)
        yield return new WaitForSeconds(freezeDuration / 2);
        abilityNotificationObject1.SetActive(false);
        abilityNotificationObject2.SetActive(true);
        yield return new WaitForSeconds(freezeDuration / 2);
        abilityNotificationObject2.SetActive(false);
    }

    public void DeathFreeze(float freezeDuration)
    {
        StartCoroutine(SpawnDeathNotification(freezeDuration));
    }

    private IEnumerator SpawnDeathNotification(float freezeDuration)
    {
        deathNotificationObject.SetActive(true);
        //Freeze all active mechs, turrets and timer (maybe idk might be too hard too do)
        yield return new WaitForSeconds(freezeDuration);
        deathNotificationObject.SetActive(false);
    }

    private void UpdateHealthText()
    {
        if (team1Turret1 != null)
        {
            team1Turret1HP = team1Turret1.health;
        }
        else
        {
            team1Turret1HP = 0;
        }
        if (team1Turret2 != null)
        {
            team1Turret2HP = team1Turret2.health;
        }
        else
        {
            team1Turret1HP = 0;
        }
        if (team1Ship != null)
        {
            team1ShipHP = team1Ship.health;
        }
        else
        {
            team1ShipHP = 0;
        }
        team1CurrentHP = team1Turret1HP + team1Turret2HP + team1ShipHP;
        team1HpText.text = Mathf.RoundToInt(((team1CurrentHP / team1MaxHp) * 100)) + "%";

        if (team2Turret1 != null)
        {
            team2Turret1HP = team2Turret1.health;
        }
        else
        {
            team2Turret1HP = 0;
        }

        if (team2Turret2 != null)
        {
            team2Turret2HP = team2Turret2.health;
        }
        else
        {
            team2Turret1HP = 0;
        }

        if (team2Ship != null)
        {
            team2ShipHP = team2Ship.health;
        }
        else
        {
            team2ShipHP = 0;
        }
        team2CurrentHP = team2Turret1HP + team2Turret2HP + team2ShipHP;
        team2HpText.text = Mathf.RoundToInt(((team2CurrentHP / team2MaxHp) * 100)) + "%";

        if (team1CurrentHP == 0 || team2CurrentHP == 0)
        {
            GameEnd();
        }
    }

    private void GameEnd()
    {
        //Freeze everything
        Time.timeScale = 0;
        winNotificationObject.SetActive(true);

        win2Text.text =  "Player: " + ((team1CurrentHP / team1MaxHp) * 100) + "% Enemy: " + ((team2CurrentHP / team2MaxHp) * 100) +"%";
        if (team1CurrentHP >= team2CurrentHP)
        {
            winText.text = "Player has won!";
        }
        else
        {

            winText.text = "Player has been defeated";
        }
    }

    private void FreezeTime()
    {
        //If i can figure it out
    }

    
}
