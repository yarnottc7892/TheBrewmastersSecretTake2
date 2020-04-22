using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private RectTransform drawDeck;
    [SerializeField] private RectTransform discardDeck;
    [SerializeField] private CardPool pool;
    [SerializeField] private Card_Base cardData;

    [SerializeField] private List<RectTransform> cardSpots = new List<RectTransform>();

    [SerializeField] private List<Card_Base> deck = new List<Card_Base>();

    private int deckSize;
    private int nextCard;
    private int handSize = 5;

    public Transform player;
    public Transform enemy;

    public CardController currentCard = null;

    // Start is called before the first frame update
    void Start()
    {
        deckSize = deck.Count;
        nextCard = deckSize - 1;

        drawHand();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            drawHand();
        }
    }

    private void drawCard(int index) 
    {
        var card = CardPool.Instance.Get();
        card.setData(cardData);
        card.gameObject.SetActive(true);
        card.GetComponent<Animator>().SetTrigger("Spawn");

        RectTransform cardRect = card.GetComponent<RectTransform>();
        cardRect.anchoredPosition = drawDeck.anchoredPosition;
        cardRect.DOAnchorPos(cardSpots[index].anchoredPosition, 0.5f).OnComplete(() => setCardPos(card, cardRect));
    }

    private void setCardPos(CardController card, RectTransform cardRect) 
    {
        card.startPos = cardRect.anchoredPosition;
    }

    private void drawHand() 
    {
        Debug.Log("Draw Hand");
        for (int i = 0; i < handSize; i++)
        {
            drawCard(i);
        }
    }
}
