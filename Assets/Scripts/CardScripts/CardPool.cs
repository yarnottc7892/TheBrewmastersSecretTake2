using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPool : MonoBehaviour
{
    [SerializeField] private CardController cardPrefab;

    [SerializeField] private RectTransform discardPile;
    [SerializeField] private RectTransform playSpot;
    [SerializeField] private BattleManager battle;
    [SerializeField] private Canvas canvas;

    private Queue<CardController> cards = new Queue<CardController>();

    public static CardPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public CardController Get() 
    {
        if(cards.Count == 0)
        {
            AddCards(1);
        }

        return cards.Dequeue();
    }

    private void AddCards(int amount) 
    {
        for (int i = 0; i < amount; i++)
        {
            CardController newCard = Instantiate(cardPrefab, this.transform.parent);
            newCard.discardPile = discardPile;
            newCard.playSpot = playSpot;
            newCard.battle = battle;
            newCard.canvas = canvas;
            newCard.gameObject.SetActive(false);
            cards.Enqueue(newCard);
        }
    }

    public void ReturnToPool(CardController card) 
    {
        card.gameObject.SetActive(false);
        card.GetComponent<RectTransform>().rotation = Quaternion.identity;
        cards.Enqueue(card);
    }
}
