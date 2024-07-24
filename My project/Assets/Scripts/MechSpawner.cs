using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MechSpawner : MonoBehaviour
{
    private static MechSpawner instance;

    [SerializeField] private SelectedLoadout loadout;
    [SerializeField] private GameObject playerFighterMechPrefab;
    [SerializeField] private GameObject playerRusherMechPrefab;
    [SerializeField] private GameObject playerDefenderMechPrefab;

    [SerializeField] private GameObject enemyFighterMechPrefab;
    [SerializeField] private GameObject enemyRusherMechPrefab;
    [SerializeField] private GameObject enemyDefenderMechPrefab;

    protected List<GameObject> enemyObjects = new List<GameObject>();

    [SerializeField] private CinemachineVirtualCamera[] mechCameras;
    [SerializeField] private Transform[] spawnPoints;

    public GameObject mech1;
    public GameObject mech2;
    public GameObject mech3;
    public GameObject mech4;
    public GameObject mech5;

    public static MechSpawner Instance
    {
        get
        {
            if (instance == null)
            {
                // Find an existing instance in the scene
                instance = FindObjectOfType<MechSpawner>();

                // If no instance is found, create a new one
                if (instance == null)
                {
                    GameObject singleton = new GameObject("GameManager");
                    instance = singleton.AddComponent<MechSpawner>();

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
        if (loadout.sentFromStart) //If sent from the start scene, update all the data of the mechs/pilot individually
        {
            switch (loadout.selectedPilotStats1.pilotType)
            {
                case 0:
                    mech1 = Instantiate(playerFighterMechPrefab);
                    break;
                case 1:
                    mech1 = Instantiate(playerRusherMechPrefab);
                    break;
                case 2:
                    mech1 = Instantiate(playerDefenderMechPrefab);
                    break;
            }

            switch (loadout.selectedPilotStats2.pilotType)
            {
                case 0:
                    mech2 = Instantiate(playerFighterMechPrefab);
                    break;
                case 1:
                    mech2 = Instantiate(playerRusherMechPrefab);
                    break;
                case 2:
                    mech2 = Instantiate(playerDefenderMechPrefab);
                    break;
            }

            switch (loadout.selectedPilotStats3.pilotType)
            {
                case 0:
                    mech3 = Instantiate(playerFighterMechPrefab);
                    break;
                case 1:
                    mech3 = Instantiate(playerRusherMechPrefab);
                    break;
                case 2:
                    mech3 = Instantiate(playerDefenderMechPrefab);
                    break;
            }

            switch (loadout.selectedPilotStats4.pilotType)
            {
                case 0:
                    mech4 = Instantiate(playerFighterMechPrefab);
                    break;
                case 1:
                    mech4 = Instantiate(playerRusherMechPrefab);
                    break;
                case 2:
                    mech4 = Instantiate(playerDefenderMechPrefab);
                    break;
            }

            switch (loadout.selectedPilotStats5.pilotType)
            {
                case 0:
                    mech5 = Instantiate(playerFighterMechPrefab);
                    break;
                case 1:
                    mech5 = Instantiate(playerRusherMechPrefab);
                    break;
                case 2:
                    mech5 = Instantiate(playerDefenderMechPrefab);
                    break;
            }

            BaseAI temp;
            temp = mech1.GetComponent<BaseAI>();
            temp.mechActiveType = loadout.selectedMechStats1.mechAbility;
            temp.mechHealth = loadout.selectedMechStats1.mechHealth;
            temp.mechMeleeDamage = loadout.selectedMechStats1.mechMeleeDamage;
            temp.mechRangedDamage = loadout.selectedMechStats1.mechRangedDamage;
            temp.mechRangedRange = loadout.selectedMechStats1.mechRange;
            temp.mechMobility = loadout.selectedMechStats1.mechMobility;
            temp.mechCost = loadout.selectedMechStats1.mechCost;
            temp.pilotPassiveType = loadout.selectedPilotStats1.pilotAbility;
            temp.pilotHealth = loadout.selectedPilotStats1.pilotHealth;
            temp.pilotMeleeDamage = loadout.selectedPilotStats1.pilotMeleeDamage;
            temp.pilotRangedDamage = loadout.selectedPilotStats1.pilotRangedDamage;
            temp.pilotMobility = loadout.selectedPilotStats1.pilotMobility;
            temp.pilotCost = loadout.selectedPilotStats1.pilotCost;
            temp.InstantiateStart();

            temp = mech2.GetComponent<BaseAI>();
            temp.mechActiveType = loadout.selectedMechStats2.mechAbility;
            temp.mechHealth = loadout.selectedMechStats2.mechHealth;
            temp.mechMeleeDamage = loadout.selectedMechStats2.mechMeleeDamage;
            temp.mechRangedDamage = loadout.selectedMechStats2.mechRangedDamage;
            temp.mechRangedRange = loadout.selectedMechStats2.mechRange;
            temp.mechMobility = loadout.selectedMechStats2.mechMobility;
            temp.mechCost = loadout.selectedMechStats2.mechCost;
            temp.pilotPassiveType = loadout.selectedPilotStats2.pilotAbility;
            temp.pilotHealth = loadout.selectedPilotStats2.pilotHealth;
            temp.pilotMeleeDamage = loadout.selectedPilotStats2.pilotMeleeDamage;
            temp.pilotRangedDamage = loadout.selectedPilotStats2.pilotRangedDamage;
            temp.pilotMobility = loadout.selectedPilotStats2.pilotMobility;
            temp.pilotCost = loadout.selectedPilotStats2.pilotCost;
            temp.InstantiateStart();

            temp = mech3.GetComponent<BaseAI>();
            temp.mechActiveType = loadout.selectedMechStats3.mechAbility;
            temp.mechHealth = loadout.selectedMechStats3.mechHealth;
            temp.mechMeleeDamage = loadout.selectedMechStats3.mechMeleeDamage;
            temp.mechRangedDamage = loadout.selectedMechStats3.mechRangedDamage;
            temp.mechRangedRange = loadout.selectedMechStats3.mechRange;
            temp.mechMobility = loadout.selectedMechStats3.mechMobility;
            temp.mechCost = loadout.selectedMechStats3.mechCost;
            temp.pilotPassiveType = loadout.selectedPilotStats3.pilotAbility;
            temp.pilotHealth = loadout.selectedPilotStats3.pilotHealth;
            temp.pilotMeleeDamage = loadout.selectedPilotStats3.pilotMeleeDamage;
            temp.pilotRangedDamage = loadout.selectedPilotStats3.pilotRangedDamage;
            temp.pilotMobility = loadout.selectedPilotStats3.pilotMobility;
            temp.pilotCost = loadout.selectedPilotStats3.pilotCost;
            temp.InstantiateStart();

            temp = mech4.GetComponent<BaseAI>();
            temp.mechActiveType = loadout.selectedMechStats4.mechAbility;
            temp.mechHealth = loadout.selectedMechStats4.mechHealth;
            temp.mechMeleeDamage = loadout.selectedMechStats4.mechMeleeDamage;
            temp.mechRangedDamage = loadout.selectedMechStats4.mechRangedDamage;
            temp.mechRangedRange = loadout.selectedMechStats4.mechRange;
            temp.mechMobility = loadout.selectedMechStats4.mechMobility;
            temp.mechCost = loadout.selectedMechStats4.mechCost;
            temp.pilotPassiveType = loadout.selectedPilotStats4.pilotAbility;
            temp.pilotHealth = loadout.selectedPilotStats4.pilotHealth;
            temp.pilotMeleeDamage = loadout.selectedPilotStats4.pilotMeleeDamage;
            temp.pilotRangedDamage = loadout.selectedPilotStats4.pilotRangedDamage;
            temp.pilotMobility = loadout.selectedPilotStats4.pilotMobility;
            temp.pilotCost = loadout.selectedPilotStats4.pilotCost;
            temp.InstantiateStart();

            temp = mech5.GetComponent<BaseAI>();
            temp.mechActiveType = loadout.selectedMechStats5.mechAbility;
            temp.mechHealth = loadout.selectedMechStats5.mechHealth;
            temp.mechMeleeDamage = loadout.selectedMechStats5.mechMeleeDamage;
            temp.mechRangedDamage = loadout.selectedMechStats5.mechRangedDamage;
            temp.mechRangedRange = loadout.selectedMechStats5.mechRange;
            temp.mechMobility = loadout.selectedMechStats5.mechMobility;
            temp.mechCost = loadout.selectedMechStats5.mechCost;
            temp.pilotPassiveType = loadout.selectedPilotStats5.pilotAbility;
            temp.pilotHealth = loadout.selectedPilotStats5.pilotHealth;
            temp.pilotMeleeDamage = loadout.selectedPilotStats5.pilotMeleeDamage;
            temp.pilotRangedDamage = loadout.selectedPilotStats5.pilotRangedDamage;
            temp.pilotMobility = loadout.selectedPilotStats5.pilotMobility;
            temp.pilotCost = loadout.selectedPilotStats5.pilotCost;
            temp.InstantiateStart();

        }

        mech1.SetActive(false);
        mech2.SetActive(false);
        mech3.SetActive(false);
        mech4.SetActive(false);
        mech5.SetActive(false);

        mechCameras[0].Follow = mech1.transform;
        mechCameras[1].Follow = mech2.transform;
        mechCameras[2].Follow = mech3.transform;
        mechCameras[3].Follow = mech4.transform;
        mechCameras[4].Follow = mech5.transform;
    }

    public void RespawnMech1()
    {
        switch (loadout.selectedPilotStats1.pilotType)
        {
            case 0:
                mech1 = Instantiate(playerFighterMechPrefab);
                break;
            case 1:
                mech1 = Instantiate(playerRusherMechPrefab);
                break;
            case 2:
                mech1 = Instantiate(playerDefenderMechPrefab);
                break;
        }
        BaseAI temp;
        temp = mech1.GetComponent<BaseAI>();
        temp.mechActiveType = loadout.selectedMechStats1.mechAbility;
        temp.mechHealth = loadout.selectedMechStats1.mechHealth;
        temp.mechMeleeDamage = loadout.selectedMechStats1.mechMeleeDamage;
        temp.mechRangedDamage = loadout.selectedMechStats1.mechRangedDamage;
        temp.mechRangedRange = loadout.selectedMechStats1.mechRange;
        temp.mechMobility = loadout.selectedMechStats1.mechMobility;
        temp.mechCost = loadout.selectedMechStats1.mechCost;
        temp.pilotPassiveType = loadout.selectedPilotStats1.pilotAbility;
        temp.pilotHealth = loadout.selectedPilotStats1.pilotHealth;
        temp.pilotMeleeDamage = loadout.selectedPilotStats1.pilotMeleeDamage;
        temp.pilotRangedDamage = loadout.selectedPilotStats1.pilotRangedDamage;
        temp.pilotMobility = loadout.selectedPilotStats1.pilotMobility;
        temp.pilotCost = loadout.selectedPilotStats1.pilotCost;
        temp.InstantiateStart();

        mech1.SetActive(false);
        mechCameras[0].Follow = mech1.transform;
    }

    public void RespawnMech2()
    {
        switch (loadout.selectedPilotStats2.pilotType)
        {
            case 0:
                mech2 = Instantiate(playerFighterMechPrefab);
                break;
            case 1:
                mech2 = Instantiate(playerRusherMechPrefab);
                break;
            case 2:
                mech2 = Instantiate(playerDefenderMechPrefab);
                break;
        }
        BaseAI temp;
        temp = mech2.GetComponent<BaseAI>();
        temp.mechActiveType = loadout.selectedMechStats2.mechAbility;
        temp.mechHealth = loadout.selectedMechStats2.mechHealth;
        temp.mechMeleeDamage = loadout.selectedMechStats2.mechMeleeDamage;
        temp.mechRangedDamage = loadout.selectedMechStats2.mechRangedDamage;
        temp.mechRangedRange = loadout.selectedMechStats2.mechRange;
        temp.mechMobility = loadout.selectedMechStats2.mechMobility;
        temp.mechCost = loadout.selectedMechStats2.mechCost;
        temp.pilotPassiveType = loadout.selectedPilotStats2.pilotAbility;
        temp.pilotHealth = loadout.selectedPilotStats2.pilotHealth;
        temp.pilotMeleeDamage = loadout.selectedPilotStats2.pilotMeleeDamage;
        temp.pilotRangedDamage = loadout.selectedPilotStats2.pilotRangedDamage;
        temp.pilotMobility = loadout.selectedPilotStats2.pilotMobility;
        temp.pilotCost = loadout.selectedPilotStats2.pilotCost;
        temp.InstantiateStart();

        mech2.SetActive(false);
        mechCameras[1].Follow = mech2.transform;
    }

    public void RespawnMech3()
    {
        switch (loadout.selectedPilotStats3.pilotType)
        {
            case 0:
                mech3 = Instantiate(playerFighterMechPrefab);
                break;
            case 1:
                mech3 = Instantiate(playerRusherMechPrefab);
                break;
            case 2:
                mech3 = Instantiate(playerDefenderMechPrefab);
                break;
        }
        BaseAI temp;
        temp = mech3.GetComponent<BaseAI>();
        temp.mechActiveType = loadout.selectedMechStats3.mechAbility;
        temp.mechHealth = loadout.selectedMechStats3.mechHealth;
        temp.mechMeleeDamage = loadout.selectedMechStats3.mechMeleeDamage;
        temp.mechRangedDamage = loadout.selectedMechStats3.mechRangedDamage;
        temp.mechRangedRange = loadout.selectedMechStats3.mechRange;
        temp.mechMobility = loadout.selectedMechStats3.mechMobility;
        temp.mechCost = loadout.selectedMechStats3.mechCost;
        temp.pilotPassiveType = loadout.selectedPilotStats3.pilotAbility;
        temp.pilotHealth = loadout.selectedPilotStats3.pilotHealth;
        temp.pilotMeleeDamage = loadout.selectedPilotStats3.pilotMeleeDamage;
        temp.pilotRangedDamage = loadout.selectedPilotStats3.pilotRangedDamage;
        temp.pilotMobility = loadout.selectedPilotStats3.pilotMobility;
        temp.pilotCost = loadout.selectedPilotStats3.pilotCost;
        temp.InstantiateStart();

        mech3.SetActive(false);
        mechCameras[2].Follow = mech3.transform;
    }

    public void RespawnMech4()
    {
        switch (loadout.selectedPilotStats4.pilotType)
        {
            case 0:
                mech4 = Instantiate(playerFighterMechPrefab);
                break;
            case 1:
                mech4 = Instantiate(playerRusherMechPrefab);
                break;
            case 2:
                mech4 = Instantiate(playerDefenderMechPrefab);
                break;
        }
        BaseAI temp;
        temp = mech4.GetComponent<BaseAI>();
        temp.mechActiveType = loadout.selectedMechStats4.mechAbility;
        temp.mechHealth = loadout.selectedMechStats4.mechHealth;
        temp.mechMeleeDamage = loadout.selectedMechStats4.mechMeleeDamage;
        temp.mechRangedDamage = loadout.selectedMechStats4.mechRangedDamage;
        temp.mechRangedRange = loadout.selectedMechStats4.mechRange;
        temp.mechMobility = loadout.selectedMechStats4.mechMobility;
        temp.mechCost = loadout.selectedMechStats4.mechCost;
        temp.pilotPassiveType = loadout.selectedPilotStats4.pilotAbility;
        temp.pilotHealth = loadout.selectedPilotStats4.pilotHealth;
        temp.pilotMeleeDamage = loadout.selectedPilotStats4.pilotMeleeDamage;
        temp.pilotRangedDamage = loadout.selectedPilotStats4.pilotRangedDamage;
        temp.pilotMobility = loadout.selectedPilotStats4.pilotMobility;
        temp.pilotCost = loadout.selectedPilotStats4.pilotCost;
        temp.InstantiateStart();

        mech4.SetActive(false);
        mechCameras[3].Follow = mech4.transform;
    }

    public void RespawnMech5()
    {
        switch (loadout.selectedPilotStats5.pilotType)
        {
            case 0:
                mech5 = Instantiate(playerFighterMechPrefab);
                break;
            case 1:
                mech5 = Instantiate(playerRusherMechPrefab);
                break;
            case 2:
                mech5 = Instantiate(playerDefenderMechPrefab);
                break;
        }
        BaseAI temp;
        temp = mech5.GetComponent<BaseAI>();
        temp.mechActiveType = loadout.selectedMechStats5.mechAbility;
        temp.mechHealth = loadout.selectedMechStats5.mechHealth;
        temp.mechMeleeDamage = loadout.selectedMechStats5.mechMeleeDamage;
        temp.mechRangedDamage = loadout.selectedMechStats5.mechRangedDamage;
        temp.mechRangedRange = loadout.selectedMechStats5.mechRange;
        temp.mechMobility = loadout.selectedMechStats5.mechMobility;
        temp.mechCost = loadout.selectedMechStats5.mechCost;
        temp.pilotPassiveType = loadout.selectedPilotStats5.pilotAbility;
        temp.pilotHealth = loadout.selectedPilotStats5.pilotHealth;
        temp.pilotMeleeDamage = loadout.selectedPilotStats5.pilotMeleeDamage;
        temp.pilotRangedDamage = loadout.selectedPilotStats5.pilotRangedDamage;
        temp.pilotMobility = loadout.selectedPilotStats5.pilotMobility;
        temp.pilotCost = loadout.selectedPilotStats5.pilotCost;
        temp.InstantiateStart();

        mech5.SetActive(false);
        mechCameras[4].Follow = mech5.transform;
    }

    public void SpawnEnemyAttackWave(float multiplier)
    {
        for (int i = 0; i < 2 + multiplier; i++)
        {
            GameObject enemy;
            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0:
                    enemy = Instantiate(enemyFighterMechPrefab); 
                    break;
                case 1:
                    enemy = Instantiate(enemyRusherMechPrefab);
                    break;
                case 2:
                    enemy = Instantiate(enemyDefenderMechPrefab);
                    break;
                default: 
                    enemy = Instantiate(enemyFighterMechPrefab);
                    break;
            }

            BaseAI temp;
            temp = enemy.GetComponent<BaseAI>();
            temp.mechHealth = Random.Range(200, 800);
            temp.mechMeleeDamage = Random.Range(200, 800);
            temp.mechRangedDamage = Random.Range(200, 800);
            temp.mechRangedRange = Random.Range(2, 5);
            temp.mechMobility = Random.Range(100, 300);
            temp.pilotHealth = Random.Range(200, 800);
            temp.pilotMeleeDamage = Random.Range(200, 800);
            temp.pilotRangedDamage = Random.Range(200, 800);
            temp.pilotMobility = Random.Range(100, 300);
            enemy.transform.position = spawnPoints[Random.Range(0, 16)].position;
            temp.InstantiateStart();
        }
    }
}
