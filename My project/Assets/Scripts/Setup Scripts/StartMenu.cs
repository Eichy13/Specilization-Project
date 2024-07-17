using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private SelectedLoadout loadout;

    [SerializeField] private SelectedMechStats[] listOfMechStats;
    [SerializeField] private SelectedPilotStats[] listOfPilotStats;
    [SerializeField] private GameObject[] listOfCanvas;
    [SerializeField] private Image[] selectedMechImages;
    [SerializeField] private Image[] selectedPilotImages;

    [SerializeField] private Image[] loadoutMechHealth;
    [SerializeField] private Image[] loadoutMechRangedDamage;
    [SerializeField] private Image[] loadoutMechMeleeDamage;
    [SerializeField] private Image[] loadoutMechMobility;
    [SerializeField] private Image[] loadoutMechCost;

    [SerializeField] private GameObject[] mechButtons;
    [SerializeField] private GameObject[] pilotButtons;

    [SerializeField] private GameObject unitSelectionScreen;
    [SerializeField] private Image unitImage;
    [SerializeField] private TMP_Text currentUnitName;
    [SerializeField] private TMP_Text currentUnitDescription;
    [SerializeField] private TMP_Text currentUnitAbility;
    [SerializeField] private Image currentUnitHealth;
    [SerializeField] private Image currentUnitRangedDamage;
    [SerializeField] private Image currentUnitMeleeDamage;
    [SerializeField] private Image currentUnitMobility;
    [SerializeField] private Image currentUnitCost;

    [SerializeField] private GameObject[] mechExclusiveButtons;
    [SerializeField] private GameObject[] pilotExclusiveButtons;

    [SerializeField] private GameObject sortieButton;

    [SerializeField] private GameObject baseSelectionScreen;
    [SerializeField] private GameObject[] baseSelectionBG;
    [SerializeField] private SelectedBaseAbilities[] baseAbilityList;
    [SerializeField] private Image baseImage1;
    [SerializeField] private Image baseImage2;
    [SerializeField] private TMP_Text baseText1;
    [SerializeField] private TMP_Text baseText2;


    private int currentMech;
    private int selectedMech;

    private int currentPilot;
    private int selectedPilot;

    private int currentBase;

    public void Start()
    {
        loadout.Clear();
    }

    public void DebugText(string debugText)
    {
        Debug.Log(debugText);
    }

    public void LoadoutScreen()
    {
        for (int i = 0; i < listOfCanvas.Length; i++) 
        {
            listOfCanvas[i].gameObject.SetActive(false);
        }

        listOfCanvas[1].SetActive(true);
    }

    public void StartScreen()
    {
        Reset();
        loadout.Clear();
        for (int i = 0; i < listOfCanvas.Length; i++)
        {
            listOfCanvas[i].gameObject.SetActive(false);
        }

        listOfCanvas[0].SetActive(true);
    }

    public void MechSelectionScreen(int i)
    {
        currentMech = i;

        for (int k = 0; k < mechButtons.Length; k++)
        {
            mechButtons[k].SetActive(false);
            pilotButtons[k].SetActive(false);
        }

        for (int l = 0; l < 8; l++)
        {
            mechButtons[l].SetActive(true);
        }
   
        unitSelectionScreen.SetActive(true);
        for (int t = 0; t < mechExclusiveButtons.Length; t++)
        {
            mechExclusiveButtons[t].SetActive(true);
            pilotExclusiveButtons[t].SetActive(false);
        }
        mechExclusiveButtons[2].SetActive(false);
    }

    public void PilotSelectionScreen(int i)
    {
        currentPilot = i;

        for (int k = 0; k < mechButtons.Length; k++)
        {
            mechButtons[k].SetActive(false);
            pilotButtons[k].SetActive(false);
        }

        for (int l = 0; l < 8; l++)
        {
            pilotButtons[l].SetActive(true);
        }

        unitSelectionScreen.SetActive(true);
        for (int t = 0; t < mechExclusiveButtons.Length; t++)
        {
            mechExclusiveButtons[t].SetActive(false);
            pilotExclusiveButtons[t].SetActive(true);
        }
        pilotExclusiveButtons[2].SetActive(false);
    }

    public void StartGame()
    {
        if (loadout.CheckIfBaseFull())
        {
            loadout.sentFromStart = true;
            SceneManager.LoadScene(sceneName: "MainScene");
        }
    }

    public void LoadMechInfo(int i) //Load all info for the user to see
    {
        Reset();
        selectedMech = i;
        mechExclusiveButtons[2].SetActive(true);
        unitImage.sprite = listOfMechStats[i].mechImage1;
        currentUnitName.text = listOfMechStats[i].mechName;
        currentUnitDescription.text = listOfMechStats[i].mechDescription;
        currentUnitAbility.text = listOfMechStats[i].mechAbilityDescription;
        currentUnitHealth.fillAmount = listOfMechStats[i].mechHealth / 1000f;
        currentUnitMeleeDamage.fillAmount = listOfMechStats[i].mechMeleeDamage / 1000f;
        currentUnitRangedDamage.fillAmount = listOfMechStats[i].mechRangedDamage / 1000f;
        currentUnitMobility.fillAmount = listOfMechStats[i].mechMobility / 1000f;
        currentUnitCost.fillAmount = listOfMechStats[i].mechCost / 10f;
    }

    public void LoadPilotInfo(int i)
    {
        Reset();
        selectedPilot = i;
        pilotExclusiveButtons[2].SetActive(true);
        unitImage.sprite = listOfPilotStats[i].pilotImage1;
        currentUnitName.text = listOfPilotStats[i].pilotName;
        currentUnitDescription.text = listOfPilotStats[i].pilotDescription;
        currentUnitAbility.text = listOfPilotStats[i].pilotAbilityDescription;
        currentUnitHealth.fillAmount = listOfPilotStats[i].pilotHealth / 1000f;
        currentUnitMeleeDamage.fillAmount = listOfPilotStats[i].pilotMeleeDamage / 1000f;
        currentUnitRangedDamage.fillAmount = listOfPilotStats[i].pilotRangedDamage / 1000f;
        currentUnitMobility.fillAmount = listOfPilotStats[i].pilotMobility / 1000f;
        currentUnitCost.fillAmount = listOfPilotStats[i].pilotCost / 10f;
    }

    public void MechPage(int j)
    {
        for (int i = 0; i < mechButtons.Length; i++)
        {
            mechButtons[i].SetActive(false);
        }

        if (j == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                mechButtons[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 8; i < mechButtons.Length; i++)
            {
                mechButtons[i].SetActive(true);
            }
        }
    }

    public void PilotPage(int j)
    {
        for (int i = 0; i < mechButtons.Length; i++)
        {
            pilotButtons[i].SetActive(false);
        }

        if (j == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                pilotButtons[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 8; i < mechButtons.Length; i++)
            {
                pilotButtons[i].SetActive(true);
            }
        }
    }

    public void SelectMech()
    {
        unitSelectionScreen.SetActive(false);

        //if (loadout.selectedMechStats1 == listOfMechStats[selectedMech] || loadout.selectedMechStats2 == listOfMechStats[selectedMech] || loadout.selectedMechStats3 == listOfMechStats[selectedMech] || loadout.selectedMechStats4 == listOfMechStats[selectedMech] || loadout.selectedMechStats5 == listOfMechStats[selectedMech])
        //{
        //    return; //Check if any mech is already selected
        //}

        switch (currentMech)
        {
            case 0:
                loadout.selectedMechStats1 = listOfMechStats[selectedMech];
                if (loadout.selectedPilotStats1 != null)
                {
                    LoadoutUpdate1();
                }
                break;

            case 1:
                loadout.selectedMechStats2 = listOfMechStats[selectedMech];
                if (loadout.selectedPilotStats2 != null)
                {
                    LoadoutUpdate1();
                }
                break;

            case 2:
                loadout.selectedMechStats3 = listOfMechStats[selectedMech];
                if (loadout.selectedPilotStats3 != null)
                {
                    LoadoutUpdate1();
                }
                break;

            case 3:
                loadout.selectedMechStats4 = listOfMechStats[selectedMech];
                if (loadout.selectedPilotStats4 != null)
                {
                    LoadoutUpdate1();
                }
                break;

            case 4:
                loadout.selectedMechStats5 = listOfMechStats[selectedMech];
                if (loadout.selectedPilotStats5 != null)
                {
                    LoadoutUpdate1();
                }
                break;
        }
        
        selectedMechImages[currentMech].sprite = unitImage.sprite;
        selectedMechImages[currentMech].color = Color.white;

        
        Reset();
    }

    public void SelectPilot()
    {
        unitSelectionScreen.SetActive(false);

        //if (loadout.selectedMechStats1 == listOfMechStats[selectedMech] || loadout.selectedMechStats2 == listOfMechStats[selectedMech] || loadout.selectedMechStats3 == listOfMechStats[selectedMech] || loadout.selectedMechStats4 == listOfMechStats[selectedMech] || loadout.selectedMechStats5 == listOfMechStats[selectedMech])
        //{
        //    return; //Check if any mech is already selected
        //}

        switch (currentPilot)
        {
            case 0:
                loadout.selectedPilotStats1 = listOfPilotStats[selectedPilot];
                if (loadout.selectedMechStats1 != null)
                {
                    LoadoutUpdate2();
                }
                break;

            case 1:
                loadout.selectedPilotStats2 = listOfPilotStats[selectedPilot];
                if (loadout.selectedMechStats2 != null)
                {
                    LoadoutUpdate2();
                }
                break;

            case 2:
                loadout.selectedPilotStats3 = listOfPilotStats[selectedPilot];
                if (loadout.selectedMechStats3 != null)
                {
                    LoadoutUpdate2();
                }
                break;

            case 3:
                loadout.selectedPilotStats4 = listOfPilotStats[selectedPilot];
                if (loadout.selectedMechStats4 != null)
                {
                    LoadoutUpdate2();
                }
                break;

            case 4:
                loadout.selectedPilotStats5 = listOfPilotStats[selectedPilot];
                if (loadout.selectedMechStats5 != null)
                {
                    LoadoutUpdate2();
                }
                break;
        }

        selectedPilotImages[currentPilot].sprite = unitImage.sprite;
        selectedPilotImages[currentPilot].color = Color.white;


        Reset();
    }

    private void LoadoutUpdate1()
    {
        switch (currentMech)
        {
            case 0:
                loadoutMechHealth[currentMech].fillAmount = (loadout.selectedPilotStats1.pilotHealth + loadout.selectedMechStats1.mechHealth) / 2000f;
                loadoutMechMeleeDamage[currentMech].fillAmount = (loadout.selectedPilotStats1.pilotMeleeDamage + loadout.selectedMechStats1.mechMeleeDamage) / 2000f;
                loadoutMechRangedDamage[currentMech].fillAmount = (loadout.selectedPilotStats1.pilotRangedDamage + loadout.selectedMechStats1.mechRangedDamage) / 2000f;
                loadoutMechMobility[currentMech].fillAmount = (loadout.selectedPilotStats1.pilotMobility + loadout.selectedMechStats1.mechMobility) / 2000f;
                loadoutMechCost[currentMech].fillAmount = (loadout.selectedPilotStats1.pilotCost + loadout.selectedMechStats1.mechCost) / 20f;
                break;

            case 1:
                loadoutMechHealth[currentMech].fillAmount = (loadout.selectedPilotStats2.pilotHealth + loadout.selectedMechStats2.mechHealth) / 2000f;
                loadoutMechMeleeDamage[currentMech].fillAmount = (loadout.selectedPilotStats2.pilotMeleeDamage + loadout.selectedMechStats2.mechMeleeDamage) / 2000f;
                loadoutMechRangedDamage[currentMech].fillAmount = (loadout.selectedPilotStats2.pilotRangedDamage + loadout.selectedMechStats2.mechRangedDamage) / 2000f;
                loadoutMechMobility[currentMech].fillAmount = (loadout.selectedPilotStats2.pilotMobility + loadout.selectedMechStats2.mechMobility) / 2000f;
                loadoutMechCost[currentMech].fillAmount = (loadout.selectedPilotStats2.pilotCost + loadout.selectedMechStats2.mechCost) / 20f;
                break;

            case 2:
                loadoutMechHealth[currentMech].fillAmount = (loadout.selectedPilotStats3.pilotHealth + loadout.selectedMechStats3.mechHealth) / 2000f;
                loadoutMechMeleeDamage[currentMech].fillAmount = (loadout.selectedPilotStats3.pilotMeleeDamage + loadout.selectedMechStats3.mechMeleeDamage) / 2000f;
                loadoutMechRangedDamage[currentMech].fillAmount = (loadout.selectedPilotStats3.pilotRangedDamage + loadout.selectedMechStats3.mechRangedDamage) / 2000f;
                loadoutMechMobility[currentMech].fillAmount = (loadout.selectedPilotStats3.pilotMobility + loadout.selectedMechStats3.mechMobility) / 2000f;
                loadoutMechCost[currentMech].fillAmount = (loadout.selectedPilotStats3.pilotCost + loadout.selectedMechStats3.mechCost) / 20f;
                break;

            case 3:
                loadoutMechHealth[currentMech].fillAmount = (loadout.selectedPilotStats4.pilotHealth + loadout.selectedMechStats4.mechHealth) / 2000f;
                loadoutMechMeleeDamage[currentMech].fillAmount = (loadout.selectedPilotStats4.pilotMeleeDamage + loadout.selectedMechStats4.mechMeleeDamage) / 2000f;
                loadoutMechRangedDamage[currentMech].fillAmount = (loadout.selectedPilotStats4.pilotRangedDamage + loadout.selectedMechStats4.mechRangedDamage) / 2000f;
                loadoutMechMobility[currentMech].fillAmount = (loadout.selectedPilotStats4.pilotMobility + loadout.selectedMechStats4.mechMobility) / 2000f;
                loadoutMechCost[currentMech].fillAmount = (loadout.selectedPilotStats4.pilotCost + loadout.selectedMechStats4.mechCost) / 20f;
                break;

            case 4:
                loadoutMechHealth[currentMech].fillAmount = (loadout.selectedPilotStats5.pilotHealth + loadout.selectedMechStats5.mechHealth) / 2000f;
                loadoutMechMeleeDamage[currentMech].fillAmount = (loadout.selectedPilotStats5.pilotMeleeDamage + loadout.selectedMechStats5.mechMeleeDamage) / 2000f;
                loadoutMechRangedDamage[currentMech].fillAmount = (loadout.selectedPilotStats5.pilotRangedDamage + loadout.selectedMechStats5.mechRangedDamage) / 2000f;
                loadoutMechMobility[currentMech].fillAmount = (loadout.selectedPilotStats5.pilotMobility + loadout.selectedMechStats5.mechMobility) / 2000f;
                loadoutMechCost[currentMech].fillAmount = (loadout.selectedPilotStats5.pilotCost + loadout.selectedMechStats5.mechCost) / 20f;
                break;
        }

        if (loadout.CheckIfFull())
        {
            sortieButton.SetActive(true);
        }
    }

    private void LoadoutUpdate2()
    {
        switch (currentPilot)
        {
            case 0:
                loadoutMechHealth[currentMech].fillAmount = (loadout.selectedPilotStats1.pilotHealth + loadout.selectedMechStats1.mechHealth) / 2000f;
                loadoutMechMeleeDamage[currentMech].fillAmount = (loadout.selectedPilotStats1.pilotMeleeDamage + loadout.selectedMechStats1.mechMeleeDamage) / 2000f;
                loadoutMechRangedDamage[currentMech].fillAmount = (loadout.selectedPilotStats1.pilotRangedDamage + loadout.selectedMechStats1.mechRangedDamage) / 2000f;
                loadoutMechMobility[currentMech].fillAmount = (loadout.selectedPilotStats1.pilotMobility + loadout.selectedMechStats1.mechMobility) / 2000f;
                loadoutMechCost[currentMech].fillAmount = (loadout.selectedPilotStats1.pilotCost + loadout.selectedMechStats1.mechCost) / 20f;
                break;

            case 1:
                loadoutMechHealth[currentMech].fillAmount = (loadout.selectedPilotStats2.pilotHealth + loadout.selectedMechStats2.mechHealth) / 2000f;
                loadoutMechMeleeDamage[currentMech].fillAmount = (loadout.selectedPilotStats2.pilotMeleeDamage + loadout.selectedMechStats2.mechMeleeDamage) / 2000f;
                loadoutMechRangedDamage[currentMech].fillAmount = (loadout.selectedPilotStats2.pilotRangedDamage + loadout.selectedMechStats2.mechRangedDamage) / 2000f;
                loadoutMechMobility[currentMech].fillAmount = (loadout.selectedPilotStats2.pilotMobility + loadout.selectedMechStats2.mechMobility) / 2000f;
                loadoutMechCost[currentMech].fillAmount = (loadout.selectedPilotStats2.pilotCost + loadout.selectedMechStats2.mechCost) / 20f;
                break;

            case 2:
                loadoutMechHealth[currentMech].fillAmount = (loadout.selectedPilotStats3.pilotHealth + loadout.selectedMechStats3.mechHealth) / 2000f;
                loadoutMechMeleeDamage[currentMech].fillAmount = (loadout.selectedPilotStats3.pilotMeleeDamage + loadout.selectedMechStats3.mechMeleeDamage) / 2000f;
                loadoutMechRangedDamage[currentMech].fillAmount = (loadout.selectedPilotStats3.pilotRangedDamage + loadout.selectedMechStats3.mechRangedDamage) / 2000f;
                loadoutMechMobility[currentMech].fillAmount = (loadout.selectedPilotStats3.pilotMobility + loadout.selectedMechStats3.mechMobility) / 2000f;
                loadoutMechCost[currentMech].fillAmount = (loadout.selectedPilotStats3.pilotCost + loadout.selectedMechStats3.mechCost) / 20f;
                break;

            case 3:
                loadoutMechHealth[currentMech].fillAmount = (loadout.selectedPilotStats4.pilotHealth + loadout.selectedMechStats4.mechHealth) / 2000f;
                loadoutMechMeleeDamage[currentMech].fillAmount = (loadout.selectedPilotStats4.pilotMeleeDamage + loadout.selectedMechStats4.mechMeleeDamage) / 2000f;
                loadoutMechRangedDamage[currentMech].fillAmount = (loadout.selectedPilotStats4.pilotRangedDamage + loadout.selectedMechStats4.mechRangedDamage) / 2000f;
                loadoutMechMobility[currentMech].fillAmount = (loadout.selectedPilotStats4.pilotMobility + loadout.selectedMechStats4.mechMobility) / 2000f;
                loadoutMechCost[currentMech].fillAmount = (loadout.selectedPilotStats4.pilotCost + loadout.selectedMechStats4.mechCost) / 20f;
                break;

            case 4:
                loadoutMechHealth[currentMech].fillAmount = (loadout.selectedPilotStats5.pilotHealth + loadout.selectedMechStats5.mechHealth) / 2000f;
                loadoutMechMeleeDamage[currentMech].fillAmount = (loadout.selectedPilotStats5.pilotMeleeDamage + loadout.selectedMechStats5.mechMeleeDamage) / 2000f;
                loadoutMechRangedDamage[currentMech].fillAmount = (loadout.selectedPilotStats5.pilotRangedDamage + loadout.selectedMechStats5.mechRangedDamage) / 2000f;
                loadoutMechMobility[currentMech].fillAmount = (loadout.selectedPilotStats5.pilotMobility + loadout.selectedMechStats5.mechMobility) / 2000f;
                loadoutMechCost[currentMech].fillAmount = (loadout.selectedPilotStats5.pilotCost + loadout.selectedMechStats5.mechCost) / 20f;
                break;
        }

        if (loadout.CheckIfFull())
        {
            sortieButton.SetActive(true);
        }
    }

    public void BasePage()
    {
        baseSelectionScreen.SetActive(true);
    }

    public void LoadBase(int i)
    {
        currentBase = i;
        baseSelectionBG[0].SetActive(false);
        baseSelectionBG[1].SetActive(true);
    }

    public void SelectBase(int i)
    {
        baseSelectionBG[0].SetActive(true);
        baseSelectionBG[1].SetActive(false);
        if (currentBase == 0)
        {
            loadout.selectedBaseAbility1 = baseAbilityList[i];
            baseImage1.sprite = baseAbilityList[i].abilityImage;
            baseText1.text = baseAbilityList[i].abilityDescription;
        }
        else
        {
            loadout.selectedBaseAbility2 = baseAbilityList[i];
            baseImage2.sprite = baseAbilityList[i].abilityImage;
            baseText2.text = baseAbilityList[i].abilityDescription;
        }
    }

    public void Reset()
    {
        selectedMech = 0;
        selectedPilot = 0;
        unitImage.sprite = null;
        currentUnitName.text = null;
        currentUnitDescription.text = null;
        currentUnitAbility.text = null;
        currentUnitHealth.fillAmount = 0;
        currentUnitMeleeDamage.fillAmount = 0f;
        currentUnitRangedDamage.fillAmount = 0f;
        currentUnitMobility.fillAmount = 0f;
        currentUnitCost.fillAmount = 0f;
    }
}
