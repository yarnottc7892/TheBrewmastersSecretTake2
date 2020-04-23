using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CardController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    // Card elements to be set
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI description;

    // Places the card needs to go
    public RectTransform discardPile;
    public RectTransform playSpot;
    

    // Utilities
    public BattleManager battle;
    public Canvas canvas;
    private CanvasGroup canvasGroup;
    private Animator anim;
    private bool selected = false;
    private bool interactable = true;
    private bool mouseOver = false;
    

    // Order in the sibling list
    private int sibOrder;

    // Things that need to be accessed by other classes
    public Card_Base data;
    public Vector3 startPos;
    public RectTransform rectTransform;
    public bool targeting = false;
    public bool playSelfTargetedCard = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
        anim = GetComponent<Animator>();
    }

    public void setData(Card_Base newData) 
    {
        data = newData;
        cost.text = data.cost.ToString();
        description.text = data.setDescription();
    }

    // Check what to do when the mouse button is lifted
    public void OnPointerUp(PointerEventData eventData) 
    {
        selected = false;
        anim.SetBool("HoveredOver", false);
        if (!data.checkSelfTargeting())
        {
            if (!targeting)
            {
                rectTransform.DOAnchorPos(startPos, 0.1f).OnComplete(() => setInteractable(true));
                transform.SetSiblingIndex(sibOrder);

            } 
            else
            {
                data.Play(battle.player, battle.enemy);
                discard();
            }
        }    
        else
        {
            if (playSelfTargetedCard)
            {
                data.Play(battle.player, battle.enemy);
                discard();
            } 
            else
            {
                rectTransform.DOAnchorPos(startPos, 0.1f).OnComplete(() => setInteractable(true));
                transform.SetSiblingIndex(sibOrder);

            }
        }


        battle.currentCard = null;
        
    }
    // Card is clicked on
    public void OnPointerDown(PointerEventData eventData) 
    {
        sibOrder = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        rectTransform.DOAnchorPos(playSpot.anchoredPosition, 0.05f).OnComplete(() => setInteractable(false));
        anim.SetBool("HoveredOver", true);
        battle.currentCard = this;
        selected = true;
    }

    // Card is hovered over
    public void OnPointerEnter(PointerEventData eventData) 
    {
        mouseOver = true;

        if (!selected && interactable)
        {
            setInteractable(true);
        } 
    }

    // Card not hovered over
    public void OnPointerExit(PointerEventData eventData) 
    {
        mouseOver = false;

        if (!selected)
        {
            anim.SetBool("HoveredOver", false);
            transform.SetSiblingIndex(sibOrder);
            rectTransform.DOAnchorPos(startPos, 0.1f).OnComplete(() => setInteractable(true));
        }
    }

    public void discard() {

        GetComponent<Animator>().SetTrigger("Discard");

        targeting = false;

        interactable = true;

        battle.removeCard(this);

        Vector3 start = rectTransform.anchoredPosition;

        rectTransform.DOAnchorPos(discardPile.anchoredPosition, 0.5f).OnComplete(() => CardPool.Instance.ReturnToPool(this));
    }

    private void setInteractable(bool interact) 
    {
        interactable = interact;

        if (interactable == true && mouseOver)
        {
            sibOrder = transform.GetSiblingIndex();
            transform.SetAsLastSibling();
            anim.SetBool("HoveredOver", true);
            rectTransform.DOAnchorPos(rectTransform.anchoredPosition + new Vector2(0, 125), 0.1f);
            interactable = false;
        }
    }
}
