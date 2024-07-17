using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using System.Drawing;

public class TeamManager : MonoBehaviour
{ 
    private int cost;
    private float timer;
    [SerializeField] private CinemachineVirtualCamera defaultCamera;
    [SerializeField] private CinemachineVirtualCamera[] mechCameras;
    [SerializeField] private SelectedLoadout loadout;

    [SerializeField] private MechSpawner mechSpawner;

    [SerializeField] private Transform[] spawnPoints;
    private int selectedMech;

    private bool mech1Active;
    private int mech1Cost;
    [SerializeField] private TMP_Text mech1CostText;
    [SerializeField] private GameObject mech1AbilityButton;
    [SerializeField] private GameObject mech1BlackBG;
    private bool mech1Dead;
    private int mech1DeathCD;


    private bool mech2Active;
    private int mech2Cost;
    [SerializeField] private TMP_Text mech2CostText;
    [SerializeField] private GameObject mech2AbilityButton;
    [SerializeField] private GameObject mech2BlackBG;
    private bool mech2Dead;
    private int mech2DeathCD;

    private bool mech3Active;
    private int mech3Cost;
    [SerializeField] private TMP_Text mech3CostText;
    [SerializeField] private GameObject mech3AbilityButton;
    [SerializeField] private GameObject mech3BlackBG;
    private bool mech3Dead;
    private int mech3DeathCD;

    private bool mech4Active;
    private int mech4Cost;
    [SerializeField] private TMP_Text mech4CostText;
    [SerializeField] private GameObject mech4AbilityButton;
    [SerializeField] private GameObject mech4BlackBG;
    private bool mech4Dead;
    private int mech4DeathCD;

    private int mech5Cost;
    private bool mech5Active;
    [SerializeField] private TMP_Text mech5CostText;
    [SerializeField] private GameObject mech5AbilityButton;
    [SerializeField] private GameObject mech5BlackBG;
    private bool mech5Dead;
    private int mech5DeathCD;

    [SerializeField] private TMP_Text costText; //Only neeeded for player

    [SerializeField] private Image abilityImage;
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text abilityDescription;

    [SerializeField] private TMP_Text deathName;

    // Start is called before the first frame update
    void Start()
    {
        //Save the cost of each mech
        mech1Cost = loadout.selectedMechStats1.mechCost + loadout.selectedPilotStats1.pilotCost;
        mech2Cost = loadout.selectedMechStats2.mechCost + loadout.selectedPilotStats2.pilotCost;
        mech3Cost = loadout.selectedMechStats3.mechCost + loadout.selectedPilotStats3.pilotCost;
        mech4Cost = loadout.selectedMechStats4.mechCost + loadout.selectedPilotStats4.pilotCost;
        mech5Cost = loadout.selectedMechStats5.mechCost + loadout.selectedPilotStats5.pilotCost;

        mech1CostText.text = "Cost :" + mech1Cost;
        mech2CostText.text = "Cost :" + mech2Cost;
        mech3CostText.text = "Cost :" + mech3Cost;
        mech4CostText.text = "Cost :" + mech4Cost;
        mech5CostText.text = "Cost :" + mech5Cost;

        cost = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (mech1Dead && timer >= 1)
        {
            mech1DeathCD -= 1;
            mech1CostText.text = "Repair: " + mech1DeathCD;
            if (mech1DeathCD <= 0)
            {
                mech1CostText.text = "Cost :" + mech1Cost;
                mech1BlackBG.SetActive(false);
                mech1Dead = false;
            }
        }
        if (mechSpawner.mech1.GetComponent<BaseAI>().currentHealth <= 0 && !mech1Dead)
        {
            Debug.Log("Dead");
            mech1Active = false;
            deathName.text = loadout.selectedMechStats1.mechName + " has been destroyed!";
            GameManager.Instance.DeathFreeze(4f);
            ResetCamera();
            Destroy(mechSpawner.mech1);
            MechSpawner.Instance.RespawnMech1();
            mech1AbilityButton.SetActive(false);
            mech1BlackBG.SetActive(true);
            mech1Dead = true;
            mech1DeathCD = 20;
            mech1CostText.text = "Repair: " + mech1DeathCD;
        }

        if (mech2Dead && timer >= 1)
        {
            mech2DeathCD -= 1;
            mech2CostText.text = "Repair: " + mech2DeathCD;
            if (mech2DeathCD <= 0)
            {
                mech2CostText.text = "Cost :" + mech2Cost;
                mech2BlackBG.SetActive(false);
                mech2Dead = false;
            }
        }
        if (mechSpawner.mech2.GetComponent<BaseAI>().currentHealth <= 0 && !mech2Dead)
        {
            Debug.Log("Dead");
            mech2Active = false;
            deathName.text = loadout.selectedMechStats2.mechName + " has been destroyed!";
            GameManager.Instance.DeathFreeze(4f);
            ResetCamera();
            Destroy(mechSpawner.mech2);
            MechSpawner.Instance.RespawnMech2();
            mech2AbilityButton.SetActive(false);
            mech2BlackBG.SetActive(true);
            mech2Dead = true;
            mech2DeathCD = 20;
            mech2CostText.text = "Repair: " + mech2DeathCD;
        }

        if (mech3Dead && timer >= 1)
        {
            mech3DeathCD -= 1;
            mech3CostText.text = "Repair: " + mech3DeathCD;
            if (mech3DeathCD <= 0)
            {
                mech3CostText.text = "Cost :" + mech3DeathCD;
                mech3BlackBG.SetActive(false);
                mech3Dead = false;
            }
        }
        if (mechSpawner.mech3.GetComponent<BaseAI>().currentHealth <= 0 && !mech3Dead)
        {
            Debug.Log("Dead");
            mech3Active = false;
            deathName.text = loadout.selectedMechStats3.mechName + " has been destroyed!";
            GameManager.Instance.DeathFreeze(4f);
            ResetCamera();
            Destroy(mechSpawner.mech3);
            MechSpawner.Instance.RespawnMech3();
            mech3AbilityButton.SetActive(false);
            mech3BlackBG.SetActive(true);
            mech3Dead = true;
            mech3DeathCD = 20;
            mech3CostText.text = "Repair: " + mech3DeathCD;
        }

        if (mech4Dead && timer >= 1)
        {
            mech4DeathCD -= 1;
            mech4CostText.text = "Repair: " + mech4DeathCD;
            if (mech4DeathCD <= 0)
            {
                mech4CostText.text = "Cost :" + mech4Cost;
                mech4BlackBG.SetActive(false);
                mech4Dead = false;
            }
        }
        if (mechSpawner.mech4.GetComponent<BaseAI>().currentHealth <= 0 && !mech4Dead)
        {
            Debug.Log("Dead");
            mech4Active = false;
            deathName.text = loadout.selectedMechStats4.mechName + " has been destroyed!";
            GameManager.Instance.DeathFreeze(4f);
            ResetCamera();
            Destroy(mechSpawner.mech4);
            MechSpawner.Instance.RespawnMech4();
            mech4AbilityButton.SetActive(false);
            mech4BlackBG.SetActive(true);
            mech4Dead = true;
            mech4DeathCD = 20;
            mech4CostText.text = "Repair: " + mech4DeathCD;
        }

        if (mech2Dead && timer >= 1)
        {
            mech2DeathCD -= 1;
            mech2CostText.text = "Repair: " + mech2DeathCD;
            if (mech2DeathCD <= 0)
            {
                mech2CostText.text = "Cost :" + mech2Cost;
                mech2BlackBG.SetActive(false);
                mech2Dead = false;
            }
        }
        if (mechSpawner.mech5.GetComponent<BaseAI>().currentHealth <= 0 && !mech5Dead)
        {
            Debug.Log("Dead");
            mech5Active = false;
            deathName.text = loadout.selectedMechStats5.mechName + " has been destroyed!";
            GameManager.Instance.DeathFreeze(4f);
            ResetCamera();
            Destroy(mechSpawner.mech5);
            MechSpawner.Instance.RespawnMech5();
            mech5AbilityButton.SetActive(false);
            mech5BlackBG.SetActive(true);
            mech5Dead = true;
            mech5DeathCD = 20;
            mech5CostText.text = "Repair: " + mech5DeathCD;
        }

        if (timer >= 1)
        {
            timer = 0;
        }
    }

    public void AddCost()
    {
        cost++;
        costText.text = "Cost: " + cost;
    }

    public void MechButton(int mechnumber)
    {
        //Check if open up map with enough cost
        switch (mechnumber)
        {
            case (1):
            {
                if (cost >= mech1Cost && !mech1Active)
                {
                    selectedMech = 1;
                    GameManager.Instance.BlackBGSpawn();
                    GameManager.Instance.MapSpawn();
                }
                else if (mech1Active)
                {
                    //Fix Camera
                    defaultCamera.Priority = 10;
                    //Future add how abilities are toggled here
                    for (int i = 0; i < mechCameras.Length; i++)
                    {
                        mechCameras[i].Priority = 10;
                    }
                    mechCameras[0].Priority = 11;

                }
                    break;
            }
            case (2): 
            {
                if (cost >= mech2Cost && !mech2Active)
                {
                    selectedMech = 2;
                        GameManager.Instance.BlackBGSpawn();
                        GameManager.Instance.MapSpawn();
                    }
                else if (mech2Active)
                {
                    defaultCamera.Priority = 10;
                    //Future add how abilities are toggled here
                    for (int i = 0; i < mechCameras.Length; i++)
                    {
                        mechCameras[i].Priority = 10;
                    }
                    mechCameras[1].Priority = 11;

                }
                break;
            }
            case (3):
            {
                if (cost >= mech3Cost && !mech3Active)
                {
                    selectedMech = 3;
                    GameManager.Instance.BlackBGSpawn();
                    GameManager.Instance.MapSpawn();
                    }
                else if (mech3Active)
                {
                    defaultCamera.Priority = 10;
                    //Future add how abilities are toggled here
                    for (int i = 0; i < mechCameras.Length; i++)
                    {
                        mechCameras[i].Priority = 10;
                    }
                    mechCameras[2].Priority = 11;

                }
                break;
            }
            case (4):
            {
                if (cost >= mech4Cost && !mech4Active)
                {
                    selectedMech = 4;
                    GameManager.Instance.BlackBGSpawn();
                    GameManager.Instance.MapSpawn();
                    }
                else if (mech4Active)
                {
                    defaultCamera.Priority = 10;
                    //Future add how abilities are toggled here
                    for (int i = 0; i < mechCameras.Length; i++)
                    {
                        mechCameras[i].Priority = 10;
                    }
                    mechCameras[3].Priority = 11;

                }
                break;
            }
            case (5):
            {
                if (cost >= mech5Cost && !mech5Active)
                {
                    selectedMech = 5;
                    GameManager.Instance.BlackBGSpawn();
                    GameManager.Instance.MapSpawn();
                    }
                else if (mech5Active)
                {
                    defaultCamera.Priority = 10;
                    //Future add how abilities are toggled here
                    for (int i = 0; i < mechCameras.Length; i++)
                    {
                        mechCameras[i].Priority = 10;
                    }
                    mechCameras[4].Priority = 11;

                }
                break;
            }
        }

        
        selectedMech = mechnumber;
    }

    public void WayPointSelect(int waypoint)
    {
        Transform temp = spawnPoints[waypoint];
        Debug.Log(temp.position);
        switch (selectedMech)
        {
            case 1:
            {
                SpawnMech1(temp);
                break;
            }
            case 2:
            {
                SpawnMech2(temp);
                break;
            }
            case 3:
            {
                SpawnMech3(temp);
                break;
            }
            case 4:
            {
                SpawnMech4(temp);
                break;
            }
            case 5:
            {
                SpawnMech5(temp);
                break;
            }

        }
    }

    private void SpawnMech1(Transform waypoint)
    {
        //Code to spawn them at their correct spot and drop them for now lol
        cost -= mech1Cost;
        mechSpawner.mech1.transform.position = waypoint.position;
        costText.text = "Cost: " + cost;
        mech1CostText.text = "Ability: " + loadout.selectedMechStats1.mechAbilityCost;
        mech1Active = true;
        mech1AbilityButton.SetActive(true);
        
        StartCoroutine(SpawnDelay(mechSpawner.mech1, 2f));

        GameManager.Instance.BlackBGClose();
        GameManager.Instance.MapClose();
        GameManager.Instance.SpawnMechFreeze(2f);
    }

    public void Mech1Ability()
    {
        if (cost >= loadout.selectedMechStats1.mechAbilityCost && mech1Active)
        {
            cost -= loadout.selectedMechStats1.mechAbilityCost;
            costText.text = "Cost: " + cost;
            BaseAI temp = mechSpawner.mech1.GetComponent<BaseAI>();
            Debug.Log(temp);
            temp.MechActive();
            mech1AbilityButton.SetActive(false);
            StartCoroutine(SpawnDelay(mech1AbilityButton, 10f));

            abilityImage.sprite = loadout.selectedMechStats1.mechImage1;
            abilityName.text = loadout.selectedMechStats1.mechAbilityName;
            abilityDescription.text = loadout.selectedMechStats1.mechAbilityDescription;

            GameManager.Instance.AbilityFreeze(4f);
            //Add cooldown for the ability (Not now)
        }
    }

    private void SpawnMech2(Transform waypoint)
    {
        //Code to spawn them at their correct spot and drop them for now lol
        cost -= mech2Cost;
        mechSpawner.mech2.transform.position = waypoint.position;
        costText.text = "Cost: " + cost;
        mech2CostText.text = "Ability: " + loadout.selectedMechStats2.mechAbilityCost;
        mech2Active = true;
        mech2AbilityButton.SetActive(true);

        StartCoroutine(SpawnDelay(mechSpawner.mech2, 2f));

        GameManager.Instance.BlackBGClose();
        GameManager.Instance.MapClose();
        GameManager.Instance.SpawnMechFreeze(2f);
    }

    public void Mech2Ability()
    {
        if (cost >= loadout.selectedMechStats2.mechAbilityCost && mech2Active)
        {
            cost -= loadout.selectedMechStats2.mechAbilityCost;
            costText.text = "Cost: " + cost;
            BaseAI temp = mechSpawner.mech2.GetComponent<BaseAI>();
            Debug.Log(temp);
            temp.MechActive();
            mech2AbilityButton.SetActive(false);
            StartCoroutine(SpawnDelay(mech2AbilityButton, 10f));

            abilityImage.sprite = loadout.selectedMechStats2.mechImage1;
            abilityName.text = loadout.selectedMechStats2.mechAbilityName;
            abilityDescription.text = loadout.selectedMechStats2.mechAbilityDescription;

            GameManager.Instance.AbilityFreeze(4f);
            //Add cooldown for the ability (Not now)
        }
    }

    private void SpawnMech3(Transform waypoint)
    {
        cost -= mech3Cost;
        mechSpawner.mech3.transform.position = waypoint.position;
        costText.text = "Cost: " + cost;
        mech3CostText.text = "Ability: " + loadout.selectedMechStats3.mechAbilityCost;
        mech3Active = true;
        mech3AbilityButton.SetActive(true);   

        StartCoroutine(SpawnDelay(mechSpawner.mech3, 2f));

        GameManager.Instance.BlackBGClose();
        GameManager.Instance.MapClose();
        GameManager.Instance.SpawnMechFreeze(2f);
    }

    public void Mech3Ability()
    {
        if (cost >= loadout.selectedMechStats3.mechAbilityCost && mech3Active)
        {
            cost -= loadout.selectedMechStats3.mechAbilityCost;
            costText.text = "Cost: " + cost;
            BaseAI temp = mechSpawner.mech3.GetComponent<BaseAI>();
            Debug.Log(temp);
            temp.MechActive();
            mech3AbilityButton.SetActive(false);
            StartCoroutine(SpawnDelay(mech3AbilityButton, 10f));

            abilityImage.sprite = loadout.selectedMechStats3.mechImage1;
            abilityName.text = loadout.selectedMechStats3.mechAbilityName;
            abilityDescription.text = loadout.selectedMechStats3.mechAbilityDescription;

            GameManager.Instance.AbilityFreeze(4f);
            //Add cooldown for the ability (Not now)
        }
    }

    private void SpawnMech4(Transform waypoint)
    {
        cost -= mech4Cost;
        mechSpawner.mech4.transform.position = waypoint.position;
        costText.text = "Cost: " + cost;
        mech4CostText.text = "Ability: " + loadout.selectedMechStats4.mechAbilityCost;
        mech4Active = true;
        mech4AbilityButton.SetActive(true);

        StartCoroutine(SpawnDelay(mechSpawner.mech4, 2f));

        GameManager.Instance.BlackBGClose();
        GameManager.Instance.MapClose();
        GameManager.Instance.SpawnMechFreeze(2f);
    }

    public void Mech4Ability()
    {
        if (cost >= loadout.selectedMechStats4.mechAbilityCost && mech4Active)
        {
            cost -= loadout.selectedMechStats4.mechAbilityCost;
            costText.text = "Cost: " + cost;
            BaseAI temp = mechSpawner.mech4.GetComponent<BaseAI>();
            Debug.Log(temp);
            temp.MechActive();
            mech4AbilityButton.SetActive(false);
            StartCoroutine(SpawnDelay(mech4AbilityButton, 10f));

            abilityImage.sprite = loadout.selectedMechStats4.mechImage1;
            abilityName.text = loadout.selectedMechStats4.mechAbilityName;
            abilityDescription.text = loadout.selectedMechStats4.mechAbilityDescription;

            GameManager.Instance.AbilityFreeze(4f);
            //Add cooldown for the ability (Not now)
        }
    }

    private void SpawnMech5(Transform waypoint)
    {
        cost -= mech5Cost;
        mechSpawner.mech5.transform.position = waypoint.position;
        costText.text = "Cost: " + cost;
        mech5CostText.text = "Ability: " + loadout.selectedMechStats5.mechAbilityCost;
        mech5Active = true;
        mech5AbilityButton.SetActive(true);

        GameManager.Instance.BlackBGClose();
        GameManager.Instance.MapClose();
        GameManager.Instance.SpawnMechFreeze(2f);
    }

    public void Mech5Ability()
    {
        if (cost >= loadout.selectedMechStats5.mechAbilityCost && mech5Active)
        {
            cost -= loadout.selectedMechStats5.mechAbilityCost;
            costText.text = "Cost: " + cost;
            BaseAI temp = mechSpawner.mech5.GetComponent<BaseAI>();
            Debug.Log(temp);
            temp.MechActive();
            mech5AbilityButton.SetActive(false);
            StartCoroutine(SpawnDelay(mech5AbilityButton, 10f));

            abilityImage.sprite = loadout.selectedMechStats5.mechImage1;
            abilityName.text = loadout.selectedMechStats5.mechAbilityName;
            abilityDescription.text = loadout.selectedMechStats5.mechAbilityDescription;

            GameManager.Instance.AbilityFreeze(4f);
            //Add cooldown for the ability (Not now)
        }
    }

    public void ResetCamera()
    {
        defaultCamera.Priority = 11;
        for (int i = 0; i < mechCameras.Length; i++)
        {
            mechCameras[i].Priority = 10;
        }
    }

    private IEnumerator SpawnDelay(GameObject mech, float duration)
    {
        yield return new WaitForSeconds(duration);
        mech.SetActive(true);
    }
}
