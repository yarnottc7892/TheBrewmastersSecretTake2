using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageCard", menuName = "Cards/Healing")]
public class Card_Heal : Card_Base
{
    public int amount;

    public override void Play() {
        // Heal player
        Debug.Log("Heal: " + amount + "health");
    }
}
