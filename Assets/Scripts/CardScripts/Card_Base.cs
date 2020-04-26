using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/Card")]
public class Card_Base : ScriptableObject
{
    public string name;
    public int cost;
    public Sprite art;
    public string description = "";

    // The list of the cards effects
    [SerializeField] private List<Effect_Base> effects = new List<Effect_Base>();

    // Do each effect on the correct target
    public void Play(Transform player, Transform enemy) 
    {
        foreach(Effect_Base effect in effects)
        {
            if (effect.targetPlayer)
            {
                effect.DoEffect(player);
            }
            else
            {
                effect.DoEffect(enemy);
            }
        }
    }

    // Generate the full description based on the effect descriptions
    public string setDescription() 
    {
        description = "";
        foreach(Effect_Base effect in effects)
        {
            description += effect.generateDescription() + "\n";
            
        }
        return description;
    }

    // Check if all the effects target the player
    public bool checkSelfTargeting() 
    {
        foreach(Effect_Base effect in effects)
        {
            if (!effect.targetPlayer)
            {
                return false;
            }
        }

        return true;
    }

}
