using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageCard", menuName = "Cards/Damage")]
public class Card_Damage : Card_Base
{
    public int damage;

    public override void Play() 
    {
        // Deal Damage to Enemy
        Debug.Log("Deal: " + damage + "damage");
    }
}
