using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant_Base : MonoBehaviour
{
    // This is where effects would go ie: strength,block,weakened,etc.

    // This is where functions for those effect go ie: SetStrength

    public void TakeDamage(int damage) 
    {
        Debug.Log(name + " took " + damage + " damage");
    }

    public void Heal(int amount) 
    {
        Debug.Log(name + " healed for " + amount);
    }
}
