using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/Card")]
public class Card_Base : ScriptableObject
{

    public int cost;
    public Sprite art;
    public string description = "";

    [SerializeField] private List<Effect_Base> effects = new List<Effect_Base>();

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

    public string setDescription() 
    {
        description = "";
        foreach(Effect_Base effect in effects)
        {
            description += effect.generateDescription() + "\n";
            
        }
        return description;
    }

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
