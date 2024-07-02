using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;

public class TeamManager : MonoBehaviour
{
    private int cost;
    [SerializeField] private CinemachineVirtualCamera defaultCamera;
    [SerializeField] private CinemachineVirtualCamera[] mechCameras;
    [SerializeField] private SelectedLoadout loadout;

    //In the future the mechs are loaded in by the mechManager
    [SerializeField] private GameObject mech1;
    private bool mech1Active;
    private int mech1Cost;
    [SerializeField] private TMP_Text mech1CostText;

    [SerializeField] private GameObject mech2;
    private bool mech2Active;
    private int mech2Cost;
    [SerializeField] private TMP_Text mech2CostText;

    [SerializeField] private GameObject mech3;
    private bool mech3Active;
    private int mech3Cost;
    [SerializeField] private TMP_Text mech3CostText;

    [SerializeField] private GameObject mech4;
    private bool mech4Active;
    private int mech4Cost;
    [SerializeField] private TMP_Text mech4CostText;

    [SerializeField] private GameObject mech5;
    private int mech5Cost;
    private bool mech5Active;
    [SerializeField] private TMP_Text mech5CostText;

    [SerializeField] private TMP_Text costText; //Only neeeded for player

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

    public void AddCost()
    {
        cost++;
        costText.text = "Cost: " + cost;
    }

    public void SpawnMech1()
    {
        if (cost >= mech1Cost && !mech1Active)
        {
            cost -= mech1Cost;
            mech1.SetActive(true);
            costText.text = "Cost: " + cost;
            mech1CostText.enabled = false;
            mech1Active = true;
        }
        else if (mech1Active)
        {
            defaultCamera.Priority = 10;
            //Future add how abilities are toggled here
            for (int i = 0; i < mechCameras.Length; i++) 
            {
                mechCameras[i].Priority = 10;
            }
            mechCameras[0].Priority = 11;

        }
    }

    public void SpawnMech2()
    {
        if (cost >= mech2Cost && !mech2Active)
        {
            cost -= mech2Cost;
            mech2.SetActive(true);
            costText.text = "Cost: " + cost;
            mech2CostText.enabled = false;
            mech2Active = true;
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
    }

    public void SpawnMech3()
    {
        if (cost >= mech3Cost && !mech3Active)
        {
            cost -= mech3Cost;
            mech3.SetActive(true);
            costText.text = "Cost: " + cost;
            mech3CostText.enabled = false;
            mech3Active = true;
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
    }

    public void SpawnMech4()
    {
        if (cost >= mech4Cost && !mech4Active)
        {
            cost -= mech4Cost;
            mech4.SetActive(true);
            costText.text = "Cost: " + cost;
            mech4CostText.enabled = false;
            mech4Active = true;
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
    }

    public void SpawnMech5()
    {
        if (cost >= mech5Cost && !mech5Active)
        {
            cost -= mech5Cost;
            mech5.SetActive(true);
            costText.text = "Cost: " + cost;
            mech5CostText.enabled = false;
            mech5Active = true;
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
    }

    public void ResetCamera()
    {
        defaultCamera.Priority = 11;
        for (int i = 0; i < mechCameras.Length; i++)
        {
            mechCameras[i].Priority = 10;
        }
    }
}
