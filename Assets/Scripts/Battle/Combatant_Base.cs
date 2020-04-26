using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Combatant_Base : MonoBehaviour
{
    // This is where effects would go ie: strength,block,weakened,etc.

    // This is where functions for those effect go ie: SetStrength

    [SerializeField] private ParticleSystem heal;

    [SerializeField] private int health;
    [SerializeField] private int shield = 0;
    [SerializeField] private int poison = 0;

    public void TakeDamage(int damage) 
    {
        transform.DOShakePosition(0.1f, vibrato: 100, randomness: 100);
        shield -= damage;

        // If there is still damage left, apply it to the health
        if (damage < 0)
        {
            health += damage;
        }     
    }

    public void Heal(int amount) 
    {
        heal.Play();
        health += amount;
    }

    public void applyPoison(int amount) 
    {
        poison += amount;
    }

    public void takePoison() 
    {
        health -= poison;
        poison--;
    }

    public void applyShield(int amount) 
    {
        shield += amount;
    }


}
