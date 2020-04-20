using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayAreaController : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) 
    {
        CardController card = eventData.pointerDrag.GetComponent<CardController>();
        card.data.Play();
        StartCoroutine(card.discard());
    }
}
