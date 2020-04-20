using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rT;
    private CanvasGroup cG;
    private Vector2 previousPos;
    private void Awake()
    {
        rT = GetComponent<RectTransform>();
        cG = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        previousPos = rT.anchoredPosition;
        Debug.Log("Begin Drag");
        cG.alpha = .7f;
        cG.blocksRaycasts = false;
        //throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        rT.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == null || !eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>())
        {
            Debug.LogError("Not over slot");
            rT.anchoredPosition = previousPos;
        }
        Debug.Log("End Drag");
        cG.alpha = 1.0f;
        cG.blocksRaycasts = true;
        //throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }
}
