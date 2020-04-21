using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "Cards/Effect/Healing")]
public class Healing : Effect_Base
{
    public override void DoEffect(Transform target) 
    {
        Debug.Log("Heal yourself for: " + effectValue + "health");
    }

    public override string generateDescription() 
    {
        return "Heal " + effectValue + " health";
    }
}
