using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayAreaController : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) 
    {
        eventData.pointerDrag.GetComponent<CardController>().data.Play();
    }
}
