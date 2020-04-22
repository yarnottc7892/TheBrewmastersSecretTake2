using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private InventoryManager inventoryManager;
    public RectTransform rT;
    void Start()
    {
        rT = GetComponent<RectTransform>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<InventoryDragDrop>().invSlot == this)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rT.anchoredPosition;
            if(eventData.pointerDrag.GetComponent<InventoryDragDrop>().currSlot == eventData.pointerDrag.GetComponent<InventoryDragDrop>().craftSlot1 || eventData.pointerDrag.GetComponent<InventoryDragDrop>().currSlot == eventData.pointerDrag.GetComponent<InventoryDragDrop>().craftSlot2)
            {
                inventoryManager.CraftUnslotted(this);
            }
            eventData.pointerDrag.GetComponent<InventoryDragDrop>().currSlot = this;
        }
        else if(eventData.pointerDrag != null && (eventData.pointerDrag.GetComponent<InventoryDragDrop>().craftSlot1 == this || eventData.pointerDrag.GetComponent<InventoryDragDrop>().craftSlot2 == this))
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rT.anchoredPosition;
            eventData.pointerDrag.GetComponent<InventoryDragDrop>().currSlot = this;
            inventoryManager.CraftSlotted(eventData.pointerDrag.GetComponent<IngredientDisplay>().ingredient, this);
        }
    }
}
