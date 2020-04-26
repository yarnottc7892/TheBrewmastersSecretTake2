using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatantUIManager : MonoBehaviour
{
    [SerializeField] private Combatant_Base combatant;
    [SerializeField] private Slider healthbar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Transform shield;
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private Transform poison;
    [SerializeField] private TextMeshProUGUI poisonText;

    // Start is called before the first frame update
    void Start()
    {
        combatant.OnSetup += Setup;
        combatant.OnHealthChange += ChangeHealth;
        combatant.OnApplyPoison += ApplyPoison;
        combatant.OnTakePoison += TakePoison;
        combatant.OnChangeShield += ChangeShield;
    }

    private void Setup(int maxHealth) 
    {
        healthbar.maxValue = maxHealth;
        healthbar.value = maxHealth;
        string healthFraction = maxHealth + "/" + maxHealth;
        healthText.text = healthFraction;
    }

    private void ChangeHealth(int health, int maxHealth) 
    {
        healthbar.value = health;
        string healthFraction = health + "/" + maxHealth;
        healthText.text = healthFraction;
    }

    private void ApplyPoison(int amount) 
    {
        if (amount > 0)
        {
            poison.gameObject.SetActive(true);
            poisonText.text = amount.ToString();
        }
    }

    private void TakePoison(int amount) 
    {
        poisonText.text = amount.ToString();

        if(amount <= 0)
        {
            poison.gameObject.SetActive(false);
        }
    }

    private void ChangeShield(int amount) 
    {
        shieldText.text = amount.ToString();

        if (amount > 0)
        {
            shield.gameObject.SetActive(true);
        } 
        else
        {
            shield.gameObject.SetActive(false);
        }
    }


}
