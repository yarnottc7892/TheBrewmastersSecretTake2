using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Combatant_Base : MonoBehaviour
{
    [SerializeField] protected BattleManager battle;

    [SerializeField] private ParticleSystem heal;

    public int health;
    protected int maxHealth;
    [SerializeField] private int shield = 0;
    [SerializeField] private int poison = 0;

    // Events to change the canvas
    public Action<int> OnSetup;
    public Action<int, int> OnHealthChange;
    public Action<int> OnApplyPoison;
    public Action<int> OnTakePoison;
    public Action<int> OnChangeShield;

    private void Start() 
    {
        maxHealth = 30;
        OnSetup?.Invoke(maxHealth);
    }

    public void TakeDamage(int damage) 
    {
        transform.DOShakePosition(0.1f, vibrato: 100, randomness: 100);
        shield -= damage;

        // If there is still damage left, apply it to the health
        if (shield < 0)
        {
            health += shield;
            OnHealthChange?.Invoke(health, maxHealth);
            shield = 0;
        }

        if (health < 0)
        {
            health = 0;
        }

        OnChangeShield?.Invoke(shield);
    }

    public void Heal(int amount) 
    {
        if (health != maxHealth)
        {
            heal.Play();
            health += amount;
        }
        
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        OnHealthChange?.Invoke(health, maxHealth);
    }

    public void applyPoison(int amount) 
    {
        poison += amount;
        OnApplyPoison?.Invoke(poison);
    }

    public void takePoison() 
    {
        if (poison != 0)
        {
            health -= poison;
            if(health < 0)
            {
                health = 0;
            }
            poison--;
            OnHealthChange.Invoke(health, maxHealth);
            OnTakePoison?.Invoke(poison);
        }
    }

    public void applyShield(int amount) 
    {
        shield += amount;
        OnChangeShield?.Invoke(shield);
    }

    public void startTurn() 
    {
        takePoison();
    }
}
