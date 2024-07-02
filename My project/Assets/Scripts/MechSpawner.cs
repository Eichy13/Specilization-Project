using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private SelectedLoadout loadout;
    [SerializeField] private GameObject fighterMechPrefab;
    [SerializeField] private GameObject rusherMechPrefab;
    [SerializeField] private GameObject defenderMechPrefab;

    public GameObject mech1;
    public GameObject mech2;
    public GameObject mech3;
    public GameObject mech4;
    public GameObject mech5;

    // Start is called before the first frame update
    void Start()
    {
        if (loadout.sentFromStart) //If sent from the start scene, update all the data of the mechs/pilot individually
        {
            switch (loadout.selectedPilotStats1.pilotType)
            {
                case 0:
                    mech1 = Instantiate(fighterMechPrefab);
                    break;
                case 1:
                    mech1 = Instantiate(rusherMechPrefab);
                    break;
                case 2:
                    mech1 = Instantiate(defenderMechPrefab);
                    break;
            }

            switch (loadout.selectedPilotStats2.pilotType)
            {
                case 0:
                    mech2 = Instantiate(fighterMechPrefab);
                    break;
                case 1:
                    mech2 = Instantiate(rusherMechPrefab);
                    break;
                case 2:
                    mech2 = Instantiate(defenderMechPrefab);
                    break;
            }

            switch (loadout.selectedPilotStats3.pilotType)
            {
                case 0:
                    mech3 = Instantiate(fighterMechPrefab);
                    break;
                case 1:
                    mech3 = Instantiate(rusherMechPrefab);
                    break;
                case 2:
                    mech3 = Instantiate(defenderMechPrefab);
                    break;
            }

            switch (loadout.selectedPilotStats4.pilotType)
            {
                case 0:
                    mech4 = Instantiate(fighterMechPrefab);
                    break;
                case 1:
                    mech4 = Instantiate(rusherMechPrefab);
                    break;
                case 2:
                    mech4 = Instantiate(defenderMechPrefab);
                    break;
            }

            switch (loadout.selectedPilotStats5.pilotType)
            {
                case 0:
                    mech5 = Instantiate(fighterMechPrefab);
                    break;
                case 1:
                    mech5 = Instantiate(rusherMechPrefab);
                    break;
                case 2:
                    mech5 = Instantiate(defenderMechPrefab);
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

            mech1.SetActive(false);
            mech2.SetActive(false);
            mech3.SetActive(false);
            mech4.SetActive(false);
            mech5.SetActive(false);
        }
    }
}
