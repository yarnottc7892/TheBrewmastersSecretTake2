using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "Cards/Effect/Damage")]
public class Damage : Effect_Base
{
    public override void DoEffect(Transform target) {
        Debug.Log("Do: " + effectValue + " damage to " + target.name);
    }

    public override string generateDescription() 
    {
        if (targetPlayer)
        {
            return "Deal " + effectValue + " damage to self.";
        }
        else
        {
            return "Deal " + effectValue + "damage to an enemy.";
        }

        
    }
}
