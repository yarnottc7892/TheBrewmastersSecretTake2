﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] public Canvas canvas;
    public RectTransform rT;
    private CanvasGroup cG;
    private Vector2 previousPos;
    public InventorySlot craftSlot1, craftSlot2, invSlot, productSlot, currSlot;
    private Ingredient ingredient;
    //public float invManagerScale;

    private void Awake()
    {
        rT = GetComponent<RectTransform>();
        cG = GetComponent<CanvasGroup>();
    }

    public void SetAlpha(float num)
    {
        cG.alpha = num;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(GetComponent<IngredientDisplay>().ingredient.invAmount != -1)
        {
            previousPos = rT.anchoredPosition;
            Debug.Log("Begin Drag");
            //cG.alpha = .7f;
            cG.blocksRaycasts = false;
            //throw new System.NotImplementedException();
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GetComponent<IngredientDisplay>().ingredient.invAmount != -1)
        {
            //Debug.Log("Drag");
            rT.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
            
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GetComponent<IngredientDisplay>().ingredient.invAmount != -1)
        {
            
            if (currSlot != invSlot &&
                (eventData.pointerCurrentRaycast.gameObject == null ||
                (eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>() == null || 
                (eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>() != invSlot && 
                eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>() != craftSlot1 && 
                eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>() != craftSlot2))))
            {
                //rT.anchoredPosition = previousPos;
                invSlot.SetBackToInvSlot(gameObject.GetComponent<IngredientDisplay>());
            }
            else if((eventData.pointerCurrentRaycast.gameObject == null ||
                eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>() == null ||
                (eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>() != invSlot &&
                eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>() != craftSlot1 &&
                eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>() != craftSlot2)))
            {
                rT.anchoredPosition = previousPos;
            }
            //Debug.Log("End Drag");
            //cG.alpha = 1.0f;
            cG.blocksRaycasts = true;
            //throw new System.NotImplementedException();
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }
}
