using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ingredient : ScriptableObject
{
    public Sprite sprite;
    public string ingredientID;
    public string ingredientDisplayName;
    public string ingredientDesc;
    public int invAmount = -1;

}
