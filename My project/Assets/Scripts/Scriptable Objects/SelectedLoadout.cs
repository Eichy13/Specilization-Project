using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectedLoadout", menuName = "ScriptableObjects/SelectedLoadout")]
public class SelectedLoadout : ScriptableObject
{
    public SelectedPilotStats selectedPilotStats1;
    public SelectedPilotStats selectedPilotStats2;
    public SelectedPilotStats selectedPilotStats3;
    public SelectedPilotStats selectedPilotStats4;
    public SelectedPilotStats selectedPilotStats5;

    public SelectedMechStats selectedMechStats1;
    public SelectedMechStats selectedMechStats2;
    public SelectedMechStats selectedMechStats3;
    public SelectedMechStats selectedMechStats4;
    public SelectedMechStats selectedMechStats5;

    public SelectedBaseAbilities selectedBaseAbility1;
    public SelectedBaseAbilities selectedBaseAbility2;

    public bool sentFromStart = false;

    public void Clear()
    {
        selectedPilotStats1 = null;
        selectedPilotStats2 = null;
        selectedPilotStats3 = null;
        selectedPilotStats4 = null;
        selectedPilotStats5 = null;

        selectedMechStats1 = null;
        selectedMechStats2 = null;
        selectedMechStats3 = null;
        selectedMechStats4 = null;
        selectedMechStats5 = null;

        selectedBaseAbility1 = null;
        selectedBaseAbility2 = null;
        sentFromStart = false;
    }

    public bool CheckIfFull()
    {
        return selectedPilotStats1 != null &&
              selectedPilotStats2 != null &&
              selectedPilotStats3 != null &&
              selectedPilotStats4 != null &&
              selectedPilotStats5 != null &&
              selectedMechStats1 != null &&
              selectedMechStats2 != null &&
              selectedMechStats3 != null &&
              selectedMechStats4 != null &&
              selectedMechStats5 != null;
    }

    public bool CheckIfBaseFull()
    {
        return selectedBaseAbility1 != null && selectedBaseAbility2 != null;
    }
}
