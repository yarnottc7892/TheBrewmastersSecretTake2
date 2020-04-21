using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Effect_Base : ScriptableObject
{
    [Tooltip("The value for the effect (ie. amount of damage/healing/etc.")]
    [SerializeField] protected int effectValue;

    public bool targetPlayer;


    public abstract void DoEffect(Transform target);
    public abstract string generateDescription();
}
