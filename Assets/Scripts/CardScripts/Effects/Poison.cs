using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "Cards/Effect/Poison")]
public class Poison : Effect_Base
{
    public override void DoEffect(Transform target) {

        target.GetComponent<Combatant_Base>().applyPoison(effectValue);
    }

    public override string generateDescription() {

        return "Apply " + effectValue + " poison";
    }
}
