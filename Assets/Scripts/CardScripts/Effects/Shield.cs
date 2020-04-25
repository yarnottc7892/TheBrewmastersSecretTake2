using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "Cards/Effect/Shield")]
public class Shield : Effect_Base
{
    public override void DoEffect(Transform target) {

        target.GetComponent<Combatant_Base>().applyShield(effectValue);
    }

    public override string generateDescription() {

        return "Add " + effectValue + " shield";
    }
}
