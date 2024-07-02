using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SelectedPilotStats", menuName = "ScriptableObjects/SelectedPilotStats")]
public class SelectedPilotStats : ScriptableObject
{
    public int pilotHealth;
    public int pilotMeleeDamage;
    public int pilotRangedDamage;
    public int pilotMobility;
    public int pilotCost;
    public int pilotType;
    public int pilotAbility;

    public string pilotName;
    [TextArea(5, 10)]
    public string pilotDescription;
    [TextArea(5, 10)]
    public string pilotAbilityDescription;
    public Sprite pilotImage1;
    public Sprite pilotImage2;
}
