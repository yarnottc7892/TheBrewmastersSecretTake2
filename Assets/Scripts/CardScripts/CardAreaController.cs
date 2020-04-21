using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardAreaController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] BattleManager battle;

    public void OnPointerEnter(PointerEventData eventData) {
        if (battle.currentCard != null)
        {
            battle.currentCard.playSelfTargetedCard = false;
        }   
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (battle.currentCard != null)
        {
            battle.currentCard.playSelfTargetedCard = true;
        }
    }
}
