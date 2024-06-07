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

    //In the future the mechs are loaded in by the mechManager
    public GameObject mech1;
    private BaseAI mech1AI;
    private bool mech1Active;
    private int mech1Cost;
    [SerializeField] private TMP_Text mech1CostText;

    public GameObject mech2;
    private BaseAI mech2AI;
    private bool mech2Active;
    private int mech2Cost;
    [SerializeField] private TMP_Text mech2CostText;

    public GameObject mech3;
    private BaseAI mech3AI;
    private bool mech3Active;
    private int mech3Cost;
    [SerializeField] private TMP_Text mech3CostText;

    public GameObject mech4;
    private BaseAI mech4AI;
    private bool mech4Active;
    private int mech4Cost;
    [SerializeField] private TMP_Text mech4CostText;

    public GameObject mech5;
    private BaseAI mech5AI;
    private bool mech5Active;
    private int mech5Cost;
    [SerializeField] private TMP_Text mech5CostText;

    [SerializeField] private TMP_Text costText; //Only neeeded for player

    // Start is called before the first frame update
    void Start()
    {
        mech1AI = mech1.GetComponent<BaseAI>();
        mech2AI = mech2.GetComponent<BaseAI>();
        mech3AI = mech3.GetComponent<BaseAI>();
        mech4AI = mech4.GetComponent<BaseAI>();
        mech5AI = mech5.GetComponent<BaseAI>();

        //Save the cost of each mech
        mech1Cost = mech1AI.pilotCost + mech1AI.mechCost;
        mech2Cost = mech2AI.pilotCost + mech2AI.mechCost;
        mech3Cost = mech3AI.pilotCost + mech3AI.mechCost;
        mech4Cost = mech4AI.pilotCost + mech4AI.mechCost;
        mech5Cost = mech5AI.pilotCost + mech5AI.mechCost;

        mech1CostText.text = "Cost :" + mech1Cost;
        mech2CostText.text = "Cost :" + mech2Cost;
        mech3CostText.text = "Cost :" + mech3Cost;
        mech4CostText.text = "Cost :" + mech4Cost;
        mech5CostText.text = "Cost :" + mech5Cost;

        cost = 0;

        //Disable the ai after their start function works
        mech1.SetActive(false);
        mech2.SetActive(false);
        mech3.SetActive(false);
        mech4.SetActive(false);
        mech5.SetActive(false);
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
