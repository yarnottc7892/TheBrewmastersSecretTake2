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
    [SerializeField] private float hoveredHeight;

    // Places the card needs to go
    public RectTransform discardPile;
    public RectTransform playSpot;
    

    // Utilities
    public BattleManager battle;
    public Canvas canvas;
    private CanvasGroup canvasGroup;
    private enum CardState { Normal, Hovered, Selected, Played }
    private CardState currentState = CardState.Normal;
    

    // Order in the sibling list
    private int sibOrder;

    // Things that need to be accessed by other classes
    public Card_Base data;
    public Vector3 startPos;
    public Vector3 startRot;
    public RectTransform rectTransform;
    public bool targeting = false;
    public bool playSelfTargetedCard = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        canvasGroup = GetComponent<CanvasGroup>();
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
        if (!data.checkSelfTargeting())
        {
            playCardIfCan(targeting);
        }
        else
        {
            playCardIfCan(playSelfTargetedCard);
        }

        battle.currentCard = null; 
    }

    // Card is clicked on
    public void OnPointerDown(PointerEventData eventData)
    {
        changeState(CardState.Selected);
        rectTransform.DOAnchorPos(playSpot.anchoredPosition, 0.05f);
        battle.currentCard = this;
    }

    // Card is hovered over
    public void OnPointerEnter(PointerEventData eventData) 
    {
        if (currentState != CardState.Selected && currentState != CardState.Played)
        {
            hover();
        } 
    }

    // Card not hovered over
    public void OnPointerExit(PointerEventData eventData) 
    {

        if (currentState == CardState.Hovered)
        {
            noLongerHover();
        }
    }

    public void discard() 
    {

        targeting = false;

        playSelfTargetedCard = false;

        battle.removeCard(this);

        rectTransform.DOAnchorPos(discardPile.anchoredPosition, 0.5f).OnComplete(() => changeState(CardState.Played));
        rectTransform.DORotate(new Vector3(0, 0, 180), 0.5f);
        rectTransform.DOScale(Vector3.zero, 0.5f);
    }

    private void hover() 
    {
        sibOrder = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        DOTween.Kill(this.rectTransform);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, hoveredHeight);
        rectTransform.localScale = Vector3.one * 1.5f;
        rectTransform.rotation = new Quaternion(0f, 0f, 0f, rectTransform.rotation.w);
        changeState(CardState.Hovered);
    }

    private void noLongerHover() 
    {
        transform.SetSiblingIndex(sibOrder);
        rectTransform.DOAnchorPos(startPos, 0.3f).OnComplete(() => changeState(CardState.Normal));
        rectTransform.DOScale(Vector3.one, 0.3f);
        rectTransform.DORotate(startRot, 0.3f);
    }

    private void changeState(CardState newState) 
    {
        currentState = newState;

        if (currentState == CardState.Normal)
        {
            canvasGroup.blocksRaycasts = true;
        }
        else if (currentState == CardState.Selected)
        {
            canvasGroup.blocksRaycasts = false;
        }
        else if (currentState == CardState.Played)
        {
            canvasGroup.blocksRaycasts = true;
            currentState = CardState.Normal;
            CardPool.Instance.ReturnToPool(this);
        }
    }

    private void playCardIfCan(bool check) 
    {
        if (check && battle.currentEnergy >= data.cost)
        {
            data.Play(battle.player, battle.enemy);
            discard();
        } 
        else
        {
            rectTransform.DOAnchorPos(startPos, 0.1f).OnComplete(() => changeState(CardState.Normal));
            noLongerHover();
        }
    }
}
