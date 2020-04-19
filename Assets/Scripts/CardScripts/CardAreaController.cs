using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardAreaController : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        CardController card = eventData.pointerDrag.GetComponent<CardController>();
        card.rectTransform.anchoredPosition = card.startPos;
    }
}
