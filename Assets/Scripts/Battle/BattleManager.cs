using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private RectTransform drawDeck;
    [SerializeField] private RectTransform discardDeck;

    [SerializeField] private List<Transform> cards = new List<Transform>();
    [SerializeField] private List<RectTransform> cardSpots = new List<RectTransform>();

    private int cardsInHand = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (int i = 0; i < cards.Count; i++)
            {
                StartCoroutine(drawCard(i));
            }
        }
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

            yield return null;
        }

        
    }
}
