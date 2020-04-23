using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class BattleManager : MonoBehaviour
{
    [SerializeField] private RectTransform drawDeck;
    [SerializeField] private RectTransform discardDeck;
    [SerializeField] private CardPool pool;
    [SerializeField] private Deck deck;
    [SerializeField] private TextMeshProUGUI cardsInDraw;
    [SerializeField] private TextMeshProUGUI cardsInDiscard;

    [SerializeField] private RectTransform middleCardSpot;

    [SerializeField] private int handSize = 5;
    private float distanceBetweenCards = 200f;
    private List<CardController> cardsInHand = new List<CardController>();

    public Transform player;
    public Transform enemy;

    public CardController currentCard = null;

    // Start is called before the first frame update
    void Start() 
    {
        deck.shuffleDraw();
        // drawHand();
        cardsInDraw.text = deck.draw.Count.ToString();
        cardsInDiscard.text = 0.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            drawHand();
        }
    }

    private void drawCard() 
    {
        var card = CardPool.Instance.Get();

        if (deck.draw.Count == 0)
        {
            deck.shuffleDiscard();
            cardsInDraw.text = deck.draw.Count.ToString();
            cardsInDiscard.text = 0.ToString();
        }

        card.setData(deck.draw[deck.draw.Count - 1]);
        deck.removeCardFromDrawAt(deck.draw.Count - 1);
        cardsInDraw.text = deck.draw.Count.ToString();
        card.gameObject.SetActive(true);
        card.GetComponent<Animator>().SetTrigger("Spawn");

        cardsInHand.Add(card);

        RectTransform cardRect = card.GetComponent<RectTransform>();
        cardRect.anchoredPosition = drawDeck.anchoredPosition;

        generateCardPositions();

    }

    private void drawHand() 
    {
        for (int i = 0; i < handSize; i++)
        {
            drawCard();
        }
    }

    private void generateCardPositions() {

        float distanceFromMiddle = 0f;
        float firstCardXPos = 0f;

        if (cardsInHand.Count % 2 == 0)
        {
            distanceFromMiddle = distanceBetweenCards / 2;
            firstCardXPos = middleCardSpot.anchoredPosition.x - distanceFromMiddle - (distanceBetweenCards * ((cardsInHand.Count / 2) - 1));
        } 
        else
        {
            firstCardXPos = middleCardSpot.anchoredPosition.x - distanceFromMiddle - (distanceBetweenCards * (cardsInHand.Count / 2));
        }

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            RectTransform cardRect = cardsInHand[i].GetComponent<RectTransform>();

            float newXPos = firstCardXPos + (distanceBetweenCards * i);
            Vector2 newPos = new Vector2(newXPos, cardRect.anchoredPosition.y);
            cardRect.DOAnchorPos(newPos, 0.5f).OnComplete(setCardPositions);

        }
    }

    private void setCardPositions() 
    {
        foreach(CardController card in cardsInHand)
        {
            card.startPos = card.GetComponent<RectTransform>().anchoredPosition;
        }
        
    }

    public void removeCard(CardController card) 
    {
        cardsInHand.Remove(card);
        deck.addCardToDiscard(card.data);
        cardsInDiscard.text = deck.discard.Count.ToString();
        generateCardPositions();
    }
}
