using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SelectedPilotStats", menuName = "ScriptableObjects/SelectedBaseAbilities")]
public class SelectedBaseAbilities : ScriptableObject
{
    public int abilityID;

    [TextArea(5, 10)]
    public string abilityName;

    [TextArea(5, 10)]
    public string abilityDescription;

    public Sprite abilityImage;
}
