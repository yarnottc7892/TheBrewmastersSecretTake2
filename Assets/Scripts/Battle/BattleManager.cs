using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private RectTransform drawDeck;
    [SerializeField] private RectTransform discardDeck;
    // [SerializeField] private Card_Base cardPrefab;

    [SerializeField] private List<Transform> cards = new List<Transform>();
    [SerializeField] private List<RectTransform> cardSpots = new List<RectTransform>();

    [SerializeField] private List<Card_Base> deck = new List<Card_Base>();

    private int deckSize;
    private int nextCard;

    public Transform player;
    public Transform enemy;

    public CardController currentCard = null;

    // Start is called before the first frame update
    void Start()
    {
        deckSize = deck.Count;
        nextCard = deckSize - 1;

        for (int i = 0; i < cards.Count; i++)
        {
            StartCoroutine(drawCard(i));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator drawCard(int index) 
    {

        RectTransform cardRect = cards[index].GetComponent<RectTransform>();
        cardRect.anchoredPosition = drawDeck.anchoredPosition;
        cards[index].GetComponent<Animator>().SetTrigger("Spawn");

        float journeyTime = 0.5f;
        float timePassed = 0f;
        

        while (cardRect.anchoredPosition != cardSpots[index].anchoredPosition)
        {
            float fracJourney = timePassed / journeyTime;
            cardRect.anchoredPosition = Vector3.Lerp(drawDeck.anchoredPosition, cardSpots[index].anchoredPosition, fracJourney);

            timePassed += Time.deltaTime;

            if (timePassed >= journeyTime)
            {
                cards[index].GetComponent<CardController>().startPos = cardRect.anchoredPosition;
            }

            yield return null;
        }
    }
}
