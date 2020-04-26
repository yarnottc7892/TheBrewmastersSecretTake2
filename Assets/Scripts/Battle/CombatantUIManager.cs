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
    // Start is called before the first frame update
    void Start()
    {
        combatant.OnSetup += Setup;
        combatant.OnHealthChange += ChangeHealth;
    }

    private void Setup(int maxHealth) 
    {
        healthbar.maxValue = maxHealth;
        string healthFraction = maxHealth + "/" + maxHealth;
        healthText.text = healthFraction;
    }

    private void ChangeHealth(int health, int maxHealth) 
    {
        healthbar.value = health;
        string healthFraction = health + "/" + maxHealth;
        healthText.text = healthFraction;
    }


}
