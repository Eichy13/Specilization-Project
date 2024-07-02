using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SelectedMechStats", menuName = "ScriptableObjects/SelectedMechStats")]
public class SelectedMechStats : ScriptableObject
{
    public int mechHealth;
    public int mechMeleeDamage;
    public int mechRangedDamage;
    public int mechMobility;
    public int mechRange;
    public int mechCost;
    public int mechAbility;

    public string mechName;
    [TextArea(5, 10)]
    public string mechDescription;
    [TextArea(5, 10)]
    public string mechAbilityDescription;
    public Sprite mechImage1;
    public Sprite mechImage2;
}
