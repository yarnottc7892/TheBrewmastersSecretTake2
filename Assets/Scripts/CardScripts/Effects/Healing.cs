using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "Cards/Effect/Healing")]
public class Healing : Effect_Base
{
    public override void DoEffect(Transform target) 
    {
        target.GetComponent<Combatant_Base>().Heal(effectValue);
    }

    public override string generateDescription() 
    {
        return "Heal " + effectValue + " health";
    }
}
