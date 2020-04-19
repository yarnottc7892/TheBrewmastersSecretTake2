using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card_Base : ScriptableObject
{

    public int cost;
    public Sprite art;
    public string description;

    public abstract void Play();
}
