using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    public Sprite charPic;
    public string charName;
}
